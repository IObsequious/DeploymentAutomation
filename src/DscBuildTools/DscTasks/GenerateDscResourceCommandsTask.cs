using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Text;
using DscBuildTools.Model;
using DscBuildTools.Types;
using DscBuildTools.Utilities;
using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;
using Microsoft.CSharp;

namespace DscBuildTools.DscTasks
{
    public class GenerateDscResourceCommandsTask : AbstractDscResourceTask
    {
        [Required]
        public ITaskItem[] Configuration { get; set; }

        [Output]
        public string[] OutputFile { get; set; }

        protected override bool ExecuteCore()
        {
            foreach (var dscResourceItem in Configuration)
            {
                var dscConfiguration = GetDesiredStateConfiguration(dscResourceItem);
                if (dscConfiguration == null)
                {
                    LogError("Invalid Configuration", "DSC002", "The configuration passed to the task is invalid. Consider starting it over from scratch or analyzing it more closely for errors.");
                }
                WriteDscResourceCommands(dscConfiguration);
                break;
            }

            return true;
        }

        private void WriteDscResourceCommands(DesiredStateConfiguration configuration)
        {
            List<string> outputFileList = new List<string>();

            foreach(var dscResource in configuration.DscResources)
            {
                string outputFile = Path.Combine(configuration.OutputDirectory, $"{dscResource.FriendlyName}.Commands.g.cs");

                StringBuilder builder = new StringBuilder();
                using (StringWriter writer = new StringWriter(builder))
                {
                    CodeCompileUnit unit = new CodeCompileUnit();
                    CodeNamespace ns = new CodeNamespace(configuration.RootNamespace);

                    CodeTypeDeclaration getCommand = CreateTypeDeclaration(CommandType.GetTargetResource, dscResource);
                    CodeTypeDeclaration setCommand = CreateTypeDeclaration(CommandType.SetTargetResource, dscResource);
                    CodeTypeDeclaration testCommand = CreateTypeDeclaration(CommandType.TestTargetResource, dscResource);

                    ns.Imports.Add(new CodeNamespaceImport("System"));
                    ns.Imports.Add(new CodeNamespaceImport("System.Collections"));
                    ns.Imports.Add(new CodeNamespaceImport("System.Collections.Generic"));
                    ns.Imports.Add(new CodeNamespaceImport("System.Management.Automation"));
                    ns.Imports.Add(new CodeNamespaceImport("Microsoft.Management.Infrastructure"));
                    ns.Types.Add(getCommand);
                    ns.Types.Add(setCommand);
                    ns.Types.Add(testCommand);

                    unit.Namespaces.Add(ns);

                    CodeGeneratorOptions options = new CodeGeneratorOptions();
                    options.BlankLinesBetweenMembers = true;
                    options.VerbatimOrder = true;
                    options.IndentString = "    ";
                    options.BracingStyle = "C";

                    CSharpCodeProvider provider = new CSharpCodeProvider();

                    provider.GenerateCodeFromCompileUnit(unit, writer, options);
                }

                string text = builder.ToString();
                text = text.Replace("};", "}");
                text = text.Replace("()]", "]");

                File.WriteAllText(outputFile, text);
                outputFileList.Add(outputFile);
            }

            OutputFile = outputFileList.ToArray();

        }

        private static CodeTypeDeclaration CreateTypeDeclaration(CommandType type, DscResource resource)
        {
            string verb = string.Empty;
            string verbClass = string.Empty;
            string outputType = string.Empty;
            switch (type)
            {
                case CommandType.GetTargetResource:
                    {
                        verbClass = "VerbsCommon";
                        verb = "Get";
                        outputType = "Hashtable";
                        break;
                    }
                case CommandType.SetTargetResource:
                    {
                        verbClass = "VerbsCommon";
                        verb = "Set";
                        outputType = "void";
                        break;
                    }
                case CommandType.TestTargetResource:
                    {
                        verbClass = "VerbsDiagnostic";
                        verb = "Test";
                        outputType = "bool";
                        break;
                    }
            }

            CodeTypeDeclaration typeDeclaration = new CodeTypeDeclaration
            {
                Name = $"{verb}TargetResourceCommand",
                IsClass = true,
                IsPartial = true,
                TypeAttributes = System.Reflection.TypeAttributes.Public,
            };
            typeDeclaration.BaseTypes.Add(new CodeTypeReference("PSCmdlet"));

            CodeAttributeDeclaration cmdletAttribute = new CodeAttributeDeclaration();
            cmdletAttribute.Name = "Cmdlet";
            cmdletAttribute.Arguments.Add(
                new CodeAttributeArgument(new CodeVariableReferenceExpression($"{verbClass}.{verb}")));
            cmdletAttribute.Arguments.Add(
                new CodeAttributeArgument(new CodePrimitiveExpression("TargetResource")));

            typeDeclaration.CustomAttributes.Add(cmdletAttribute);


            CodeAttributeDeclaration outputTypeAttribute = new CodeAttributeDeclaration();
            outputTypeAttribute.Name = "OutputType";
            outputTypeAttribute.Arguments.Add(
                new CodeAttributeArgument(new CodeVariableReferenceExpression($"typeof({outputType})")));

            typeDeclaration.CustomAttributes.Add(outputTypeAttribute);

            foreach(var property in resource.Properties)
            {
                CodeMemberField memberProperty = new CodeMemberField();
                memberProperty.Name = property.Name + " { get; set; }";
                memberProperty.Type = new CodeTypeReference(DscResourceTypeConverter.ConvertToType(property.PropertyType));

                CodeAttributeDeclaration parameterAttribute = new CodeAttributeDeclaration();
                parameterAttribute.Name = "Parameter";
                if (property.Attribute == DscResourceAttribute.Key || property.Attribute == DscResourceAttribute.Required)
                {
                    parameterAttribute.Arguments.Add(
                        new CodeAttributeArgument
                        {
                            Name = "Mandatory",
                            Value = new CodePrimitiveExpression(true)
                        });
                }
                memberProperty.CustomAttributes.Add(parameterAttribute);
                if (property.PossibleValues?.Count > 0)
                {
                    CodeAttributeDeclaration validateSetAttribute = new CodeAttributeDeclaration();
                    validateSetAttribute.Name = "ValidateSet";
                    foreach(var possibleValue in property.Values)
                    {
                        validateSetAttribute.Arguments.Add(
                            new CodeAttributeArgument(new CodePrimitiveExpression(possibleValue)));
                    }
                    memberProperty.CustomAttributes.Add(validateSetAttribute);
                }               
                typeDeclaration.Members.Add(memberProperty);
            }
            return typeDeclaration;
        }

        private enum CommandType
        {
            GetTargetResource,
            SetTargetResource,
            TestTargetResource
        }
    }
}

using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DscBuildTools.Utilities
{
    internal class ModuleManifestWriter
    {
        private readonly IndentedTextWriter _writer;

        public ModuleManifestWriter(TextWriter writer)
        {
            if (writer is IndentedTextWriter indentedTextWriter)
            {
                _writer = indentedTextWriter;
            }
            else
            {
                _writer = new IndentedTextWriter(writer);
            }          
        }

        private int Indent
        {
            get
            {
                return _writer.Indent;
            }
            set
            {
                _writer.Indent = value;
            }
        }


        /// <summary>
        /// Writes the Path property. This is required.
        /// </summary>
        public void WritePath(string value)
        {
            Indent = 0;
            _writer.Write("Path");
            _writer.Write(" = ");
            _writer.Write($"\"{value}\"");
        }

        /// <summary>
        /// Writes the NestedModules property. This is optional.
        /// </summary>
        public void WriteNestedModules(params object[] value)
        {
            Indent = 0;
            _writer.Write("NestedModules");
            _writer.Write(" = ");
            _writer.WriteLine("@(");
            Indent = 3;
            for (int i = 0; i < value.Length; i++)
            {
                var current = value[i];
                _writer.Write('"');
                _writer.Write(current);
                _writer.Write('"');
                if (i != value.Length - 1)
                {
                    _writer.Write(", ");
                }
            }
            _writer.WriteLine();
            _writer.WriteLine(")");
            Indent = 0;
            _writer.Write($"\"{value}\"");
        }

        /// <summary>
        /// Writes the Guid property. This is optional.
        /// </summary>
        public void WriteGuid(Guid value)
        {
            Indent = 0;
            _writer.Write("Guid");
            _writer.Write(" = ");
            _writer.Write($"\"{value}\"");
        }

        /// <summary>
        /// Writes the Author property. This is optional.
        /// </summary>
        public void WriteAuthor(string value)
        {
            Indent = 0;
            _writer.Write("Author");
            _writer.Write(" = ");
            _writer.Write($"\"{value}\"");
        }

        /// <summary>
        /// Writes the CompanyName property. This is optional.
        /// </summary>
        public void WriteCompanyName(string value)
        {
            Indent = 0;
            _writer.Write("CompanyName");
            _writer.Write(" = ");
            _writer.Write($"\"{value}\"");
        }

        /// <summary>
        /// Writes the Copyright property. This is optional.
        /// </summary>
        public void WriteCopyright(string value)
        {
            Indent = 0;
            _writer.Write("Copyright");
            _writer.Write(" = ");
            _writer.Write($"\"{value}\"");
        }

        /// <summary>
        /// Writes the RootModule property. This is optional.
        /// </summary>
        public void WriteRootModule(string value)
        {
            Indent = 0;
            _writer.Write("RootModule");
            _writer.Write(" = ");
            _writer.Write($"\"{value}\"");
        }

        /// <summary>
        /// Writes the ModuleVersion property. This is optional.
        /// </summary>
        public void WriteModuleVersion(Version value)
        {
            Indent = 0;
            _writer.Write("ModuleVersion");
            _writer.Write(" = ");
            _writer.Write($"\"{value}\"");
        }

        /// <summary>
        /// Writes the Description property. This is optional.
        /// </summary>
        public void WriteDescription(string value)
        {
            Indent = 0;
            _writer.Write("Description");
            _writer.Write(" = ");
            _writer.Write($"\"{value}\"");
        }

        /// <summary>
        /// Writes the ProcessorArchitecture property. This is optional.
        /// </summary>
        public void WriteProcessorArchitecture(ProcessorArchitecture value)
        {
            Indent = 0;
            _writer.Write("ProcessorArchitecture");
            _writer.Write(" = ");
            _writer.Write($"\"{value}\"");
        }

        /// <summary>
        /// Writes the PowerShellVersion property. This is optional.
        /// </summary>
        public void WritePowerShellVersion(Version value)
        {
            Indent = 0;
            _writer.Write("PowerShellVersion");
            _writer.Write(" = ");
            _writer.Write($"\"{value}\"");
        }

        /// <summary>
        /// Writes the ClrVersion property. This is optional.
        /// </summary>
        public void WriteClrVersion(Version value)
        {
            Indent = 0;
            _writer.Write("ClrVersion");
            _writer.Write(" = ");
            _writer.Write($"\"{value}\"");
        }

        /// <summary>
        /// Writes the DotNetFrameworkVersion property. This is optional.
        /// </summary>
        public void WriteDotNetFrameworkVersion(Version value)
        {
            Indent = 0;
            _writer.Write("DotNetFrameworkVersion");
            _writer.Write(" = ");
            _writer.Write($"\"{value}\"");
        }

        /// <summary>
        /// Writes the PowerShellHostName property. This is optional.
        /// </summary>
        public void WritePowerShellHostName(string value)
        {
            Indent = 0;
            _writer.Write("PowerShellHostName");
            _writer.Write(" = ");
            _writer.Write($"\"{value}\"");
        }

        /// <summary>
        /// Writes the PowerShellHostVersion property. This is optional.
        /// </summary>
        public void WritePowerShellHostVersion(Version value)
        {
            Indent = 0;
            _writer.Write("PowerShellHostVersion");
            _writer.Write(" = ");
            _writer.Write($"\"{value}\"");
        }

        /// <summary>
        /// Writes the RequiredModules property. This is optional.
        /// </summary>
        public void WriteRequiredModules(params object[] value)
        {
            Indent = 0;
            _writer.Write("RequiredModules");
            _writer.Write(" = ");
            _writer.WriteLine("@(");
            Indent = 3;
            for (int i = 0; i < value.Length; i++)
            {
                var current = value[i];
                _writer.Write('"');
                _writer.Write(current);
                _writer.Write('"');
                if (i != value.Length - 1)
                {
                    _writer.Write(", ");
                }
            }
            _writer.WriteLine();
            _writer.WriteLine(")");
            Indent = 0;
            _writer.Write($"\"{value}\"");
        }

        /// <summary>
        /// Writes the TypesToProcess property. This is optional.
        /// </summary>
        public void WriteTypesToProcess(params string[] value)
        {
            Indent = 0;
            _writer.Write("TypesToProcess");
            _writer.Write(" = ");
            _writer.WriteLine("@(");
            Indent = 3;
            for (int i = 0; i < value.Length; i++)
            {
                var current = value[i];
                _writer.Write('"');
                _writer.Write(current);
                _writer.Write('"');
                if (i != value.Length - 1)
                {
                    _writer.Write(", ");
                }
            }
            _writer.WriteLine();
            _writer.WriteLine(")");
            Indent = 0;
            _writer.Write($"\"{value}\"");
        }

        /// <summary>
        /// Writes the FormatsToProcess property. This is optional.
        /// </summary>
        public void WriteFormatsToProcess(params string[] value)
        {
            Indent = 0;
            _writer.Write("FormatsToProcess");
            _writer.Write(" = ");
            _writer.WriteLine("@(");
            Indent = 3;
            for (int i = 0; i < value.Length; i++)
            {
                var current = value[i];
                _writer.Write('"');
                _writer.Write(current);
                _writer.Write('"');
                if (i != value.Length - 1)
                {
                    _writer.Write(", ");
                }
            }
            _writer.WriteLine();
            _writer.WriteLine(")");
            Indent = 0;
            _writer.Write($"\"{value}\"");
        }

        /// <summary>
        /// Writes the ScriptsToProcess property. This is optional.
        /// </summary>
        public void WriteScriptsToProcess(params string[] value)
        {
            Indent = 0;
            _writer.Write("ScriptsToProcess");
            _writer.Write(" = ");
            _writer.WriteLine("@(");
            Indent = 3;
            for (int i = 0; i < value.Length; i++)
            {
                var current = value[i];
                _writer.Write('"');
                _writer.Write(current);
                _writer.Write('"');
                if (i != value.Length - 1)
                {
                    _writer.Write(", ");
                }
            }
            _writer.WriteLine();
            _writer.WriteLine(")");
            Indent = 0;
            _writer.Write($"\"{value}\"");
        }

        /// <summary>
        /// Writes the RequiredAssemblies property. This is optional.
        /// </summary>
        public void WriteRequiredAssemblies(params string[] value)
        {
            Indent = 0;
            _writer.Write("RequiredAssemblies");
            _writer.Write(" = ");
            _writer.WriteLine("@(");
            Indent = 3;
            for (int i = 0; i < value.Length; i++)
            {
                var current = value[i];
                _writer.Write('"');
                _writer.Write(current);
                _writer.Write('"');
                if (i != value.Length - 1)
                {
                    _writer.Write(", ");
                }
            }
            _writer.WriteLine();
            _writer.WriteLine(")");
            Indent = 0;
            _writer.Write($"\"{value}\"");
        }

        /// <summary>
        /// Writes the FileList property. This is optional.
        /// </summary>
        public void WriteFileList(params string[] value)
        {
            Indent = 0;
            _writer.Write("FileList");
            _writer.Write(" = ");
            _writer.WriteLine("@(");
            Indent = 3;
            for (int i = 0; i < value.Length; i++)
            {
                var current = value[i];
                _writer.Write('"');
                _writer.Write(current);
                _writer.Write('"');
                if (i != value.Length - 1)
                {
                    _writer.Write(", ");
                }
            }
            _writer.WriteLine();
            _writer.WriteLine(")");
            Indent = 0;
            _writer.Write($"\"{value}\"");
        }

        /// <summary>
        /// Writes the ModuleList property. This is optional.
        /// </summary>
        public void WriteModuleList(params object[] value)
        {
            Indent = 0;
            _writer.Write("ModuleList");
            _writer.Write(" = ");
            _writer.WriteLine("@(");
            Indent = 3;
            for (int i = 0; i < value.Length; i++)
            {
                var current = value[i];
                _writer.Write('"');
                _writer.Write(current);
                _writer.Write('"');
                if (i != value.Length - 1)
                {
                    _writer.Write(", ");
                }
            }
            _writer.WriteLine();
            _writer.WriteLine(")");
            Indent = 0;
            _writer.Write($"\"{value}\"");
        }

        /// <summary>
        /// Writes the FunctionsToExport property. This is optional.
        /// </summary>
        public void WriteFunctionsToExport(params string[] value)
        {
            Indent = 0;
            _writer.Write("FunctionsToExport");
            _writer.Write(" = ");
            _writer.WriteLine("@(");
            Indent = 3;
            for (int i = 0; i < value.Length; i++)
            {
                var current = value[i];
                _writer.Write('"');
                _writer.Write(current);
                _writer.Write('"');
                if (i != value.Length - 1)
                {
                    _writer.Write(", ");
                }
            }
            _writer.WriteLine();
            _writer.WriteLine(")");
            Indent = 0;
            _writer.Write($"\"{value}\"");
        }

        /// <summary>
        /// Writes the AliasesToExport property. This is optional.
        /// </summary>
        public void WriteAliasesToExport(params string[] value)
        {
            Indent = 0;
            _writer.Write("AliasesToExport");
            _writer.Write(" = ");
            _writer.WriteLine("@(");
            Indent = 3;
            for (int i = 0; i < value.Length; i++)
            {
                var current = value[i];
                _writer.Write('"');
                _writer.Write(current);
                _writer.Write('"');
                if (i != value.Length - 1)
                {
                    _writer.Write(", ");
                }
            }
            _writer.WriteLine();
            _writer.WriteLine(")");
            Indent = 0;
            _writer.Write($"\"{value}\"");
        }

        /// <summary>
        /// Writes the VariablesToExport property. This is optional.
        /// </summary>
        public void WriteVariablesToExport(params string[] value)
        {
            Indent = 0;
            _writer.Write("VariablesToExport");
            _writer.Write(" = ");
            _writer.WriteLine("@(");
            Indent = 3;
            for (int i = 0; i < value.Length; i++)
            {
                var current = value[i];
                _writer.Write('"');
                _writer.Write(current);
                _writer.Write('"');
                if (i != value.Length - 1)
                {
                    _writer.Write(", ");
                }
            }
            _writer.WriteLine();
            _writer.WriteLine(")");
            Indent = 0;
            _writer.Write($"\"{value}\"");
        }

        /// <summary>
        /// Writes the CmdletsToExport property. This is optional.
        /// </summary>
        public void WriteCmdletsToExport(params string[] value)
        {
            Indent = 0;
            _writer.Write("CmdletsToExport");
            _writer.Write(" = ");
            _writer.WriteLine("@(");
            Indent = 3;
            for (int i = 0; i < value.Length; i++)
            {
                var current = value[i];
                _writer.Write('"');
                _writer.Write(current);
                _writer.Write('"');
                if (i != value.Length - 1)
                {
                    _writer.Write(", ");
                }
            }
            _writer.WriteLine();
            _writer.WriteLine(")");
            Indent = 0;
            _writer.Write($"\"{value}\"");
        }

        /// <summary>
        /// Writes the DscResourcesToExport property. This is optional.
        /// </summary>
        public void WriteDscResourcesToExport(params string[] value)
        {
            Indent = 0;
            _writer.Write("DscResourcesToExport");
            _writer.Write(" = ");
            _writer.WriteLine("@(");
            Indent = 3;
            for (int i = 0; i < value.Length; i++)
            {
                var current = value[i];
                _writer.Write('"');
                _writer.Write(current);
                _writer.Write('"');
                if (i != value.Length - 1)
                {
                    _writer.Write(", ");
                }
            }
            _writer.WriteLine();
            _writer.WriteLine(")");
            Indent = 0;
            _writer.Write($"\"{value}\"");
        }

        /// <summary>
        /// Writes the CompatiblePSEditions property. This is optional.
        /// </summary>
        public void WriteCompatiblePSEditions(params string[] value)
        {
            Indent = 0;
            _writer.Write("CompatiblePSEditions");
            _writer.Write(" = ");
            _writer.WriteLine("@(");
            Indent = 3;
            for (int i = 0; i < value.Length; i++)
            {
                var current = value[i];
                _writer.Write('"');
                _writer.Write(current);
                _writer.Write('"');
                if (i != value.Length - 1)
                {
                    _writer.Write(", ");
                }
            }
            _writer.WriteLine();
            _writer.WriteLine(")");
            Indent = 0;
            _writer.Write($"\"{value}\"");
        }

        /// <summary>
        /// Writes the PrivateData property. This is optional.
        /// </summary>
        public void WritePrivateData(object value)
        {
            Indent = 0;
            _writer.Write("PrivateData");
            _writer.Write(" = ");
            _writer.Write($"\"{value}\"");
        }

        /// <summary>
        /// Writes the Tags property. This is optional.
        /// </summary>
        public void WriteTags(params string[] value)
        {
            Indent = 0;
            _writer.Write("Tags");
            _writer.Write(" = ");
            _writer.WriteLine("@(");
            Indent = 3;
            for (int i = 0; i < value.Length; i++)
            {
                var current = value[i];
                _writer.Write('"');
                _writer.Write(current);
                _writer.Write('"');
                if (i != value.Length - 1)
                {
                    _writer.Write(", ");
                }
            }
            _writer.WriteLine();
            _writer.WriteLine(")");
            Indent = 0;
            _writer.Write($"\"{value}\"");
        }

        /// <summary>
        /// Writes the ProjectUri property. This is optional.
        /// </summary>
        public void WriteProjectUri(Uri value)
        {
            Indent = 0;
            _writer.Write("ProjectUri");
            _writer.Write(" = ");
            _writer.Write($"\"{value}\"");
        }

        /// <summary>
        /// Writes the LicenseUri property. This is optional.
        /// </summary>
        public void WriteLicenseUri(Uri value)
        {
            Indent = 0;
            _writer.Write("LicenseUri");
            _writer.Write(" = ");
            _writer.Write($"\"{value}\"");
        }

        /// <summary>
        /// Writes the IconUri property. This is optional.
        /// </summary>
        public void WriteIconUri(Uri value)
        {
            Indent = 0;
            _writer.Write("IconUri");
            _writer.Write(" = ");
            _writer.Write($"\"{value}\"");
        }

        /// <summary>
        /// Writes the ReleaseNotes property. This is optional.
        /// </summary>
        public void WriteReleaseNotes(string value)
        {
            Indent = 0;
            _writer.Write("ReleaseNotes");
            _writer.Write(" = ");
            _writer.Write($"\"{value}\"");
        }

        /// <summary>
        /// Writes the HelpInfoUri property. This is optional.
        /// </summary>
        public void WriteHelpInfoUri(string value)
        {
            Indent = 0;
            _writer.Write("HelpInfoUri");
            _writer.Write(" = ");
            _writer.Write($"\"{value}\"");
        }

        /// <summary>
        /// Writes the DefaultCommandPrefix property. This is optional.
        /// </summary>
        public void WriteDefaultCommandPrefix(string value)
        {
            Indent = 0;
            _writer.Write("DefaultCommandPrefix");
            _writer.Write(" = ");
            _writer.Write($"\"{value}\"");
        }


    }
}

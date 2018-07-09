using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using DscBuildTools.DscTasks;
using DscBuildTools.Model;
using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;
using System.Xaml;
using System.Xml;
using DscBuildTools.Types;

namespace DscBuildTools
{
    public class ImportDscResourceConfigurationFileTask : AbstractDscResourceTask
    {
        [Required]
        public string FilePath { get; set; }

        public ITaskItem[] Configuration { get; set; }

        [Output]
        public ITaskItem DscResource { get; set; }

        [Output]
        public ITaskItem[] DscResourceProperties { get; set; }


        protected override bool ExecuteCore()
        {
            if (!File.Exists(FilePath))
            {
                LogError("FilePath Does Not Exist", "DSC001", "Task failed because the configuration file at path '{0}' simply does not exist.", FilePath);
                return false;
            }

            if (Configuration != null)
            {
                ITaskItem configuration = Configuration[0];

                DesiredStateConfiguration xresource;
                using (XmlReader xmlReader = XmlReader.Create(configuration.ItemSpec, new XmlReaderSettings()
                {
                    DtdProcessing = DtdProcessing.Ignore,
                    XmlResolver = null
                }))
                xresource = (DesiredStateConfiguration)XamlServices.Load(xmlReader);
            }


            DscResource resource = Model.DscResource.FromJson(File.ReadAllText(FilePath));


            CustomTaskItem resourceItem = new CustomTaskItem();
            resourceItem.EvaluatedIncludeEscaped = "_DscResource";
            resourceItem.SetMetadata("DscResourceName", resource.Name);
            resourceItem.SetMetadata("DscResourceFriendlyName", resource.FriendlyName);
            resourceItem.SetMetadata("DscResourceClassVersion", resource.ClassVersion.GetVersion());

            DscResource = resourceItem;
            ArrayBuilder<ITaskItem2> builder = new ArrayBuilder<ITaskItem2>();

            foreach (var property in resource.Properties)
            {
                CustomTaskItem propertyItem = new CustomTaskItem();
                propertyItem.EvaluatedIncludeEscaped = "_DscResourceProperty";
                propertyItem.SetMetadata("DscResourcePropertyName", property.Name);
                propertyItem.SetMetadata("DscResourcePropertyType", property.GetClrType());
                propertyItem.SetMetadata("DscResourcePropertyAttribute", property.Attribute.ToString());
                propertyItem.SetMetadata("DscResourcePropertyValues", property.GetPossibleValues());

                builder.Add(propertyItem);
            }

            DscResourceProperties = builder;
            return true;
        }
    }
}

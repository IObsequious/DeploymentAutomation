using System;
using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;

namespace DscBuildTools.DscTasks
{
    public class CreateDscResourceModuleTask : AbstractDscResourceTask
    {
        [Required]
        public string ConfigurationFilePath { get; set; }

        [Required]
        public string OutputPath { get; set; }

        [Required]
        public string IntermediateOutputPath { get; set; }


        protected override bool ExecuteCore()
        {





            return true;
        }
    }
}

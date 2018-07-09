using System;
using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;

namespace DscBuildTools.DscTasks
{
    public class WriteDscResourceMOFTask : AbstractDscResourceTask
    {
        [Required]
        public ITaskItem[] DscResources { get; set; }

        [Required]
        public ITaskItem[] DscResourceProperties { get; set; }


        protected override bool ExecuteCore()
        {




            return true;
        }
    }
}

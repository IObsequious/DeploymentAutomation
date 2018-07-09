using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;
using Microsoft.Build.Framework.XamlTypes;

namespace DscBuildTools.Model
{
    [ContentProperty("DscResources")]
    public class DesiredStateConfiguration
    {
        public string RootNamespace { get; set; }

        public string OutputDirectory { get; set; }

        public DesiredStateConfiguration()
        {
            
            DscResources = new List<DscResource>();
        }
        public List<DscResource> DscResources { get; set; }
    }
}

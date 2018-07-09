using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DscBuildTools.Types;
using Microsoft.Build.Framework;

namespace DscBuildTools.DscTasks
{
    public static class TaskItemConverter
    {
        //public static DscResource Convert(ITaskItem resource, ITaskItem[] properties)
        //{
        //    DscResource info = new DscResource();
        //    info.Name = resource.GetMetadata("DscResourceName");
        //    info.FriendlyName = resource.GetMetadata("DscResourceFriendlyName");
        //    foreach(var property in properties)
        //    {
        //        info.Properties.Add(Convert(property));
        //    }

        //    return info;
        //}
        //public static DscResourceProperty Convert(ITaskItem taskItem)
        //{
        //    string attribute = taskItem.GetMetadata()

        //    DscResourceProperty property = new DscResourceProperty();


        //    return property;
        //}
    }
}

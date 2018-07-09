using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using DscBuildTools.DscTasks;
using DscBuildTools.Model;
using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;

namespace DscBuildTools
{
    public class CustomTaskItem : ITaskItem2
    {
        private Dictionary<string, string> _metadata;

        public CustomTaskItem()
        {
            _metadata = new Dictionary<string, string>();
        }

        public string EvaluatedIncludeEscaped { get; set; }
        public string ItemSpec { get; set; }

        public ICollection MetadataNames => _metadata.Keys;

        public int MetadataCount => _metadata.Count;

        public IDictionary CloneCustomMetadata()
        {
            Dictionary<string, string> clone = new Dictionary<string, string>(_metadata);
            return clone;
        }

        public IDictionary CloneCustomMetadataEscaped()
        {
            Dictionary<string, string> clone = new Dictionary<string, string>(_metadata);
            return clone;
        }

        public void CopyMetadataTo(ITaskItem destinationItem)
        {
            foreach(var metadataEntry in _metadata)
            {
                destinationItem.SetMetadata(metadataEntry.Key, metadataEntry.Value);
            }
        }

        public string GetMetadata(string metadataName)
        {
            return _metadata[metadataName];
        }

        public string GetMetadataValueEscaped(string metadataName)
        {
            return System.Security.SecurityElement.Escape(_metadata[metadataName]);
        }

        public void RemoveMetadata(string metadataName)
        {
            _metadata.Remove(metadataName);
        }

        public void SetMetadata(string metadataName, string metadataValue)
        {
            _metadata[metadataName] = System.Security.SecurityElement.Escape(metadataValue);
        }

        public void SetMetadataValueLiteral(string metadataName, string metadataValue)
        {
            SetMetadata(metadataName, metadataValue);
        }
    }
}

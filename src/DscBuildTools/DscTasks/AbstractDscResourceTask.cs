using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xaml;
using System.Xml;
using DscBuildTools.Types;
using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;

namespace DscBuildTools.DscTasks
{
    public abstract class AbstractDscResourceTask : Task
    {
        private int _startLineNumber;
        private int _startColumnNumber;


        public sealed override bool Execute()
        {
            _startColumnNumber = BuildEngine.ColumnNumberOfTaskNode;
            _startLineNumber = BuildEngine.LineNumberOfTaskNode;

            LogMessage($"{this.GetType().Name}");
            bool result = ExecuteCore();
            return result;
        }

        protected abstract bool ExecuteCore();

        protected DesiredStateConfiguration GetDesiredStateConfiguration(ITaskItem taskItem)
        {
            DesiredStateConfiguration configuration;
            using (XmlReader xmlReader = XmlReader.Create(taskItem.ItemSpec, new XmlReaderSettings()
            {
                DtdProcessing = DtdProcessing.Ignore,
                XmlResolver = null
            }))
            configuration = (DesiredStateConfiguration)XamlServices.Load(xmlReader);
            configuration.Initialize();
            return configuration;
        }

        protected void LogVerbose(string message, params object[] messageArgs)
        {
            Log.LogMessageFromText(string.Format(message, messageArgs), MessageImportance.Low);
        }

        protected void LogError(string category, string errorCode, string message, params object[] messageArgs)
        {
            LogError(category, errorCode, errorCode, BuildEngine.ProjectFileOfTaskNode, _startLineNumber, _startLineNumber, BuildEngine.LineNumberOfTaskNode, BuildEngine.ColumnNumberOfTaskNode, message, messageArgs);
        }

        public void LogError(string subcategory, string errorCode, string helpKeyword, string file, int lineNumber, int columnNumber, int endLineNumber, int endColumnNumber, string message, params object[] messageArgs)
        {
            Log.LogError(
                subcategory,
                errorCode,
                helpKeyword,
                file,
                lineNumber,
                columnNumber,
                endLineNumber,
                endColumnNumber,
                message,
                messageArgs
                );
        }

        protected void LogMessage(string message)
        {
            Log.LogMessageFromText($"{message}", MessageImportance.Normal);
        }
    }
}

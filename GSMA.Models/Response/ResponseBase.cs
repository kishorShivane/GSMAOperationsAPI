using System;
using System.Collections.Generic;
using System.Text;

namespace GSMA.Models.Response
{
    public class ResponseBase
    {
        public ResponseBase(Guid _trackingInfo)
        {
            TrackingInformation = _trackingInfo;
            Messages = new List<string>();
        }

        public ResponseBase()
        {
            TrackingInformation = Guid.NewGuid();
            Messages = new List<string>();
        }
        public int ErrorCount { get; set; } = 0;
        public int InformationCount { get; set; } = 0;
        public List<string> Messages { get; set; }
        public Guid TrackingInformation { get; set; }
        public int WarningCount { get; set; } = 0;

        public void AddErrorMessage(string messageText)
        {
            ErrorCount++;
            AddMessage(messageText);
        }
        public void AddInformationMessage(string messageText)
        {
            InformationCount++;
            AddMessage(messageText);
        }
        private void AddMessage(string messageText)
        {
            Messages.Add(messageText);
        }
        public void AddWarningMessage(string messageText)
        {
            AddMessage(messageText);
        }
    }
}

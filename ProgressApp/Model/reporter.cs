using System;
using System.Collections.Generic;
using System.Text;

namespace ProgressApp.Model
{
    public class reporter
    {
        public string ReporterFullname { get; set; }
        public string ReporterPhoneNo { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public string CategorySubType { get; set; }
        public string LongX { get; set; }
        public string LatY { get; set; }
        public string CapturedImage { get; set; }
        public string CapturedIssueDate { get; set; }
    }

    public class incidenceApp
    {
        public string incidenceId { get; set; }
        public string reporterFullname { get; set; }
        public string reporterPhoneNo { get; set; }
        public string description { get; set; }
        public string category { get; set; }
        public string categorySubType { get; set; }
        public string longX { get; set; }
        public string latY { get; set; }
        public string capturedImage { get; set; }
        public string capturedIssueDate { get; set; }
        public string districtId { get; set; }
        public string districtName { get; set; }
        public string feederId { get; set; }
        public string feederName { get; set; }
        public string dssId { get; set; }
        public string dssName { get; set; }
        public string createdby { get; set; }
        public string createdat { get; set; }
        public string editedby { get; set; }
        public string editedat { get; set; }

    }
}

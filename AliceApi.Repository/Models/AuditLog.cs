//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AliceApi.Repository.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class AuditLog
    {
        public System.Guid AuditLogId { get; set; }
        public string UserName { get; set; }
        public Nullable<System.DateTime> EventDateTime { get; set; }
        public string EventType { get; set; }
        public string TableName { get; set; }
        public Nullable<System.Guid> RecordId { get; set; }
        public string ColumnName { get; set; }
        public string OriginalValue { get; set; }
        public string NewValue { get; set; }
    }
}

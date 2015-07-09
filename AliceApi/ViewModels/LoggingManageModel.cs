using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

using AliceApi.Models;
using AliceApi.Services.Paging;

namespace AliceApi.ViewModels
{
    public class LoggingManageModel
    {
        public IPagedList<LogEvent> LogEvents { get; set; }

        [DataType("DateTime")]
        public DateTime StartDate { get; set; }

        [DataType("DateTime")]
        public DateTime EndDate { get; set; }

        public string LogSourceName { get; set; }

        public string LogLevels { get; set; }
        public string LogLevel { get; set; }

        public string LoggerProviderName { get; set; }

        public string Period { get; set; }

        public int CurrentPageIndex { get; set; }
        public int PageSize { get; set; }

        public LoggingManageModel()
        {
            CurrentPageIndex = 0;
            PageSize = 20;
        }        
    }
}
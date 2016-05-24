﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Web;
using System.Web.Mvc;

using AliceApi.Helpers;
using AliceApi.Models.Repository;
using AliceApi.Models;
using AliceApi.Models;

using AliceApi.Models.Google.Visualization;
using AliceApi.Services.Paging;
using AliceApi.Services.RSS;

namespace AliceApi.Controllers
{
    //[Authorize]
    public partial class LoggingController : Controller
    {
        private readonly ILogReportingFacade loggingRepository;

        public LoggingController()
        {
            loggingRepository = new LogReportingFacade();
        }

        public LoggingController(ILogReportingFacade repository)
        {
            loggingRepository = repository;
        }

        /// <summary>
        /// Returns the Index view
        /// </summary>
        /// <param name="Period">Text representation of the date time period. eg: Today, Yesterday, Last Week etc.</param>
        /// <param name="LoggerProviderName">Elmah, Log4Net, NLog, Health Monitoring etc</param>
        /// <param name="LogLevel">Debug, Info, Warning, Error, Fatal</param>
        /// <param name="page">The current page index (0 based)</param>
        /// <param name="PageSize">The number of records per page</param>
        /// <returns></returns>
        public ActionResult Index(string Period, string LoggerProviderName, string LogLevel, int? page, int? PageSize)
        {            
            // Set up our default values
            string defaultPeriod = Session["Period"] == null ? "Today" : Session["Period"].ToString();
            string defaultLogType = Session["LoggerProviderName"] == null ? "All" : Session["LoggerProviderName"].ToString();
            string defaultLogLevel = Session["LogLevel"] == null ? "Error" : Session["LogLevel"].ToString();

            // Set up our view model
            LoggingIndexModel model = new LoggingIndexModel();
            

            model.Period = (Period == null) ? defaultPeriod : Period;
            model.LoggerProviderName = (LoggerProviderName == null) ? defaultLogType : LoggerProviderName;
            model.LogLevel = (LogLevel == null) ? defaultLogLevel : LogLevel;
            model.CurrentPageIndex = page.HasValue ? page.Value - 1 : 0;
            model.PageSize = PageSize.HasValue ? PageSize.Value : 20;

            TimePeriod timePeriod = TimePeriodHelper.GetUtcTimePeriod(model.Period);            

            // Grab the data from the database
            model.LogEvents = loggingRepository.GetByDateRangeAndType(model.CurrentPageIndex, model.PageSize, timePeriod.Start, timePeriod.End, model.LoggerProviderName, model.LogLevel);

            // Put this into the ViewModel so our Pager can get at these values
            ViewData["Period"] = model.Period;
            ViewData["LoggerProviderName"] = model.LoggerProviderName;
            ViewData["LogLevel"] = model.LogLevel;
            ViewData["PageSize"] = model.PageSize;

            // Put the info into the Session so that when we browse away from the page and come back that the last settings are rememberd and used.
            Session["Period"] = model.Period;
            Session["LoggerProviderName"] = model.LoggerProviderName;
            Session["LogLevel"] = model.LogLevel;

            int i = model.LogEvents.TotalItemCount;
            int ii = model.LogEvents.PageNumber;
            int iii = model.LogEvents.PageCount;
            
            return View(model);
        }

        //
        // GET: /Logging/Details/5
        public ActionResult Details(string loggerProviderName, string id)
        {
            LogEvent logEvent = loggingRepository.GetById(loggerProviderName, id);

            return View(logEvent);
        }   

        // string Period, string LoggerProviderName, string LogLevel, int? page, int? PageSize
        public ActionResult Search()
        {
            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Period"></param>
        /// <param name="LoggerProviderName"></param>
        /// <param name="LogLevel"></param>
        /// <returns></returns>
        public JsonResult ChartData(string Period, string LoggerProviderName, string LogLevel)
        {                        
            if (Period == "Today" || Period == "Yesterday")
            {
                return ChartDataByHour(Period, LoggerProviderName, LogLevel);
            }
            else
            {
                ChartDataByDay(Period, LoggerProviderName, LogLevel);

                return ChartDataByDay(Period, LoggerProviderName, LogLevel);
            }
        }        

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Period"></param>
        /// <param name="LoggerProviderName"></param>
        /// <param name="logLevel"></param>
        /// <returns></returns>
        public JsonResult ChartDataByHour(string Period, string LoggerProviderName, string logLevel)
        {
            Func<LogEvent, Object> groupByClause = c => c.LogDate.ToLocalTime().Hour;

            return ChartDataByTimePeriod(Period, LoggerProviderName, logLevel, "", groupByClause);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Period"></param>
        /// <param name="LoggerProviderName"></param>
        /// <param name="logLevel"></param>
        /// <returns></returns>
        public JsonResult ChartDataByDay(string Period, string LoggerProviderName, string logLevel)
        {
            Func<LogEvent, Object> groupByClause = c => c.LogDate.ToLocalTime().Date;
            
            return ChartDataByTimePeriod(Period, LoggerProviderName, logLevel, "dd/MM/yyyy", groupByClause);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Period"></param>
        /// <param name="LoggerProviderName"></param>
        /// <param name="logLevel"></param>
        /// <param name="keyFormatString"></param>
        /// <param name="groupByClause"></param>
        /// <returns></returns>
        public JsonResult ChartDataByTimePeriod(string Period, string LoggerProviderName, string logLevel, string keyFormatString, Func<LogEvent, Object> groupByClause)
        {
            TimePeriod timePeriod = TimePeriodHelper.GetUtcTimePeriod(Period);

            // Grab ALL entries for the chart (DO NOT PAGE REPORTING DATA!!!)
            IPagedList<LogEvent> chartEntries = loggingRepository.GetByDateRangeAndType(0, Int32.MaxValue, timePeriod.Start, timePeriod.End, LoggerProviderName, logLevel);

            var groupedByDate = chartEntries.GroupBy(groupByClause).OrderBy(y => y.Key);

            var groupedByDateAndThenName = groupedByDate.Select(group => new { Key = group.Key, NestedGroup = group.ToLookup(result => result.LoggerProviderName, result => result.Id) });

            var LoggerNames = (from logEvent in chartEntries
                               select new { Name = logEvent.LoggerProviderName }
                               ).Distinct().OrderBy(item => item.Name);

            ChartData chartData = new ChartData();

            // Add columns
            chartData.AddColumn(new ChartColumn("0", "Period", "string"));
            int columnIndex = 1;
            foreach (var name in LoggerNames)
            {
                chartData.AddColumn(new ChartColumn(columnIndex.ToString(), name.Name.ToString(), "number"));
                columnIndex++;
            }

            // add row data
            foreach (var myDate in groupedByDateAndThenName)
            {
                ChartRow row = new ChartRow();

                string dateString = (myDate.Key is DateTime) ? ((DateTime)myDate.Key).ToString(keyFormatString) : myDate.Key.ToString();
                row.AddCellItem(new ChartCellItem(dateString, ""));

                foreach (var name in LoggerNames)
                {
                    bool valueFound = false;
                    foreach (var myLogger in myDate.NestedGroup)
                    {
                        if (myLogger.Key == name.Name.ToString())
                        {
                            row.AddCellItem(new ChartCellItem(myLogger.Count(), ""));
                            valueFound = true;
                        }
                    }
                    if (!valueFound) row.AddCellItem(new ChartCellItem(0, ""));
                }

                chartData.AddRow(row);
            }

            return Json(chartData, "text/x-json", System.Text.Encoding.UTF8, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Gets the chart
        /// </summary>
        /// <param name="Period"></param>
        /// <param name="LoggerProviderName"></param>
        /// <param name="LogLevel"></param>
        /// <returns></returns>
        public ActionResult Chart(string Period, string LoggerProviderName, string LogLevel)
        {            
            string defaultPeriod = Session["Period"] == null ? "Today" : Session["Period"].ToString();
            string defaultLoggerProviderName = Session["LoggerProviderName"] == null ? "All" : Session["LoggerProviderName"].ToString();
            string defaultLogLevel = Session["LogLevel"] == null ? "Error" : Session["LogLevel"].ToString();

            LoggingIndexModel model = new LoggingIndexModel();

            model.Period = (Period == null) ? defaultPeriod : Period;
            model.LoggerProviderName = (LoggerProviderName == null) ? defaultLoggerProviderName : LoggerProviderName;
            model.LogLevel = (LogLevel == null) ? defaultLogLevel : LogLevel;

            return View(model);
        }







        #region Manage Logs
        /// <summary>
        /// Filter Button - Does the same thing as INDEX but is on the Manage View. 
        /// </summary>
        /// <param name="Period"></param>
        /// <param name="LoggerProviderName"></param>
        /// <param name="LogLevel"></param>
        /// <param name="page"></param>
        /// <param name="PageSize"></param>
        /// <returns></returns>
        [HttpPost]
        ///[MultiButtonAttribute]
        //[ActionName(AppActions.View)]
        public ActionResult btnFilter(string Period, string LoggerProviderName, string LogLevel, int? page, int? PageSize)
        {
            // NOTE: Might be able to polish this out more and abstract this to something more common 
            // because it's used by three different ActionResult method calls on this page. 

            // These values may or not already exist in the grand scheme.  
            string defaultPeriod = Session["Period"] == null ? "Today" : Session["Period"].ToString();
            string defaultLogType = Session["LoggerProviderName"] == null ? "All" : Session["LoggerProviderName"].ToString();
            string defaultLogLevel = Session["LogLevel"] == null ? "Error" : Session["LogLevel"].ToString();

            LoggingManageModel model = new LoggingManageModel();
            model.Period = (Period == null) ? defaultPeriod : Period;
            model.LoggerProviderName = (LoggerProviderName == null) ? defaultLogType : LoggerProviderName;
            model.LogLevel = (LogLevel == null) ? defaultLogLevel : LogLevel;
            model.CurrentPageIndex = page.HasValue ? page.Value - 1 : 0;
            model.PageSize = PageSize.HasValue ? PageSize.Value : 20;

            TimePeriod timePeriod = TimePeriodHelper.GetUtcTimePeriod(model.Period);// Slick... 

            model.LogEvents = loggingRepository.GetByDateRangeAndType(model.CurrentPageIndex, model.PageSize, timePeriod.Start, timePeriod.End, model.LoggerProviderName, model.LogLevel);

            ViewData["Period"] = model.Period;
            ViewData["LoggerProviderName"] = model.LoggerProviderName;
            ViewData["LogLevel"] = model.LogLevel;
            ViewData["PageSize"] = model.PageSize;

            ViewData["Message"] = "";
            Session["Message"] = "";// if it exists...

            Session["Period"] = model.Period;
            Session["LoggerProviderName"] = model.LoggerProviderName;
            Session["LogLevel"] = model.LogLevel;

            return View(model);
        }

        /// <summary>
        /// GET: /Logging/Manage - Initial load of the 'Manage' View 
        /// </summary>
        /// <param name="Period"></param>
        /// <param name="LoggerProviderName"></param>
        /// <param name="LogLevel"></param>
        /// <param name="page"></param>
        /// <param name="PageSize"></param>
        /// <param name="iWtf"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Manage(string Period, string LoggerProviderName, string LogLevel, int? page, int? PageSize)
        {
            string defaultPeriod = Session["Period"] == null ? "Today" : Session["Period"].ToString();
            string defaultLogType = Session["LoggerProviderName"] == null ? "All" : Session["LoggerProviderName"].ToString();
            string defaultLogLevel = Session["LogLevel"] == null ? "Error" : Session["LogLevel"].ToString();

            LoggingManageModel model = new LoggingManageModel();

            model.Period = (Period == null) ? defaultPeriod : Period;
            model.LoggerProviderName = (LoggerProviderName == null) ? defaultLogType : LoggerProviderName;
            model.LogLevel = (LogLevel == null) ? defaultLogLevel : LogLevel;
            model.CurrentPageIndex = page.HasValue ? page.Value - 1 : 0;
            model.PageSize = PageSize.HasValue ? PageSize.Value : 20;

            TimePeriod timePeriod = TimePeriodHelper.GetUtcTimePeriod(model.Period);

            model.LogEvents = loggingRepository.GetByDateRangeAndType(model.CurrentPageIndex, model.PageSize, timePeriod.Start, timePeriod.End, model.LoggerProviderName, model.LogLevel);

            ViewData["Period"] = model.Period;
            ViewData["LoggerProviderName"] = model.LoggerProviderName;
            ViewData["LogLevel"] = model.LogLevel;
            ViewData["PageSize"] = model.PageSize;

            //ViewData["Message"] = "";
            //Session["Message"] = "";// if it exists...

            Session["Period"] = model.Period;
            Session["LoggerProviderName"] = model.LoggerProviderName;
            Session["LogLevel"] = model.LogLevel;

            return View(model);
        }

        /// <summary>
        /// POST: /Logging/Manage/ - Ths part may still need work... 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        //[MultiButtonAttribute]// not sure this is necessary... 
        public ActionResult btnDelete(string Period, string LoggerProviderName, string LogLevel)
        {
            try
            {
                string defaultPeriod = Session["Period"] == null ? "Today" : Session["Period"].ToString();
                string defaultLogType = Session["LoggerProviderName"] == null ? "All" : Session["LoggerProviderName"].ToString();
                string defaultLogLevel = Session["LogLevel"] == null ? "Error" : Session["LogLevel"].ToString();

                LoggingManageModel model = new LoggingManageModel();
                model.LogSourceName = LoggerProviderName;
                model.Period = (Period == null) ? defaultPeriod : Period;
                model.LoggerProviderName = (LoggerProviderName == null) ? defaultLogType : LoggerProviderName;
                model.LogLevel = (LogLevel == null) ? defaultLogLevel : LogLevel;
                //model.CurrentPageIndex = page.HasValue ? page.Value - 1 : 0;
                //model.PageSize = PageSize.HasValue ? PageSize.Value : 20;

                string[] logLevels = new string[] { };
                if (LogLevel == "All")
                {
                    logLevels = new string[] { "All", "Debug", "Info", "Warn", "Error", "Fatal" };
                }
                else
                {
                    logLevels = new string[] { LogLevel };
                }

                TimePeriod timePeriod = TimePeriodHelper.GetUtcTimePeriod(Period);
                model.StartDate = timePeriod.Start;
                model.EndDate = timePeriod.End;

                // all of them or only one
                if (LoggerProviderName == "All")
                {
                    foreach (string providerName in loggingRepository.GetLogProviders().Keys)
                    {
                        loggingRepository.ClearLog(providerName, model.StartDate, model.EndDate, logLevels);
                    }
                }
                else
                {
                    loggingRepository.ClearLog(model.LogSourceName, model.StartDate, model.EndDate, logLevels);
                }

                // show a message that it was successfull.                  
                Session["Message"] = "The Log Has Been Cleared";// had to use session I couldn't get it to pass between views
                
                return RedirectToAction("Manage");
            }
            catch (Exception ex)
            {
                //ViewData["Message"] = ex.Message.ToString();
                Session["Message"] = ex.Message.ToString();
                //NLogLogger logger = new NLogLogger();
                //logger.Error(ex);

                //Log4NetLogger logger2 = new Log4NetLogger();
                //logger2.Error(ex);

                return RedirectToAction("Manage");
            }
        }

        /// <summary>
        /// Clear All Logs - Deletes ALL log files from ALL providers.  SCORTCH THE EARTH...
        /// </summary>
        /// <param name="Period"></param>
        /// <param name="LoggerProviderName"></param>
        /// <param name="LogLevel"></param>
        /// <returns></returns>
        [HttpPost]
        //[MultiButtonAttribute]// not sure this is necessary... 
        public virtual ActionResult btnClearAllLogs()
        {
            try
            {
                string[] logLevels = new string[] { "All", "Debug", "Info", "Warn", "Error", "Fatal" };

                DateTime oldestPossible = DateTime.Now.AddYears(-100);
                DateTime futurePossible = DateTime.Now.AddYears(+100);

                // work it
                foreach (string providerName in loggingRepository.GetLogProviders().Keys)
                {
                    loggingRepository.ClearLog(providerName, oldestPossible, futurePossible, logLevels);
                }

                // show a message that it was successfull.  
                Session["Message"] = "The log has been cleared";


                return RedirectToAction("Manage");
            }
            catch (Exception ex)
            {
                //Session["Message"] = ex.Message.ToString();

                //NLogLogger logger = new NLogLogger();
                //logger.Error(ex);

                //Log4NetLogger logger2 = new Log4NetLogger();
                //logger2.Error(ex);

                return RedirectToAction("Manage");
            }
        }
        #endregion
        ////
        //// GET: /Logging/Manage

        //public virtual ActionResult Manage()
        //{
            
        //    LoggingManageModel model = new LoggingManageModel();

        //    return View(model);
        //}

        ////
        //// POST: /Logging/Manage/

        //[HttpPost]
        //public virtual ActionResult Manage(LoggingManageModel model)
        //{
        //    try
        //    {
        //        string[] logLevels = new string[] { "Warn" };
        //        loggingRepository.ClearLog(model.LogSourceName, model.StartDate, model.EndDate, logLevels);
                
        //        ViewData["Message"] = "The log has been cleared";

        //        return View(model);
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}
































        /// <summary>
        /// 
        /// </summary>
        /// <param name="Period"></param>
        /// <param name="LoggerProviderName"></param>
        /// <param name="LogLevel"></param>
        /// <returns></returns>
        public virtual FeedResult RssFeed(string Period, string LoggerProviderName, string LogLevel)
        {
            string defaultPeriod = Session["Period"] == null ? "This Week" : Session["Period"].ToString();
            string defaultLoggerProviderName = Session["LoggerProviderName"] == null ? "All" : Session["LoggerProviderName"].ToString();
            string defaultLogLevel = Session["LogLevel"] == null ? "Error" : Session["LogLevel"].ToString();

            Period = (Period == null) ? defaultPeriod : Period;
            LoggerProviderName = (LoggerProviderName == null) ? defaultLoggerProviderName : LoggerProviderName;
            LogLevel = (LogLevel == null) ? defaultLogLevel : LogLevel;

            TimePeriod timePeriod = TimePeriodHelper.GetUtcTimePeriod(Period);

            // Grab ALL entries for the feed (DO NOT PAGE feed DATA!!!)
            IPagedList<LogEvent> chartEntries = loggingRepository.GetByDateRangeAndType(0, Int32.MaxValue, timePeriod.Start, timePeriod.End, LoggerProviderName, LogLevel);

            var postItems = chartEntries.Select(p => new SyndicationItem(string.Format("{0} - {1} - {2}", p.LogDate, p.Level, p.LoggerProviderName), p.Message, new Uri(Url.AbsoluteAction("Details", new { LoggerProviderName = p.LoggerProviderName, Id = p.Id }))));

            Uri feedAlternateLink = Url.ActionFull("Index", "Logging");

            var feed = new SyndicationFeed("MVC Logging Demo -> Log Reporting", string.Format("Log Provider: {0}, Log Level : {1}, From {2} to {3} ({4})", LoggerProviderName, LogLevel, timePeriod.Start.ToShortDateString(), timePeriod.End.ToShortDateString(), Period), feedAlternateLink, postItems)
            {                
                Language = "en-US"
            };

            return new FeedResult(new Rss20FeedFormatter(feed));
        }
    }
}

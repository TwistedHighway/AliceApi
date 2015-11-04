CREATE VIEW vw_aspnet_WebEvents_extended
AS
 
SELECT
 webEvent.EventId
 , webEvent.EventTimeUtc
 , webEvent.EventTime
 , webEvent.EventType
 , webEvent.EventSequence
 , webEvent.EventOccurrence
 , webEvent.EventCode
 , webEvent.EventDetailCode
 , webEvent.[Message]
 , webEvent.ApplicationPath
 , webEvent.ApplicationVirtualPath
 , webEvent.MachineName
 , webEvent.RequestUrl
 , webEvent.ExceptionType
 , webEvent.Details
 , webEventCodes.[Level]
FROM
 dbo.aspnet_WebEvent_Events AS webEvent
INNER JOIN
 dbo.aspnet_WebEvent_ErrorCodes AS webEventCodes ON webEvent.EventCode = webEventCodes.EventCode
namespace Control.Cloud.Dms.MessageRouter.Externals;

public class DmsMessageQueueClientOptions
{
    public string ServiceBusConnectionString { get; set; }
    
    public string HeartbeatQueue { get; set; }
    
    public string ModemInfoQueue { get; set; }
    
    public string StatusReportMessageQueue { get; set; }
    
    public string TwinChangeMessageQueue { get; set; }
    
    public string InitConnectionMessageQueue { get; set; }
    
    public string OtherMessageQueue { get; set; }
}
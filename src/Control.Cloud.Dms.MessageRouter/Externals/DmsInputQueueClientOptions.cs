namespace Control.Cloud.Dms.MessageRouter.Externals;

public class DmsInputQueueClientOptions
{
    public string ServiceBusConnectionString { get; set; }
    
    public string DmsInputQueue { get; set; }
}
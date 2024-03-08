namespace Control.Cloud.Dms.MessageRouter.Models.Dms;

public class DmsMessage
{
    public DateTime timestamp { get; set; }

    public String uptime { get; set; }

    public string DeviceId { get; set; }

    public string JsonData { get; set; }
        
    public IotHubs iotHub { get; set; }
    
    public DmsMessageType DmsMessageType { get; set; }

    public int MessageVersionNumber { get; set; } = 1; //Default to 1 if not supplied by iot hubs
}
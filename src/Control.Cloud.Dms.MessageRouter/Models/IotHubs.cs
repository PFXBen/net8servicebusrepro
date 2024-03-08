namespace Control.Cloud.Dms.MessageRouter.Models;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum IotHubs
{
    [Description("iot-telem-01")] IotHubTelem01,
    [Description("iot-telem-02")] IotHubTelem02,
}
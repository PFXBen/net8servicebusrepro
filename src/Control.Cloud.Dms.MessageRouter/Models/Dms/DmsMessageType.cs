using System.ComponentModel;
using System.Text.Json.Serialization;

namespace Control.Cloud.Dms.MessageRouter.Models.Dms;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum DmsMessageType
{
    [Description("deviceInfo")]
    ModemInfo, //10 mins 1.3.4, 80 seconds after 1.3.5, interface info, IMEI, IMSI etc
    [Description("initConnect")]
    InitConnect, //1.3.5 onwards init connect
    [Description("heartbeat")]
    Heartbeat, //1.3.5 on 15 second intervals
    [Description("newDevice")]
    NewDevice, //1.4 onwards, sent once per device lifetime, immediately after connection
    [Description("statusReport")]
    StatusReport,
    [Description("deviceOff")]
    DeviceOff,
    [Description("watchdog")]
    Watchdog,
    [Description("coverageAlert")]
    CoverageAlert,
    [Description("twinChangeEvent")]
    TwinChangeEvent
}
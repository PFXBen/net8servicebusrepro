namespace Control.Cloud.Dms.MessageRouter.Functions;

public class MessageRouterFunction
{
    private readonly ILogger<MessageRouterFunction> _log;
    private readonly ServiceBusClient _dmsInputClient;
    private readonly ServiceBusClient _dmsMessageClient;
    private readonly DmsMessageQueueClientOptions _dmsMessageOptions;

    public MessageRouterFunction(IAzureClientFactory<ServiceBusClient> sbClientFatory,
        ILogger<MessageRouterFunction> log, IOptions<DmsMessageQueueClientOptions> dmsMessageOptions)
    {
        _dmsMessageOptions = dmsMessageOptions.Value;
        _dmsInputClient = sbClientFatory.CreateClient("DmsInputSb");
        _dmsMessageClient = sbClientFatory.CreateClient("DmsMessageSb");
        _log = log;
    }

    [Function(nameof(ProcessInputtedMessage))]
    public async Task ProcessInputtedMessage(
        [ServiceBusTrigger("%DmsInputQueueClientOptions:DmsInputQueue%",
            Connection = "DmsInputQueueClientOptions:ServiceBusConnectionString", IsBatched = false)]
        ServiceBusReceivedMessage queueMessage)
    {
        _log.LogInformation($"Dms router function triggered " +
                            $"for message id: {queueMessage.MessageId}");

        try
        {
            byte[] buffer = queueMessage.Body.ToArray();
            string messageAsString = Encoding.UTF8.GetString(buffer, 0, buffer.Length);

            var deviceMessage = JsonSerializer.Deserialize<DmsMessage>(messageAsString);
            //Get the type and switch on that
            var messageType = deviceMessage.DmsMessageType;

            switch (messageType)
            {
                case DmsMessageType.Heartbeat:
                    var heartBeatMessage = new ServiceBusMessage(messageAsString);
                    await _dmsMessageClient.CreateSender(_dmsMessageOptions.HeartbeatQueue).SendMessageAsync(heartBeatMessage);
                    break;
                case DmsMessageType.ModemInfo:
                    var modemInfoMessage = new ServiceBusMessage(messageAsString);
                    await _dmsMessageClient.CreateSender(_dmsMessageOptions.ModemInfoQueue)
                        .SendMessageAsync(modemInfoMessage);
                    break;
                case DmsMessageType.StatusReport:
                    var statusReportMessage = new ServiceBusMessage(messageAsString);
                    //Send to both status report and location queues
                    await _dmsMessageClient.CreateSender(_dmsMessageOptions.StatusReportMessageQueue)
                        .SendMessageAsync(statusReportMessage);
                    break;
                case DmsMessageType.TwinChangeEvent:
                    var twinChangeMessage = new ServiceBusMessage(messageAsString);
                    await _dmsMessageClient.CreateSender(_dmsMessageOptions.TwinChangeMessageQueue)
                        .SendMessageAsync(twinChangeMessage);
                    break;
                case DmsMessageType.InitConnect:
                    var initConnectMessage = new ServiceBusMessage(messageAsString);
                    await _dmsMessageClient.CreateSender(_dmsMessageOptions.InitConnectionMessageQueue)
                        .SendMessageAsync(initConnectMessage);
                    break;
                case DmsMessageType.NewDevice:
                case DmsMessageType.Watchdog:
                case DmsMessageType.CoverageAlert:
                case DmsMessageType.DeviceOff:
                    var otherMessage = new ServiceBusMessage(messageAsString);
                    await _dmsMessageClient.CreateSender(_dmsMessageOptions.OtherMessageQueue)
                        .SendMessageAsync(otherMessage);
                    break;
            }
        }
        catch (Exception ex)
        {
            _log.LogError($"An error has occurred processing the queue Message");
            _log.LogError(ex.Message);
            _log.LogError(ex.StackTrace);
        }
    }

}
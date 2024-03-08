//System
global using System.Text;
global using System.ComponentModel;
global using System.Text.Json.Serialization;
global using System.Text.Json;
//Control
global using Control.Cloud.Dms.MessageRouter.Infrastructure;
global using Control.Cloud.Dms.MessageRouter.Externals;
global using Control.Cloud.Dms.MessageRouter.Models.Dms;

//Microsoft
global using Microsoft.Extensions.Options;
global using Microsoft.Extensions.Azure;
global using Microsoft.Extensions.DependencyInjection;
global using Microsoft.Azure.Functions.Worker;
global using Microsoft.Extensions.Configuration;
global using Microsoft.Extensions.Hosting;
global using Microsoft.Extensions.Logging;
global using Microsoft.ApplicationInsights.Channel;
global using Microsoft.ApplicationInsights.Extensibility;
//Third Party
global using Azure.Messaging.ServiceBus;

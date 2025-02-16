using System;
using System.Text.Json;
using System.Threading.Tasks;
using Azure;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Azure.Messaging.ServiceBus;
using Azure.Messaging.EventGrid;

public static class GradeEventProcessor
{
    private static readonly string EventGridTopicEndpoint = Environment.GetEnvironmentVariable("EVENT_GRID_TOPIC_ENDPOINT");
    private static readonly string EventGridAccessKey = Environment.GetEnvironmentVariable("EVENT_GRID_ACCESS_KEY");

    [FunctionName("ProcessGradeEvent")]
    public static async Task RunAsync(
        [ServiceBusTrigger("edu-grades-queue-sbox", Connection = "ServiceBusConnection")] ServiceBusReceivedMessage message,
        ILogger log)
    {
        log.LogInformation("Received a new grade event.");

        try
        {
            var eventBody = message.Body.ToString();
            
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            var eventData = JsonSerializer.Deserialize<GradeEvent>(eventBody, options);


            if (eventData == null)
            {
                log.LogError("Invalid event data. Message body: {eventBody}", eventBody);
                return;
            }
            log.LogError("Valid event data. Message body: {eventBody}", eventBody);

            log.LogInformation("Processing event: {eventType} for StudentId {studentId}", 
                eventData.EventType, eventData.Data.StudentId);

            // Simulate SIS Update
            log.LogInformation("Updating SIS with new grade data...");
            await Task.Delay(500); // Simulate API call

            // Publish new event for notifications
            log.LogInformation("Publishing notification event...");
            await PublishEventToEventGrid(eventData, log);

            log.LogInformation("Grade event processing complete.");
        }
        catch (Exception ex)
        {
            log.LogError(ex, "Error processing grade event.");
        }
    }

    private static async Task PublishEventToEventGrid(GradeEvent eventData, ILogger log)
    {
        try
        {
            var eventGridClient = new EventGridPublisherClient(new Uri(EventGridTopicEndpoint), new AzureKeyCredential(EventGridAccessKey));

            var eventToPublish = new EventGridEvent(
                subject: $"grades/{eventData.Data.StudentId}",
                eventType: "Grade.Notification",
                data: eventData,
                dataVersion: "1.0");

            await eventGridClient.SendEventAsync(eventToPublish);

            log.LogInformation("Published event to Event Grid successfully.");
        }
        catch (Exception ex)
        {
            log.LogError(ex, "Failed to publish event to Event Grid.");
        }
    }
}

public class GradeEvent
{
    public string Id { get; set; }
    public string EventType { get; set; }
    public string Subject { get; set; }
    public GradeData Data { get; set; }
    public string DataVersion { get; set; }
    public string MetadataVersion { get; set; }
    public string EventTime { get; set; }
    public string Topic { get; set; }
}


public class GradeData
{
    public string StudentId { get; set; }
    public string CourseId { get; set; }
    public string Grade { get; set; }
}

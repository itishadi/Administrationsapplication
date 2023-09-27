using System;

using System.Text;
using Azure.Messaging.EventHubs;
using AzureFunctions.Models;
using Microsoft.Azure.Amqp.Framing;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace AzureFunctions
{
    public class SaveToDataCosmosDb
    {

        private readonly ILogger<SaveToDataCosmosDb> _logger;
        private readonly CosmosClient _cosmosClient;
        private readonly Container _container;

        public SaveToDataCosmosDb(ILogger<SaveToDataCosmosDb> logger)
        {
            _logger = logger;
            _cosmosClient = new CosmosClient("AccountEndpoint=https://itis-kyh-cosmosdb.documents.azure.com:443/;AccountKey=AM8KgCuai3V90dKDLoeJk2ZCsnzOybbLIlgYgUzAVHkb4OWo0c1DVLdcrBpe1c0WCuYlsCOAJPatACDbEHhwdg==;");
            var dataBase = _cosmosClient.GetDatabase("kyh");
            _container = (dataBase).GetContainer("messages");
        }

        [FunctionName(nameof(SaveToDataCosmosDb))]
        public async Task Run([EventHubTrigger("iothub-ehub-kyh-myiot-25230155-9c150a13d9", Connection = "IotHubEndPoint")] EventData[] events, ILogger log)
        {
            foreach (EventData @event in events)
            {
                try
                {
                    var json = Encoding.UTF8.GetString(@event.Body.ToArray());
                    var data = JsonConvert.DeserializeObject<DataMessage>(json);
                    await _container.CreateItemAsync(data, new PartitionKey(data.id));


                    _logger.LogInformation($"Sparade meddelande {data}");
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Could not save {ex.Message}");
                }

            }
        }

    }
}
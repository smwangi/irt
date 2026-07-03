using Irt.Core.Datasources.Events;
using Microsoft.Extensions.Logging;
using MassTransit;
using Microsoft.FeatureManagement;

namespace Irt.Application.Datasources.EventHandlers
{
    public class DatasourceCreatedEventHandler
        (IPublishEndpoint publishEndpoint, IFeatureManager featureManager, ILogger<DatasourceCreatedEventHandler> logger)
    {
        public async Task Handle(DatasourceCreatedEvent domainEvent, CancellationToken cancellationToken)
        {
            logger.LogInformation("[Domain Event Handled] Datasource created: {DatasourceId}", domainEvent.GetType().Name);

            if (await featureManager.IsEnabledAsync("DatasourceCreatedEvent"))
            {
                var datasourceCreatedIntegrationEvent = domainEvent.DatasourceId;
                await publishEndpoint.Publish(datasourceCreatedIntegrationEvent, cancellationToken);
            }
        }
    }
}
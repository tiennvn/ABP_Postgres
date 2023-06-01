using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;
using Volo.Abp.EventBus;
using Volo.Abp.EventBus.Distributed;
using Volo.Abp.EventBus.Kafka;
using Volo.Abp.Guids;
using Volo.Abp.Kafka;
using Volo.Abp.MultiTenancy;
using Volo.Abp.Timing;
using Volo.Abp.Uow;

namespace Acme.BookStore.Books;

public class TmtKafkaDistributedEventBus : KafkaDistributedEventBus
{
    public TmtKafkaDistributedEventBus(IServiceScopeFactory serviceScopeFactory, ICurrentTenant currentTenant, IUnitOfWorkManager unitOfWorkManager, IOptions<AbpKafkaEventBusOptions> abpKafkaEventBusOptions, IKafkaMessageConsumerFactory messageConsumerFactory, IOptions<AbpDistributedEventBusOptions> abpDistributedEventBusOptions, IKafkaSerializer serializer, IProducerPool producerPool, IGuidGenerator guidGenerator, IClock clock, IEventHandlerInvoker eventHandlerInvoker) : base(serviceScopeFactory, currentTenant, unitOfWorkManager, abpKafkaEventBusOptions, messageConsumerFactory, abpDistributedEventBusOptions, serializer, producerPool, guidGenerator, clock, eventHandlerInvoker)
    {
    }

    protected async override Task PublishToEventBusAsync(Type eventType, object eventData)
    {
        var eventName = EventNameAttribute.GetNameOrDefault(eventType);
        if (eventName == "MyApp.Product.StockChange")
        {
            AbpKafkaEventBusOptions.TopicName = "MyApp.Product.StockChange";
        }
        else if (eventName == "Author")
        {
            AbpKafkaEventBusOptions.TopicName = "Author";
        }
        else
        {
            AbpKafkaEventBusOptions.TopicName = eventName;
        }
        await base.PublishToEventBusAsync(eventType, eventData);
    }
}



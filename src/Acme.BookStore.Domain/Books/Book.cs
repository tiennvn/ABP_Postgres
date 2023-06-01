using System;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.EventBus;

namespace Acme.BookStore.Books
{
    [EventName("MyApp.Product.StockChange")]
    public class Book : AuditedAggregateRoot<Guid>
    {
        public string Name { get; set; }

        public BookType Type { get; set; }

        public DateTime PublishDate { get; set; }

        public float Price { get; set; }
    }
}

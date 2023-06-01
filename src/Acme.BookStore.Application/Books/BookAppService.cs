using Acme.BookStore.Authors.Acme.BookStore.Authors;
using Acme.BookStore.Settings;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.EventBus.Distributed;
using Volo.Abp.SettingManagement;
using Volo.Abp.Users;

namespace Acme.BookStore.Books
{
    public class BookAppService : CrudAppService<Book, BookDto, Guid, PagedAndSortedResultRequestDto, CreateUpdateBookDto>, IBookAppService
    {
        protected ISettingManager SettingManager { get; }
        private readonly IDistributedEventBus _distributedEventBus;

        public BookAppService(IRepository<Book, Guid> repository, ISettingManager settingManager,
            IDistributedEventBus distributedEventBus)
            : base(repository)
        {
            SettingManager = settingManager;
            _distributedEventBus = distributedEventBus;
        }
        public virtual async Task<object> GetSetting(string providerName, string providerKey)
        {
            var result = new
            {
                TienSetting = await SettingManager.GetOrNullAsync(BookStoreSettings.MySetting1, providerName, providerKey)
            };

            return result;
        }

        [HttpPost]
        public virtual async Task CreateSettingAsync
            (
                string value,
                string providerName,
                string providerKey
            )
        {
            await _distributedEventBus.PublishAsync(
                new Book
                {
                    Name = "Test",
                    Price = 1,

                }
            );

            await _distributedEventBus.PublishAsync(
                    new Author
                    {
                        Name = "Tien"
                    }
                );
            await _distributedEventBus.PublishAsync(
                    new UserEto
                    {
                        UserName = "Tien",
                        Name = "UserEto"
                    }
                );
        }

    }
}


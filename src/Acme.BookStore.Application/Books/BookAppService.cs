using Acme.BookStore.Settings;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.SettingManagement;

namespace Acme.BookStore.Books
{
    public class BookAppService : CrudAppService<Book, BookDto, Guid, PagedAndSortedResultRequestDto, CreateUpdateBookDto>, IBookAppService
    {
        protected ISettingManager SettingManager { get; }

        public BookAppService(IRepository<Book, Guid> repository, ISettingManager settingManager)
            : base(repository)
        {
            SettingManager = settingManager;
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
            await SettingManager.SetAsync(BookStoreSettings.MySetting1, value, providerName, providerKey, true);
        }

    }
}


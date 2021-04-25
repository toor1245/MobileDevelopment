﻿using System.Collections.Generic;
using System.Threading.Tasks;
using MobileDevelopment.Models;

namespace MobileDevelopment.Interfaces
{
    public interface IDataStore<T>
    {
        Task<bool> AddItemAsync(T item);
        Task<bool> UpdateItemAsync(T item);
        Task<bool> DeleteItemAsync(string id);
        Task<T> GetItemAsync(string id);
        Task<BookValuation> GetItemsAsync(bool forceRefresh = false);
    }
}

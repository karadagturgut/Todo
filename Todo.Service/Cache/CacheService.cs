﻿using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Todo.Core;


namespace Todo.Service
{
    public class CacheService
    {
        private readonly IMemoryCache _memoryCache;

        public CacheService(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        public ApiResponseDTO? GetByCacheKey(string cacheKey)
        {
            if (_memoryCache.TryGetValue(cacheKey, out ApiResponseDTO result))
            {
                return result;
            }
            return null;
        }

        public ApiResponseDTO? SetCacheAndGetResponse(string cacheKey, object data, string? message)
        {
            var response = ApiResponseDTO.Success(data, message);
            _memoryCache.Set(cacheKey, response, TimeSpan.FromMinutes(5));
            return response;
        }

        public ApiResponseDTO RemoveCache(string cacheKey)
        {
            if (_memoryCache.TryGetValue(cacheKey,out var cachedObject))
            {
                _memoryCache.Remove(cacheKey);
                return ApiResponseDTO.Success(null,$"{cacheKey} cache silindi.");
            }
            return ApiResponseDTO.Success(null, "key bulunamadı.");
        }
    }
}

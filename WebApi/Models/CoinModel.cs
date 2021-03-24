﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Caching;

namespace WebApi.Models
{
    public class CoinModel
    {
        [Required]
        [StringLength(3)]
        public string Moeda { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime Data_inicio { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime Data_fim { get; set; }

        public bool RemoveItem()
        {
            var cache = MemoryCache.Default;
            var result = (List<CoinModel>)cache.Get("CoinsAdded");

            if (result != null)
            {
                result.Remove(result[0]);
                var policy = new CacheItemPolicy().AbsoluteExpiration = DateTime.Now.AddMinutes(30);
                cache.Set("CoinsAdded", result, policy);
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}
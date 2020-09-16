using System;
using Microsoft.Extensions.Caching.Memory;
using System.Collections.Generic;
using System.Threading;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;

namespace CashingApplication
{
    public class Fibonacci
    {
        private MemoryCache _cache;
        private RedisCache _distributedCache;
        
        public Fibonacci()
        {
            _cache = new MemoryCache(new MemoryCacheOptions());
            _distributedCache = new RedisCache("localhost", 6.ToString());
        }

        public List<int> GetFibonacci(int n)
        {
            var fibonacci = _distributedCache.Get(n.ToString());

            if (fibonacci == null)
            {
                fibonacci = CalculateNumbers(n);
            }

            return fibonacci;
        }

        private List<int> CalculateNumbers(int n)
        {
            List<int> fibonacci = new List<int>();

            int a = 0;
            int b = 1;
            int tmp;

            for (int i = 0; i < n; i++)
            {
                tmp = a;
                a = b;
                b += tmp;
                fibonacci.Add(a);
            }

            _distributedCache.Set(n.ToString(), fibonacci, DateTimeOffset.Now.AddDays(1));

            return fibonacci;
        }
    }
}

using System;
using System.Linq;
using System.Net.Http;
using System.Runtime.Caching;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Kreipiniai
{
    public class NameRetriever
    {
        private readonly MemoryCache _cache;
        private readonly TimeSpan _expirationTimeout;
        private readonly HttpClient _client;

        public NameRetriever(TimeSpan expirationTimeout)
        {
            _cache = MemoryCache.Default;
            _expirationTimeout = expirationTimeout;
            var address = new Uri("http://vardai.vlkk.lt/vardas/");
            _client = new HttpClient { BaseAddress = address };

        }

        public async Task<bool> CheckIfExists(string name)
        {
            var delithuanizedName = Delithuanize(name);
            var lazy = new Lazy<Task<bool>>(() => RetrieveNameDataFromVlkk(delithuanizedName), true);
            _cache.AddOrGetExisting(delithuanizedName, lazy, DateTimeOffset.Now.Add(_expirationTimeout));
            var cacheResult = (Lazy<Task<bool>>) _cache.Get(delithuanizedName);
            return await cacheResult.Value;
        }

        private async Task<bool> RetrieveNameDataFromVlkk(string name)
        {
            var response = await _client.GetAsync(name);
            var contentString = await response.Content.ReadAsStringAsync();
            return !contentString.Contains("Tokio vardo nėra.");
        }

        private static string Delithuanize(string name)
        {
            var nameBeingProcessed = name.ToLower();
            var lithuanianLetters = "ąčęėįšųūž";
            var latinLetters = "aceeisuuz";
            for (var i = 0; i < lithuanianLetters.Length; i++)
            {
                nameBeingProcessed = Regex.Replace(nameBeingProcessed, $@"{lithuanianLetters[i]}", latinLetters[i].ToString());
            }
            return nameBeingProcessed.First().ToString().ToUpper() + nameBeingProcessed.Substring(1);
        }
    }
}

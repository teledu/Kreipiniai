using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Kreipiniai
{
    public class Kreipiniai
    {
        private readonly NameRetriever _nameRetriever;

        public Kreipiniai(TimeSpan? cacheTimeout = null)
        {
            if (cacheTimeout == null)
                cacheTimeout = TimeSpan.FromDays(30);
            _nameRetriever = new NameRetriever(cacheTimeout.Value);
        }

        public async Task<string> GetFor(string name)
        {
            if (name == null)
                return null;
            if (!await _nameRetriever.CheckIfExists(name))
                return name;
            switch (name)
            {
                case var someVal when EndsWith(someVal, "as"):
                    return ReplaceEnding(name, "as", "ai");
                case var someVal when EndsWith(someVal, "is"):
                    return ReplaceEnding(name, "is", "i");
                case var someVal when EndsWith(someVal, "us"):
                    return ReplaceEnding(name, "us", "au");
                case var someVal when EndsWith(someVal, "ė"):
                    return ReplaceEnding(name, "ė", "e");
            }
            return name;
        }

        private bool EndsWith(string name, string ending)
        {
            return new Regex($@"{ending}$").IsMatch(name);
        }

        private string ReplaceEnding(string name, string ending, string newEnding)
        {
            return Regex.Replace(name, $@"{ending}$", newEnding);
        }
    }
}

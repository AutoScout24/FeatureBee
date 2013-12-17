using System;
using System.Collections.Generic;
using System.Net;
using System.Runtime.Caching;
using System.Web.Script.Serialization;

namespace FeatureBee.Configuration
{
    public interface IFeatureRepository
    {
        List<FeatureDto> GetFeatures();
    }

    public class PullFeatureRepository : IFeatureRepository
    {
        private static readonly ObjectCache Cache = new MemoryCache("FeatureBee");
        private readonly Uri _featuresUri;
        private readonly WebClient _webClient;

        public PullFeatureRepository(string url)
        {
            _webClient = new WebClient();

            _featuresUri = new Uri(string.Format("{0}/api/features", url));

            RefreshCache(null);
        }

        public List<FeatureDto> GetFeatures()
        {
            return Cache.Get("FeatureBee.Features") as List<FeatureDto> ?? new List<FeatureDto>();
        }

        private void RefreshCache(CacheEntryRemovedArguments arguments)
        {
            var features = new List<FeatureDto>();
            var expiresOn = DateTime.Now.AddSeconds(60);

            try
            {
                var result = _webClient.DownloadString(_featuresUri);
                features = Deserialize(result);
            }
            catch (Exception)
            {
                // TODO: Introduce logging interface and log exception                
            }

            Cache.Set(new CacheItem("FeatureBee.Features", features),
                      new CacheItemPolicy {AbsoluteExpiration = expiresOn, RemovedCallback = RefreshCache});
        }

        private List<FeatureDto> Deserialize(string input)
        {
            var serializer = new JavaScriptSerializer();
            return serializer.Deserialize<List<FeatureDto>>(input);
        }
    }
}
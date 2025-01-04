using System.Collections.Generic;

namespace SandevLibraryClassicNetFramework.SatusehatBaseResponse
{
    public class SatuSehatBaseResponJson<TObjectClass>
    {
        public string ResourceType { get; set; }
        public int? Total { get; set; }
        public string Type { get; set; }
        public List<LinkAddress> Link { get; set; }
        public TObjectClass Entry { get; set; }
    }
}

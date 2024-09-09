using System;

namespace NetProxyApi
{
    public class RotationResult
    {
        public string Proxy { get; set; }
        public DateTime RefreshAt { get; set; }
        public int NextChange { get; set; }
        public string AcceptIp { get; set; }
        public bool IsResidential { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Isp { get; set; }
        public string CurrentIp { get; set; }
    }
}

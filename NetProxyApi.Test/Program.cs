using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetProxyApi.Test
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var service = new NetProxyService("D93DC517CBCD1FDE5316D85428D1DD39");

            service.GetAllCountriesAsync().GetAwaiter().GetResult();
            service.RotateProxyAsync(ProxyType.Datacenter).GetAwaiter().GetResult();

            var rs = service.GetCurrentIpAsync().GetAwaiter().GetResult();
        }
    }
}

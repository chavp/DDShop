using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;

namespace DDShop.WebAPI.SystemTests
{
    using Spike.IShop.Models;
    using System.Diagnostics;
    using System.IO;
    using System.Net;
    using Xunit;

    /// <summary>
    /// http://dotnetliberty.com/index.php/2015/12/17/asp-net-5-web-api-integration-testing/
    /// https://xunit.github.io/docs/comparisons.html
    /// </summary>
    public class DDShopModuleTest : IClassFixture<DDShopWebAPIFixture>
    {
        string ddshop_webapi_url = "http://localhost:1234";
        string get_memebrs_id_orders_uri = "/ddshop/members/{0}/orders";
        string get_memebrs_id_uri = "/ddshop/members/{0}";

        [Fact]
        public async void Get_MembersOrders_NotNull()
        {
            using (var client = new HttpClient().AcceptJson())
            {
                client.BaseAddress = new Uri(this.ddshop_webapi_url);

                HttpResponseMessage response = await client.GetAsync(string.Format(get_memebrs_id_orders_uri, 1));
                Assert.Equal(HttpStatusCode.OK, response.StatusCode);

                var result = await response.Content.ReadAsJsonAsync<List<OrderModel>>();
                Assert.NotNull(result);
            }
        }

        [Fact]
        public async void Get_Members_NotNull()
        {
            using (var client = new HttpClient().AcceptJson())
            {
                client.BaseAddress = new Uri(this.ddshop_webapi_url);

                HttpResponseMessage response = await client.GetAsync(string.Format(get_memebrs_id_uri, 1));

                Assert.Equal(HttpStatusCode.OK, response.StatusCode);

                var result = await response.Content.ReadAsJsonAsync<MemberModel>();
                Assert.NotNull(result);
            }
        }

    }
}

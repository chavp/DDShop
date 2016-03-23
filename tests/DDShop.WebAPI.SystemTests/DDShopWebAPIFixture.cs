using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDShop.WebAPI.SystemTests
{
    public class DDShopWebAPIFixture : IDisposable
    {
        
        string local_ddshop_webapi_app_path = @"C:\projects\microinsurance\micro-insurance-engine\samples\DDShop.WebAPI\bin\Debug\DDShop.WebAPI.exe";

        Process proc = null;

        public DDShopWebAPIFixture()
        {
            // Prepare the process to run
            var start = new ProcessStartInfo();
            start.FileName = this.local_ddshop_webapi_app_path;

            proc = Process.Start(start);

        }

        public void Dispose()
        {
            proc.CloseMainWindow();
        }
    }
}

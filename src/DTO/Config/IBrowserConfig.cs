using System;
using System.Collections.Generic;
using System.Text;

namespace DotNetCoreXUnit1.src.DTO.Config
{
    interface IBrowserConfig
    {
        string HostName { get; set; }
        string DomainName { get; set; }
        string BrowserName { get; set; }
    }
}

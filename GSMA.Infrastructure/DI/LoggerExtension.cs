using GSMA.Logger;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace GSMA.Infrastructure.DI
{
    public static class LoggerExtension
    {
        public static IConfiguration AddLoggerConfiguration(this IConfiguration configuration)
        {
            LoggerManager.LoadConfigucation("/nlog.config");
            return configuration;
        }
    }
}

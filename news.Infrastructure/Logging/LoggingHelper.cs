using news.Infrastructure.Utilities;
using Serilog;
using System;
using System.Collections.Generic;
using System.Text;

namespace news.Infrastructure.Logging
{
    public static class LoggingHelper
    {
        private static IDiagnosticContext _diagnosticContext;

        public static void Config(IDiagnosticContext diagnosticContext)
        {
            _diagnosticContext = diagnosticContext;
        }

        public static void SetLogStep(string msg)
        {
            Helper.SetCustomLog(msg.ToString());
        }

        public static string GetLogStep()
        {
            return Helper.GetCustomLog();
        }

        public static void SetProperty(string property, object obj, bool destructureObjects = false)
        {
            _diagnosticContext.Set(property, obj, destructureObjects);
        }
    }
}

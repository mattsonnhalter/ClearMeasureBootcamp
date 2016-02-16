using System;
using System.Configuration;
using System.IO;

namespace ClearMeasure.Bootcamp.SmokeTests
{
    public static class SmokeTestPaths
    {
        public static string GetIisExpressExecArguments()
        {
            var path = AppDomain.CurrentDomain.BaseDirectory.Replace("SmokeTests\\bin\\Debug", "UI");
            var port = ConfigurationManager.AppSettings["port"];
            var arguments = $"/path:{path} /port:{port}";
            return arguments;
        }

        public static string GetIisExpressExecPath()
        {
            var path = @ConfigurationManager.AppSettings["iisExpressPath"];
            return path;
        }

        public static string GetDriversPath()
        {
            var path = AppDomain.CurrentDomain.BaseDirectory.Replace("bin\\Debug",
                "Drivers");
            return path;
        }

        public static string GetPhantomJsPath()
        {
            var path = AppDomain.CurrentDomain.BaseDirectory.Replace(@"SmokeTests\bin\Debug", "");
            var phantomJsPath = Path.Combine(path, @"packages\PhantomJS.2.1.1\tools\phantomjs\");
            return phantomJsPath;
        }
    }
}
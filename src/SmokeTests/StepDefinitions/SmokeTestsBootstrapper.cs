using System.Diagnostics;
using TechTalk.SpecFlow;

namespace SmokeTests.StepDefinitions
{
    [Binding]
    public static class SmokeTestsBootstrapper
    {
        private static Process process;

        [BeforeTestRun]
        public static void Startup()
        {
            // kill off existing IIS Express instance if present

            // start IIS Express
//            var startInfo = new ProcessStartInfo("path to iis express", "arguments");
//            process = Process.Start(startInfo);
        }

        [AfterTestRun]
        public static void Cleanup()
        {
            // stop IIS Express
            process?.Kill();
        }
    }
}
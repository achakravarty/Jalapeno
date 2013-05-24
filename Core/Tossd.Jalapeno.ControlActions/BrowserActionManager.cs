using Microsoft.VisualStudio.TestTools.UITesting;

namespace Tossd.Jalapeno.ControlActions
{
    public static class BrowserActionManager
    {
        /// <summary>
        /// Launches a BrowserWindow instance based on the launch Url provided to it
        /// </summary>
        /// <param name="launchUrl">The url to be launched in the BrowserWindow instance</param>
        /// <returns>The newly instantiated BrowserWindow</returns>
        public static BrowserWindow LaunchBrowser(string launchUrl)
        {
            return BrowserWindow.Launch(launchUrl);
        }

        public static void CloseBrowser(this BrowserWindow browserWindow)
        {
            browserWindow.Close();
        }
    }
}
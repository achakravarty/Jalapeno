using System.Collections.Specialized;
using System.Configuration;
using Microsoft.VisualStudio.TestTools.UITesting;
using Tossd.Jalapeno.Controls.Interfaces;
using Tossd.Jalapeno.Data.Interfaces;
using Tossd.Jalapeno.Model;
using Tossd.Jalapeno.ControlActions;
using Tossd.Jalapeno.Reporting;

namespace Tossd.Jalapeno.Core
{
    public class TestManager
    {
        private IControlMapper ControlMapper { get; set; }

        private IUIMapParser UIMapParser { get; set; }

        public TestDataContext TestDataContext { get; private set; }

        private ImageCapturer ImageCapturer { get; set; }

        public BrowserWindow BrowserWindow { get; private set; }

        public ControlMap ControlMap { get; private set; }

        private StringDictionary UIMap { get; set; }

        private int TestNumber { get; set; }

        public string ScreenCaptureImagesPath { get; set; }

        #region Core

        /// <summary>
        /// Why?
        /// </summary>
        public void Init()
        {
            Init(string.Empty);
        }

        public void Init(string launchUrl)
        {
            if (BrowserWindow == null)
            {
                LaunchBrowser(launchUrl);
            }
            PopulateControlMap();
            //ParseResourceMessages();
            TestDataContext = new TestDataContext();
            ImageCapturer = new ImageCapturer(ScreenCaptureImagesPath, TestNumber);
        }

        public void LaunchBrowser(string launchUrl)
        {
            if (string.IsNullOrEmpty(launchUrl))
            {
                launchUrl = ConfigurationManager.AppSettings["ApplicationUrl"];
            }
            BrowserWindow = BrowserActionManager.LaunchBrowser(launchUrl);
        }

        private void PopulateControlMap()
        {
            if (UIMap == null)
            {
                UIMapParser = InterfaceImplementationInstantiator.InstantiateUIMapParser();
                ParseUIMap();
            }
            if (ControlMapper == null)
            {
                ControlMapper = InterfaceImplementationInstantiator.InstantiateControlMapper();
            }
            ControlMap = ControlMapper.MapControls(BrowserWindow, UIMap);
        }

        private void ParseUIMap()
        {
            UIMap = UIMapParser.ParseUIMap(ConfigurationManager.AppSettings["UIMapPath"]);
        }

        public void CaptureScreen(string fileName)
        {
            ImageCapturer.CaptureScreen(fileName);
        }

        public void Dispose()
        {
            BrowserWindow.CloseBrowser();
            BrowserWindow = null;
        }

        #endregion

        //#region ResourceMessages

        //public class ResourceMessages
        //{
        //    internal static Dictionary<string, string> Messages { get; set; }

        //    public static string Get(string key)
        //    {
        //        return Messages[key];
        //    }
        //}

        //#endregion

        #region Helpers

        //private static void ParseResourceMessages()
        //{
        //    if (ResourceMessages.Messages == null)
        //    {
        //        var messageParser = ObjectFactory.Build<IMessageParser>();
        //        ResourceMessages.Messages = messageParser.ParseDataFromSource(ConfigurationManager.AppSettings["MessagesPath"]);
        //        ResourceMessages.Messages = ResourceMessages.Messages ?? new Dictionary<string, string>(StringComparer.InvariantCultureIgnoreCase);
        //    }
        //}

        #endregion
    }
}

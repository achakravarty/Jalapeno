using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Data;
using Microsoft.VisualStudio.TestTools.UITesting;
using Tossd.Jalapeno.Controls.Interfaces;
using Tossd.Jalapeno.Data.Interfaces;
using Tossd.Jalapeno.Common;
using Tossd.Jalapeno.Model;
using Tossd.Jalapeno.ControlActions;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Xml;
using System.Configuration;

namespace Tossd.Jalapeno.Core
{
    public class TestManager
    {
        private IControlMapper ControlMapper { get; set; }

        public static BrowserWindow BrowserWindow { get; set; }

        public static ControlMap ControlMap { get; set; }

        private static StringDictionary UIMap { get; set; }

        private static int TestNumber { get; set; }

        public static string ScreenCaptureImagesPath { get; set; }

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
            TestData.Init();
            GenerateDirectoryPathForCapturedScreenImages();
        }

        public void LaunchBrowser(string launchUrl)
        {
            if (string.IsNullOrEmpty(launchUrl))
            {
                launchUrl = ConfigurationManager.AppSettings["ApplicationUrl"];
            }
            BrowserWindow.LaunchBrowser(launchUrl);
        }

        private static void GenerateDirectoryPathForCapturedScreenImages()
        {
            ScreenCaptureImagesPath = ScreenCaptureImagesPath + "_Test " + TestNumber;
            TestNumber++;
            Directory.CreateDirectory(ScreenCaptureImagesPath);
        }


        private void PopulateControlMap()
        {
            if (UIMap == null)
                ParseUIMap();
            if (ControlMapper == null)
                ControlMapper = ObjectFactory.Build<IControlMapper>();
            ControlMap = ControlMapper.MapControls(BrowserWindow, UIMap);
        }

        private static void ParseUIMap()
        {
            var uiMapParser = ObjectFactory.Build<IUIMapParser>();
            UIMap = uiMapParser.ParseUIMap(ConfigurationManager.AppSettings["UIMapPath"]);
        }

        public static void Dispose()
        {
            BrowserWindow.Close();
            BrowserWindow = null;
        }

        #endregion

        #region TestDataContext

        public class TestData
        {
            internal static Dictionary<string, object> DataContext { get; set; }

            public static void Add(string key, object data)
            {
                DataContext[key] = data;
            }

            public static bool Delete(string key)
            {
                return DataContext.Remove(key);
            }

            public static T Get<T>(string key)
            {
                var data = DataContext[key];
                return data == null ? default(T) : (T)data;
            }

            public static void Init()
            {
                DataContext = new Dictionary<string, object>(StringComparer.InvariantCultureIgnoreCase);
            }
        }

        #endregion

        #region ResourceMessages

        public class ResourceMessages
        {
            internal static Dictionary<string, string> Messages { get; set; }

            public static string Get(string key)
            {
                return Messages[key];
            }
        }

        #endregion

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

        public static void CaptureScreen(string fileName)
        {
            string destinationImagePath = ScreenCaptureImagesPath + "\\" + fileName;
            var screenWidth = Screen.GetBounds(new Point(0, 0)).Width;
            var screenHeight = Screen.GetBounds(new Point(0, 0)).Height;
            Bitmap screenBitmap = new Bitmap(screenWidth, screenHeight);
            Graphics screenBitmapGFX = Graphics.FromImage(screenBitmap);
            screenBitmapGFX.CopyFromScreen(new Point(0, 0), new Point(0, 0), new Size(screenWidth, screenHeight));
            screenBitmap.Save(destinationImagePath, ImageFormat.Png);
        }
        #endregion
    }
}

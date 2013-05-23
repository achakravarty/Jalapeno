using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using Tossd.Jalapeno.Model;
using Microsoft.VisualStudio.TestTools.UITesting;

namespace Tossd.Jalapeno.Controls.Interfaces
{
    public interface IControlMapper
    {
        /// <summary>
        /// This method will create a control map from the locators specified in the control map dictionary with respect to the current browser window
        /// </summary>
        /// <param name="currentBrowserWindow">The current browser window within which the controls need to be located</param>
        /// <param name="controlMapDictionary">A dictionary of the control locator names to the control locators</param>
        /// <returns>A control map of the locator names and the actual controls</returns>
        ControlMap MapControls(BrowserWindow currentBrowserWindow, StringDictionary controlMapDictionary);
    }
}

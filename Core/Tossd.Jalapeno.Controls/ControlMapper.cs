using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UITesting;
using Tossd.Jalapeno.Controls.Interfaces;
using Tossd.Jalapeno.Model;

namespace Tossd.Jalapeno.Controls
{
    public class ControlMapper : IControlMapper
    {
        /// <summary>
        /// The current browser window
        /// </summary>
        public BrowserWindow CurrentBrowserWindow
        {
            get;
            protected set;
        }

        /// <summary>
        /// This method will create a control map from the locators specified in the control map dictionary with respect to the current browser window
        /// </summary>
        /// <param name="currentBrowserWindow">The current browser window within which the controls need to be located</param>
        /// <param name="controlMapDictionary">A dictionary of the control locator names to the control locators</param>
        /// <returns>A control map of the locator names and the actual controls</returns>
        /// <exception cref="ArgumentNullException">Thrown when the control map dictionary is null</exception>
        /// <exception cref="ArgumentNullException">Thrown when the current browser window is null</exception>
        public ControlMap MapControls(BrowserWindow currentBrowserWindow, StringDictionary controlMapDictionary)
        {
            if (controlMapDictionary == null)
            {
                throw new ArgumentNullException("controlMapDictionary");
            }
            if (currentBrowserWindow == null)
            {
                throw new ArgumentNullException("currentBrowserWindow", "Current Browser window cannot be null");
            }

            //Update the current browser window
            CurrentBrowserWindow = currentBrowserWindow;

            //Instantiate the a new object of the control configurator with the current browser window
            ControlConfigurator.BrowserWindow = currentBrowserWindow;

            return PopulateControlMap(controlMapDictionary);
        }

        /// <summary>
        /// Method to populate the control map
        /// </summary>
        /// <param name="controlMapDictionary">A dictionary of the control locator names to the control locators</param>
        /// <returns>A control map of the locator names and the actual controls</returns>
        protected virtual ControlMap PopulateControlMap(StringDictionary controlMapDictionary)
        {
            //Instantiate a new control map with a new dictionary
            var controlMap = new Dictionary<string, Control>(StringComparer.InvariantCultureIgnoreCase);

            //Populate the control map
            foreach (string key in controlMapDictionary.Keys)
            {
                controlMap.Add(key, ControlConfigurator.BuildControl(controlMapDictionary[key], new Control { UITestControl = CurrentBrowserWindow }));
            }

            //Return a control map object
            return new ControlMap(controlMap);
        }
    }
}
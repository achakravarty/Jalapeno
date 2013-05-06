using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;

namespace Tossd.Jalapeno.Model
{
    public class ControlMap
    {
        /// <summary>
        /// Parametrized constructor to initialize the Control Map
        /// </summary>
        /// <param name="controlDictionary">The control dictionary that will be the internal mapping of the control map</param>
        public ControlMap(Dictionary<string, Control> controlDictionary)
        {
            _controlDictionary = controlDictionary;
        }

        /// <summary>
        /// A Mapping of the UI controls by their locators
        /// </summary>
        private readonly Dictionary<string, Control> _controlDictionary;

        /// <summary>
        /// An indexer for the Control Map
        /// </summary>
        /// <param name="locatorName">The name of the locator used to locate the control</param>
        ///<exception cref="ArgumentNullException">Thrown when the locator name is null or empty</exception>
        /// <exception cref="KeyNotFoundException">Thrown when the given locator is not present in the dictionary</exception>
        public Control this[string locatorName]
        {
            get
            {
                if (string.IsNullOrWhiteSpace(locatorName))
                {
                    throw new ArgumentNullException("locatorName");    
                }
                if (!_controlDictionary.ContainsKey(locatorName))
                {
                    throw new KeyNotFoundException("No Control is present for the given locator name : " + locatorName);
                }
                return _controlDictionary[locatorName.ToLowerInvariant()];
                
            }
            set
            {
                if (string.IsNullOrWhiteSpace(locatorName))
                {
                    throw new ArgumentNullException("locatorName");
                }
                if (value == null)
                {
                    throw new ArgumentNullException("value", "Cannot assign null Control to Control Map");
                }
                _controlDictionary[locatorName.ToLowerInvariant()] = value;
            }
        }
    }
}
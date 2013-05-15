using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.VisualStudio.TestTools.UITesting;
using Microsoft.VisualStudio.TestTools.UITesting.HtmlControls;
using Tossd.Jalapeno.Model;

namespace Tossd.Jalapeno.Controls
{
    public static class ControlConfigurator
    {
        public static BrowserWindow BrowserWindow { get; set; }

        /// <summary>
        /// Parses the locator info to translate it into a control object
        /// </summary>
        /// <param name="locator">The actual locator used to locate the control</param>
        /// <param name="parentContext">The parent control of the control to be created</param>
        /// <returns>The parsed control with respec to the locator info that was passed</returns>
        public static Control BuildControl(string locator, Control parentContext)
        {
            var controlType = GetControlType(locator.Substring(0, locator.IndexOf("//")));
            var propertyString = GetProperties(locator.Substring(locator.IndexOf("//") + 2));
            var parentControl = GetParentControl(propertyString, parentContext);
            var childHtmlControl = (HtmlControl)Activator.CreateInstance(controlType, parentControl.UITestControl);

            var control = SetProperties(childHtmlControl, propertyString[propertyString.Count - 1]);
            control.ParentControl = parentControl;
            control.PropertyInfo = control.PropertyInfo ?? new ControlPropertyInfo();
            control.PropertyInfo.LocatorName = locator;
            return control;
        }

        /// <summary>
        /// Gets the type of control depending on the locator information
        /// </summary>
        /// <param name="controlType"></param>
        /// <returns>The type of the control that matches the control type specified in the locator</returns>
        private static Type GetControlType(string controlType)
        {
            controlType = controlType.Replace("#", "");

            switch (controlType)
            {
                case "HtmlAreaHyperlink":
                    return typeof(HtmlAreaHyperlink);
                case "HtmlButton":
                    return typeof(HtmlButton);
                case "HtmlCell":
                    return typeof(HtmlCell);
                case "HtmlCheckBox":
                    return typeof(HtmlCheckBox);
                case "HtmlComboBox":
                    return typeof(HtmlComboBox);
                case "HtmlControl":
                    return typeof(HtmlControl);
                case "HtmlCustom":
                    return typeof(HtmlCustom);
                case "HtmlDiv":
                    return typeof(HtmlDiv);
                case "HtmlDocument":
                    return typeof(HtmlDocument);
                case "HtmlEdit":
                    return typeof(HtmlEdit);
                case "HtmlEditableDiv":
                    return typeof(HtmlEditableDiv);
                case "HtmlEditableSpan":
                    return typeof(HtmlEditableSpan);
                case "HtmlFileInput":
                    return typeof(HtmlFileInput);
                case "HtmlFrame":
                    return typeof(HtmlFrame);
                case "HtmlHeaderCell":
                    return typeof(HtmlHeaderCell);
                case "HtmlHyperlink":
                    return typeof(HtmlHyperlink);
                case "HtmlIFrame":
                    return typeof(HtmlIFrame);
                case "HtmlImage":
                    return typeof(HtmlImage);
                case "HtmlInputButton":
                    return typeof(HtmlInputButton);
                case "HtmlLabel":
                    return typeof(HtmlLabel);
                case "HtmlList":
                    return typeof(HtmlList);
                case "HtmlListItem":
                    return typeof(HtmlListItem);
                case "HtmlRadioButton":
                    return typeof(HtmlRadioButton);
                case "HtmlRow":
                    return typeof(HtmlRow);
                case "HtmlScrollBar":
                    return typeof(HtmlScrollBar);
                case "HtmlSpan":
                    return typeof(HtmlSpan);
                case "HtmlTable":
                    return typeof(HtmlTable);
                case "HtmlTextArea":
                    return typeof(HtmlTextArea);
            }
            return typeof(HtmlCustom);
        }

        /// <summary>
        /// Gets the properties of a particular control from the specified locator
        /// </summary>
        /// <param name="propertyString">The properties of the locator string parsed from the control dictionary</param>
        /// <returns>A list of the properties for the control</returns>
        private static List<string> GetProperties(string propertyString)
        {
            return Regex.Split(propertyString, "//").ToList();
        }

        /// <summary>
        /// Gets the parent control for the for the given tags
        /// </summary>
        /// <param name="tags">A list of tags/properties that are used to locate a control</param>
        /// <param name="parentContext">A parent control under which the control needs to be located</param>
        /// <returns>The parent control parsed from the tags</returns>
        private static Control GetParentControl(List<string> tags, Control parentContext)
        {
            if (tags.Count < 2)
            {
                return parentContext ?? new Control { UITestControl = BrowserWindow };
            }

            var parentControl = SetProperties(parentContext == null ? new HtmlControl(BrowserWindow) : new HtmlControl(parentContext.HtmlControl), tags[0]);
            parentControl.ParentControl = parentContext;

            for (var i = 1; i < tags.Count - 1; i++)
            {
                var childControl = SetProperties(new HtmlControl(parentControl.UITestControl), tags[i]);

                childControl.ParentControl = parentControl;
                parentControl = childControl;
            }
            return parentControl;
        }

        /// <summary>
        /// Sets the search properties that are used to locate the control during runtime
        /// </summary>
        /// <param name="htmlControl">The control for which the search properties need to be set</param>
        /// <param name="propertyString">The properties which will help locate the conrol during runtime</param>
        /// <returns>A parsed control with populated search properties</returns>
        private static Control SetProperties(HtmlControl htmlControl, string propertyString)
        {
            var tempHtmlControl = htmlControl;

            var control = new Control();

            if (propertyString.Contains("["))
            {
                var tagName = propertyString.Substring(0, propertyString.IndexOf('['));
                if (!tagName.Equals("*"))
                {
                    tempHtmlControl.SearchProperties.Add("TagName", tagName, PropertyExpressionOperator.EqualTo);
                }

                var properties = propertyString.Substring(propertyString.IndexOf('[') + 1, propertyString.Length - propertyString.IndexOf('[') - 2).Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                foreach (var property in properties)
                {
                    if (property.Contains(">"))
                    {
                        var values = property.Split('>');
                        var propertyName = values[0];
                        var propertyValue = values[1];
                        tempHtmlControl.SearchProperties.Add(propertyName, propertyValue, PropertyExpressionOperator.Contains);
                    }
                    else if (property.Contains("="))
                    {
                        var values = property.Split('=');
                        var propertyName = values[0];
                        var propertyValue = values[1];
                        tempHtmlControl.SearchProperties.Add(propertyName, propertyValue, PropertyExpressionOperator.EqualTo);
                    }
                    else if (string.Equals(property, "HandleHidden", StringComparison.InvariantCultureIgnoreCase))
                    {
                        control.PropertyInfo = control.PropertyInfo ?? new ControlPropertyInfo();
                        control.PropertyInfo.HandleHidden = true;
                    }
                    else if (string.Equals(property, "WaitForReady", StringComparison.InvariantCultureIgnoreCase))
                    {
                        control.PropertyInfo = control.PropertyInfo ?? new ControlPropertyInfo();
                        control.PropertyInfo.WaitForReady = true;
                    }
                    else // In case of Index
                    {
                        control.PropertyInfo = control.PropertyInfo ?? new ControlPropertyInfo();
                        control.PropertyInfo.IsIndexed = true;
                        control.PropertyInfo.Index = Convert.ToInt32(property);
                    }
                }
            }
            else
            {
                tempHtmlControl.SearchProperties.Add("TagName", propertyString, PropertyExpressionOperator.EqualTo);
            }
            
            control.UITestControl = tempHtmlControl;
            return control;
        }
    }
}
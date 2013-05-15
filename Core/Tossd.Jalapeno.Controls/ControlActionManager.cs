using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UITesting;
using Microsoft.VisualStudio.TestTools.UITesting.HtmlControls;
using Tossd.Jalapeno.Exceptions;
using Tossd.Jalapeno.Model;

namespace Tossd.Jalapeno.Controls
{
    public static partial class ActionManager
    {
        private const int DefaultIndex = 1;

        #region Control Actions

        /// <summary>
        /// Performs Mouse.Click on the specified HTML control
        /// </summary>
        /// <param name="control">Control to click</param>
        public static void Click(this Control control)
        {
            try
            {
                control = ResolveControlByIndexAndVisibility(control);
                Mouse.Click(control.UITestControl);
            }
            catch (Exception ex)
            {
                throw new ControlActionException(ex, "Click", control.PropertyInfo.LocatorName);
            }
        }

        /// <summary>
        /// Hovers over a specified HTML control
        /// </summary>
        /// <param name="control">Control to hover over.</param>
        public static void Hover(this Control control)
        {
            try
            {
                control = ResolveControlByIndexAndVisibility(control);
                Mouse.Hover(control.HtmlControl);
            }
            catch (Exception ex)
            {
                throw new ControlActionException(ex, "Hover", control.PropertyInfo.LocatorName);
            }
        }

        /// <summary>
        /// Inputs the given data in the specified HTMLEdit control
        /// </summary>
        /// <param name="control">Control to type data</param>
        /// <param name="data">Data to type</param>
        public static void Type(this Control control, string data)
        {
            try
            {
                control = ResolveControlByIndexAndVisibility(control);
                var htmlEdit = (HtmlEdit)control.UITestControl;
                htmlEdit.Text = data;
            }
            catch (Exception ex)
            {
                throw new ControlActionException(ex, "Type", control.PropertyInfo.LocatorName);
            }
        }

        /// <summary>
        /// Sets focus on the specified control and sends the given data
        /// </summary>
        /// <param name="control">Control to send data</param>
        /// <param name="data">Data to send</param>
        public static void SendKeys(this Control control, string data)
        {
            try
            {
                control = ResolveControlByIndexAndVisibility(control);
                control.UITestControl.SetFocus();
                Keyboard.SendKeys(data);
            }
            catch (Exception ex)
            {
                throw new ControlActionException(ex, "SendKeys", control.PropertyInfo.LocatorName);
            }
        }

        /// <summary>
        /// Selects the given data in the specified HTMLCheckBox control
        /// </summary>
        /// <param name="control">Control to select</param>
        /// <param name="data">Data to select</param>
        public static void SelectCheckBox(this Control control, bool data)
        {
            try
            {
                control = ResolveControlByIndexAndVisibility(control);
                var checkBox = (HtmlCheckBox)control.UITestControl;
                if (checkBox.Enabled)
                {
                    checkBox.Checked = data;
                }
            }
            catch (Exception ex)
            {
                throw new ControlActionException(ex, "SelectCheckBox", control.PropertyInfo.LocatorName);
            }
        }

        /// <summary>
        /// Selects the given data in the specified HTMLRadioButton control
        /// </summary>
        /// <param name="control">Control to select</param>
        public static void SelectRadioButton(this Control control)
        {
            try
            {
                control = ResolveControlByIndexAndVisibility(control);
                var radioButton = (HtmlRadioButton)control.UITestControl;
                radioButton.Selected = true;
            }
            catch (Exception ex)
            {
                throw new ControlActionException(ex, "SelectRadioButton", control.PropertyInfo.LocatorName);
            }
        }

        /// <summary>
        /// Selects the given data in the specified HTMLComboBox control
        /// </summary>
        /// <param name="control">Control to select</param>
        /// <param name="data">Data to select</param>
        public static void SelectComboBoxByText(this Control control, String data)
        {
            try
            {
                control = ResolveControlByIndexAndVisibility(control);
                var comboBox = (HtmlComboBox)control.UITestControl;
                if (comboBox.SelectedItem != data)
                    return;
                comboBox.SelectedItem = data;
            }
            catch (Exception ex)
            {
                throw new ControlActionException(ex, "SelectComboBoxByText", control.PropertyInfo.LocatorName);
            }
        }

        /// <summary>
        /// Selects the given data in the specified HTMLList control
        /// </summary>
        /// <param name="control">Control to select</param>
        /// <param name="data">Comma seperated items to select</param>
        public static void SelectList(this Control control, String data)
        {
            try
            {
                control = ResolveControlByIndexAndVisibility(control);
                var htmlList = (HtmlList)control.UITestControl;
                htmlList.SelectedItemsAsString = data;
            }
            catch (Exception ex)
            {
                throw new ControlActionException(ex, "SelectList", control.PropertyInfo.LocatorName);
            }
        }

        /// <summary>
        /// Returns the inner text of the specified control
        /// </summary>
        /// <param name="control">Control to use</param>
        /// <returns></returns>
        public static string GetInnerText(this Control control)
        {
            control = ResolveControlByIndexAndVisibility(control);
            return control.HtmlControl.InnerText;
        }

        /// <summary>
        /// Resturns the associated label of the specified HTMLCheckBox control
        /// </summary>
        /// <param name="control">Control to use</param>
        /// <returns></returns>
        public static string GetLabelForCheckBox(this Control control)
        {
            control = ResolveControlByIndexAndVisibility(control);
            var htmlControl = (HtmlCheckBox)control.UITestControl;
            return htmlControl.LabeledBy;
        }

        /// <summary>
        /// Returns the control existence by checking both control presence in page and its visibility
        /// </summary>
        /// <param name="control">Control to check</param>
        /// <returns></returns>
        public static bool IsExist(this Control control)
        {
            control = ResolveControlByIndexAndVisibility(control);
            return control != null;
        }

        public static bool WaitForControlNotExist(this Control control, long? waitTime)
        {
            Playback.Wait(1000);
            waitTime = (waitTime == 0 || waitTime == null) ? Playback.PlaybackSettings.WaitForReadyTimeout : waitTime;

            bool isVisible = true;
            Stopwatch sw = Stopwatch.StartNew();
            sw.Start();
            while (sw.ElapsedMilliseconds < waitTime)
            {
                control = control.Reset();
                if (IsExist(control) == false)
                {
                    isVisible = false;
                    break;
                }
            }
            sw.Stop();
            return isVisible == false;
        }

        public static bool WaitForControlExist(this Control control, long? waitTime)
        {
            Playback.Wait(1000);
            waitTime = (waitTime == 0 || waitTime == null) ? Playback.PlaybackSettings.WaitForReadyTimeout : waitTime;

            bool isVisible = false;
            Stopwatch sw = Stopwatch.StartNew();
            sw.Start();
            while (sw.ElapsedMilliseconds < waitTime)
            {
                control = control.Reset();
                if (IsExist(control))
                {
                    isVisible = true;
                    break;
                }
            }
            sw.Stop();
            return isVisible;
        }

        #endregion

        #region Control Resolve Helpers

        /// <summary>
        /// Returns the count of the matching visible controls
        /// </summary>
        /// <param name="control">Control to match</param>
        /// <returns></returns>
        public static int GetVisibleMatchingControlsCount(this Control control)
        {
            List<Control> visibleControls = new List<Control>();
            var matchingControls = control.UITestControl.FindMatchingControls();

            var res = from ctrl in matchingControls
                      where ctrl.BoundingRectangle.Width > 0
                      select ctrl;

            return res.ToList().Count;
        }

        /// <summary>
        /// Returns the first visible matching control
        /// </summary>
        /// <param name="control">Control to match</param>
        /// <returns></returns>
        public static Control GetMatchingFirstVisibleControl(this Control control)
        {
            return GetMatchingVisibleIndexedControl(control, DefaultIndex);
        }

        /// <summary>
        /// Returns the control at the specified index among all the matching visible controls
        /// </summary>
        /// <param name="control">Control to match</param>
        /// <param name="index">Index of the matching visible controls</param>
        /// <returns></returns>
        public static Control GetMatchingVisibleIndexedControl(this Control control, int index)
        {
            try
            {
                var matchingControls = control.UITestControl.FindMatchingControls();
                index = index < matchingControls.Count ? index : matchingControls.Count;
                var res = from ctrl in matchingControls
                          where ctrl.BoundingRectangle.Width > 0
                          select ctrl;

                var resultList = res.ToList();
                control.UITestControl = (HtmlControl)resultList.ElementAt(index - 1);
                control.PropertyInfo.IsResolved = true;
                return control;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// Returns the control at the specified index among all the matching controls(Dont check control visibility)
        /// </summary>
        /// <param name="control">Control to match</param>
        /// <param name="index">Index of the matching controls</param>
        /// <returns></returns>
        public static Control GetMatchingIndexedControl(this Control control, int index)
        {
            try
            {
                var matchingControls = control.UITestControl.FindMatchingControls();
                index = index < matchingControls.Count ? index : matchingControls.Count;

                control.UITestControl = (HtmlControl)matchingControls.ElementAt(index - 1);
                control.PropertyInfo.IsResolved = true;
                return control;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// Replaces the search placeholder '#KW' specified in UIMap with the given data
        /// </summary>
        /// <param name="control">Control to add search data</param>
        /// <param name="value">Text to replace with</param>
        /// <returns></returns>
        public static Control AddSearchData(this Control control, string value)
        {
            return AddSearchData(control, "#KW", value);
        }

        /// <summary>
        /// Replaces the given search placeholder with the given data
        /// </summary>
        /// <param name="control">Control to add search data</param>
        /// <param name="replaceTo">Text to replace</param>
        /// <param name="replaceWith">Text to replace with</param>
        /// <returns></returns>
        public static Control AddSearchData(this Control control, string replaceTo, string replaceWith)
        {
            if (!string.IsNullOrEmpty(replaceWith))
            {
                foreach (var property in control.UITestControl.SearchProperties.Where(property => property.PropertyValue.Contains(replaceTo)))
                {
                    property.PropertyValue = property.PropertyValue.Replace(replaceTo, replaceWith);
                }
            }
            return control;
        }

        public static Control ResolveControlByIndexAndVisibility(this Control control)
        {
            if (control.PropertyInfo == null || control.PropertyInfo.IsResolved)
                return control;

            if (control.PropertyInfo.HandleHidden)
            {
                if (control.PropertyInfo.IsIndexed)
                {
                    return control.GetMatchingVisibleIndexedControl(control.PropertyInfo.Index);
                }
                else
                {
                    return control.GetMatchingFirstVisibleControl();
                }
            }
            else if (control.PropertyInfo.IsIndexed)
            {
                return control.GetMatchingIndexedControl(control.PropertyInfo.Index);
            }
            else if (control.PropertyInfo.WaitForReady)
            {
                return control.GetMatchingFirstVisibleControl();
            }

            return control;
        }

        public static Control ResolveControlByIndex(this Control control)
        {
            if (control.PropertyInfo == null || control.PropertyInfo.IsResolved)
                return control;

            if (control.PropertyInfo.IsIndexed)
            {
                return control.GetMatchingIndexedControl(control.PropertyInfo.Index);
            }
            else if (control.PropertyInfo.WaitForReady)
            {
                return control.GetMatchingFirstVisibleControl();
            }

            return control;
        }

        public static Control Reset(this Control control)
        {
            return ControlConfigurator.BuildControl(control.PropertyInfo.LocatorName, null);
        }

        public static Control SetParentContext(this Control childControl, Control resolvedParentControl)
        {
            return ControlConfigurator.BuildControl(childControl.PropertyInfo.LocatorName, resolvedParentControl);
        }

        #endregion
    }
}
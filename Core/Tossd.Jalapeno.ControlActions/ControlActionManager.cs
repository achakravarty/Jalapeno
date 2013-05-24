using System;
using System.Text;
using Microsoft.VisualStudio.TestTools.UITesting;
using Microsoft.VisualStudio.TestTools.UITesting.HtmlControls;
using Tossd.Jalapeno.Exceptions;
using Tossd.Jalapeno.Model;
using Tossd.Jalapeno.Controls;

namespace Tossd.Jalapeno.ControlActions
{
    public static class ControlActionManager
    {
        /// <summary>
        /// Performs Mouse.Click on the specified HTML control
        /// </summary>
        /// <param name="control">Control to click</param>
        public static void Click(this Control control)
        {
            try
            {
                control = control.ResolveControlByIndexAndVisibility();
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
                control = control.ResolveControlByIndexAndVisibility();
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
                control = control.ResolveControlByIndexAndVisibility();
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
                control = control.ResolveControlByIndexAndVisibility();
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
                control = control.ResolveControlByIndexAndVisibility();
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
                control = control.ResolveControlByIndexAndVisibility();
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
                control = control.ResolveControlByIndexAndVisibility();
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
                control = control.ResolveControlByIndexAndVisibility();
                var htmlList = (HtmlList)control.UITestControl;
                htmlList.SelectedItemsAsString = data;
            }
            catch (Exception ex)
            {
                throw new ControlActionException(ex, "SelectList", control.PropertyInfo.LocatorName);
            }
        }

        /// <summary>
        /// Resets the given control. Populates the searchproperties again and builds the control from scratch
        /// </summary>
        /// <param name="control">Control to reset</param>
        /// <returns>The control after the reset action has been performed</returns>
        public static Control Reset(this Control control)
        {
            return ControlConfigurator.BuildControl(control.PropertyInfo.LocatorName, null);
        }
    }
}
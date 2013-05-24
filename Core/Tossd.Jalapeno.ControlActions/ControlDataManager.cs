using Microsoft.VisualStudio.TestTools.UITesting.HtmlControls;
using Tossd.Jalapeno.Model;

namespace Tossd.Jalapeno.ControlActions
{
    public static class ControlDataManager
    {
        /// <summary>
        /// Returns the inner text of the specified control
        /// </summary>
        /// <param name="control">Control to use</param>
        /// <returns></returns>
        public static string GetInnerText(this Control control)
        {
            control = control.ResolveControlByIndexAndVisibility();
            return control.HtmlControl.InnerText;
        }

        /// <summary>
        /// Resturns the associated label of the specified HTMLCheckBox control
        /// </summary>
        /// <param name="control">Control to use</param>
        /// <returns></returns>
        public static string GetLabelForCheckBox(this Control control)
        {
            control = control.ResolveControlByIndexAndVisibility();
            var htmlControl = (HtmlCheckBox)control.UITestControl;
            return htmlControl.LabeledBy;
        }
    }
}
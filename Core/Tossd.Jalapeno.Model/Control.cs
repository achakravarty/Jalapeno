using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UITesting;
using Microsoft.VisualStudio.TestTools.UITesting.HtmlControls;

namespace Tossd.Jalapeno.Model
{
    public class Control
    {
        /// <summary>
        /// The base UITestControl for the control
        /// </summary>
        public UITestControl UITestControl { get; set; }

        /// <summary>
        /// The base HtmlControl for the control
        /// </summary>
        /// <returns>The base Html Control for the control. Return null if control is not a HtmlControl</returns>
        public HtmlControl HtmlControl
        {
            get
            {
                if (UITestControl != null && UITestControl is HtmlControl)
                {
                    return UITestControl as HtmlControl;
                }
                return null;
            }
        }

        /// <summary>
        /// The properties associated with the control
        /// </summary>
        public PropertyInfo PropertyInfo { get; set; }

        /// <summary>
        /// The parent control for the specified control
        /// </summary>
        public Control ParentControl { get; set; }
    }
}
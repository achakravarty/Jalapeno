using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UITesting.HtmlControls;
using Tossd.Jalapeno.Model;

namespace Tossd.Jalapeno.ControlActions
{
    public static class ControlResolutionManager
    {
        private const int DefaultIndex = 1;

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
                return control.GetMatchingFirstVisibleControl();
            }
            if (control.PropertyInfo.IsIndexed)
            {
                return control.GetMatchingIndexedControl(control.PropertyInfo.Index);
            }
            if (control.PropertyInfo.WaitForReady)
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
            if (control.PropertyInfo.WaitForReady)
            {
                return control.GetMatchingFirstVisibleControl();
            }

            return control;
        }
    }
}
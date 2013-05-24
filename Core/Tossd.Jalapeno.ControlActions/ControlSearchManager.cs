using System.Linq;
using Tossd.Jalapeno.Controls;
using Tossd.Jalapeno.Model;

namespace Tossd.Jalapeno.ControlActions
{
    public static class ControlSearchManager
    {
        /// <summary>
        /// Replaces the given search placeholder with the given data
        /// </summary>
        /// <param name="control">Control to add search data</param>
        /// <param name="replaceTo">Text to replace</param>
        /// <param name="replaceWith">Text to replace with</param>
        /// <returns></returns>
        public static Control ReplaceSearchData(this Control control, string replaceTo, string replaceWith)
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

        /// <summary>
        /// Sets the immediate parent context of the control
        /// </summary>
        /// <param name="childControl">The child control for which the parent control has to be set</param>
        /// <param name="resolvedParentControl">A fully resolved control that is the parent control of the child control</param>
        /// <returns>The fully resolved child control with the parent control set</returns>
        public static Control SetParentContext(this Control childControl, Control resolvedParentControl)
        {
            return ControlConfigurator.BuildControl(childControl.PropertyInfo.LocatorName, resolvedParentControl);
        }
    }
}
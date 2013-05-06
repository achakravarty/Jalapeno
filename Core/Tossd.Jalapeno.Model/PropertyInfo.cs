namespace Tossd.Jalapeno.Model
{
    public class PropertyInfo
    {
        /// <summary>
        /// The locator name used to identify the control
        /// </summary>
        public string LocatorName { get; set; }

        /// <summary>
        /// Property to see if the given control is identified by index
        /// </summary>
        public bool IsIndexed { get; set; }

        /// <summary>
        /// If the Control is identified by index then this is the index of the control
        /// </summary>
        public int Index { get; set; }

        /// <summary>
        /// Property to check if the control is hidden or not.
        /// </summary>
        public bool HandleHidden { get; set; }

        /// <summary>
        /// Property to check if needed to wait for the control to load
        /// </summary>
        public bool WaitForReady { get; set; }
        
        /// <summary>
        /// Property to check if the control is resolved
        /// </summary>
        public bool IsResolved { get; set; }
    }
}
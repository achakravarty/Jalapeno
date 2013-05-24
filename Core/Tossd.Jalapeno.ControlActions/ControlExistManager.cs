using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UITesting;
using Tossd.Jalapeno.Model;

namespace Tossd.Jalapeno.ControlActions
{
    public static class ControlExistManager
    {
        /// <summary>
        /// Returns the control existence by checking both control presence in page and its visibility
        /// </summary>
        /// <param name="control">Control to check</param>
        /// <returns></returns>
        public static bool IsExist(this Control control)
        {
            control = control.ResolveControlByIndexAndVisibility();
            return control != null;
        }

        /// <summary>
        /// Waits for the control to exist on the page for performing actions on it
        /// </summary>
        /// <param name="control">The control that is needed to wait for</param>
        /// <param name="waitTime">The duration of time in milliseconds for which the wait will happen</param>
        /// <returns>Returns whether the control exists on the page after the wait time is over or not</returns>
        public static bool WaitForControlExist(this Control control, long? waitTime)
        {
            Playback.Wait(1000);
            waitTime = (waitTime == 0 || waitTime == null) ? Playback.PlaybackSettings.WaitForReadyTimeout : waitTime;

            var isVisible = false;
            var sw = Stopwatch.StartNew();
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
    }
}
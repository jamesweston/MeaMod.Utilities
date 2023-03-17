using System;
using System.Windows.Forms;

namespace MeaMod.Utilities
{
    /// <summary>
    /// 
    /// </summary>
    public static class ControlExtensions
    {
        /// <summary>
        /// Executes the Action asynchronously on the UI thread, does not block execution on the calling thread.
        /// </summary>
        /// <param name="this"></param>
        /// <param name="control"></param>
        /// <param name="code"></param>
        public static void RunOnUIThread(this Control @this, Action code)
        {
            if (@this.InvokeRequired)
            {
                @this.BeginInvoke(code);
            }
            else
            {
                code.Invoke();
            }
        }
    }
}

/* Utilities.cs -- utilities
 */

#region Using directives

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

using BLToolkit;

using DevExpress.Utils.Taskbar.Core;
using DevExpress.XtraEditors;
using DevExpress.XtraReports.UI;

using JetBrains.Annotations;

using ManagedClient;
using ManagedClient.Output;

using MoonSharp.Interpreter;

using Newtonsoft.Json;

using CM = System.Configuration.ConfigurationManager;

#endregion

namespace WinFormsBoilerplate
{
    /// <summary>
    /// Utilities
    /// </summary>
    public static class Utilities
    {
        #region Properties

        #endregion

        #region Private members

        #endregion

        #region Public methods

        public static void InvokeIfRequired
            (
                this Control control,
                MethodInvoker action
            )
        {
            // When the form, thus the control, isn't visible yet,
            // InvokeRequired returns false, resulting still
            // in a cross-thread exception.
            while (!control.Visible)
            {
                System.Threading.Thread.Sleep(50);
            }

            if (control.InvokeRequired)
            {
                control.Invoke(action);
            }
            else
            {
                action();
            }
        }

        #endregion
    }
}

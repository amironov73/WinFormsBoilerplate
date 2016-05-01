/* MainForm.cs -- application main form
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
    /// Application main form
    /// </summary>
    public partial class MainForm
        : DevExpress.XtraEditors.XtraForm
    {
        #region Properties

        /// <summary>
        /// Уровень доступа к данным.
        /// </summary>
        public DataAccessLevel DAL { get { return _dal; } }

        /// <summary>
        /// Отладочная печать.
        /// </summary>
        public AbstractOutput Output { get { return _logBox.Output; } }

        #endregion

        #region Construction

        public MainForm()
        {
            InitializeComponent();
        }

        #endregion

        #region Private members

        private DataAccessLevel _dal;

        private void MainForm_FormClosed
            (
                object sender,
                FormClosedEventArgs e
            )
        {
            if (DAL != null)
            {
                DAL.Dispose();
                _dal = null;
            }
        }

        private void MainForm_Load
            (
                object sender, 
                EventArgs e
            )
        {
            ShowVersionInfoInTitle();
            ShowSystemInformation();
            InitializeDAL();
            WriteDelimiter();
        }

        private void InitializeDAL ()
        {
            _dal = new DataAccessLevel(Output);
        }

        private void ShowVersionInfoInTitle()
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            Version vi = assembly.GetName().Version;
            FileVersionInfo fvi = FileVersionInfo
                .GetVersionInfo(assembly.Location);
            FileInfo fi = new FileInfo(assembly.Location);
            Text += string.Format
                (
                    ": version {0} (file {1}) from {2}",
                    vi,
                    fvi.FileVersion,
                    fi.LastWriteTime.ToShortDateString()
                );
        }

        private void ShowSystemInformation()
        {
            WriteLine
                (
                    "OS version: {0}",
                    Environment.OSVersion
                );
            WriteLine
                (
                    "Framework version: {0}",
                    Environment.Version
                );
            Assembly assembly = Assembly.GetExecutingAssembly();
            Version vi = assembly.GetName().Version;
            FileInfo fi = new FileInfo(assembly.Location);
            WriteLine
                (
                    "Application version: {0} ({1})",
                    vi,
                    fi.LastWriteTime.ToShortDateString()
                );
            WriteLine
                (
                    "Memory: {0} Mb",
                    GC.GetTotalMemory(false) / 1024
                );
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Turns on marquee on progress bar.
        /// </summary>
        public void StartProgress()
        {
            this.InvokeIfRequired
                (
                    () => _progressBar.Style = ProgressBarStyle.Marquee
                );
        }

        /// <summary>
        /// Turns off marquee.
        /// </summary>
        public void StopProgress()
        {
            this.InvokeIfRequired
                (
                    () => _progressBar.Style = ProgressBarStyle.Blocks
                );
        }

        public void WriteDelimiter()
        {
            if (Output != null)
            {
                Output.WriteLine
                    (
                        new string('=', 60)
                    );
            }
        }

        public void WriteLine
            (
                string format,
                params object[] args
            )
        {
            if (Output != null)
            {
                string text = string.Format(format, args);
                Output.WriteLine(text);
            }
        }

        #endregion
    }
}

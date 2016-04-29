/* DataAccessLevel.cs -- data access level
 */

#region Using directives

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using BLToolkit;

using JetBrains.Annotations;

using ManagedClient;
using ManagedClient.Output;

using MoonSharp.Interpreter;

using Newtonsoft.Json;

#endregion

namespace WinFormsBoilerplate
{
    /// <summary>
    /// Data access level
    /// </summary>
    public sealed class DataAccessLevel
        : IDisposable
    {
        #region Properties

        public AbstractOutput Output { get { return _output; } }

        #endregion

        #region Construction

        public DataAccessLevel
            (
                AbstractOutput output
            )
        {
            _output = output;

            WriteLine("Data access level initialized");
        }

        #endregion

        #region Private members

        private readonly AbstractOutput _output;

        #endregion

        #region Public methods

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

        #region IDisposable members

        public void Dispose()
        {
            
        }

        #endregion
    }
}

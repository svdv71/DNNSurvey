﻿#region Copyright
// DotNetNuke® - http://www.dnnsoftware.com
// Copyright (c) 2002-2015
// by DNN Corporation
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated 
// documentation files (the "Software"), to deal in the Software without restriction, including without limitation 
// the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and 
// to permit persons to whom the Software is furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all copies or substantial portions 
// of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED 
// TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL 
// THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF 
// CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER 
// DEALINGS IN THE SOFTWARE.
#endregion

namespace Dnn.Modules.Survey.Data
{
    using System;
    using DotNetNuke.Data;

    /// <summary>
    /// </summary>
    internal abstract class SurveyRepositoryBase : IDisposable
    {
        #region [ Properties ]

        /// <summary>
        /// Gets the database owner.
        /// </summary>
        /// <value>
        /// The database owner.
        /// </value>
        protected string DatabaseOwner
        {
            get
            {
                return DataProvider.Instance()
                                   .DatabaseOwner;
            }
        }

        /// <summary>
        /// Gets the object qualifier.
        /// </summary>
        /// <value>
        /// The object qualifier.
        /// </value>
        protected string ObjectQualifier
        {
            get
            {
                return DataProvider.Instance()
                                   .ObjectQualifier;
            }
        }

        #endregion

        /// <summary>
        /// Gets the fully qualified name of the object.
        /// </summary>
        /// <param name="objectName">Name of the object.</param>
        /// <returns></returns>
        protected virtual string GetFullyQualifiedName(string objectName)
        {
            return this.DatabaseOwner + this.ObjectQualifier + objectName;
        }

        #region [ IDisposable Members ]

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            // TODO: provide default implementation... Use the Diposable pattern: https://msdn.microsoft.com/en-us/library/b1yfkh5e%28v=vs.110%29.aspx
        }

        #endregion
    }
}

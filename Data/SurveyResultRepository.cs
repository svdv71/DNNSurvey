#region Copyright
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
    using System.Collections.Generic;
    using System.Data;
    using Dnn.Modules.Survey.Models;
    using DotNetNuke.Data;

    internal class SurveyResultRepository : SurveyRepositoryBase
    {
        /// <summary>
        /// Adds the survey result.
        /// </summary>
        /// <param name="surveyOptionId">The survey option identifier.</param>
        /// <param name="userId">The user identifier.</param>
        public void AddSurveyResult(int surveyOptionId, int userId)
        {
            this.AddSurveyResultInternal("AddSurveyResult_cookie", surveyOptionId, userId);
        }

        /// <summary>
        /// Adds the survey result with check.
        /// </summary>
        /// <param name="surveyOptionId">The survey option identifier.</param>
        /// <param name="userId">The user identifier.</param>
        public void AddSurveyResultWithCheck(int surveyOptionId, int userId)
        {
            this.AddSurveyResultInternal("AddSurveyResult", surveyOptionId, userId);
        }

        /// <summary>
        /// Gets the survey result data for the specified module identifier.
        /// </summary>
        /// <param name="moduleId">The module identifier.</param>
        /// <returns></returns>
        public IEnumerable<SurveyResultInfo> Get(int moduleId)
        {
            using (var context = DataContext.Instance())
            {
                var parameters = new object[] { moduleId };
                return context.ExecuteQuery<SurveyResultInfo>(CommandType.StoredProcedure, this.GetFullyQualifiedName("GetSurveyResultData"), parameters);
            }
        }

        #region [ Helpers ]

        /// <summary>
        /// Adds the survey result internal.
        /// </summary>
        /// <param name="storedProcedureName">Name of the stored procedure.</param>
        /// <param name="surveyOptionId">The survey option identifier.</param>
        /// <param name="userId">The user identifier.</param>
        private void AddSurveyResultInternal(string storedProcedureName, int surveyOptionId, int userId)
        {
            using (var context = DataContext.Instance())
            {
                var parameters = new object[] { surveyOptionId, userId };
                context.ExecuteQuery<SurveyDto>(CommandType.StoredProcedure, this.GetFullyQualifiedName(storedProcedureName), parameters);
            }
        }

        #endregion
    }
}

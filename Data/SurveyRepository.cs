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
    using System.Collections;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using Dnn.Modules.Survey.Models;
    using DotNetNuke.Common;
    using DotNetNuke.Common.Utilities;
    using DotNetNuke.Data;

    internal class SurveyRepository : SurveyRepositoryBase
    {
        /// <summary>
        /// Adds the specified survey.
        /// </summary>
        /// <param name="survey">The survey.</param>
        public void Add(SurveyInfo survey)
        {
            this.SaveInternal(survey);
        }

        /// <summary>
        /// Deletes the specified survey identifier.
        /// </summary>
        /// <param name="surveyId">The survey identifier.</param>
        public void Delete(int surveyId)
        {
            using (var context = DataContext.Instance())
            {
                var repository = context.GetRepository<SurveyInfo>();
                var survey = repository.GetById(surveyId);
                if (survey != null)
                {
                    repository.Delete(survey);
                }
            }
        }

        /// <summary>
        /// Gets the specified module identifier.
        /// </summary>
        /// <param name="moduleId">The module identifier.</param>
        /// <returns></returns>
        public IEnumerable<SurveyInfo> Get(int moduleId)
        {
            using (var context = DataContext.Instance())
            {
                var repository = context.GetRepository<SurveyInfo>();
                return repository.Get(moduleId);
            }
        }

        /// <summary>
        /// Gets Survey by the survey identifier.
        /// </summary>
        /// <param name="moduleId">The module identifier.</param>
        /// <param name="surveyId">The survey identifier.</param>
        /// <returns></returns>
        public SurveyInfo GetById(int moduleId, int surveyId)
        {
            using (var context = DataContext.Instance())
            {
                var parameters = new object[] { moduleId, surveyId };
                return context.ExecuteQuery<SurveyDto>(CommandType.StoredProcedure, this.GetFullyQualifiedName("GetSurvey"), parameters)
                              .FirstOrDefault();
            }
        }

        /// <summary>
        /// Updates the specified survey.
        /// </summary>
        /// <param name="survey">The survey.</param>
        public void Update(SurveyInfo survey)
        {
            this.SaveInternal(survey);
        }

        #region [ Helpers ]

        /// <summary>
        /// Saves the internal.
        /// </summary>
        /// <param name="survey">The survey.</param>
        private void SaveInternal(SurveyInfo survey)
        {
            Requires.NotNull("survey", survey);
            using (var context = DataContext.Instance())
            {
                var repository = context.GetRepository<SurveyInfo>();
                if (Null.IsNull(survey.SurveyId))
                {
                    repository.Insert(survey);
                }
                else
                {
                    repository.Update(survey);
                }
            }
        }

        #endregion
    }
}

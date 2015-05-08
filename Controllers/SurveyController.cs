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

namespace Dnn.Modules.Survey.Controllers
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Web.Mvc;
    using Dnn.Modules.Survey.Controllers.Contracts;
    using Dnn.Modules.Survey.Data;
    using Dnn.Modules.Survey.Models;
    using DotNetNuke.Common.Utilities;
    using DotNetNuke.Services.Localization;
    using DotNetNuke.Web.Mvc.Framework.ActionFilters;
    using DotNetNuke.Web.Mvc.Framework.Controllers;

    /// <summary>
    /// </summary>
    [DnnHandleErrorAttribute]
    public class SurveyController : DnnController
    {
        private const string EditTitleKey = "Edit";
        private const string EditControlKey = "Edit";

        /// <summary>
        /// </summary>
        private struct ResourceKeys
        {
            public const string MultipleSelection = "SelectionMulti.Text";

            public const string SingleSelection = "SelectionSingle.Text";
        }

        /// <summary>
        /// Indexes this instance.
        /// </summary>
        /// <returns></returns>
        [ModuleAction(TitleKey = EditTitleKey, ControlKey = EditControlKey)]
        public ActionResult Index()
        {
            var repository = new SurveyRepository();
            var surveys = repository.Get(this.ActiveModule.ModuleID)
                                    .OrderBy(survey => survey.ViewOrder)
                                    .ToList();
            var surveyList = new SurveyListInfo() { Surveys = surveys };
            return this.View(surveyList);
        }

        /// <summary>
        /// Edits this instance.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Edit()
        {
            var survey = new SurveyInfo() {  };

            return this.View(survey);
        }

        /// <summary>
        /// Saves the modified survey.
        /// </summary>
        /// <param name="survey">The survey.</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateInput(true)]
        public ActionResult Edit(SurveyInfo survey)
        {
            // Enrich survey
            survey.CreatedByUser = this.User.UserID;
            survey.CreatedDate = DateTime.Now;
            survey.ModuleId = this.ActiveModule.ModuleID;

            var repository = new SurveyRepository();
            if (Null.IsNull(survey.SurveyId))
            {
                repository.Add(survey);
            }
            else
            {
                repository.Update(survey);
            }

            return this.RedirectToDefaultRoute();
        }

        /// <summary>
        /// Updates this instance.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Update()
        {
            return this.RedirectToDefaultRoute();
        }

        /// <summary>
        /// Cancels this instance.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Cancel()
        {
            return this.RedirectToDefaultRoute();
        }


        /// <summary>
        /// Adds the option.
        /// </summary>
        /// <param name="surveyId">The survey identifier.</param>
        /// <param name="optionName">Name of the option.</param>
        /// <param name="isCorrect">if set to <c>true</c> [is correct].</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult AddOption(int surveyId, string optionName, bool isCorrect)
        {
            var survey = this.GetSurvey(this.ActiveModule.ModuleID, surveyId);

            using (var repository = new SurveyOptionsRepository())
            {
                var surveyOptions = repository.GetForSurvey(surveyId);

                if (survey.OptionType == "R" && isCorrect)
                {
                    var existingCorrectOption = surveyOptions.FirstOrDefault(option => option.IsCorrect);
                    if (existingCorrectOption != null)
                    {
                        existingCorrectOption.IsCorrect = false;
                        // repository.Update(existingCorrectOption);
                    }
                }

                var lastViewOrder = surveyOptions.Max(option => option.ViewOrder);
                var surveyOption = new SurveyOptionInfo() { IsCorrect = isCorrect, OptionName = optionName, SurveyId = surveyId, ViewOrder = lastViewOrder + 1 };

                // repository.Add(surveyOption);
            }

            var viewModel = this.GetSurveyOptionsViewModel(surveyId);

            return Json(viewModel);
        }

        /// <summary>
        /// Gets the option types.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<SelectListItem> GetOptionTypes()
        {
            this.LocalResourceFile = Path.Combine(this.ActiveModule.DesktopModule.FolderName, Localization.LocalResourceDirectory, "edit.resx");
            var items = new List<SelectListItem>
                            {
                                new SelectListItem() { Text = this.LocalizeString(ResourceKeys.MultipleSelection), Value = "C" },
                                new SelectListItem() { Text = this.LocalizeString(ResourceKeys.SingleSelection), Value = "R" }
                            };
            return items;
        }

        /// <summary>
        /// Gets the survey.
        /// </summary>
        /// <param name="moduleId">The module identifier.</param>
        /// <param name="surveyId">The survey identifier.</param>
        /// <returns></returns>
        private SurveyInfo GetSurvey(int moduleId, int surveyId)
        {
            using (var repository = new SurveyRepository())
            {
                return repository.GetById(moduleId, surveyId);
            }
        }

        /// <summary>
        /// Gets the survey options.
        /// </summary>
        /// <param name="surveyId">The survey identifier.</param>
        /// <returns></returns>
        private IEnumerable<SurveyOptionInfo> GetSurveyOptions(int surveyId)
        {
            using (var repository = new SurveyOptionsRepository())
            {
                return repository.GetForSurvey(surveyId);
            }
        }

        /// <summary>
        /// Gets the survey options.
        /// </summary>
        /// <param name="surveyId">The survey identifier.</param>
        /// <returns></returns>
        private SurveyOptionsViewModel GetSurveyOptionsViewModel(int surveyId)
        {
            var surveyOptions = this.GetSurveyOptions(surveyId);

            var viewModel = new SurveyOptionsViewModel() { SurveyOptions = surveyOptions.Select(SurveyOption.FromEntity)
                                                                                        .ToList() };

            return viewModel;
        }
    }
}

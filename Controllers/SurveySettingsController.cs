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

namespace Dnn.Modules.Survey.Controllers
{
    using System.Web.Mvc;
    using Dnn.Modules.Survey.Models;
    using DotNetNuke.Web.Mvc.Framework.Controllers;

    /// <summary>
    /// </summary>
    public class SurveySettingsController : DnnController
    {
        /// <summary>
        /// Settingses this instance.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Settings()
        {
            var settings = new SurveyModuleSettingsInfo(ModuleContext.Configuration.ModuleSettings);
            return this.View(settings);
        }

        /// <summary>
        /// Updates the ModuleSettings.
        /// </summary>
        /// <param name="surveyClosingDate">The survey closing date.</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Settings(string surveyClosingDate)
        {
            // ModuleContext.Configuration.ModuleSettings["Html_ReplaceTokens"] = supportsTokens.ToString();
            return RedirectToDefaultRoute();
        }

    }
}
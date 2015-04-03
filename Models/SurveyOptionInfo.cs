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

namespace Dnn.Modules.Survey.Models
{
    using DotNetNuke.ComponentModel.DataAnnotations;

    /// <summary>
    /// </summary>
    [TableName("SurveyOptions")]
    [PrimaryKey("SurveyOptionId")]
    [Scope("SurveyId")]
    public class SurveyOptionInfo
    {
        /// <summary>
        /// Gets or sets the survey option identifier.
        /// </summary>
        /// <value>
        /// The survey option identifier.
        /// </value>
        public int SurveyOptionId { get; set; }

        /// <summary>
        /// Gets or sets the survey identifier.
        /// </summary>
        /// <value>
        /// The survey identifier.
        /// </value>
        public int SurveyId { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is correct.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is correct; otherwise, <c>false</c>.
        /// </value>
        public bool IsCorrect { get; set; }

        /// <summary>
        /// Gets or sets the name of the option.
        /// </summary>
        /// <value>
        /// The name of the option.
        /// </value>
        public string OptionName { get; set; }

        /// <summary>
        /// Gets or sets the view order.
        /// </summary>
        /// <value>
        /// The view order.
        /// </value>
        public int ViewOrder { get; set; }

        /// <summary>
        /// Gets or sets the votes.
        /// </summary>
        /// <value>
        /// The votes.
        /// </value>
        public int Votes { get; set; }
    }
}

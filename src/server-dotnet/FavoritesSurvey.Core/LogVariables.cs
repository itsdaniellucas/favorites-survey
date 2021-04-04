using System;
using System.Collections.Generic;
using System.Text;

namespace FavoritesSurvey.Core
{
    public class LogVariables
    {
        public string CorrelationKey { get; set; } = string.Empty;
        public string RequestPath { get; set; } = string.Empty;
        public string HttpVerb { get; set; } = string.Empty;
        public string CurrentUser { get; set; } = string.Empty;
        public string Environment { get; set; } = string.Empty;
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace FavoritesSurvey.Core.Architecture.DAL
{
    public class PaginationParameter
    {
        public PaginationParameter()
        {
            Page = 1;
            ItemsPerPage = 50;
        }

        public int Page { get; set; } = 1;
        public int ItemsPerPage { get; set; } = 50;
    }
}

﻿using System;
using AERP.Base.DTO;

namespace AERP.DTO
{
   public class GeneralNationalityMasterSearchRequest : Request
    {
        public int ID
        {
            get;
            set;
        }
        public int CountryID
        {
            get;
            set;
        }
        public string SortOrder
        {
            get;
            set;
        }


        public string SortBy
        {
            get;
            set;
        }

        public int StartRow
        {
            get;
            set;
        }

        public int RowLength
        {
            get;
            set;
        }

        public int EndRow
        {
            get;
            set;
        }
        public string SearchBy { get; set; }
        public string SortDirection { get; set; }
    }
}


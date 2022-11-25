﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AERP.Base.DTO;

namespace AERP.DTO
{
  public  class CCRMComplaintLoggingMasterSearchRequest :Request
    {
        public int ID
        {
            get;
            set;
        }
        public string SortOrder
        {
            get;
            set;
        }
        public string CallTktNo
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
        public string SearchWord
        {
            get; set;
        }
        public string SearchBy { get; set; }
        public string SortDirection { get; set; }
        public int CCRMComplaintLoggingMasterID
        {
            get;
            set;
        }
    }
}

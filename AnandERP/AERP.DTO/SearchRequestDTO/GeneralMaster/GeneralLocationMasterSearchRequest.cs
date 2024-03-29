﻿using AERP.Base.DTO;
namespace AERP.DTO
{
    public class GeneralLocationMasterSearchRequest : Request
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
        public int CityID
        {
            get;
            set;
        }
        public int RegionID
        {
            get;
            set;
        }
        public string SearchWord { get; set; }
        public int MaxResults
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


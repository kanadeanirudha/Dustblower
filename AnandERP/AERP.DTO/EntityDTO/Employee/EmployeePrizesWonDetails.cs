﻿using AMS.Base.DTO;
using System;
namespace AMS.DTO
{
    public class EmployeePrizesWonDetails : BaseDTO
    {
        public int ID
        {
            get;
            set;
        }
        public int EmployeeID
        {
            get;
            set;
        }
        public int GeneralLevelMasterID
        {
            get;
            set;
        }
        public string GeneralLevelName
        {
            get;
            set;
        }
        public string PrizeName
        {
            get;
            set;
        }
        public string PrizeGivenBy
        {
            get;
            set;
        }
        public string PrizeReceivingDate
        {
            get;
            set;

        }
        public string PrizeIssuingAuthority
        {
            get;
            set;
        }
        public string Remark
        {
            get;
            set;
        }
        public bool IsActive
        {
            get;
            set;
        }
        public bool IsDeleted
        {
            get;
            set;
        }
        public int CreatedBy
        {
            get;
            set;
        }
        public DateTime CreatedDate
        {
            get;
            set;
        }
        public int? ModifiedBy
        {
            get;
            set;
        }
        public DateTime? ModifiedDate
        {
            get;
            set;
        }
        public int? DeletedBy
        {
            get;
            set;
        }
        public DateTime? DeletedDate
        {
            get;
            set;
        }
        public string errorMessage { get; set; }
    }
}

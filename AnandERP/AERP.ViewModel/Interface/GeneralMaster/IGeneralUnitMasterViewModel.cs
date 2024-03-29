﻿using AERP.DTO;
using System;
using System.Collections.Generic;

namespace AERP.ViewModel
{

    public interface IGeneralUnitMasterViewModel
    {
        GeneralUnitMaster GeneralUnitMasterDTO
        {
            get;
            set;
        }
        byte ID
        {
            get;
            set;
        }
        string UnitDescription
        {
            get;
            set;
        }
       string ShortCode
        {
            get;
            set;
        }
      
        bool IsDeleted
        {
            get;
            set;
        }
        int CreatedBy
        {
            get;
            set;
        }
        DateTime CreatedDate
        {
            get;
            set;
        }
        int? ModifiedBy
        {
            get;
            set;
        }
        DateTime? ModifiedDate
        {
            get;
            set;
        }
       int? DeletedBy
        {
            get;
            set;
        }
        DateTime? DeletedDate
        {
            get;
            set;
        }
        string errorMessage { get; set; }
    }
}

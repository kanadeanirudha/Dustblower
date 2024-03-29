﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AERP.Common;
using AERP.DTO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace AERP.ViewModel
{
    public class InventoryReportViewModel
    {
        public InventoryReportViewModel()
        {
            InventoryReportDTO = new InventoryReport();
            ListInventoryReport = new List<InventoryReport>();
            ListGeneralUnits = new List<GeneralUnits>();
            ListGetAdminRoleApplicableCentre = new List<AdminRoleApplicableDetails>();
        }


        public List<InventoryReport> ListInventoryReport { get; set; }
        public List<InventoryReport> GetGeneralUnitsForItemmaster { get; set; }
        public List<GeneralUnits> ListGeneralUnits
        {
            get;
            set;
        }
        public IEnumerable<SelectListItem> ListGetGeneralUnitsItems
        {
            get
            {
                return new SelectList(ListGeneralUnits, "ID", "UnitName");
            }
        }

        public List<AdminRoleApplicableDetails> ListGetAdminRoleApplicableCentre
        {
            get;
            set;
        }
        public IEnumerable<SelectListItem> ListGetAdminRoleApplicableCentreItems
        {
            get
            {
                return new SelectList(ListGetAdminRoleApplicableCentre, "CentreCode", "CentreName");
            }
        }

        public InventoryReport InventoryReportDTO { get; set; }

        public string DeliveryNumber {
            get
            {
                return (InventoryReportDTO != null) ? InventoryReportDTO.DeliveryNumber : string.Empty;
            }
            set
            {
                InventoryReportDTO.DeliveryNumber = value;
            }
        }

        public DateTime DeliveryTransDate {
            get
            {
                return (InventoryReportDTO != null) ? InventoryReportDTO.DeliveryTransDate : DateTime.Now;
            }
            set
            {
                InventoryReportDTO.DeliveryTransDate = value;
            }
        }

        public decimal SaleCost {
            get
            {
                return (InventoryReportDTO != null) ? InventoryReportDTO.SaleCost : new decimal();
            }
            set
            {
                InventoryReportDTO.SaleCost = value;
            }
        }

        public decimal PurchaseCost
        {
            get
            {
                return (InventoryReportDTO != null) ? InventoryReportDTO.PurchaseCost : new decimal();
            }
            set
            {
                InventoryReportDTO.PurchaseCost = value;
            }
        }

        public decimal CostDiff {
            get
            {
                return (InventoryReportDTO != null) ? InventoryReportDTO.CostDiff : new decimal();
            }
            set
            {
                InventoryReportDTO.CostDiff = value;
            }
        }

        public decimal ProfitLossPercentage
        {
            get
            {
                return (InventoryReportDTO != null) ? InventoryReportDTO.ProfitLossPercentage : new decimal();
            }
            set
            {
                InventoryReportDTO.ProfitLossPercentage = value;
            }
        }

        public string ReportFor
        {
            get
            {
                return (InventoryReportDTO != null) ? InventoryReportDTO.ReportFor : string.Empty;
            }
            set
            {
                InventoryReportDTO.ReportFor = value;
            }
        }
        public Int16 GeneralUnitsID
        {
            get
            {
                return (InventoryReportDTO != null) ? InventoryReportDTO.GeneralUnitsID : new Int16();
            }
            set
            {
                InventoryReportDTO.GeneralUnitsID = value;
            }
        }
        public string ItemReportList
        {
            get
            {
                return (InventoryReportDTO != null) ? InventoryReportDTO.ItemReportList : string.Empty;
            }
            set
            {
                InventoryReportDTO.ItemReportList = value;
            }
        }
        public string GeneralUnitsList
        {
            get
            {
                return (InventoryReportDTO != null) ? InventoryReportDTO.GeneralUnitsList : string.Empty;
            }
            set
            {
                InventoryReportDTO.GeneralUnitsList = value;
            }
        }
        public string ListAllUnits
        {
            get
            {
                return (InventoryReportDTO != null) ? InventoryReportDTO.ListAllUnits : string.Empty;
            }
            set
            {
                InventoryReportDTO.ListAllUnits = value;
            }
        }
        public bool IsPosted
        {
            get
            {
                return (InventoryReportDTO != null) ? InventoryReportDTO.IsPosted : false;
            }
            set
            {
                InventoryReportDTO.IsPosted = value;
            }
        }
        public string GeneralUnitsName
        {
            get
            {
                return (InventoryReportDTO != null) ? InventoryReportDTO.GeneralUnitsName : string.Empty;
            }
            set
            {
                InventoryReportDTO.GeneralUnitsName = value;
            }
        }
        public string CentreName
        {
            get
            {
                return (InventoryReportDTO != null) ? InventoryReportDTO.CentreName : string.Empty;
            }
            set
            {
                InventoryReportDTO.CentreName = value;
            }
        }
        public string MonthName
        {
            get
            {
                return (InventoryReportDTO != null) ? InventoryReportDTO.MonthName : string.Empty;
            }
            set
            {
                InventoryReportDTO.MonthName = value;
            }
        }
        [Display(Name = "Centre")]
        public string CentreCode
        {
            get
            {
                return (InventoryReportDTO != null) ? InventoryReportDTO.CentreCode : string.Empty;
            }
            set
            {
                InventoryReportDTO.CentreCode = value;
            }
        }
        [Display(Name = "Month")]
        public string MonthReport
        {
            get
            {
                return (InventoryReportDTO != null) ? InventoryReportDTO.MonthReport : string.Empty;
            }
            set
            {
                InventoryReportDTO.MonthReport = value;
            }
        }
        [Display(Name = "Year")]
        public string YearReport
        {
            get
            {
                return (InventoryReportDTO != null) ? InventoryReportDTO.YearReport : string.Empty;
            }
            set
            {
                InventoryReportDTO.YearReport = value;
            }
        }
        [Display(Name = "Item Description")]
        public string ItemDescription
        {
            get
            {
                return (InventoryReportDTO != null) ? InventoryReportDTO.ItemDescription: string.Empty;
            }
            set
            {
                InventoryReportDTO.ItemDescription = value;
            }
        }
        [Display(Name = "Upto Date")]
        public string UptoDate
        {
            get
            {
                return (InventoryReportDTO != null) ? InventoryReportDTO.UptoDate : string.Empty;
            }
            set
            {
                InventoryReportDTO.UptoDate = value;
            }
        }
        [Display(Name = "From Date")]
        public string FromDate
        {
            get
            {
                return (InventoryReportDTO != null) ? InventoryReportDTO.FromDate : string.Empty;
            }
            set
            {
                InventoryReportDTO.FromDate = value;
            }
        }
        
        
        public Int32 GeneralItemMasterID
        {
            get
            {
                return (InventoryReportDTO != null) ? InventoryReportDTO.GeneralItemMasterID : new Int32();
            }
            set
            {
                InventoryReportDTO.GeneralItemMasterID = value;
            }
        }
    }
}


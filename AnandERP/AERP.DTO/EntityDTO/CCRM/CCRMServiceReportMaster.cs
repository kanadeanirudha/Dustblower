﻿using System;
using AERP.Base.DTO;
using System.Collections.Generic;

namespace AERP.DTO
{
    public class CCRMServiceReportMaster : BaseDTO
    {
        public Int32 ID { get; set; }
        public Int32 MIFID { get; set; }
        public string SCRNo { get; set; }
        public string MIFName { get; set; }
        public string SerialNo { get; set; }
        public string ModelNo { get; set; }
        public string CallCloseDate { get; set; }
        public byte Priority { get; set; }
        public Int32 SrNo { get; set; }
        public Int32 LocationID { get; set; }
        public string LocationTypeCode { get; set; }
        public Int16 MachineFamilyID { get; set; }
        public string MachineFamilyName { get; set; }
        public string ContractCode { get; set; }
        public Int32 ContractTypeID { get; set; }
        public Int32 ComplaintSrNo { get; set; }
        public string CallDate { get; set; }
        public Int32 CallTypeID { get; set; }
        public string ComPlaint { get; set; }
        public string AllotDate { get; set; }
        public decimal AllotPeriod { get; set; }
        public string callCloseDate { get; set; }
        public string SRDate { get; set; }
        public string ArrivalDate { get; set; }
        public string CompletionDate { get; set; }
        public decimal ResponseTime { get; set; }
        public decimal DownTime { get; set; }
        public decimal TotalDownTime { get; set; }
        public decimal CompletionPeriod { get; set; }
        public decimal ArrivalPeriod { get; set; }
        public Int32 CurrentReadA4Mono { get; set; }
        public Int32 CurrentReadA4Col { get; set; }
        public Int32 CurrentReadA3Mono { get; set; }
        public Int32 CurrentReadA3Col { get; set; }
        public Int32 SymptomID { get; set; }
        public Int32 CauseID { get; set; }
        public Int32 ActionID { get; set; }
        public string SymptomCode { get; set; }
        public string CauseTitle { get; set; }
        public string ActionTitle { get; set; }
        public string CauseCode { get; set; }
        public string ActionCode { get; set; }
        public string SymptomTitle { get; set; }
        public Int32 EngineerID { get; set; }
        public string Remarks { get; set; }
        public byte CallStatus { get; set; }
        public string Status { get; set; }
        public string ReasonCode { get; set; }
        public string BrokenReason { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public Nullable<int> DeletedBy { get; set; }
        public Nullable<System.DateTime> DeletedDate { get; set; }
        public string errorMessage { get; set; }
        public string CallTktNo { get; set; }
        public string EnggName { get; set; }
        public string ItemDescription { get; set; }
        public string CentreCode { get; set; }
        public Int32 AdminRoleMasterID { get; set; }
        public string RightName { get; set; }
        public Int32 EmployeeID { get; set; }
        public string EmployeeCode { get; set; }
        public string EmployeeName { get; set; }
        public string CallTypeName { get; set; }
        public string CallerName { get; set; }
        public Int32 PerviousA4Mono { get; set; }
        public Int32 PerviousA4Col { get; set; }
        public Int32 PerviousA3Mono { get; set; }
        public Int32 PerviousA3Col { get; set; }
        public string SymptomDescrip { get; set; }
        public string CauseDescrip { get; set; }
        public string ActionDescrip { get; set; }
        public string Feedback { get; set; }
        public string CallerPh { get; set; }
        public string SignedBy { get; set; }
        public string PhoneNo { get; set; }
        public string ContractType { get; set; }
        public Int32 ItemID { get; set; }
        public string ItemName { get; set; }
        public string ItemCategoryCode { get; set; }
        public decimal Rate { get; set; }
        public Int32 Quantity { get; set; }
        public Int32 ItemNumber { get; set; }
        public byte Requierd { get; set; }
        public Int32 A4Mono { get; set; }
        public Int32 A4Col { get; set; }
        public Int32 A3Mono { get; set; }
        public Int32 A3Col { get; set; }
        public Int32 SignedID { get; set; }
        public string JobstartDate { get; set; }
        public string JobEndDate { get; set; }
        public decimal JobPeriod { get; set; }
        public bool SCNSubmitted { get; set; }
        public Int32 FeedbackID { get; set; }
        public string Symptom { get; set; }
        public string ServEnggName { get; set; }
        public Int32 CallId { get; set; }
        public Int32 SCRCount { get; set; }
        public string DispDate { get; set; }
        public Int32 UserID { get; set; }
        public string XmlString { get; set; }
        public string FeedbackName { get; set; }
        public string SubmittedOn { get; set; }
        public string VersionNumber { get; set; }
        public Int32 ReasonCodeID { get; set; }
        public string FileName { get; set; }
        public string TimeStamp { get; set; }
        public List<CCRMServiceReportMaster> ListOfItemsDetails
        {
            get; set;
        }

    }
}

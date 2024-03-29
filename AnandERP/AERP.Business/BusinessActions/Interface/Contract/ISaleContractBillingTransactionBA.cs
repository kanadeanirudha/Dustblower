﻿using AERP.Base.DTO;
using AERP.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AERP.Business.BusinessAction
{
    public interface ISaleContractBillingTransactionBA
    {
        IBaseEntityCollectionResponse<SaleContractBillingTransaction> GetSaleContractBillingTransactionBySearch(SaleContractBillingTransactionSearchRequest searchRequest);
        IBaseEntityCollectionResponse<SaleContractBillingTransaction> GetBillingTransactionForGeneration(SaleContractBillingTransactionSearchRequest searchRequest);
        IBaseEntityResponse<SaleContractBillingTransaction> GenerateSaleContractInvoiceTransaction(SaleContractBillingTransaction item);
        IBaseEntityCollectionResponse<SaleContractBillingTransaction> GetBillingTransactionDetailsByID(SaleContractBillingTransactionSearchRequest searchRequest);
        IBaseEntityCollectionResponse<SaleContractBillingTransaction> GetBillingTransactionDetailsByIDForInvoicePDF(SaleContractBillingTransactionSearchRequest searchRequest);
        IBaseEntityCollectionResponse<SaleContractBillingTransaction> GetSummerySheetForBillingTransactionDetails(SaleContractBillingTransactionSearchRequest searchRequest);
        IBaseEntityCollectionResponse<SaleContractBillingTransaction> GetSummerySheetForBillingTransactionDetails_Second(SaleContractBillingTransactionSearchRequest searchRequest);
        IBaseEntityResponse<SaleContractBillingTransaction> CancelSaleContractInvoiceTransaction(SaleContractBillingTransaction item);

        IBaseEntityResponse<SaleContractBillingTransaction> SaveSaleContractInvoiceTransaction(SaleContractBillingTransaction item); 
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EVS_CRM_To_BC_Integration.Clases
{
    class ModelClass
    {
        public class Contact
        {
            public string No { get; set; }
            public string Name { get; set; }
            public string CFDI_Customer_Name { get; set; }
            public string Name_2 { get; set; }
            public string Search_Name { get; set; }
            public string Blocked { get; set; }
            public bool Privacy_Blocked { get; set; }
            public bool Disable_Search_by_Name { get; set; }
            public string Address { get; set; }
            public string Address_2 { get; set; }
            public string Country_Region_Code { get; set; }
            public string City { get; set; }
            public string County { get; set; }
            public string Post_Code { get; set; }
            public string ShowMap { get; set; }
            public string Phone_No { get; set; }
            public string MobilePhoneNo { get; set; }
            public string E_Mail { get; set; }
            public string Fax_No { get; set; }
            public string Home_Page { get; set; }
            public string Language_Code { get; set; }
            public string Primary_Contact_No { get; set; }
            public string ContactName { get; set; }
            public string Bill_to_Customer_No { get; set; }
            public string Customer_Posting_Group { get; set; }
            public string Gen_Bus_Posting_Group { get; set; }
        }


        public class token
        {
            public string token_type { get; set; }
            public int expires_in { get; set; }
            public int ext_expires_in { get; set; }
            public string access_token { get; set; }
        }


        public class GetCustomer
        {

            public string odatacontext { get; set; }
            public List<CustomerValue> value { get; set; }
        }

        public class CustomerValue
        {

            public string odataetag { get; set; }
            public string No { get; set; }
            public string Name { get; set; }
            public string CFDI_Customer_Name { get; set; }
            public string Name_2 { get; set; }
            public string Search_Name { get; set; }
            public string IC_Partner_Code { get; set; }
            public int Balance_LCY { get; set; }
            public int BalanceAsVendor { get; set; }
            public int Balance_Due_LCY { get; set; }
            public int Credit_Limit_LCY { get; set; }
            public string Blocked { get; set; }
            public bool Privacy_Blocked { get; set; }
            public string Salesperson_Code { get; set; }
            public string Responsibility_Center { get; set; }
            public string Service_Zone_Code { get; set; }
            public string Document_Sending_Profile { get; set; }
            public int TotalSales2 { get; set; }
            public double CustSalesLCY_CustProfit_AdjmtCostLCY { get; set; }
            public double AdjCustProfit { get; set; }
            public double AdjProfitPct { get; set; }
            public string CFDI_Purpose { get; set; }
            public string CFDI_Relation { get; set; }
            public string CFDI_Export_Code { get; set; }
            public string SAT_Tax_Regime_Classification { get; set; }
            public bool CFDI_General_Public { get; set; }
            public string CFDI_Period { get; set; }
            public string Last_Date_Modified { get; set; }
            public bool Disable_Search_by_Name { get; set; }
            public string Address { get; set; }
            public string Address_2 { get; set; }
            public string Country_Region_Code { get; set; }
            public string City { get; set; }
            public string County { get; set; }
            public string Post_Code { get; set; }
            public string ShowMap { get; set; }
            public string Phone_No { get; set; }
            public string MobilePhoneNo { get; set; }
            public string E_Mail { get; set; }
            public string Fax_No { get; set; }
            public string Home_Page { get; set; }
            public string Language_Code { get; set; }
            public string Primary_Contact_No { get; set; }
            public string ContactName { get; set; }
            public string Bill_to_Customer_No { get; set; }
            public string VAT_Registration_No { get; set; }
            public string EORI_Number { get; set; }
            public string GLN { get; set; }
            public bool Use_GLN_in_Electronic_Document { get; set; }
            public string Copy_Sell_to_Addr_to_Qte_From { get; set; }
            public bool Tax_Liable { get; set; }
            public string Tax_Area_Code { get; set; }
            public string Tax_Identification_Type { get; set; }
            public string Tax_Exemption_No { get; set; }
            public string RFC_No { get; set; }
            public string CURP_No { get; set; }
            public string State_Inscription { get; set; }
            public string Gen_Bus_Posting_Group { get; set; }
            public string VAT_Bus_Posting_Group { get; set; }
            public string Customer_Posting_Group { get; set; }
            public string Currency_Code { get; set; }
            public string Price_Calculation_Method { get; set; }
            public string Customer_Price_Group { get; set; }
            public string Customer_Disc_Group { get; set; }
            public bool Allow_Line_Disc { get; set; }
            public string Invoice_Disc_Code { get; set; }
            public bool Prices_Including_VAT { get; set; }
            public int Prepayment_Percent { get; set; }
            public string Application_Method { get; set; }
            public string Partner_Type { get; set; }
            public string Intrastat_Partner_Type { get; set; }
            public string Payment_Terms_Code { get; set; }
            public string Payment_Method_Code { get; set; }
            public string Reminder_Terms_Code { get; set; }
            public string Fin_Charge_Terms_Code { get; set; }
            public string Cash_Flow_Payment_Terms_Code { get; set; }
            public bool Print_Statements { get; set; }
            public int Last_Statement_No { get; set; }
            public bool Block_Payment_Tolerance { get; set; }
            public string Preferred_Bank_Account_Code { get; set; }
            public string Bank_Communication { get; set; }
            public string Check_Date_Format { get; set; }
            public string Check_Date_Separator { get; set; }
            public string Ship_to_Code { get; set; }
            public string Location_Code { get; set; }
            public bool Combine_Shipments { get; set; }
            public string Reserve { get; set; }
            public string Shipping_Advice { get; set; }
            public string Shipment_Method_Code { get; set; }
            public string Shipping_Agent_Code { get; set; }
            public string Shipping_Agent_Service_Code { get; set; }
            public string Shipping_Time { get; set; }
            public string Base_Calendar_Code { get; set; }
            public string Customized_Calendar { get; set; }
            public int ExpectedCustMoneyOwed { get; set; }
            public int TotalMoneyOwed { get; set; }
            public int CalcCreditLimitLCYExpendedPct { get; set; }
            public int Balance_Due { get; set; }
            public int Payments_LCY { get; set; }
            public int CustomerMgt_AvgDaysToPay_No { get; set; }
            public int DaysPaidPastDueDate { get; set; }
            public int AmountOnPostedInvoices { get; set; }
            public int AmountOnCrMemo { get; set; }
            public int AmountOnOutstandingInvoices { get; set; }
            public int AmountOnOutstandingCrMemos { get; set; }
            public int Totals { get; set; }
            public int CustInvDiscAmountLCY { get; set; }
            public string Global_Dimension_1_Filter { get; set; }
            public string Global_Dimension_2_Filter { get; set; }
            public string Currency_Filter { get; set; }
            public string Date_Filter { get; set; }
        }

        public class GetOrder
        {

            public string odatacontext { get; set; }
            public List<OrderValue> value { get; set; }
        }

        public class OrderValue
        {

            public string odataetag { get; set; }
            public string Document_Type { get; set; }
            public string No { get; set; }
            public string Sell_to_Customer_No { get; set; }
            public string Sell_to_Customer_Name { get; set; }
            public string Quote_No { get; set; }
            public string Posting_Description { get; set; }
            public string Sell_to_Address { get; set; }
            public string Sell_to_Address_2 { get; set; }
            public string Sell_to_City { get; set; }
            public string Sell_to_County { get; set; }
            public string Sell_to_Post_Code { get; set; }
            public string Sell_to_Country_Region_Code { get; set; }
            public string Sell_to_Contact_No { get; set; }
            public string Sell_to_Phone_No { get; set; }
            public string SellToMobilePhoneNo { get; set; }
            public string Sell_to_E_Mail { get; set; }
            public string Sell_to_Contact { get; set; }
            public int No_of_Archived_Versions { get; set; }
            public string Document_Date { get; set; }
            public string Posting_Date { get; set; }
            public string VAT_Reporting_Date { get; set; }
            public string Order_Date { get; set; }
            public string Due_Date { get; set; }
            public string Requested_Delivery_Date { get; set; }
            public string Promised_Delivery_Date { get; set; }
            public string External_Document_No { get; set; }
            public string Your_Reference { get; set; }
            public string ShpfyOrderNo { get; set; }
            public string ShpfyShopify_Risk_Level { get; set; }
            public string Salesperson_Code { get; set; }
            public string Campaign_No { get; set; }
            public string Opportunity_No { get; set; }
            public string Responsibility_Center { get; set; }
            public string Assigned_User_ID { get; set; }
            public string Job_Queue_Status { get; set; }
            public string Status { get; set; }
            public string Process_Status { get; set; }
            public string CFDI_Purpose { get; set; }
            public string CFDI_Relation { get; set; }
            public string CFDI_Export_Code { get; set; }
            public string CFDI_Period { get; set; }
            public string WorkDescription { get; set; }
            public string Currency_Code { get; set; }
            public string Company_Bank_Account_Code { get; set; }
            public bool Prices_Including_VAT { get; set; }
            public string VAT_Bus_Posting_Group { get; set; }
            public string Customer_Posting_Group { get; set; }
            public string Payment_Terms_Code { get; set; }
            public string Payment_Method_Code { get; set; }
            public bool EU_3_Party_Trade { get; set; }
            public bool Tax_Liable { get; set; }
            public string Tax_Area_Code { get; set; }
            public string SelectedPayments { get; set; }
            public string Shortcut_Dimension_1_Code { get; set; }
            public string Shortcut_Dimension_2_Code { get; set; }
            public int Payment_Discount_Percent { get; set; }
            public string Pmt_Discount_Date { get; set; }
            public string Journal_Templ_Name { get; set; }
            public string Direct_Debit_Mandate_ID { get; set; }
            public string ShippingOptions { get; set; }
            public string Ship_to_Code { get; set; }
            public string Ship_to_Name { get; set; }
            public string Ship_to_Address { get; set; }
            public string Ship_to_Address_2 { get; set; }
            public string Ship_to_City { get; set; }
            public string Ship_to_County { get; set; }
            public string Ship_to_Post_Code { get; set; }
            public string Ship_to_Country_Region_Code { get; set; }
            public string Ship_to_UPS_Zone { get; set; }
            public string Ship_to_Contact { get; set; }
            public string Shipment_Method_Code { get; set; }
            public string Shipping_Agent_Code { get; set; }
            public string Shipping_Agent_Service_Code { get; set; }
            public string Package_Tracking_No { get; set; }
            public string BillToOptions { get; set; }
            public string Bill_to_Name { get; set; }
            public string Bill_to_Address { get; set; }
            public string Bill_to_Address_2 { get; set; }
            public string Bill_to_City { get; set; }
            public string Bill_to_County { get; set; }
            public string Bill_to_Post_Code { get; set; }
            public string Bill_to_Country_Region_Code { get; set; }
            public string Bill_to_Contact_No { get; set; }
            public string Bill_to_Contact { get; set; }
            public string BillToContactPhoneNo { get; set; }
            public string BillToContactMobilePhoneNo { get; set; }
            public string BillToContactEmail { get; set; }
            public string Location_Code { get; set; }
            public string Shipment_Date { get; set; }
            public string Shipping_Advice { get; set; }
            public string Outbound_Whse_Handling_Time { get; set; }
            public string Shipping_Time { get; set; }
            public bool Late_Order_Shipping { get; set; }
            public bool Combine_Shipments { get; set; }
            public bool Completely_Shipped { get; set; }
            public string Transaction_Specification { get; set; }
            public string Transaction_Type { get; set; }
            public string Transport_Method { get; set; }
            public string Exit_Point { get; set; }
            public string Area { get; set; }
            public string Language_Code { get; set; }
            public int Prepayment_Percent { get; set; }
            public bool Compress_Prepayment { get; set; }
            public string Prepmt_Payment_Terms_Code { get; set; }
            public string Prepayment_Due_Date { get; set; }
            public int Prepmt_Payment_Discount_Percent { get; set; }
            public string Prepmt_Pmt_Discount_Date { get; set; }
            public bool Prepmt_Include_Tax { get; set; }
            public string Transit_to_Location { get; set; }
            public int Transport_Operators { get; set; }
            public DateTime Transit_from_Date_Time { get; set; }
            public int Transit_Hours { get; set; }
            public int Transit_Distance { get; set; }
            public string Vehicle_Code { get; set; }
            public string Trailer_1 { get; set; }
            public string Trailer_2 { get; set; }
            public bool Control1310005 { get; set; }
            public string Insurer_Name { get; set; }
            public string Insurer_Policy_Number { get; set; }
            public string Medical_Insurer_Name { get; set; }
            public string Medical_Ins_Policy_Number { get; set; }
            public string SAT_Weight_Unit_Of_Measure { get; set; }
            public string Date_Filter { get; set; }
            public string Location_Filter { get; set; }
        }


        public class PostOrder
        {
            public string Document_Type { get; set; }
            public string No { get; set; }
            public string Sell_to_Customer_No { get; set; }
            public string Sell_to_Customer_Name { get; set; }
            public string Posting_Description { get; set; }
            //public string Document_Date { get; set; }
            //public string Posting_Date { get; set; }
            //public string VAT_Reporting_Date { get; set; }
            public string Order_Date { get; set; }
            //public string Due_Date { get; set; }
            public string Requested_Delivery_Date { get; set; }
            public string Promised_Delivery_Date { get; set; }
            public string External_Document_No { get; set; }
            public string ShippingOptions { get; set; }
            public string BillToOptions { get; set; }
            public string Location_Code { get; set; }
            //public string Shipment_Date { get; set; }
            //public string Shipping_Advice { get; set; }
        }

        public class PostOrderLine
        {
            public string Document_Type { get; set; }
            public string Document_No { get; set; }
            public int Line_No { get; set; }
            public string Type { get; set; }
            public string FilteredTypeField { get; set; }
            public string No { get; set; }
            public string Item_Reference_No { get; set; }
            public string Description { get; set; }
            public string Location_Code { get; set; }
            public string Control50 { get; set; }
            public decimal Quantity { get; set; }
            public string Unit_of_Measure_Code { get; set; }
            public string Unit_of_Measure { get; set; }
            public int Unit_Cost_LCY { get; set; }
            public bool SalesPriceExist { get; set; }
            public decimal Unit_Price { get; set; }
            public bool Tax_Liable { get; set; }
        }
        public class AccountModel
        {

            public string accountnumber { get; set; }

        }
        public class Error
        {
            public string code { get; set; }
            public string message { get; set; }
        }

        public class ErrorRoot
        {
            public Error error { get; set; }
        }



    }
}

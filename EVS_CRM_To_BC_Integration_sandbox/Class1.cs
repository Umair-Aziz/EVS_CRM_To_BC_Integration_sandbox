using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using Microsoft.Xrm.Sdk.Workflow;
using System;
using System.Activities;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using static EVS_CRM_To_BC_Integration.Clases.ModelClass;

namespace EVS_CRM_To_BC_Integration
{
    public class Class1 : CodeActivity
    {

        protected override void Execute(CodeActivityContext Execution)
        {
            IWorkflowContext context = Execution.GetExtension<IWorkflowContext>();
            ITracingService tracingService = Execution.GetExtension<ITracingService>();

            //create iorganization service object
            Guid userId = context.InitiatingUserId;
            tracingService.Trace("Tracing : Execute");

            IOrganizationServiceFactory serviceFactory = Execution.GetExtension<IOrganizationServiceFactory>();

            IOrganizationService _service = serviceFactory.CreateOrganizationService(context.InitiatingUserId);

            Guid SalesOrderHeaderId = Guid.Empty;
            string Globalvariable = "";
            try
                {
                    tracingService.Trace("before PRIMARY ID");
                    //Guid SalesOrderHeaderId = Guid.Empty;
                    SalesOrderHeaderId = context.PrimaryEntityId;
                    string Errormsg = "";
                    string CustomerNo = "";
                    string OrderNumber = "";
                    string ContactName = "";
                    string ContactEmail = "";
                    string Telephone = "";
                    string Addressline1 = "";
                    string Addressline2 = "";
                    string Addressline3 = "";
                    string Address1City = "";
                    string Address1State = "";
                    string Address1Postal = "";
                    string Address1Country = "";
                    string RequestDelieveryDate = "";
                    string DateFulfilled = "";
                    string AccNo = null;

                    //Guid SalesOrderHeaderId = Guid.Empty;
                    Guid ContactId = Guid.Empty;
                    int i = 0;
                    int Lno = 0;
                    //-------- get latest customer from BC-----------//
                    //Entity salesOrder = new Entity("salesorder");
                    Entity account = new Entity("account");

                    Entity salesorder = _service.Retrieve("salesorder", SalesOrderHeaderId, new ColumnSet(true));

                    //SalesOrderHeaderId = new Guid(salesorder.Attributes["salesorderid"].ToString());

                    ContactId = ((EntityReference)salesorder.Attributes["customerid"]).Id;
                    ContactName = ((EntityReference)salesorder.Attributes["customerid"]).Name;
                    account = _service.Retrieve("account", ContactId, new ColumnSet(true));

                    if (account.Attributes.Contains("accountnumber"))
                    {
                    tracingService.Trace("Customer existed, thus not creating customer");
                    AccNo = account.Attributes["accountnumber"].ToString();
                    }



                    ConditionExpression condition1 = new ConditionExpression();
                    condition1.AttributeName = "salesorderid";
                    condition1.Operator = ConditionOperator.Equal;
                    condition1.Values.Add(SalesOrderHeaderId);

                    FilterExpression filter1 = new FilterExpression();
                    filter1.Conditions.Add(condition1);


                    QueryExpression query = new QueryExpression("salesorderdetail");
                    query.ColumnSet.AddColumns("productid", "quantity", "priceperunit", "baseamount", "extendedamount", "uomid");
                    query.Criteria.AddFilter(filter1);

                    EntityCollection orderline = _service.RetrieveMultiple(query);


                    if (orderline.Entities.Count > 0)
                    {
                        i = orderline.Entities.Count;
                    }


                    if (account.Attributes.Contains("emailaddress1"))
                    {
                        ContactEmail = account.Attributes["emailaddress1"].ToString();
                    }
                    if (account.Attributes.Contains("telephone1"))
                    {
                        Telephone = account.Attributes["telephone1"].ToString();
                    }
                    if (account.Attributes.Contains("address1_line1"))
                    {
                        Addressline1 = account.Attributes["address1_line1"].ToString();
                    }
                   if (account.Attributes.Contains("address1_line2"))
                    {
                        Addressline2 = account.Attributes["address1_line2"].ToString();
                    }
                    if (account.Attributes.Contains("address1_line3"))
                    {
                        Addressline3 = account.Attributes["address1_line3"].ToString();
                    }
                   if (account.Attributes.Contains("address1_city"))
                    {
                        Address1City = account.Attributes["address1_city"].ToString();
                    }
                   if (account.Attributes.Contains("address1_stateorprovince"))
                    {
                        Address1State = account.Attributes["address1_stateorprovince"].ToString();
                    }
                    if (account.Attributes.Contains("address1_postalcode"))
                    {
                        Address1Postal = account.Attributes["address1_postalcode"].ToString();
                    }
                    if (account.Attributes.Contains("address1_country"))
                    {
                        Address1Country = account.Attributes["address1_country"].ToString();
                    }

                    if (salesorder.Attributes.Contains("requestdeliveryby"))
                    {
                        RequestDelieveryDate = salesorder.Attributes["requestdeliveryby"].ToString();
                        DateTime dateTime = Convert.ToDateTime(RequestDelieveryDate);
                        RequestDelieveryDate = dateTime.ToString("yyyy-MM-dd");
                    }

                    if (salesorder.Attributes.Contains("datefulfilled"))
                    {
                        DateFulfilled = salesorder.Attributes["datefulfilled"].ToString();
                        DateTime dateTime = Convert.ToDateTime(DateFulfilled);
                        DateFulfilled = dateTime.ToString("yyyy-MM-dd");
                    }

                    if (AccNo == null)
                    {
                    tracingService.Trace("Customer not Existed, thus Creating Customer");

                    string Token1 = TokenGenerator();
                        string GetAccountBC = "https://api.businesscentral.dynamics.com/v2.0/edb43d17-548c-4b01-8258-27a8d7679cf2/Sandbox_QAS/ODataV4/Company('FluxErgy')/Customer_Card";
                        var client1 = new HttpClient();
                        client1.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token1);
                        var latestcontent = client1.GetStringAsync(GetAccountBC);
                        var results = client1.GetStringAsync(GetAccountBC).Result;
                        var result2 = new JavaScriptSerializer().Deserialize<GetCustomer>(results);
                        int count = result2.value.Count - 1;
                        var converter = result2.value[count].No;
                        converter = converter.Remove(0, 1);
                        int Num = Convert.ToInt32(converter);
                        int CustNo = Num + 1;
                        var chk = CustNo.ToString().Length;
                        if (chk == 3)
                        {
                            CustomerNo = "C00" + CustNo;
                        }
                        else if (chk == 2)
                        {
                            CustomerNo = "C000" + CustNo;
                        }
                        else if (chk == 4)
                        {
                            CustomerNo = "C0" + CustNo;
                        }
                        else if (chk == 5)
                        {
                            CustomerNo = "C" + CustNo;
                        }
                        else if (chk == 1)
                        {
                            CustomerNo = "C0000" + CustNo;
                        }

                        //-------- get latest customer from BC-----------//

                        //-------- Post customer in BC-----------//



                        string Token2 = TokenGenerator();

                        string PostAccountBC = "https://api.businesscentral.dynamics.com/v2.0/edb43d17-548c-4b01-8258-27a8d7679cf2/Sandbox_QAS/ODataV4/Company('FluxErgy')/Customer_Card";
                        var client2 = new HttpClient();
                        client2.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token2);
                        client2.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                        Contact customer = new Contact();
                        customer.Address = Addressline1;
                        customer.Address_2 = Addressline2;
                        customer.Phone_No = Telephone;
                        customer.E_Mail = ContactEmail;
                        customer.Name = ContactName;
                        customer.Search_Name = ContactName;
                        customer.Privacy_Blocked = false;
                        customer.Disable_Search_by_Name = false;
                        customer.Blocked = " ";
                        customer.Name_2 = "";
                        customer.CFDI_Customer_Name = "";
                        customer.Country_Region_Code = Address1Country;
                        customer.City = Address1City;
                        customer.Post_Code = Address1Postal;
                        customer.County = Address1State;
                        customer.ShowMap = "Show on Map";
                        customer.No = CustomerNo;
                        customer.Customer_Posting_Group = "General";
                        customer.Gen_Bus_Posting_Group = "General";

                        var jsonobject = new JavaScriptSerializer().Serialize(customer);

                        //var jsonObject = "";

                        var content = new StringContent(jsonobject.ToString(), Encoding.UTF8, "application/json");
                        client2.BaseAddress = new Uri(PostAccountBC);

                        var response = client2.PostAsync(PostAccountBC, content);


                        var result = client2.PostAsync(PostAccountBC, content).Result;
                        if (result.ReasonPhrase == "Created" || response.Result.ReasonPhrase == "Created")
                        {
                            Console.WriteLine("Customer Created");
                            tracingService.Trace("Customer Created");
                        Globalvariable = "Customer Created " + Environment.NewLine;
                    }

                        else
                        {
                            tracingService.Trace("Customer Not Created");
                            var contents = response.Result.Content.ReadAsStringAsync();
                            var cust = contents.Result;
                            var lin = new JavaScriptSerializer().Deserialize<ErrorRoot>(cust);
                            Errormsg = lin.error.message;

                            throw new InvalidWorkflowException(Errormsg);
                        }

                    //--------update account in CRM ----------------//
                        tracingService.Trace("Updating Customer Account Number in CRM");
                        string CRMToken = TokenGeneratorCRM();
                        AccountModel accModel = new AccountModel();

                        accModel.accountnumber = CustomerNo;
                        string UpdateURL = "https://fluxergysandbox.api.crm.dynamics.com/api/data/v9.2/accounts(" + ContactId + ")";
                        var client7 = new HttpClient();
                        client7.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", CRMToken);
                        client7.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));


                        var jsonupdate = new JavaScriptSerializer().Serialize(accModel);

                        //var jsonObject = "";

                        var Updatecontent = new StringContent(jsonupdate.ToString(), Encoding.UTF8, "application/json");
                        client7.BaseAddress = new Uri(UpdateURL);
                        var urlpost = new Uri(UpdateURL);

                        var Updateresponse = client7.PatchAsync(urlpost, Updatecontent);


                        var Updateresult = client7.PatchAsync(urlpost, Updatecontent).Result;
                        if (Updateresult.ReasonPhrase == "No Content" || Updateresponse.Result.ReasonPhrase == "No Content")
                        {
                            Console.WriteLine("Contact Updated in CRM");
                            tracingService.Trace("Customer Updated in CRM");
                        Globalvariable = Globalvariable + "Contact Updated in CRM " + Environment.NewLine;
                    }
                        else
                        {
                        tracingService.Trace("Customer not Updated in CRM");
                        var contents = Updateresponse.Result.Content.ReadAsStringAsync();
                            var cust = contents.Result;
                            var lin = new JavaScriptSerializer().Deserialize<ErrorRoot>(cust);
                            Errormsg = lin.error.message;

                           
                            throw new InvalidWorkflowException(Errormsg);
                    }


                        //--------update account in CRM ----------------//

                    }
                    else
                    {
                        CustomerNo = AccNo;
                    }
                //-------- Post customer in BC-----------//

                //----------- Get Sales Order No from BC -------------//

                tracingService.Trace("Creating Order in CRM");
                string GetOrderBC = "https://api.businesscentral.dynamics.com/v2.0/edb43d17-548c-4b01-8258-27a8d7679cf2/Sandbox_QAS/ODataV4/Company('FluxErgy')/Sales_Orders_Header";
                    string Token3 = TokenGenerator();
                    var client3 = new HttpClient();
                    client3.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token3);
                    var latestcontent1 = client3.GetStringAsync(GetOrderBC);
                    var results1 = client3.GetStringAsync(GetOrderBC).Result;
                    var result3 = new JavaScriptSerializer().Deserialize<GetOrder>(results1);
                    int Ordercount = result3.value.Count - 1;
                    var Orderconverter = result3.value[Ordercount].No;
                    Orderconverter = Orderconverter.Remove(0, 5);
                    int OrderNum = Convert.ToInt32(Orderconverter);
                    int OrderNo = OrderNum + 1;
                    var orderchk = OrderNo.ToString().Length;

                    if (orderchk == 1)
                    {
                        OrderNumber = "S-ORD00000" + OrderNo;
                    }
                    else if (orderchk == 2)
                    {
                        OrderNumber = "S-ORD0000" + OrderNo;
                    }
                    else if (orderchk == 3)
                    {
                        OrderNumber = "S-ORD000" + OrderNo;
                    }
                    else if (orderchk == 4)
                    {
                        OrderNumber = "S-ORD00" + OrderNo;
                    }
                    else if (orderchk == 5)
                    {
                        OrderNumber = "S-ORD0" + OrderNo;
                    }
                    else if (orderchk == 6)
                    {
                        OrderNumber = "S-ORD" + OrderNo;
                    }


                    //----------- Get Sales Order No from BC -------------//

                    //-------- Post Sales Order Header in BC-----------//

                    string Token4 = TokenGenerator();

                    string OrderURL = "https://api.businesscentral.dynamics.com/v2.0/edb43d17-548c-4b01-8258-27a8d7679cf2/Sandbox_QAS/ODataV4/Company('FluxErgy')/Sales_Orders_Header";
                    var client4 = new HttpClient();
                    client4.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token4);
                    client4.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    PostOrder postOrder = new PostOrder();

                    //postOrder.Document_Date = "";
                    postOrder.Document_Type = "Order";
                    //postOrder.Due_Date = "";
                    postOrder.External_Document_No = "abcd123";
                    postOrder.Location_Code = "";
                    postOrder.No = OrderNumber;
                    postOrder.Sell_to_Customer_No = CustomerNo;
                    postOrder.Sell_to_Customer_Name = ContactName;
                    postOrder.Posting_Description = "Order " + OrderNumber;
                    //postOrder.Posting_Date = DateTime.Now.ToString();

                    //postOrder.Order_Date = DateTime.Now.ToString();
                    postOrder.Requested_Delivery_Date = RequestDelieveryDate;
                    postOrder.Promised_Delivery_Date = DateFulfilled;
                    postOrder.ShippingOptions = "Custom Address";
                    postOrder.BillToOptions = "Custom Address";
                    //postOrder.Shipment_Date = "";


                    var jsonobjectOrder = new JavaScriptSerializer().Serialize(postOrder);

                    //var jsonObject = "";

                    var Ordercontent = new StringContent(jsonobjectOrder.ToString(), Encoding.UTF8, "application/json");
                    client4.BaseAddress = new Uri(OrderURL);

                    var Orderresponse = client4.PostAsync(OrderURL, Ordercontent);


                    var Orderresult = client4.PostAsync(OrderURL, Ordercontent).Result;
                    if (Orderresult.ReasonPhrase == "Created" || Orderresponse.Result.ReasonPhrase == "Created")
                    {
                        Console.WriteLine("Order Header Created");
                        tracingService.Trace(" Order Header Created in CRM");
                    Globalvariable = Globalvariable + "Order Header Created " + Environment.NewLine;
                }
                    else
                    {

                        var contents = Orderresponse.Result.Content.ReadAsStringAsync();
                        var err = contents.Result;
                        var lin = new JavaScriptSerializer().Deserialize<ErrorRoot>(err);
                        Errormsg = lin.error.message;
                        tracingService.Trace(" Order not Created in CRM");


                    throw new InvalidWorkflowException(Errormsg);
                    }


                    //-------- Post Sales Order Header in BC-----------//


                    //-------- Post Sales Order line in BC-----------//
                    for (int k = 0; k < i; k++)
                    {
                    tracingService.Trace(" Creating  Order lines in CRM");
                    string Token5 = TokenGenerator();

                        Lno = Lno + 10000;
                        decimal quantity = Convert.ToDecimal(orderline.Entities[k].Attributes["quantity"]);
                        quantity = Math.Round(quantity, 2);
                        Money UnitPrice = (Money)(orderline.Entities[k].Attributes["priceperunit"]);
                        decimal unitprice = Convert.ToDecimal(UnitPrice.Value);
                        unitprice = Math.Round(unitprice, 2);
                        var ProductNo = ((EntityReference)orderline.Entities[k].Attributes["productid"]).Id;
                        var Description = ((EntityReference)orderline.Entities[k].Attributes["productid"]).Name;
                        var UOM = ((EntityReference)orderline.Entities[k].Attributes["uomid"]).Name;



                        Entity product = _service.Retrieve("product", ProductNo, new ColumnSet(true));
                        var productnumber = product.Attributes["productnumber"].ToString();
                        PostOrderLine OrderLine = new PostOrderLine();

                        OrderLine.Document_Type = "Order";
                        OrderLine.Document_No = OrderNumber;
                        OrderLine.Line_No = Lno;
                        OrderLine.Type = "Item";
                        OrderLine.FilteredTypeField = "Item";
                        OrderLine.No = productnumber;
                        OrderLine.Item_Reference_No = "";
                        OrderLine.Description = Description;
                        OrderLine.Location_Code = "";
                        OrderLine.Quantity = quantity;


                        OrderLine.Unit_of_Measure_Code = UOM;
                        OrderLine.Unit_of_Measure = "Each";
                        OrderLine.Unit_Price = unitprice;
                        OrderLine.Tax_Liable = false;

                        string PostOrderLine = "https://api.businesscentral.dynamics.com/v2.0/edb43d17-548c-4b01-8258-27a8d7679cf2/Sandbox_QAS/ODataV4/Company('FluxErgy')/Sales_Order_Lines";
                        var client5 = new HttpClient();
                        client5.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token5);
                        client5.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                        var jsonobjectline = new JavaScriptSerializer().Serialize(OrderLine);

                        //var jsonObject = "";

                        var contentline = new StringContent(jsonobjectline.ToString(), Encoding.UTF8, "application/json");
                        client5.BaseAddress = new Uri(PostOrderLine);

                        var responseline = client5.PostAsync(PostOrderLine, contentline);


                        var resultline = client5.PostAsync(PostOrderLine, contentline).Result;

                        //var contents = responseline.Result.Content.ReadAsStringAsync();
                        //var ab = contents.Result;
                        //var lin = new JavaScriptSerializer().Deserialize<PostOrderLine>(ab);

                        if (resultline.ReasonPhrase == "Created" || responseline.Result.ReasonPhrase == "Created")
                        {
                            Console.WriteLine("Order Line Created");
                        tracingService.Trace(" Order line Created in CRM");
                        Globalvariable = "Order Line Created " + Environment.NewLine;
                    }
                        else
                        {
                            var contents = responseline.Result.Content.ReadAsStringAsync();
                            var cust = contents.Result;
                            var lin = new JavaScriptSerializer().Deserialize<ErrorRoot>(cust);
                            Errormsg = lin.error.message;
                        tracingService.Trace(" Order line not Created in CRM");

                        //Entity newNote = new Entity("annotation");
                        //newNote["objectid"] = new EntityReference("salesorder", SalesOrderHeaderId);
                        //newNote["objecttypecode"] = "salesorder";
                        //newNote["subject"] = "Error Message:";
                        //newNote["notetext"] = Errormsg;
                        //_service.Create(newNote);
                        throw new InvalidWorkflowException(Errormsg);
                        }



                    }
                try { 
                tracingService.Trace(" updating record status in CRM");
                salesorder["statecode"] = new OptionSetValue(1);
                salesorder["statuscode"] = new OptionSetValue(3);
                _service.Update(salesorder);
                }
                catch (Exception ex)
                {
                    tracingService.Trace(" Didn't update record status in CRM");
                }


                    //-------- Post Sales Order line in BC-----------//
            }
                catch (Exception ex)
                {

                Entity newNote = new Entity("annotation");
                newNote["objectid"] = new EntityReference("salesorder", SalesOrderHeaderId);
                newNote["objecttypecode"] = "salesorder";
                newNote["subject"] = "Error Message:";
                newNote["notetext"] = Globalvariable + " " + ex.ToString();
                _service.Create(newNote);

                throw new InvalidWorkflowException(ex.ToString()); 
                }


                //tracingService.Trace("GET PRIMARY ID");
                //EntityCollection notes = RetrieveAnnotations("evs_accountapplication", accountId, _service);
                //Guid emailId = Guid.Empty;
                //if (notes.Entities.Count > 0)
                //{
                //    List<EmailData> listEmailData = new List<EmailData>();
                //    foreach (Entity note in notes.Entities)
                //    {
                //        bool isDocument = Convert.ToBoolean(note["isdocument"]);

                //        if (isDocument)
                //        {
                //            EmailData emailData = new EmailData();
                //            // Should check these attributes exist in Note
                //            string documentBody = note["documentbody"].ToString();
                //            string mimeType = note["mimetype"].ToString();
                //            string fileName = note["filename"].ToString();

                //            emailData.documentBody = documentBody;
                //            emailData.mimeType = mimeType;
                //            emailData.fileName = fileName;

                //            listEmailData.Add(emailData);

                //        }


                //    }

                //    // Need to get information to generate email message                         
                //    Entity From = new Entity("activityparty");
                //    From["partyid"] = new EntityReference("systemuser", userId);
                //    Entity TOContact1 = new Entity("activityparty");
                //    TOContact1["partyid"] = new EntityReference("evs_accountapplication", accountId);
                //    string Emsubject = "New Customer Requests";
                //    String des = String.Format("Thank you for showing interest in our brands. We expect to make this process as simple as possible. To start, please sign and return the attached completed forms to ");
                //    des += "<a href='mailto: accountsreceivable@kalorik.com' target='_blank'>accountsreceivable@kalorik.com</a>";
                //    des += ".<br/><br/>" + System.Environment.NewLine + "\n We look forward to supporting your business needs.<br/><br/><br/>";
                //    des = des + "<html> <img  src='https://cdn.shopify.com/s/files/1/0093/2537/9669/files/logo-black_dc313de1-2b03-49f3-b942-a593387f3cbb_260x.png?v=1566363211'/></html>";


                //    tracingService.Trace("Description" + des);

                //    emailId = CreateEmailMessage(From, TOContact1, Emsubject, des, _service);

                //    AddAttachmentsToListEmailData(emailId, listEmailData, _service, tracingService);

                //    SendEmail(emailId, _service);
                //    tracingService.Trace("Email Sent");

                //}


            
        }



       
        public static string TokenGenerator()
        {
            string AccessToken = null;
            var serializer = new JavaScriptSerializer();
            string endPoint = "https://login.microsoftonline.com/edb43d17-548c-4b01-8258-27a8d7679cf2/oauth2/v2.0/token";
            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("Accept", "application/x-www-form-urlencoded");

            var data = new[]
            {
                         new KeyValuePair<string, string>("grant_type", "client_credentials"),
                         new KeyValuePair<string, string>("client_id", "51c2a387-93fe-4499-a298-7f312bb7620c"),
                         new KeyValuePair<string, string>("client_secret", "~Cx8Q~Kgbxz4mhD_hWsc717F5F_uk5Z7urkpHat5"),
                         new KeyValuePair<string, string>("scope", "https://api.businesscentral.dynamics.com/.default"),
                };
            var response = client.PostAsync(endPoint, new FormUrlEncodedContent(data)).GetAwaiter().GetResult();
            var contents = response.Content.ReadAsStringAsync();
            var re = contents.Result;
            var result = new JavaScriptSerializer().Deserialize<token>(re);
            AccessToken = result.access_token;
            return AccessToken;
        }


        public static string TokenGeneratorCRM()
        {
            string AccessToken = null;
            var serializer = new JavaScriptSerializer();
            string endPointCRM = "https://login.microsoftonline.com/edb43d17-548c-4b01-8258-27a8d7679cf2/oauth2/v2.0/token";
            var client6 = new HttpClient();
            client6.DefaultRequestHeaders.Add("Accept", "application/x-www-form-urlencoded");

            var data = new[]
            {
                         new KeyValuePair<string, string>("grant_type", "client_credentials"),
                         new KeyValuePair<string, string>("client_id", "bdafe33a-5d44-4feb-9a40-554b6fb5fd53"),
                         new KeyValuePair<string, string>("client_secret", "b7n8Q~6GRJO-4uE2qC.w6w8CiWWubL.UqOtXPduI"),
                         new KeyValuePair<string, string>("scope", "https://fluxergysandbox.crm.dynamics.com/.default"),
                };
            var crmresponse = client6.PostAsync(endPointCRM, new FormUrlEncodedContent(data)).GetAwaiter().GetResult();
            var contentscrm = crmresponse.Content.ReadAsStringAsync();
            var tokenNo = contentscrm.Result;
            var result = new JavaScriptSerializer().Deserialize<token>(tokenNo);
            AccessToken = result.access_token;
            return AccessToken;
        }

    }
}
public static class HttpClientExtensions
{
    public static async Task<HttpResponseMessage> PatchAsync(this HttpClient client, Uri requestUri, HttpContent iContent)
    {
        var method = new HttpMethod("PATCH");
        var request = new HttpRequestMessage(method, requestUri)
        {
            Content = iContent
        };

        HttpResponseMessage response = new HttpResponseMessage();
        try
        {
            response = await client.SendAsync(request);
        }
        catch (TaskCanceledException e)
        {
            Console.WriteLine("ERROR: " + e.ToString());
        }

        return response;
    }
}


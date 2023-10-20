using PayPal.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace B2B.PayPalPayment
{
    public class PaymentPrepare
    {
        /// <summary>
        /// Paymentdetails
        /// </summary>
        public PaymentDetails PaymentDetails = null;

        /// <summary>
        /// Transaktion Beschreibung
        /// </summary>
        public string Description = string.Empty;

        /// <summary>
        /// Cancel URL
        /// </summary>
        public string UrlCancel = string.Empty;

        /// <summary>
        /// Return URL
        /// </summary>
        public string UrlReturn = string.Empty;

        public PayPal.Api.Payment CreatePayment()
        {
            //string intent = "sale";

            // ### Api Context
            // Pass in a `APIContext` object to authenticate 
            // the call and to send a unique request id 
            // (that ensures idempotency). The SDK generates
            // a request id if you do not pass one explicitly. 
            var apiContext = PaymentConfiguration.GetAPIContext();

            // Payment Resource
            var payment = new PayPal.Api.Payment()
            {
                intent = "sale",    // `sale` or `authorize`
                payer = new Payer() { payment_method = "paypal" },
                transactions = GetTransactionsList(),
                redirect_urls = GetReturnUrls(),
            };

            // Create a payment using a valid APIContext
            var createdPayment = payment.Create(apiContext);

            return createdPayment;
        }

        private List<Transaction> GetTransactionsList()
        {
            // A transaction defines the contract of a payment
            // what is the payment for and who is fulfilling it. 
            var transactionList = new List<Transaction>();

            // The Payment creation API requires a list of Transaction; 
            // add the created Transaction to a List
            transactionList.Add(new Transaction()
            {
                description = Description,
                invoice_number = PaymentDetails.GetString("InvoiceNumber"),
                amount = new Amount()
                {
                    currency = "EUR",
                    total = PaymentDetails.GetString("Total"),
                    details = new Details()     // Details: Let's you specify details of a payment amount.
                    {
                        tax = "0",
                        shipping = "0",
                        subtotal = PaymentDetails.GetString("Total")
                    }
                },
                item_list = new ItemList()
                {
                    items = new List<Item>()
            {
                new Item()
                {
                    name = PaymentDetails.GetString("ItemName"),
                    description= PaymentDetails.GetString("ItemDescription"),
                    currency = "EUR",
                    price = PaymentDetails.GetString("Total"),
                    quantity = PaymentDetails.GetString("Quantity"),
                    sku = PaymentDetails.GetString("ArticleNumber")
                }
            }
                }
            });
            return transactionList;
        }

        private RedirectUrls GetReturnUrls()
        {
            // Redirect URLS
            // These URLs will determine how the user is redirected from PayPal 
            // once they have either approved or canceled the payment.
            return new RedirectUrls()
            {
                cancel_url = UrlCancel,
                return_url = UrlReturn
            };
        }
    }
}
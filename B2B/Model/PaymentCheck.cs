using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace B2B.Model
{
    public class PaymentCheck
    {
        private Payment payment = null;

        public PaymentCheck(Payment payment)
        {
            this.payment = payment;
        }

        public int Id
        {
            get
            {
                return payment == null ? 0 : payment.Id;
            }
        }

        public int OrderId
        {
            get
            {
                return payment == null ? 0 : payment.OrderId ?? 0;
            }
        }
        public string DateOfPay
        {
            get
            {
                return payment == null ? "" : payment.DateOfPay?.ToString("dd/MM/yyyy HH.mm");
            }
        }


        public double Amount
        {
            get
            {
                return payment == null ? 0 : (payment.Amount ?? 0);
            }
        }
        public string PaypalTransitionID
        {
            get
            {
                return payment == null ? "" : payment.PaypalTransitionID;
            }
        }

        public int Method
        {
            get
            {
                return payment?.Method ?? 0;
            }
        }
    }
}
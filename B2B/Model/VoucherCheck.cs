using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace B2B.Model
{
    public class VoucherCheck
    {
        private Voucher voucher = null;

        public VoucherCheck(Voucher voucher) 
        {
            this.voucher = voucher;
        }

        public int Id
        {
            get
            {
                return voucher == null ? 0 : voucher.Id;
            }
        }

        public double Amount
        {
            get
            {
                return voucher == null ? 0 : voucher.Amount ?? 0;
            }
        }
        public string SerialNumberGenerator
        {
            get
            {
                return voucher == null ? "" : voucher.SerialNumberGenerator;
            }
        }

        public string Note
        {
            get
            {
                return voucher == null ? "" : voucher.Note;
            }
        }

        public string Summary
        {
            get; set;
        }

        public string Owner
        {
            get; set;
        }
    }
}
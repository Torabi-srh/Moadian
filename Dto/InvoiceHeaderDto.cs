namespace Moadian.Dto
{
    public class InvoiceHeaderDto : PrimitiveDto
    {
        /**
         * unique tax ID (should be generated using InvoiceIdService)
         */
        public string taxid { get; set; }

        /**
         * invoice timestamp (milliseconds from epoch)
         */
        public long indatim { get; set; }

        /**
         * invoice creation timestamp (milliseconds from epoch)
         */
        public long indati2m { get; set; }

        /**
         * invoice type
         */
        public int inty { get; set; }

        /**
         * internal invoice number
         */
        public string inno { get; set; }

        /**
         * invoice reference tax ID
         */
        public string irtaxid { get; set; }

        /**
         * invoice pattern
         */
        public int inp { get; set; }

        /**
         * invoice subject
         */
        public int ins { get; set; }

        /**
         * seller tax identification number
         */
        public string tins { get; set; }

        /**
         * type of buyer
         */
        public int? tob { get; set; }

        /**
         * buyer ID
         */
        public string bid { get; set; }

        /**
         * buyer tax identification number
         */
        public string tinb { get; set; }

        /**
         * seller branch code
         */
        public string sbc { get; set; }

        /**
         * buyer postal code
         */
        public string bpc { get; set; }

        /**
         * buyer branch code
         */
        public string bbc { get; set; }

        /**
         * flight type
         */
        public int? ft { get; set; }

        /**
         * buyer passport number
         */
        public string bpn { get; set; }

        /**
         * seller customs licence number
         */
        public int? scln { get; set; }

        /**
         * seller customs code
         */
        public string scc { get; set; }

        /**
         * contract registration number
         */
        public int? crn { get; set; }

        /**
         * billing ID
         */
        public string billid { get; set; }

        /**
         * total pre discount
         */
        public int tprdis { get; set; }

        /**
         * total discount
         */
        public int tdis { get; set; }

        /**
         * total after discount
         */
        public int tadis { get; set; }

        /**
         * total VAT amount
         */
        public int tvam { get; set; }

        /**
         * total other duty amount
         */
        public int todam { get; set; }

        /**
         * total bill
         */
        public int tbill { get; set; }

        /**
         * settlement type
         */
        public int? setm { get; set; }

        /**
         * cash payment
         */
        public int? cap { get; set; }

        /**
         * installment payment
         */
        public int? insp { get; set; }

        /**
         * total VAT of payment
         */
        public int tvop { get; set; }

        /**
         * tax17
         */
        public int tax17 { get; set; }
    }
}

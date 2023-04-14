using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moadian.Dto
{
    public class InvoiceBodyDto : PrimitiveDto
    {
        // service stuff ID
        public string? sstid { get; set; }
        // service stuff title
        public string sstt { get; set; }

        // amount
        public int am { get; set; }

        // measurement unit
        public string mu { get; set; }

        // fee (pure price per item)
        public int fee { get; set; }

        // fee in foreign currency
        public float? cfee { get; set; }

        // currency type
        public string? cut { get; set; }

        // exchange rate
        public int? exr { get; set; }

        // pre discount
        public int prdis { get; set; }

        // discount
        public int dis { get; set; }

        // after discount
        public int adis { get; set; }

        // VAT rate
        public int vra { get; set; }

        // VAT amount
        public int vam { get; set; }

        // over duty title
        public string? odt { get; set; }

        // over duty rate
        public float? odr { get; set; }

        // over duty amount
        public int? odam { get; set; }

        // other legal title
        public string? olt { get; set; }

        // other legal rate
        public float? olr { get; set; }

        // other legal amount
        public int? olam { get; set; }

        // construction fee
        public int? consfee { get; set; }

        // seller profit
        public int? spro { get; set; }

        // broker salary
        public int? bros { get; set; }

        // total construction profit broker salary
        public int? tcpbs { get; set; }

        // cash share of payment
        public int? cop { get; set; }

        // vat of payment
        public string? vop { get; set; }

        // buyer register number
        public string? bsrn { get; set; }

        // total service stuff amount
        public int tsstam { get; set; }
    }
}
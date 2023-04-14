using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moadian.Dto
{
    public class InvoicePaymentDto : PrimitiveDto
    {
        private string? iinn;
        private string? acn;
        private string? trmn;
        private string? trn;
        private string? pcn;
        private string? pid;
        private int? pmt;
        private int? pdt;

        public string? Iinn
        {
            get { return iinn; }
            set { iinn = value; }
        }

        public string? Acn
        {
            get { return acn; }
            set { acn = value; }
        }

        public string? Trmn
        {
            get { return trmn; }
            set { trmn = value; }
        }

        public string? Trn
        {
            get { return trn; }
            set { trn = value; }
        }

        public string? Pcn
        {
            get { return pcn; }
            set { pcn = value; }
        }

        public string? Pid
        {
            get { return pid; }
            set { pid = value; }
        }

        public int? Pmt
        {
            get { return pmt; }
            set { pmt = value; }
        }

        public int? Pdt
        {
            get { return pdt; }
            set { pdt = value; }
        }
    }
}

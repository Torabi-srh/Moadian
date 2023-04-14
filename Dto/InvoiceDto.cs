using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
    
namespace Moadian.Dto
{
    public class InvoiceDto : PrimitiveDto
    {
        public InvoiceHeaderDto header { get; set; }
        public List<InvoiceBodyDto> body { get; set; }
        public List<InvoicePaymentDto> payments { get; set; }
         
    }
}

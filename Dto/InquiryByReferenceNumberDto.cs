using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moadian.Dto
{
    public class InquiryByReferenceNumberDto : PrimitiveDto
    {
        private string[] referenceNumber;

        public void SetReferenceNumber(string referenceNumber)
        {
            this.referenceNumber = new string[] { referenceNumber };
        }

        public string[] GetReferenceNumber()
        {
            return referenceNumber;
        }
    }
}

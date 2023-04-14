using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moadian.Services
{
    public class InvoiceIdService
    {
        private static readonly Dictionary<char, int> CHARACTER_TO_NUMBER_CODING = new Dictionary<char, int>()
    {
        {'A', 65}, {'B', 66}, {'C', 67}, {'D', 68}, {'E', 69}, {'F', 70}, {'G', 71}, {'H', 72}, {'I', 73},
        {'J', 74}, {'K', 75}, {'L', 76}, {'M', 77}, {'N', 78}, {'O', 79}, {'P', 80}, {'Q', 81}, {'R', 82},
        {'S', 83}, {'T', 84}, {'U', 85}, {'V', 86}, {'W', 87}, {'X', 88}, {'Y', 89}, {'Z', 90},
    };

        private readonly string clientId;

        public InvoiceIdService(string clientId)
        {
            this.clientId = clientId;
        }

        public string GenerateInvoiceId(DateTime date, int internalInvoiceId)
        {
            int daysPastEpoch = GetDaysPastEpoch(date);
            string daysPastEpochPadded = daysPastEpoch.ToString().PadLeft(6, '0');
            string hexDaysPastEpochPadded = daysPastEpoch.ToString("X").PadLeft(5, '0');

            string numericClientId = ClientIdToNumber(clientId);

            string internalInvoiceIdPadded = internalInvoiceId.ToString().PadLeft(12, '0');
            string hexInternalInvoiceIdPadded = internalInvoiceId.ToString("X").PadLeft(10, '0');

            string decimalInvoiceId = numericClientId + daysPastEpochPadded + internalInvoiceIdPadded;

            int checksum = VerhoeffService.CheckSum(decimalInvoiceId);

            return (clientId + hexDaysPastEpochPadded + hexInternalInvoiceIdPadded + checksum.ToString()).ToUpper();
        }

        private int GetDaysPastEpoch(DateTime date)
        {
            return (int)date.Subtract(new DateTime(1970, 1, 1)).TotalSeconds / (3600 * 24);
        }

        private string ClientIdToNumber(string clientId)
        {
            string result = "";
            foreach (char c in clientId)
            {
                if (char.IsDigit(c))
                {
                    result += c;
                }
                else
                {
                    result += CHARACTER_TO_NUMBER_CODING[c];
                }
            }
            return result;
        }
    }
}

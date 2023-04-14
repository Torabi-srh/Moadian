using Moadian.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moadian.Services
{
    public static class SimpleNormalizer
    {
        public static string Normalize(string data, Dictionary<string, string> headers)
        {
            if (headers == null || headers.Count == 0)
            {
                return data;
            }

            if (headers.ContainsKey(TransferConstants.AUTHORIZATION_HEADER))
            {
                data += headers[TransferConstants.AUTHORIZATION_HEADER];
            }

            if (headers.ContainsKey(TransferConstants.REQUEST_TRACE_ID_HEADER))
            {
                data += headers[TransferConstants.REQUEST_TRACE_ID_HEADER];
            }

            if (headers.ContainsKey(TransferConstants.TIMESTAMP_HEADER))
            {
                data += headers[TransferConstants.TIMESTAMP_HEADER];
            }

            return data;
        }
    }
}

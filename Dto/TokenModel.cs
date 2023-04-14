using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moadian.Dto
{
    public class TokenModel
    {
        public TokenModel(string token, int expiresAt)
        {
            this.Token = token;
            this.ExpiresAt = expiresAt;
        }

        public string Token { get; }

        public int ExpiresAt { get; }

        public bool IsExpired()
        {
            return (int)(DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalMilliseconds >= (this.ExpiresAt - 100 * 1000);
        }
    }
}

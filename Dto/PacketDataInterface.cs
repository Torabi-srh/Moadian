using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moadian.Dto
{
    public interface PacketDataInterface
    {
        public Dictionary<string, object> ToArray(); 
    }
}

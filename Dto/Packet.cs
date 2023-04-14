using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moadian.Dto
{
    using System;

    public class Packet : PrimitiveDto
    {
        public string uid { get; set; } = "";
        public string packetType { get; set; } = "";
        public bool retry { get; set; } = false;
        public object data { get; set; } = null;
        public string encryptionKeyId { get; set; } = null;
        public string symmetricKey { get; set; } = null;
        public string iv { get; set; } = null;
        public string fiscalId { get; set; } = "";
        public string dataSignature { get; set; } = null;
        public string signatureKeyId { get; set; } = null;

        public Packet(string packetType, object data = null)
        {
            this.packetType = packetType;
            this.data = data;
            this.uid = Guid.NewGuid().ToString();
        }

        //public object ToArray()
        //{
        //    if (this.signatureKeyId != null)
        //    {
        //        var array = new
        //        {
        //            uid = this.uid,
        //            packetType = this.packetType,
        //            retry = this.retry,
        //            data = this.data is string ? this.data : ((PacketDataInterface)this.data)?.ToArray(),
        //            encryptionKeyId = this.encryptionKeyId,
        //            symmetricKey = this.symmetricKey,
        //            iv = this.iv,
        //            fiscalId = this.fiscalId,
        //            dataSignature = this.dataSignature,
        //            signatureKeyId = this.signatureKeyId
        //        };
        //        return array;
        //    }
        //    else
        //    {
        //        var array = new
        //        {
        //            uid = this.uid,
        //            packetType = this.packetType,
        //            retry = this.retry,
        //            data = this.data is string ? this.data : ((PacketDataInterface)this.data)?.ToArray(),
        //            encryptionKeyId = this.encryptionKeyId,
        //            symmetricKey = this.symmetricKey,
        //            iv = this.iv,
        //            fiscalId = this.fiscalId,
        //            dataSignature = this.dataSignature,
        //        };

        //        return array;
        //    }
        //}
    }

}

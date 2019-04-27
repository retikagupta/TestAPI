using System;
using System.Collections.Generic;

namespace TestAPI.Models
{
    public partial class PubInfo
    {
        public string PubId { get; set; }
        public byte[] Logo { get; set; }
        public string PrInfo { get; set; }

        public virtual Publishers Pub { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace DocumentClient.Domain
{
    public class DBSetting
    {
        public string EndPointURL { get; set; }

        public string Key { get; set; }

        public string DatabaseId { get; set; }

        public string CollectionId { get; set; }
    }
}

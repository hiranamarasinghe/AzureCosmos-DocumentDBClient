using System;
using System.Collections.Generic;
using System.Text;

namespace DocumentClient.Services.Shared
{
    public class BaseDto
    {
        public string Id { get; set; }

        public string ETag { get; set; }

        public string LastUpdatedByUserId { get; set; }

        public DateTime LastUpdatedOn { get; set; }
    }
}

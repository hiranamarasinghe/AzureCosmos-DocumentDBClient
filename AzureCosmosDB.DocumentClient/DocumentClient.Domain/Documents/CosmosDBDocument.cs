using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace DocumentClientDemo.Domain.Documents
{
    public abstract class CosmosDBDocument
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("createdDate")]
        public DateTime CreatedDate { get; set; }

        [JsonProperty("createdByUserId")]
        public string CreatedByUserId { get; set; }

        [JsonProperty("updatedByUserId")]
        public string UpdatedByUserId { get; set; }

        [JsonProperty("partitionKey")]
        public string PartitionKey { get; set; }

        [JsonProperty("updatedOn")]
        public DateTime UpdatedOn { get; set; }

        [JsonProperty("docType")]
        public abstract string DocType { get; }

        [JsonProperty("customerId")]
        public string CustomerId { get; set; }

        [JsonProperty("eTag")]
        public string ETag { get; set; }
    }
}

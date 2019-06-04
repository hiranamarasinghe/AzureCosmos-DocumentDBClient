using DocumentClient.Domain.Constants;
using DocumentClientDemo.Domain.Enums;
using Newtonsoft.Json;

namespace DocumentClientDemo.Domain.Documents
{
    public class User : CosmosDBDocument
    {
        [JsonProperty("firstName")]
        public string FirstName { get; set; }

        [JsonProperty("lastName")]
        public string LastName { get; set; }

        [JsonProperty("gender")]
        public Gender Gender { get; set; }

        [JsonProperty("address")]
        public Address Address { get; set; }

        [JsonProperty("mobile")]
        public string Mobile { get; set; }

        [JsonProperty("nic")]
        public string NIC { get; set; }

        public override string DocType => DocumentTypes.User;
    }

    public class Address
    {
        [JsonProperty("streetName1")]
        public string StreetName1 { get; set; }

        [JsonProperty("streetName2")]
        public string StreetName2 { get; set; }

        [JsonProperty("province")]
        public string Province { get; set; }

    }
}

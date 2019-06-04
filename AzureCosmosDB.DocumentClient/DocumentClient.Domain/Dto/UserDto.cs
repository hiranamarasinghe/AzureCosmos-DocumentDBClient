using DocumentClient.Services.Shared;
using DocumentClientDemo.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace DocumentClientDemo.Domain.Dto
{
    public class UserDto : BaseDto
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public Gender Gender { get; set; }

        public string CustomerId { get; set; }

        public AddressDto Address { get; set; }

        public string Mobile { get; set; }

        public string NIC { get; set; }

    }

    public class AddressDto
    {
        public string StreetName1 { get; set; }

        public string StreetName2 { get; set; }

        public string Province { get; set; }
    }
}

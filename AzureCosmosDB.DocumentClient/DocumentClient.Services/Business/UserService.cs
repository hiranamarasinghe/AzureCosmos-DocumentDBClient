using DocumentClient.Domain.Contracts.Shared;
using DocumentClient.Domain.Dto;
using DocumentClientDemo.Domain.Contracts.Business;
using DocumentClientDemo.Domain.Documents;
using DocumentClientDemo.Domain.Dto;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentClientDemo.Services.Business
{
    public class UserService : IUserService
    {
        private IDocumentDBService _cosmosDBService;
        public UserService(IDocumentDBService cosmosDBService)
        {
            _cosmosDBService = cosmosDBService;
        }

        public async Task<UserDto> GetAsync(string id)
        {
            var result = await _cosmosDBService.GetDocumentAsync<User>(id);
            var userDto = new UserDto()
            {
                Id = result.Id,
                FirstName = result.FirstName,
                LastName = result.LastName,
                Address = new AddressDto()
                {
                    StreetName1 = result.Address?.StreetName1,
                    Province = result.Address?.Province,
                    StreetName2 = result.Address?.StreetName2
                },
                Gender = result.Gender,
                NIC = result.NIC,
                Mobile = result.Mobile,
                ETag = result.ETag,
                CustomerId = result.CustomerId
            };

            return userDto;
        }

        public async Task<List<UserDto>> GetUsersAsync(string name)
        {
            var usersFromDb = await _cosmosDBService.GetDocumentsAsync<User, UserDto>(q => q.Where(u => u.FirstName == name).Select(u => new UserDto()
            {
                FirstName =u.FirstName
            }));

            return usersFromDb;
        }

        public async Task<DocumentDBResultDto> CreateAsync(UserDto userDto)
        {
            var user = CreateUser(userDto);
            var result = await _cosmosDBService.CreateDocument(user);
            return result;
        }

        public async Task<DocumentDBResultDto> UpdateAsync(UserDto userDto)
        {
            var userFromDb = await _cosmosDBService.GetDocumentAsync<User>(userDto.Id);
            if (userFromDb != null)
            {
                var user = CreateUser(userDto, userFromDb);
                var result = await _cosmosDBService.UpdateDocumentAsync(user);
                return result;
            }
            return null;
        }

        public async Task DeleteAsync(string id)
        {
            await _cosmosDBService.DeleteDocument(id);
        }

        private User CreateUser(UserDto userDto, User user = null)
        {
            if (user == null)
            {
                user = new User();
            }
            user.FirstName = userDto.FirstName;
            user.LastName = userDto.LastName;
            user.PartitionKey = "*";
            user.Address = new Address()
            {
                StreetName1 = userDto.Address.StreetName1,
                StreetName2 = userDto.Address.StreetName2,
                Province = userDto.Address.Province
            };
            user.Gender = userDto.Gender;
            user.Mobile = userDto.Mobile;
            return user;
        }
    }
}

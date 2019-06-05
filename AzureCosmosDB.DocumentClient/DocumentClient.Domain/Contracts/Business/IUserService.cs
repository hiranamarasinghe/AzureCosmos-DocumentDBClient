using DocumentClient.Domain.Dto;
using DocumentClientDemo.Domain.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DocumentClientDemo.Domain.Contracts.Business
{
    public interface IUserService
    {
        Task<UserDto> GetAsync(string id);

        Task<List<UserDto>> GetUsersAsync(string name);

        Task<DocumentDBResultDto> CreateAsync(UserDto user);

        Task<DocumentDBResultDto> UpdateAsync(UserDto user);

        Task DeleteAsync(string id);
    }
}

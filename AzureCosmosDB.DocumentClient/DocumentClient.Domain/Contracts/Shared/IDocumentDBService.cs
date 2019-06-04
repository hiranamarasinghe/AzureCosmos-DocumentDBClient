using DocumentClient.Domain.Dto;
using DocumentClientDemo.Domain.Documents;
using System.Threading.Tasks;

namespace DocumentClient.Domain.Contracts.Shared
{
    public interface IDocumentDBService
    {
        Task<T> GetDocumentAsync<T>(string id);

        Task<DocumentDBResultDto> UpdateDocumentAsync(CosmosDBDocument document);

        Task<DocumentDBResultDto> CreateDocument(CosmosDBDocument document);

        Task DeleteDocument(string id);
    }
}

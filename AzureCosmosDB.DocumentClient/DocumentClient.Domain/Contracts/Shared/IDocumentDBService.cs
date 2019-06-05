using DocumentClient.Domain.Dto;
using DocumentClientDemo.Domain.Documents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentClient.Domain.Contracts.Shared
{
    public interface IDocumentDBService
    {
        Task<T> GetDocumentAsync<T>(string id);

        Task<List<TResult>> GetDocumentsAsync<T, TResult>(Func<IQueryable<T>, IQueryable<TResult>> query);

        Task<DocumentDBResultDto> UpdateDocumentAsync(CosmosDBDocument document);

        Task<DocumentDBResultDto> CreateDocument(CosmosDBDocument document);

        Task DeleteDocument(string id);
    }
}

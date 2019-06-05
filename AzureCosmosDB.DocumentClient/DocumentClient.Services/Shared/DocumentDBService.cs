using DocumentClient.Domain;
using DocumentClient.Domain.Contracts.Shared;
using DocumentClient.Domain.Dto;
using DocumentClientDemo.Domain.Documents;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Microsoft.Azure.Documents.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentClientDemo.Services.Shared
{
    public class DocumentDBService : IDocumentDBService, IDisposable
    {
        private DBSetting _dbSetting;
        private Microsoft.Azure.Documents.Client.DocumentClient _cosmosDBClient;

        public DocumentDBService(DBSetting dbSetting)
        {
            _dbSetting = dbSetting;
            _cosmosDBClient = new Microsoft.Azure.Documents.Client.DocumentClient(new Uri(_dbSetting.EndPointURL), _dbSetting.Key);
        }

        public async Task<T> GetDocumentAsync<T>(string id)
        {
            try
            {
                var result = await _cosmosDBClient.ReadDocumentAsync<T>(UriFactory.CreateDocumentUri(_dbSetting.DatabaseId, _dbSetting.CollectionId, id),
                    CreateRequestOptions("*"));
                return result.Document;
            }
            catch (DocumentClientException e)
            {
                if (e.Error.Code == "NotFound")
                {
                    return default(T);
                }
                else
                {
                    throw;
                }
            }
        }

        public async Task<DocumentDBResultDto> CreateDocument(CosmosDBDocument document)
        {
            var result = await _cosmosDBClient.CreateDocumentAsync(UriFactory.CreateDocumentCollectionUri(_dbSetting.DatabaseId, _dbSetting.CollectionId),
                document, CreateRequestOptions("*"));
            return new DocumentDBResultDto()
            {
                Id = result.Resource.Id,
                ETag = result.Resource.ETag
            };
        }

        public async Task DeleteDocument(string id)
        {
            await _cosmosDBClient.DeleteDocumentAsync(UriFactory.CreateDocumentUri(_dbSetting.DatabaseId, _dbSetting.CollectionId, id), CreateRequestOptions("*"));
        }

        public async Task<DocumentDBResultDto> UpdateDocumentAsync(CosmosDBDocument document)
        {
            var result = await _cosmosDBClient.ReplaceDocumentAsync(UriFactory.CreateDocumentUri(_dbSetting.DatabaseId, _dbSetting.CollectionId, document.Id), document, CreateRequestOptions("*"));
            return new DocumentDBResultDto()
            {
                Id = result.Resource.Id,
                ETag = result.Resource.ETag
            };
        }

        public async Task<List<TResult>> GetDocumentsAsync<T, TResult>(Func<IQueryable<T>, IQueryable<TResult>> query)
        {
            var feedOptions = new FeedOptions
            {
                PartitionKey = new PartitionKey("*")
            };
            var collectionUri = UriFactory.CreateDocumentCollectionUri(_dbSetting.DatabaseId, _dbSetting.CollectionId);
            var q = _cosmosDBClient.CreateDocumentQuery<T>(collectionUri, feedOptions);
            var docQuery = query(q).AsDocumentQuery();

            var results = new List<TResult>();
            FeedResponse<TResult> response = null;

            do
            {
                response = await docQuery.ExecuteNextAsync<TResult>();
                results.AddRange(response.ToList());

            }
            while (docQuery.HasMoreResults);
            return results;
        }

        public void Dispose()
        {
            _cosmosDBClient.Dispose();
        }

        private RequestOptions CreateRequestOptions(string partitionKey)
        {
            return new RequestOptions
            {
                PartitionKey = new PartitionKey("*")
            };
        }
    }
}

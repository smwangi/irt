
using Irt.Core.Datasources;
using Irt.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace Irt.Infrastructure.Domain.Datasources
{
    public class DatasourceRepository(IrtDbContext context) : IDatasourceRepository
    {
        private IMongoCollection<Datasource> _datasources = context.GetCollection<Datasource>("Datasources");

        public async Task<Datasource> AddAsync(Datasource datasource, CancellationToken cancellationToken)
        {
            //_datasources.DeleteMany(FilterDefinition<Datasource>.Empty);
            
            await _datasources.InsertOneAsync(datasource, cancellationToken: CancellationToken.None);
            //await context.Datasources.AddAsync(datasource);

            return datasource;
        }

        /*public async Task<bool> ExistsByNameAsync(DatasourceName name, CancellationToken cancellationToken)
        {
           var filter = Builders<Datasource>.Filter.Eq(d => d.Name, name);
            bool resp = await _datasources
                .Find(filter)
                .AnyAsync();
            return resp;
             /*return await context.Datasources.AnyAsync(d => d.Name == name);*/
       /* }*/

        public async Task<Datasource> GetByIdAsync(DatasourceId datasourceId, CancellationToken cancellationToken)
        {
            var filter = Builders<Datasource>.Filter.Eq(d => d.Id, datasourceId);
            return await _datasources.Find(filter).FirstOrDefaultAsync();
        }

        public async Task<List<Datasource>> GetAllAsync(CancellationToken cancellationToken)
        {
            //_datasources.DeleteMany(FilterDefinition<Datasource>.Empty);
            var filter = Builders<Datasource>.Filter.Empty;
            return await _datasources
                .Find(filter)
                .ToListAsync();

            /*return await context.Datasources
                .Where(p => p.Name.Value != null)
                .ToListAsync();*/
        }

        public async Task<bool> DeleteAsync(Datasource datasource, CancellationToken cancellationToken)
        {
            var response = await _datasources
                .DeleteOneAsync(d => d.Id == datasource.Id);
            
            return response.DeletedCount > 0;
            /*var response =  context.Datasources.Remove(datasource);
            await _unitOfWork.CommitAsync();
            return response.State == EntityState.Deleted;*/
        }

        public async Task<bool> UpdateAsync(Datasource datasource, CancellationToken cancellationToken)
        {
            var response = await _datasources
                .ReplaceOneAsync(d => d.Id == datasource.Id, datasource);

            return response.ModifiedCount > 0;
            /*var response = context.Datasources.Update(datasource);
            await _unitOfWork.CommitAsync();
            return response.State == EntityState.Modified;*/
        }
    }
}
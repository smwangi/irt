
using Irt.Core.Datasources;
namespace Irt.Infrastructure.Domain.Datasources
{
    public class DatasourceRepository : IDatasourceRepository
    {
        public Task<Datasource> AddAsync(Datasource datasource, CancellationToken cancellationToken)
        {
            //_datasources.DeleteMany(FilterDefinition<Datasource>.Empty);
            
            //await _datasources.InsertOneAsync(datasource, cancellationToken: CancellationToken.None);
            //await context.Datasources.AddAsync(datasource);

            return null;
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

        public  Task<Datasource?> GetByIdAsync(DatasourceId datasourceId, CancellationToken cancellationToken)
        {
            return null;
        }

        public async Task<List<Datasource>> GetAllAsync(CancellationToken cancellationToken)
        {
            //_datasources.DeleteMany(FilterDefinition<Datasource>.Empty);
            return null;

            /*return await context.Datasources
                .Where(p => p.Name.Value != null)
                .ToListAsync();*/
        }

        public  Task<bool> DeleteAsync(Datasource datasource, CancellationToken cancellationToken)
        {
            return null;
            /*var response =  context.Datasources.Remove(datasource);
            await _unitOfWork.CommitAsync();
            return response.State == EntityState.Deleted;*/
        }

        public  Task<bool> UpdateAsync(Datasource datasource, CancellationToken cancellationToken)
        {
            return null;
            /*var response = context.Datasources.Update(datasource);
            await _unitOfWork.CommitAsync();
            return response.State == EntityState.Modified;*/
        }
    }
}
using Irt.Core.SeedWork;
using Irt.Core.ValueObjects;

namespace Irt.Core.Datasources
{
    public class DatasourceNameMustBeUniqueRule(IDatasourceUniqueChecker datasourceUniqueChecker, DatasourceName name) 
    {
        private readonly IDatasourceUniqueChecker _datasourceUniqueChecker = datasourceUniqueChecker;
        private readonly DatasourceName _name = name;

        public async Task<bool> IsBroken() => await _datasourceUniqueChecker.IsDatasourceNameUnique(_name);

        public string Message => "Datasource with this name already exists";
    }
}
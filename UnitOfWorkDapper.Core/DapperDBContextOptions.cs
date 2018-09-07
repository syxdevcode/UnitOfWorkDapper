using Microsoft.Extensions.Options;

namespace UnitOfWorkDapper.Core
{
    public class DapperDBContextOptions : IOptions<DapperDBContextOptions>
    {
        public string Configuration { get; set; }

        //DapperDBContextOptions IOptions<DapperDBContextOptions>.Value
        //{
        //    get { return this; }
        //}

        public DapperDBContextOptions Value => this;
    }
}
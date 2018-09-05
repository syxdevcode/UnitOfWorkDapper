using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UnitOfWorkDapper.Core;
using UnitOfWorkDapper.Services.Entity;
using UnitOfWorkDapper.Services.Repositories.Interfaces;

namespace UnitOfWorkDapper.Services.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly DapperDBContext _context;

        public ProductRepository(IContext context)
        {
            _context = (DapperDBContext)context;
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _context.QueryAsync<Product>("SELECT * FROM Product");
        }

        public async Task<Product> GetByIdAsync(int id)
        {
            return await _context.QueryFirstOrDefaultAsync<Product>("SELECT * FROM Product WHERE Id=@Id", new { Id = id });
        }

        public async Task<bool> InsertAsync(Product product)
        {
            var b= await _context.ExecuteAsync("INSERT INTO Product VALUES (@Id, @Name, @Price)", product) > 0;
            return b;
        }

        public async Task<bool> UpdateAsync(Product product)
        {
            return await _context.ExecuteAsync("UPDATE Product SET Name=@Name, Price=@Price WHERE Id=@Id", product) > 0;
        }
    }
}

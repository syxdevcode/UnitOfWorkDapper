using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UnitOfWorkDapper.Services.Entity;
using UnitOfWorkDapper.Services.Repositories.Interfaces;

namespace UnitOfWorkDapper.Services.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly MyDbContext _context;

        public ProductRepository(MyDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _context.QueryAsync<Product>("SELECT * FROM Products");
        }

        public async Task<Product> GetByIdAsync(int id)
        {
            return await _context.QueryFirstOrDefaultAsync<Product>("SELECT * FROM Products WHERE Id=@Id", new { Id = id });
        }

        public async Task<bool> InsertAsync(Product product)
        {
            return await _context.ExecuteAsync("INSERT INTO Products VALUES (@Name, @Price)", product) > 0;
        }

        public async Task<bool> UpdateAsync(Product product)
        {
            return await _context.ExecuteAsync("UPDATE Products SET Name=@Name, Price=@Price WHERE Id=@Id", product) > 0;
        }
    }
}

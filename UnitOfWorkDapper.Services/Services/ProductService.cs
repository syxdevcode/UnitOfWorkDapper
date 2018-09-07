using System.Collections.Generic;
using System.Threading.Tasks;
using UnitOfWorkDapper.Core;
using UnitOfWorkDapper.Services.Entity;
using UnitOfWorkDapper.Services.Repositories.Interfaces;
using UnitOfWorkDapper.Services.Services.Interfaces;

namespace UnitOfWorkDapper.Services.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ProductService(IProductRepository productRepository, IUnitOfWork unitOfWork)
        {
            _productRepository = productRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _productRepository.GetAllAsync();
        }

        public async Task<Product> GetByIdAsync(int id)
        {
            return await _productRepository.GetByIdAsync(id);
        }

        public async Task<bool> SaveAsync(Product product)
        {
            if (product.Id > 0)
            {
                // update data
                await _productRepository.UpdateAsync(product);
            }
            else
            {
                // insert data
                await _productRepository.InsertAsync(product);
            }
            var b = _unitOfWork.SaveChanges();

            return b;
        }
    }
}
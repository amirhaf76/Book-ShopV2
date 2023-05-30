using BookShop.ModelsLayer.DataAccessLayer.DataBaseModels;
using BookShop.ModelsLayer.DataAccessLayer.Dtos;
using BookShop.ModelsLayer.DataBaseLayer.DataModelRepositoryAbstraction;

namespace BookShop.ModelsLayer.DataAccessLayer.DataModelRepositoryAbstraction
{
    public interface IStockRepository : IBaseRepository<Stock>
    {
        Task<IEnumerable<Stock>> GetStocksAsync();

        Task<IEnumerable<Stock>> GetStocksAsync(StockFilter stockFilter);
    }
}

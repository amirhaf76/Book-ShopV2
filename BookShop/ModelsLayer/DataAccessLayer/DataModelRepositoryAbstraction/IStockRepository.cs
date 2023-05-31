using BookShop.ModelsLayer.DataAccessLayer.DataBaseModels;
using BookShop.ModelsLayer.DataAccessLayer.Dtos;

namespace BookShop.ModelsLayer.DataAccessLayer.DataModelRepositoryAbstraction
{
    public interface IStockRepository : IBaseRepository<Stock>
    {
        Task<IEnumerable<Stock>> GetStocksAsync();

        Task<IEnumerable<Stock>> GetStocksAsync(StockFilter stockFilter);

        Task<IEnumerable<Stock>> GetStocksCompletlyAsync();

        Task<IEnumerable<Stock>> GetStocksCompletlyAsync(StockFilter stockFilter);



    }
}

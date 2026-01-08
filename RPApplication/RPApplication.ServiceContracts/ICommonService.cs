namespace RPApplication.ServiceContracts
{
    public interface ICommonService<TAdd, TResponse, TUpdate>
    {
        Task<TResponse> CreateItem(TAdd? addRequest);
        Task<TResponse> UpdateItem(TUpdate? updateRequest);
        Task<TResponse?> GetItemById(string itemId);
        Task<bool> DeleteItem(string itemId);
    }
}

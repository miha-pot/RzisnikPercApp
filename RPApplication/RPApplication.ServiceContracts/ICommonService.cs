namespace RPApplication.ServiceContracts
{
    public interface ICommonService<TDto>
    {
        Task<TDto> CreateItem(TDto? addRequest);
        Task<TDto> UpdateItem(TDto? updateRequest);
        Task<TDto?> GetItemById(string itemId);
        Task<bool> DeleteItem(string itemId);
    }
}

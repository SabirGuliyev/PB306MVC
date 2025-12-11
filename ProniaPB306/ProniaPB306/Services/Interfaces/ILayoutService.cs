

using ProniaPB306.ViewModels;

namespace ProniaPB306.Services.Interfaces
{
    public interface ILayoutService
    {
       Task<Dictionary<string, string>> GetSettingsAsync();
        Task<BasketVM> GetBasketAsync();
    }
}

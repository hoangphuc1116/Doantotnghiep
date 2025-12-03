using Ecommerce_WatchShop.Models.Momo;
using Ecommerce_WatchShop.Models.ViewModels;

namespace Ecommerce_WatchShop.Services
{
    public interface IMomoService
    {
        Task<MomoCreatePaymentResponseModel> CreatePaymentAsync(CheckoutVM model);
        MomoExecuteResponseModel PaymentExecuteAsync(IQueryCollection collection);
    }
}

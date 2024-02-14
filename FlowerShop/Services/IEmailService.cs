using FlowerShop.Dto;
using FlowerShop.ViewModels;

namespace FlowerShop.Services
{
    public interface IEmailService
    {
        void SendEmail(CartListWithGrandTotal request);
    }
}

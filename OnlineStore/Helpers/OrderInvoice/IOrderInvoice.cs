using OnlineStore.Models;
using System.Threading.Tasks;

namespace OnlineStore.Helpers.OrderInvoice
{
    public interface IOrderInvoice
    {
        Task GenerateInvoice(Order order);
    }
}

using System.Collections.Generic;

namespace Empyreal.ViewModels.Display
{
    public class OrderStateViewModel
    {
        public List<OrderDetailViewModel> Orders { get; set; }
        public List<OrderDetailViewModel> AcceptOrders { get; set; }
        public List<OrderDetailViewModel> ShippingOrders { get; set; }
        public List<OrderDetailViewModel> DoneOrders { get; set; }
        public List<OrderDetailViewModel> CancelOrders { get; set; }

        public OrderStateViewModel()
        {
            Orders = new List<OrderDetailViewModel>();
            AcceptOrders = new List<OrderDetailViewModel>();
            ShippingOrders = new List<OrderDetailViewModel>();
            DoneOrders = new List<OrderDetailViewModel>();
            CancelOrders = new List<OrderDetailViewModel>();
        }
    }
}

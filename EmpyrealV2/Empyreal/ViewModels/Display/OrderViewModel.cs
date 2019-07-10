using Empyreal.Models;
using System;

namespace Empyreal.ViewModels.Display
{
    public class OrderViewModel
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public double? PriceSum { get; set; }
        public DateTime? CreateDate { get; set; }
        public int? State { get; set; }
        public string Address { get; set; }
        public string AddressType { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }

        public OrderViewModel(Order order)
        {
            this.Id = order.Id;
            this.UserId = order.UserId;
            this.PriceSum = order.PriceSum;
            this.CreateDate = order.CreateDate;
            this.State = order.Id;
            this.Address = order.Address;
            this.AddressType = order.AddressType;
            this.Name = order.Name;
            this.PhoneNumber = order.PhoneNumber;
        }

        public OrderViewModel() { }
    }
}

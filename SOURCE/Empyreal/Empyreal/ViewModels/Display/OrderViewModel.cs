using Empyreal.Models;
using System;
using System.Globalization;

namespace Empyreal.ViewModels.Display
{
    public class OrderViewModel
    {
        CultureInfo cul = CultureInfo.GetCultureInfo("vi-VN");

        public int Id { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public double? PriceSum { get; set; }
        public string PriceSumText { get; set; }
        public DateTime? ShippingDate { get; set; }
        public DateTime? DeadlineShip { get; set; }
        public DateTime? CreateDate { get; set; }
        public int? State { get; set; }
        public string Address { get; set; }
        public string AddressType { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Shipper { get; set; }
        public string ShipperName { get; set; }
        public double? ShippingFee { get; set; }
        public double? FinalPrice
        {
            get
            {
                return PriceSum + ShippingFee;
            }
        }

        public string DisplayFinalPrice
        {
            get
            {
                return string.Format(cul, "{0:c0}", this.FinalPrice);
            }
        }

        public string DisplayPriceSum
        {
            get
            {
                return string.Format(cul, "{0:c0}", this.PriceSum);
            }
        }

        public string DisplayShippingFee
        {
            get
            {
                return string.Format(cul, "{0:c0}", this.ShippingFee);
            }
        }

        public OrderViewModel(Order order)
        {
            this.Id = order.Id;
            this.UserId = order.UserId;
            this.PriceSum = order.PriceSum;
            this.CreateDate = order.CreateDate;
            this.State = order.State;
            this.Address = order.Address;
            this.AddressType = order.AddressType;
            this.Name = order.Name;
            this.PhoneNumber = order.PhoneNumber;
            this.ShippingDate = order.ShippingDate;
            this.ShippingFee = order.ShippingFee;
            this.DeadlineShip = order.DeadlineShip;
            this.Shipper = order.Shipper;
        }

        public OrderViewModel() { }
    }
}

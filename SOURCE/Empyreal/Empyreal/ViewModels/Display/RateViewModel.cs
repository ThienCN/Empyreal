using Empyreal.Models;
using System;
using System.Linq;

namespace Empyreal.ViewModels.Display
{
    public class RateViewModel
    {
        public int Id { get; set; }
        public string Contents { get; set; }
        public int? ProductId { get; set; }
        public string UserId { get; set; }
        public int? Star { get; set; }
        public DateTime? CreateDate { get; set; }
        public string Tilte { get; set; }
        public int? State { get; set; }
        public string HoTen { get; set; }
        public string Image
        {
            get
            {
                string imageString =  new string(HoTen.Split(' ').Select(x => x[0]).ToArray());
                if(imageString.Length > 2)
                {
                    imageString = imageString.Substring(imageString.Length - 2);
                }
                return imageString;
            }   
        }
        public string RangeDate
        {
            get
            {
                int range =  Math.Abs(DateTime.Today.Subtract(CreateDate.GetValueOrDefault()).Days);
                if ((range / 365 ) > 0)
                {
                    return range / 365 + " năm trước";
                }
                else
                {
                    if ((range % 365) / 31 > 0)
                        return (range % 365) / 31 + " tháng trước";
                    else
                    {
                        if ((range % 365 % 31) / 7 > 0)
                            return (range % 365 % 31) / 7 + " tuần trước";
                        else return range + " ngày trước";
                    }
                }
            }
        }

        public RateViewModel(Rate rate)
        {
            this.Id = rate.Id;
            this.ProductId = rate.ProductId;
            this.UserId = rate.UserId;
            this.Contents = rate.Contents;
            this.Star = rate.Star;
            this.CreateDate = rate.CreateDate;
            this.Tilte = rate.Tilte;
            this.State = rate.State;
            this.HoTen = rate.RateUser.HoTen;
        }
    }
}

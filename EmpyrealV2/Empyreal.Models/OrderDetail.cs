namespace Empyreal.Models
{
    public partial class OrderDetail
    {
        public int Id { get; set; }
        public int? OrderId { get; set; }
        public int? ProductDetailId { get; set; }
        public double? Price { get; set; }
        public int? State { get; set; }

        public virtual Order Order { get; set; }
        public virtual ProductDetail ProductDetail { get; set; }
    }
}

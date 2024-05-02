namespace webapi.Model.Product
{
    public class ProductTag
    {
        public int TagId { get; set; }
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }
        public virtual Tag Tag { get; set; }
    }
}
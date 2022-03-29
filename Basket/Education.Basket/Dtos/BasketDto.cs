namespace Education.Basket.Dtos
{
    public class BasketDto
    {
        public string UserId { get; set; }
        public string DiscountCode { get; set; }
        public List<BasketItemDto> BasketItemDtos { get; set; }
        public decimal TotalPrice => BasketItemDtos.Sum(x => x.Quantity * x.Price);
    }
}

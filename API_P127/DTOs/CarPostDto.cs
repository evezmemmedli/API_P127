namespace API_P127.DTOs
{
    public class CarPostDto
    {
        public string Brand { get; set; }
        public string Model { get; set; }
        public decimal Price { get; set; }
        public string Color { get; set; }
        public bool Display { get; set; } = false;

    }
}

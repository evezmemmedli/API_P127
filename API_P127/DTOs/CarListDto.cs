using System.Collections.Generic;

namespace API_P127.DTOs
{
    public class CarListDto
    {
        public List<CarListItemDto> carListItemDtos { get; set; }
        public int TotalCount { get; set; }
    }
}

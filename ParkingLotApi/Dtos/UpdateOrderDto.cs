using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParkingLotApi.Dtos
{
    public class UpdateOrderDto
    {
        public UpdateOrderDto()
        {
        }

        public UpdateOrderDto(string orderStatus)
        {
            this.OrderStatus = orderStatus;
        }

        public string OrderStatus { get; set; }
    }
}

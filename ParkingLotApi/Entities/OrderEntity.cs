using ParkingLotApi.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParkingLotApi.Entities
{
    public class OrderEntity
    {
        public OrderEntity()
        {
        }

        public OrderEntity(OrderDto orderDto)
        {
            this.OrderNumber = orderDto.OrderNumber;
            this.ParkingLotName = orderDto.ParkingLotName;
            this.PlateNumber = orderDto.PlateNumber;
            this.CreationTime = orderDto.CreationTime;
            this.CloseTime = orderDto.CloseTime;
            this.OrderStatus = orderDto.OrderStatus;
        }

        public int Id { get; set; }
        public string OrderNumber { get; set; }
        public string ParkingLotName { get; set; }
        public string PlateNumber { get; set; }
        public string CreationTime { get; set; }
        public string CloseTime { get; set; }
        public string OrderStatus { get; set; }
    }
}

using ParkingLotApi.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParkingLotApi.Dtos
{
    public class OrderDto
    {
        public OrderDto()
        {
        }

        public OrderDto(OrderEntity orderEntity)
        {
            this.OrderNumber = orderEntity.OrderNumber;
            this.ParkingLotName = orderEntity.ParkingLotName;
            this.PlateNumber = orderEntity.PlateNumber;
            this.CreationTime = orderEntity.CreationTime;
            this.CloseTime = orderEntity.CloseTime;
            this.OrderStatus = orderEntity.OrderStatus;
        }

        public string OrderNumber { get; set; }
        public string ParkingLotName { get; set; }
        public string PlateNumber { get; set; }
        public string CreationTime { get; set; }
        public string CloseTime { get; set; }
        public string OrderStatus { get; set; }
        public override bool Equals(object obj)
        {
            if (!(obj is OrderDto))
            {
                return false;
            }

            OrderDto other = (OrderDto)obj;
            return OrderNumber == other.OrderNumber 
                && ParkingLotName == other.ParkingLotName 
                && PlateNumber == other.PlateNumber 
                && CreationTime == other.CreationTime 
                && CloseTime == other.CloseTime;              
        }
    }
}

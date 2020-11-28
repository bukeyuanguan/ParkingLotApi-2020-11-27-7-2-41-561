using ParkingLotApi.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParkingLotApi.Dtos
{
    public class ParkingLotDto
    {
        public ParkingLotDto()
        {
        }

        public ParkingLotDto(ParkingLotEntity parkingLotEntity)
        {
            this.Name = parkingLotEntity.Name;
            this.Locatoin = parkingLotEntity.Locatoin;
            this.Cars = parkingLotEntity.Cars?.Select(carEntity => new CarDto(carEntity)).ToList();
            this.Orders = parkingLotEntity.Orders?.Select(orderEntity => new OrderDto(orderEntity)).ToList();
        }

        public string Name { get; set; }
        public int Capacity { get; set; }
        public string Locatoin { get; set; }
        public List<CarDto> Cars { get; set; }
        public List<OrderDto> Orders { get; set; }
        public override bool Equals(object obj)
        {
            if (!(obj is ParkingLotDto))
            {
                return false;
            }

            ParkingLotDto other = (ParkingLotDto)obj;
            return Name == other.Name
                && Capacity == other.Capacity
                && Locatoin == other.Locatoin
                && Cars == other.Cars
                && Orders == other.Orders;
        }
    }
}

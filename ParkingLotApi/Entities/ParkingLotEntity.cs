using ParkingLotApi.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParkingLotApi.Entities
{
    public class ParkingLotEntity
    {
        public ParkingLotEntity()
        {
        }

        public ParkingLotEntity(ParkingLotDto parkingLotDto)
        {
            this.Name = parkingLotDto.Name;
            this.Locatoin = parkingLotDto.Locatoin;
            this.Cars = parkingLotDto.Cars.Select(carDto => new CarEntity(carDto)).ToList();
            this.Orders = parkingLotDto.Orders.Select(orderDto => new OrderEntity(orderDto)).ToList();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int Capacity { get; set; }
        public string Locatoin { get; set; }
        public List<CarEntity> Cars { get; set; }
        public List<OrderEntity> Orders { get; set; }
    }
}

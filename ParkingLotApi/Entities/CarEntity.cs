using ParkingLotApi.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParkingLotApi.Entities
{
    public class CarEntity
    {
        public CarEntity()
        {
        }

        public CarEntity(CarDto carDto)
        {
            this.PlateNumber = carDto.PlateNumber;
        }

        public int Id { get; set; }
        public string PlateNumber { get; set; }
    }
}

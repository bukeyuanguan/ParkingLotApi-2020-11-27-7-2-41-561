using ParkingLotApi.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParkingLotApi.Dtos
{
    public class CarDto
    {
        public CarDto()
        {
        }

        public CarDto(CarEntity carEntity)
        {
            this.PlateNumber = carEntity.PlateNumber;
        }

        public string PlateNumber { get; set; }
        public override bool Equals(object obj)
        {
            if (!(obj is CarDto))
            {
                return false;
            }

            CarDto other = (CarDto)obj;
            return PlateNumber == other.PlateNumber;
        }
    }
}

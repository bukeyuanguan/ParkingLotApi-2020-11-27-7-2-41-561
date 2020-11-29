using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParkingLotApi.Dtos
{
    public class UpdateParkingLotDto
    {
        public UpdateParkingLotDto()
        {
        }

        public UpdateParkingLotDto(int capacity)
        {
            this.Capacity = capacity;
        }

        public int Capacity { get; set; }
    }
}

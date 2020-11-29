using Microsoft.EntityFrameworkCore;
using ParkingLotApi.Dtos;
using ParkingLotApi.Entities;
using ParkingLotApi.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParkingLotApi.Services
{
    public class CarService
    {
        private readonly ParkingLotDbContext parkingLotDbContext;
        public CarService(ParkingLotDbContext parkingLotDbContext)
        {
            this.parkingLotDbContext = parkingLotDbContext;
        }

        public async Task<string> AddCar(string name, CarDto carDto)
        {
            var foundParkingLotEntity = await this.parkingLotDbContext.ParkingLots
                .Include(parkingLot => parkingLot.Cars)
                .Include(parkingLot => parkingLot.Orders)
                .FirstOrDefaultAsync(parkingLotEntity => parkingLotEntity.Name == name);
            if (foundParkingLotEntity != null)
            {
                var leftPosition = foundParkingLotEntity.Capacity - foundParkingLotEntity.Cars.Count;
                if (leftPosition > 0)
                {
                    CarEntity newCarEntity = new CarEntity(carDto);
                    await this.parkingLotDbContext.Cars.AddAsync(newCarEntity);
                    await this.parkingLotDbContext.SaveChangesAsync();
                    return newCarEntity.PlateNumber;
                }

                return "The parking lot is full.";
            }

            return "The parking lot is not exist.";
        }
    }
}

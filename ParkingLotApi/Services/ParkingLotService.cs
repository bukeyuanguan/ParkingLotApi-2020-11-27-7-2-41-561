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
    public class ParkingLotService
    {
        private readonly ParkingLotDbContext parkingLotDbContext;

        public ParkingLotService(ParkingLotDbContext parkingLotDbContext)
        {
            this.parkingLotDbContext = parkingLotDbContext;
        }

        public async Task<List<ParkingLotDto>> GetAll()
        {
            var parkingLots = await this.parkingLotDbContext.ParkingLots
                .Include(parkingLot => parkingLot.Cars)
                .Include(parkingLot => parkingLot.Orders).ToListAsync();
            return parkingLots.Select(parkingLotEntity => new ParkingLotDto(parkingLotEntity)).ToList();
        }

        public async Task<ParkingLotDto> GetByName(string name)
        {
            var foundParkingLotEntity = await this.parkingLotDbContext.ParkingLots
                .Include(parkingLot => parkingLot.Cars)
                .Include(parkingLot => parkingLot.Orders)
                .FirstOrDefaultAsync(parkingLotEntity => parkingLotEntity.Name == name);
            return new ParkingLotDto(foundParkingLotEntity);
        }

        public async Task<string> AddParkingLot(ParkingLotDto parkingLotDto)
        {
            ParkingLotEntity parkingLotEntity = new ParkingLotEntity(parkingLotDto);

            await this.parkingLotDbContext.ParkingLots.AddAsync(parkingLotEntity);
            await this.parkingLotDbContext.SaveChangesAsync();
            return parkingLotEntity.Name;
        }

        public async Task DeleteParkingLot(int id)
        {
            var foundParkingLot = await this.parkingLotDbContext.ParkingLots.FirstOrDefaultAsync(parkingLot => parkingLot.Id == id);
            this.parkingLotDbContext.ParkingLots.Remove(foundParkingLot);
            await this.parkingLotDbContext.SaveChangesAsync();
        }

        public async Task DeleteAllParkingLot()
        {
            this.parkingLotDbContext.ParkingLots.RemoveRange(parkingLotDbContext.ParkingLots);
            await this.parkingLotDbContext.SaveChangesAsync();
        }
    }
}

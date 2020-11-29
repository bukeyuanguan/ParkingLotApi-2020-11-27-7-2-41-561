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
            if (foundParkingLotEntity == null)
            {
                return null;
            }

            return new ParkingLotDto(foundParkingLotEntity);
        }

        public async Task<ParkingLotDto> UpdateParkingLot(string name, UpdateParkingLotDto updateParkingLotDto)
        {
            var foundParkingLotEntity = await this.parkingLotDbContext.ParkingLots
                .Include(parkingLot => parkingLot.Cars)
                .Include(parkingLot => parkingLot.Orders)
                .FirstOrDefaultAsync(parkingLotEntity => parkingLotEntity.Name == name);
            if (foundParkingLotEntity == null)
            {
                return null;
            }

            foundParkingLotEntity.Capacity = updateParkingLotDto.Capacity;
            return new ParkingLotDto(foundParkingLotEntity);
        }

        public async Task<List<ParkingLotDto>> GetByPageSizeAndIndex(int? pageSize, int? pageIndex)
        {
            var allParkingLot = await this.parkingLotDbContext.ParkingLots
               .Include(parkingLot => parkingLot.Cars)
               .Include(parkingLot => parkingLot.Orders).ToListAsync();
            var foundParkingLots = allParkingLot.Where((parkingLotEntity, index) =>
               (pageSize == null || (pageIndex == null ||
               (index >= pageSize * (pageIndex - 1) &&
               index < pageSize * pageIndex))));
            return foundParkingLots.Select(parkingLotEntity => new ParkingLotDto(parkingLotEntity)).ToList();
           // return new List<ParkingLotDto> { new ParkingLotDto(allParkingLot[0] };
        }

        public async Task<string> AddParkingLot(ParkingLotDto parkingLotDto)
        {
            ParkingLotEntity newParkingLotEntity = new ParkingLotEntity(parkingLotDto);
            var isNameExist = this.parkingLotDbContext.ParkingLots.Any(parkingLotEntity => parkingLotEntity.Name == newParkingLotEntity.Name);
            if (isNameExist)
            {
                return string.Empty;
            }

            await this.parkingLotDbContext.ParkingLots.AddAsync(newParkingLotEntity);
            await this.parkingLotDbContext.SaveChangesAsync();
            return newParkingLotEntity.Name;
        }

        public async Task DeleteParkingLot(string name)
        {
            var foundParkingLot = await this.parkingLotDbContext.ParkingLots.FirstOrDefaultAsync(parkingLot => parkingLot.Name == name);
            if (foundParkingLot != null)
            {
                this.parkingLotDbContext.ParkingLots.Remove(foundParkingLot);
                await this.parkingLotDbContext.SaveChangesAsync();
            }
        }

        public async Task DeleteAllParkingLot()
        {
            this.parkingLotDbContext.ParkingLots.RemoveRange(parkingLotDbContext.ParkingLots);
            await this.parkingLotDbContext.SaveChangesAsync();
        }
    }
}

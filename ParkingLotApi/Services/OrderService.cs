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
    public class OrderService
    {
        private readonly ParkingLotDbContext parkingLotDbContext;
        public OrderService(ParkingLotDbContext parkingLotDbContext)
        {
            this.parkingLotDbContext = parkingLotDbContext;
        }

        public async Task<string> AddOrder(OrderDto orderDto)
        {
            var foundParkingLotEntity = await this.parkingLotDbContext.ParkingLots
                .Include(parkingLot => parkingLot.Cars)
                .Include(parkingLot => parkingLot.Orders)
                .FirstOrDefaultAsync(parkingLotEntity => parkingLotEntity.Name == orderDto.ParkingLotName);
            if (foundParkingLotEntity != null)
            {
                var leftPosition = foundParkingLotEntity.Capacity - this.parkingLotDbContext.Cars.Count();
                if (leftPosition > 0)
                {
                    OrderEntity newOrderEntity = new OrderEntity(orderDto);
                    await this.parkingLotDbContext.Orders.AddAsync(newOrderEntity);
                    await this.parkingLotDbContext.SaveChangesAsync();
                    return newOrderEntity.OrderNumber;
                }

                return "The parking lot is full.";
            }

            return "The parking lot is not exist.";
        }

        public async Task<OrderDto> UpdateOrder(string orderNumber, UpdateOrderDto updateOrderDto)
        {
            var foundOrderEntity = await this.parkingLotDbContext.Orders.FirstOrDefaultAsync(orderEntity => orderEntity.OrderNumber == orderNumber);
            if (foundOrderEntity == null)
            {
                return null;
            }

            foundOrderEntity.OrderStatus = updateOrderDto.OrderStatus;
            return new OrderDto(foundOrderEntity);
        }
    }
}

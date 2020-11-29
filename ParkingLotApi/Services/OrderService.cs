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
            OrderEntity newOrderEntity = new OrderEntity(orderDto);
            var isNameExist = this.parkingLotDbContext.Orders.Any(orderEntity => orderEntity.OrderNumber == newOrderEntity.OrderNumber);
            if (isNameExist)
            {
                return string.Empty;
            }

            await this.parkingLotDbContext.Orders.AddAsync(newOrderEntity);
            await this.parkingLotDbContext.SaveChangesAsync();
            return newOrderEntity.OrderNumber;
        }
    }
}

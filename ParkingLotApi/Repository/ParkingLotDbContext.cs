﻿using Microsoft.EntityFrameworkCore;
using ParkingLotApi.Entities;

namespace ParkingLotApi.Repository
{
    public class ParkingLotDbContext : DbContext
    {
        public ParkingLotDbContext(DbContextOptions<ParkingLotDbContext> options)
            : base(options)
        {
        }

        public DbSet<ParkingLotEntity> ParkingLots { get; set; }
        public DbSet<CarEntity> Cars { get; set; }
        public DbSet<OrderEntity> Orders { get; set; }
    }
}
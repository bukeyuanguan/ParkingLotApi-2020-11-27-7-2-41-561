﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ParkingLotApi.Dtos;
using ParkingLotApi.Entities;
using ParkingLotApi.Repository;
using ParkingLotApi.Services;

namespace ParkingLotApi.Controllers
{
    [ApiController]
    [Route("parkingLots")]
    public class ParkingLotController : ControllerBase
    {
        private readonly ParkingLotService parkingLotService;
        public ParkingLotController(ParkingLotService parkingLotService)
        {
            this.parkingLotService = parkingLotService;
        }

        [HttpGet("{name}")]
        public async Task<ActionResult<ParkingLotDto>> GetByName(string name)
        {
            var parkingLotDto = await this.parkingLotService.GetByName(name);
            return Ok(parkingLotDto);
        }

        [HttpGet]
        public async Task<ActionResult<List<ParkingLotDto>>> GetAll()
        {
            var parkingLotDto = await this.parkingLotService.GetAll();
            return Ok(parkingLotDto);
        }

        [HttpPost]
        public async Task<ActionResult<ParkingLotDto>> Add(ParkingLotDto parkingLotDto)
        {
            var name = await this.parkingLotService.AddParkingLot(parkingLotDto);
            return CreatedAtAction(nameof(GetByName), new { name = name }, parkingLotDto);
            //return parkingLotDto;
        }

        [HttpPatch("{name}")]
        public async Task<ActionResult<ParkingLotDto>> UpdateByName(string name, UpdateParkingLotDto updateParkingLotDto)
        {
            var parkingLotDto = await this.parkingLotService.UpdateParkingLot(name, updateParkingLotDto);
            return CreatedAtAction(nameof(GetByName), new { name = name }, parkingLotDto);
            //return NoContent();
        }

        [HttpDelete("{name}")]
        public async Task<ActionResult<ParkingLotDto>> DeleteByName(string name)
        {
            await this.parkingLotService.DeleteParkingLot(name);
            return NoContent();
        }

        [HttpDelete("clear")]
        public async Task<ActionResult> DeleteAll()
        {
            await this.parkingLotService.DeleteAllParkingLot();

            return this.NoContent();
        }
    }
}

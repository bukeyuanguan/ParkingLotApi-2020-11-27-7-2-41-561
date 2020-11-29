using System;
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
        private readonly OrderService orderService;
        public ParkingLotController(ParkingLotService parkingLotService, OrderService orderService)
        {
            this.parkingLotService = parkingLotService;
            this.orderService = orderService;
        }

        [HttpDelete("clear")]
        public async Task<ActionResult> DeleteAll()
        {
            await this.parkingLotService.DeleteAllParkingLot();

            return this.NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<ParkingLotDto>> Add(ParkingLotDto parkingLotDto)
        {
            var name = await this.parkingLotService.AddParkingLot(parkingLotDto);
            if (name == string.Empty)
            {
                return Conflict();
            }

            return CreatedAtAction(nameof(GetByName), new { name = name }, parkingLotDto);
            //return parkingLotDto;
        }

        [HttpGet("{name}")]
        public async Task<ActionResult<ParkingLotDto>> GetByName(string name)
        {
            var parkingLotDto = await this.parkingLotService.GetByName(name);
            if (parkingLotDto == null)
            {
                return NotFound();
            }

            return Ok(parkingLotDto);
        }

        [HttpGet]
        public async Task<ActionResult<List<ParkingLotDto>>> GetAll()
        {
            var parkingLotDto = await this.parkingLotService.GetAll();
            return Ok(parkingLotDto);
        }

        //[HttpGet]
        //public async Task<ActionResult<List<ParkingLotDto>>> GetXParkingLotsInPageY(int? pageSize, int? pageIndex)
        //{
        //    //var parkingLotDto = await this.parkingLotService.GetByName("IBM");
        //    var parkingLotsList = await this.parkingLotService.GetByPageSizeAndIndex(pageSize, pageIndex);
        //    return Ok(parkingLotsList);
        //}

        [HttpPatch("{name}")]
        public async Task<ActionResult<ParkingLotDto>> UpdateByName(string name, UpdateParkingLotDto updateParkingLotDto)
        {
            var parkingLotDto = await this.parkingLotService.UpdateParkingLot(name, updateParkingLotDto);
            // return CreatedAtAction(nameof(GetByName), new { name = name }, parkingLotDto);
            if (parkingLotDto == null)
            {
                return NotFound();
            }

            return Ok(parkingLotDto);
        }

        [HttpDelete("{name}")]
        public async Task<ActionResult<ParkingLotDto>> DeleteByName(string name)
        {
            await this.parkingLotService.DeleteParkingLot(name);
            return NoContent();
        }

        [HttpPost("{name}/orders")]
        public async Task<ActionResult<ParkingLotDto>> AddOrder(OrderDto orderLotDto)
        {
            var orderNumber = await this.orderService.AddOrder(orderLotDto);
            if (orderNumber == string.Empty)
            {
                return Conflict();
            }

            return CreatedAtAction(nameof(GetByName), new { orderNumber = orderNumber }, orderLotDto);
            //return parkingLotDto;
        }
    }
}

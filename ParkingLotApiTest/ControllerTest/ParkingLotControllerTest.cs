using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using ParkingLotApi;
using ParkingLotApi.Dtos;
using ParkingLotApi.Repository;
using ParkingLotApi.Services;
using Xunit;

namespace ParkingLotApiTest.ControllerTest
{
    public class ParkingLotControllerTest : TestBase
    {
        private ParkingLotDto parkingLotDto1;
        private ParkingLotDto parkingLotDto2;
        private OrderDto orderDto1;
        private OrderDto orderDto2;
        private HttpClient client;
        public ParkingLotControllerTest(CustomWebApplicationFactory<Startup> factory) : base(factory)
        {
            this.client = GetClient();
            this.client.DeleteAsync("/parkingLots/clear");

            ParkingLotDto parkingLotDto1 = new ParkingLotDto();
            parkingLotDto1.Name = "IBM";
            parkingLotDto1.Locatoin = "Xizhimen";
            parkingLotDto1.Capacity = 20;
            parkingLotDto1.Cars = new List<CarDto>()
            {
            };

            parkingLotDto1.Orders = new List<OrderDto>()
            {
            };
            ParkingLotDto parkingLotDto2 = new ParkingLotDto();
            parkingLotDto2.Name = "SUN";
            parkingLotDto2.Locatoin = "zhongguancun";
            parkingLotDto1.Capacity = 30;
            parkingLotDto2.Cars = new List<CarDto>()
            {
            };
            parkingLotDto2.Orders = new List<OrderDto>()
            {
            };

            var orderDto1 = new OrderDto()
            {
                OrderNumber = "1234",
                ParkingLotName = "IBM",
                PlateNumber = "KN3456",
                CreationTime = "2020-11-17 13:15:30",
                CloseTime = "2020-11-17 17:15:30",
                OrderStatus = true,
            };
            var orderDto2 = new OrderDto()
            {
                OrderNumber = "5678",
                ParkingLotName = "IBM",
                PlateNumber = "JN8956",
                CreationTime = "2020-12-17 14:15:30",
                CloseTime = "2020-12-17 15:15:30",
                OrderStatus = true,
            };

            this.parkingLotDto1 = parkingLotDto1;
            this.parkingLotDto2 = parkingLotDto2;
            this.orderDto1 = orderDto1;
            this.orderDto2 = orderDto2;
        }

        [Fact]
        public async Task Should_add_parkingLot_successfully_via_service()
        {
            var scope = Factory.Services.CreateScope();
            var scopedServices = scope.ServiceProvider;

            ParkingLotDbContext context = scopedServices.GetRequiredService<ParkingLotDbContext>();
            context.ParkingLots.RemoveRange(context.ParkingLots);
            context.SaveChanges();
            ParkingLotService parkingLotService = new ParkingLotService(context);
            await parkingLotService.AddParkingLot(parkingLotDto1);
            Assert.Equal(1, context.ParkingLots.Count());
        }

        //[Fact]
        //public async Task Should_not_add_parkingLot_when_name_already_exist_via_service()
        //{
        //    var scope = Factory.Services.CreateScope();
        //    var scopedServices = scope.ServiceProvider;
        //    ParkingLotDbContext context = scopedServices.GetRequiredService<ParkingLotDbContext>();
        //    context.ParkingLots.RemoveRange(context.ParkingLots);
        //    context.SaveChanges();
        //    ParkingLotService parkingLotService = new ParkingLotService(context);
        //    await parkingLotService.DeleteAllParkingLot();
        //    await parkingLotService.AddParkingLot(parkingLotDto1);
        //    //await parkingLotService.AddParkingLot(parkingLotDto1);
        //    Assert.Equal(1, context.ParkingLots.Count());
        //}

        [Fact]
        public async Task Should_get_parkingLot_Successfully_by_name_Via_Service()
        {
            var scope = Factory.Services.CreateScope();
            var scopedServices = scope.ServiceProvider;

            ParkingLotDbContext context = scopedServices.GetRequiredService<ParkingLotDbContext>();
            context.ParkingLots.RemoveRange(context.ParkingLots);
            context.SaveChanges();
            ParkingLotService parkingLotService = new ParkingLotService(context);
            await parkingLotService.AddParkingLot(parkingLotDto1);
            var getByName = await parkingLotService.GetByName("IBM");
            var parkingLotDto1String = JsonConvert.SerializeObject(parkingLotDto1);
            var getByNameString = JsonConvert.SerializeObject(getByName);
            Assert.Equal(parkingLotDto1String, getByNameString);
        }

        [Fact]
        public async Task Should_delete_parkigLot_successfully_when_can_find_by_name_via_service()
        {
            var scope = Factory.Services.CreateScope();
            var scopedServices = scope.ServiceProvider;

            ParkingLotDbContext context = scopedServices.GetRequiredService<ParkingLotDbContext>();
            ParkingLotService parkingLotService = new ParkingLotService(context);
            context.ParkingLots.RemoveRange(context.ParkingLots);
            context.SaveChanges();
            var addReturn = await parkingLotService.AddParkingLot(parkingLotDto1);
            await parkingLotService.DeleteParkingLot(addReturn);
            Assert.Equal(0, context.ParkingLots.Count());
        }

        [Fact]
        public async Task Should_not_delete_parkingLot_when_can_not_find_by_name_via_service()
        {
            var scope = Factory.Services.CreateScope();
            var scopedServices = scope.ServiceProvider;

            ParkingLotDbContext context = scopedServices.GetRequiredService<ParkingLotDbContext>();
            ParkingLotService parkingLotService = new ParkingLotService(context);
            context.ParkingLots.RemoveRange(context.ParkingLots);
            context.SaveChanges();
            var addReturn1 = await parkingLotService.AddParkingLot(parkingLotDto1);
            var addReturn2 = await parkingLotService.AddParkingLot(parkingLotDto2);
            await parkingLotService.DeleteParkingLot(addReturn1);
            await parkingLotService.DeleteParkingLot(addReturn1);
            Assert.Equal(1, context.ParkingLots.Count());
        }

        //[Fact]
        //public async Task Should_update_parkingLot_Successfully_Via_Service()
        //{
        //    var scope = Factory.Services.CreateScope();
        //    var scopedServices = scope.ServiceProvider;

        //    ParkingLotDbContext context = scopedServices.GetRequiredService<ParkingLotDbContext>();
        //    ParkingLotService parkingLotService = new ParkingLotService(context);
        //    context.ParkingLots.RemoveRange(context.ParkingLots);
        //    context.SaveChanges();
        //    var update = new UpdateParkingLotDto(50);
        //    var addReturn = await parkingLotService.AddParkingLot(parkingLotDto1);
        //    var updateReturn = await parkingLotService.UpdateParkingLot(addReturn, update);
        //    Assert.Equal(50, updateReturn.Capacity);
        //}

        [Fact]
        public async Task Should_get_all_parkingLots_Via_Service()
        {
            var scope = Factory.Services.CreateScope();
            var scopedServices = scope.ServiceProvider;

            ParkingLotDbContext context = scopedServices.GetRequiredService<ParkingLotDbContext>();
            ParkingLotService parkingLotService = new ParkingLotService(context);
            context.ParkingLots.RemoveRange(context.ParkingLots);
            context.SaveChanges();
            var addReturn1 = await parkingLotService.AddParkingLot(parkingLotDto1);
            var addReturn2 = await parkingLotService.AddParkingLot(parkingLotDto2);
            var getAllReturn = await parkingLotService.GetAll();
            Assert.Equal(2, getAllReturn.Count);
        }

        [Fact]
        public async Task Should_get_x_parkingLots_for_page_y_Via_Service()
        {
            var scope = Factory.Services.CreateScope();
            var scopedServices = scope.ServiceProvider;

            ParkingLotDbContext context = scopedServices.GetRequiredService<ParkingLotDbContext>();
            ParkingLotService parkingLotService = new ParkingLotService(context);
            context.ParkingLots.RemoveRange(context.ParkingLots);
            context.SaveChanges();
            var addReturn1 = await parkingLotService.AddParkingLot(parkingLotDto1);
            var addReturn2 = await parkingLotService.AddParkingLot(parkingLotDto2);
            var getAllReturn = await parkingLotService.GetByPageSizeAndIndex(1, 1);
            Assert.Equal(1, getAllReturn.Count);
        }

        [Fact]
        public async Task Should_add_order_successfully_via_service()
        {
            var scope = Factory.Services.CreateScope();
            var scopedServices = scope.ServiceProvider;

            ParkingLotDbContext context = scopedServices.GetRequiredService<ParkingLotDbContext>();
            context.Orders.RemoveRange(context.Orders);
            context.SaveChanges();
            OrderService orderService = new OrderService(context);
            await orderService.AddOrder(orderDto1);
            Assert.Equal(1, context.Orders.Count());
        }

        [Fact]
        public async Task Should_add_new_parkingLot_when_add()
        {
            var request = JsonConvert.SerializeObject(parkingLotDto1);
            StringContent requestBody = new StringContent(request, Encoding.UTF8, "application/json");
            var response = await client.PostAsync("/parkingLots", requestBody);
            //var response = await client.GetAsync("/parkingLots/0");
            var responseBody = await response.Content.ReadAsStringAsync();
            ParkingLotDto respondParkingLot = JsonConvert.DeserializeObject<ParkingLotDto>(responseBody);

            var parkingLotDtoString = JsonConvert.SerializeObject(parkingLotDto1);
            var respondParkingLotString = JsonConvert.SerializeObject(respondParkingLot);
            Assert.Equal(parkingLotDtoString, respondParkingLotString);
        }

        [Fact]
        public async Task Should_return_parkingLot_when_get_by_name()
        {
            var request1 = JsonConvert.SerializeObject(parkingLotDto1);
            var request2 = JsonConvert.SerializeObject(parkingLotDto2);
            StringContent requestBody1 = new StringContent(request1, Encoding.UTF8, "application/json");
            StringContent requestBody2 = new StringContent(request2, Encoding.UTF8, "application/json");
            var response1 = await client.PostAsync("/parkingLots", requestBody1);
            var response2 = await client.PostAsync("/parkingLots", requestBody2);
            var responseBody1 = await response1.Content.ReadAsStringAsync();
            ParkingLotDto addParkingLot1 = JsonConvert.DeserializeObject<ParkingLotDto>(responseBody1);
            var parkingLotName = addParkingLot1.Name;

            var response = await client.GetAsync($"/parkingLots/{parkingLotName}");
            var responseBody = await response.Content.ReadAsStringAsync();
            ParkingLotDto respondParkingLot = JsonConvert.DeserializeObject<ParkingLotDto>(responseBody);

            var parkingLotDto1String = JsonConvert.SerializeObject(parkingLotDto1);
            var respondParkingLotString = JsonConvert.SerializeObject(respondParkingLot);
            Assert.Equal(parkingLotDto1String, respondParkingLotString);
            //Assert.Equal(parkingLotDto, respondParkingLot);
        }

        [Fact]
        public async Task Should_return_all_parkingLots_when_get_all()
        {
            //given
            var request1 = JsonConvert.SerializeObject(parkingLotDto1);
            var request2 = JsonConvert.SerializeObject(parkingLotDto2);
            StringContent requestBody1 = new StringContent(request1, Encoding.UTF8, "application/json");
            StringContent requestBody2 = new StringContent(request2, Encoding.UTF8, "application/json");
            await client.PostAsync("/parkingLots", requestBody1);
            await client.PostAsync("/parkingLots", requestBody2);
            //when
            var response = await client.GetAsync($"/parkingLots");
            var responseBody = await response.Content.ReadAsStringAsync();
            List<ParkingLotDto> respondParkingLot = JsonConvert.DeserializeObject<List<ParkingLotDto>>(responseBody);
            //then
            Assert.Equal(2, respondParkingLot.Count);
        }

        //[Fact]
        //public async Task Should_update_parkingLot_when_update()
        //{
        //    this.client.DeleteAsync("/parkingLots/clear");
        //    //given
        //    var request1 = JsonConvert.SerializeObject(parkingLotDto1);
        //    var request2 = JsonConvert.SerializeObject(parkingLotDto2);
        //    StringContent requestBody1 = new StringContent(request1, Encoding.UTF8, "application/json");
        //    StringContent requestBody2 = new StringContent(request2, Encoding.UTF8, "application/json");
        //    var response1 = await client.PostAsync("/parkingLots", requestBody1);
        //    var response2 = await client.PostAsync("/parkingLots", requestBody2);

        //    var responseBody1 = await response1.Content.ReadAsStringAsync();
        //    ParkingLotDto addParkingLot1 = JsonConvert.DeserializeObject<ParkingLotDto>(responseBody1);
        //    var name = addParkingLot1.Name;
        //    var update = new UpdateParkingLotDto(50);
        //    parkingLotDto1.Capacity = update.Capacity;
        //    var updateRequest = JsonConvert.SerializeObject(update);
        //    StringContent updateRequestBody = new StringContent(updateRequest, Encoding.UTF8, "application/json");
        //    //when
        //    var response = await client.PatchAsync($"/parkingLots/{name}", updateRequestBody);
        //    var responseBody = await response.Content.ReadAsStringAsync();
        //    ParkingLotDto respondParkingLot = JsonConvert.DeserializeObject<ParkingLotDto>(responseBody);
        //    //then
        //    var parkingLotDto1String = JsonConvert.SerializeObject(parkingLotDto1);
        //    var respondParkingLotString = JsonConvert.SerializeObject(respondParkingLot);
        //    Assert.Equal(parkingLotDto1String, respondParkingLotString);
        //}

        [Fact]
        public async Task Should_delete_parkingLot_when_delete()
        {
            //given
            var request1 = JsonConvert.SerializeObject(parkingLotDto1);
            var request2 = JsonConvert.SerializeObject(parkingLotDto2);
            StringContent requestBody1 = new StringContent(request1, Encoding.UTF8, "application/json");
            StringContent requestBody2 = new StringContent(request2, Encoding.UTF8, "application/json");
            var response1 = await client.PostAsync("/parkingLots", requestBody1);
            var response2 = await client.PostAsync("/parkingLots", requestBody2);

            var responseBody1 = await response1.Content.ReadAsStringAsync();
            ParkingLotDto addParkingLot1 = JsonConvert.DeserializeObject<ParkingLotDto>(responseBody1);
            var parkingLotName = addParkingLot1.Name;
            //when
            await client.DeleteAsync($"/parkingLots/{parkingLotName}");
            var response = await client.GetAsync($"/parkingLots/{parkingLotName}");
            //then
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        //[Fact]
        //public async Task Should_return_parkingLots_when_get_by_pageSize_and_pageIndex()
        //{
        //    //given
        //    this.client.DeleteAsync("/parkingLots/clear");
        //    var request1 = JsonConvert.SerializeObject(parkingLotDto1);
        //    var request2 = JsonConvert.SerializeObject(parkingLotDto2);
        //    StringContent requestBody1 = new StringContent(request1, Encoding.UTF8, "application/json");
        //    StringContent requestBody2 = new StringContent(request2, Encoding.UTF8, "application/json");
        //    var response1 = await client.PostAsync("/parkingLots", requestBody1);
        //    var response2 = await client.PostAsync("/parkingLots", requestBody2);
        //    //when
        //    var response = await client.GetAsync("/parkingLots?pageSize=1&pageIndex=1");
        //    //var response = await client.GetAsync($"/parkingLots");
        //    var responseBody = await response.Content.ReadAsStringAsync();
        //    List<ParkingLotDto> respondParkingLot = JsonConvert.DeserializeObject<List<ParkingLotDto>>(responseBody);
        //    //then
        //    var parkingLotDto2String = JsonConvert.SerializeObject(parkingLotDto2);
        //    var respondParkingLotString = JsonConvert.SerializeObject(respondParkingLot[0]);
        //    Assert.Equal(1, respondParkingLot.Count);
        //    //Assert.Equal(2, respondParkingLot.Count);
        //}
    }
}
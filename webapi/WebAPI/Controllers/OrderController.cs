using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using webapi.CQRS.Command.CommandOrder;
using webapi.CQRS.Query.QueryOrder;
using webapi.CQRS.ViewModels;
using webapi.WebAPI.Common;
using UpdateCartItemDTO = webapi.AppService.DTO.DTOCartItem.UpdateCartItemDTO;

namespace webapi.WebAPI.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class OrderControllerV1 : BaseController
    {

        //added 5:02 1/24/2023
        public OrderControllerV1(IMediator mediator, IMapper mapper) : base(mediator, mapper)
        {
        }


        [HttpGet("GetAllOrder")]
        public async Task<IActionResult> GetAllOrders()
        {
            return await Handle<IEnumerable<OrderViewModel>>(new GetAllOrdersQuery());
        }



        [HttpGet("GetOrderBy")]
        public async Task<IActionResult> GetOrderById([FromQuery] GetOrderByIdQuery query)
        {
            return await Handle<OrderViewModel>(query);
        }


        [HttpGet("GetAllOrderByStatus")]
        public async Task<IActionResult> GetAllOrderByStatus([FromQuery] GetAllOrderByStatusQuery query)
        {
            return await Handle<IEnumerable<OrderViewModel>>(query);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteOrder([FromQuery] DeleteOrderCommand dto)
        {
            //use UpdateCartItemDTO since they almost share the same method/properties
            return await Handle<UpdateCartItemDTO, DeleteOrderCommand, object>(dto);
        }
    }

    [ApiController]
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class OrderControllerV2 : BaseController
    {

        //added 5:02 1/24/2023
        public OrderControllerV2(IMediator mediator, IMapper mapper) : base(mediator, mapper)
        {
        }


        [HttpGet("GetAllOrderByStatic")]
        public async Task<IActionResult> GetAllOrderByStatus()
        {
            return await Handle<IEnumerable<OrderViewModel>>( new GetAllOrderByStaticQuery());
        }

    }
}

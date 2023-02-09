using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using webapi.AppService.DTO.DTOCheckout;
using webapi.CQRS.Command.CommandCheckout;
using webapi.WebAPI.Common;

namespace webapi.WebAPI.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class CheckoutOrderV1 : BaseController
    {

        //added 5:02 1/24/2023
        public CheckoutOrderV1(IMediator mediator, IMapper mapper) : base(mediator, mapper)
        {
        }

        [HttpPost]
        public async Task<IActionResult> CheckoutOrders([FromBody] CheckoutOrderDTO dto)
        {
            return await Handle<CheckoutOrderDTO, CheckoutOrderCommand, Guid>(dto);
        }

    }

    [ApiController]
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class CheckoutOrderV2 : BaseController
    {

        //added 5:02 1/24/2023
        public CheckoutOrderV2(IMediator mediator, IMapper mapper) : base(mediator, mapper)
        {
        }

        [HttpPost]
        public async Task<IActionResult> CheckoutOrders([FromBody] CheckoutOrderDTO dto)
        {
            return await Handle<CheckoutOrderDTO, CheckoutOrderCommand, Guid>(dto);
        }

    }
}

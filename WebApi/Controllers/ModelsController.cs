using Application.Features.Models.Commands.Create;
using Application.Features.Models.Queries.GetList;
using Application.Features.Models.Queries.GetListByDynamic;
using Core.Application.Requests;
using Core.Application.Responses;
using Core.Persistence.Dynamic;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ModelsController : BaseController
    {
        [HttpGet]
        public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
        {
            GetListModelQuery getListModelQuery = new() { PageRequest = pageRequest };
            GetListResponse<GetListModelListItemDto> response = await Mediator.Send(getListModelQuery);
            return Ok(response);
        }

        [HttpPost("GetList/ByDynamic")]
        public async Task<IActionResult> GetListByDynamic([FromQuery] PageRequest pageRequest, [FromBody] DynamicQuery? dynamicQuery = null)
        {
            GetListByDynamicModelQuery getListByDynamicModelQuery = new() { PageReguest = pageRequest, DynamicQuery = dynamicQuery };
            GetListResponse<GetListByDynamicModelListItemDto> response = await Mediator.Send(getListByDynamicModelQuery);
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CreateModelCommand createModelCommand)
        {
            CreatedModelResponse? createModelResponse = await Mediator.Send(createModelCommand);
            return Ok(createModelResponse);
        }
    }
}

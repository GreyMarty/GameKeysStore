using Application.UseCases.Keys.PurchaseKey;
using MediatR;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace UI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PurchaseController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly AuthenticationStateProvider _authenticationStateProvider;

        public PurchaseController(IMediator mediator, AuthenticationStateProvider authenticationStateProvider)
        {
            _mediator = mediator;
            _authenticationStateProvider = authenticationStateProvider;
        }

        [HttpGet]
        public async Task<IActionResult> Test() 
        {
            return Ok();
        }
    }
}

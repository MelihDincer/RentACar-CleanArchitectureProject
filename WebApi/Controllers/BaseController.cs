using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

public class BaseController : ControllerBase
{
    //_mediator?? kodunun anlamı: _mediator set edilmişse 
    //Eğer daha önce mediator enjecte edilmişse onu döndür. Yoksa(null ise) Injection (IoC) ortamına bak IMediator'ın karşılığını bana ver.
    private IMediator? _mediator;
    protected IMediator? Mediator => _mediator??= HttpContext.RequestServices.GetService<IMediator>();
}
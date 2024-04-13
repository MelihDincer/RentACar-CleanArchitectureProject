using Application.Features.Brands.Dtos;
using MediatR;

namespace Application.Features.Brands.Commands.Create;

public class CreateBrandCommand : IRequest<CreatedBrandResponse>
{
    public string Name { get; set; }

    //Sana CreateBrandCommand gönderilirse aşağıdaki handler'ın içini çalıştır demiş oluyoruz.
    public class CreateBrandCommandHandler : IRequestHandler<CreateBrandCommand, CreatedBrandResponse>
    {
        public Task<CreatedBrandResponse>? Handle(CreateBrandCommand request, CancellationToken cancellationToken)
        {
            CreatedBrandResponse createdBrandResponse = new();
            createdBrandResponse.Name = request.Name;
            createdBrandResponse.Id = new Guid();
            return null;        
        }
    }
}

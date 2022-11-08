using AutoMapper;

namespace Shop.Application.ApplicationServices;

public class SharedAppService : BaseAppService
{
    public SharedAppService(IMapper mapper) : base(mapper)
    {
    }
}
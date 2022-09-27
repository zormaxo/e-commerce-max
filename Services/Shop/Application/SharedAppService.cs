using AutoMapper;

namespace Application;

public class SharedAppService : BaseAppService
{
    public SharedAppService(IMapper mapper) : base(mapper)
    {
    }
}
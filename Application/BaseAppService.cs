using AutoMapper;

namespace Application;

public class BaseAppService
{
    protected readonly IMapper _mapper;

    public BaseAppService(IMapper mapper)
    {
        _mapper = mapper;
    }
}
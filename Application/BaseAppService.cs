using AutoMapper;

namespace Application;

public class BaseAppService
{
    protected readonly IMapper _mapper;
    protected readonly StoreContext _context;
    public BaseAppService(IMapper mapper)
    {
        _mapper = mapper;
    }

    public BaseAppService(IMapper mapper, StoreContext context) : this(mapper)
    {
        _context = context;
    }


}
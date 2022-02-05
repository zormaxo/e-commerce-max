using AutoMapper;
using Infrastructure.Data;

namespace Service.Base
{
  public class BaseService
  {
    protected readonly StoreContext _context;
    protected readonly IMapper _mapper;

    public BaseService(StoreContext context, IMapper mapper)
    {
      _mapper = mapper;
      _context = context;
    }

    public BaseService(IMapper mapper)
    {
      _mapper = mapper;
    }
  }
}
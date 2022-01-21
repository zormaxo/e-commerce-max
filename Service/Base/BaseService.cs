using AutoMapper;
using Infrastructure.Data;

namespace Service.Base
{
  public class BaseService : IBaseService
  {
    protected readonly StoreContext _context;
    protected readonly IMapper _mapper;

    public BaseService(StoreContext context, IMapper mapper)
    {
      _mapper = mapper;
      _context = context;
    }
  }
}
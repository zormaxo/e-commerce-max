using AutoMapper;
using Infrastructure.Data;

namespace Service
{
  public class BaseService
  {
    protected readonly IMapper _mapper;

    public BaseService(IMapper mapper)
    {
      _mapper = mapper;
    }
  }
}
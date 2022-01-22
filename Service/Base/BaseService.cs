using AutoMapper;
using Core.Interfaces;
using Infrastructure.Data;

namespace Service.Base
{
  public class BaseService : IBaseService
  { 
    protected  IMapper Mapper{ get; set; }
    protected  IUnitOfWork Uow{ get; set; }

    public BaseService(IMapper mapper, IUnitOfWork uow)
    {
      Mapper = mapper;
      Uow = uow;
    }    
  }
}
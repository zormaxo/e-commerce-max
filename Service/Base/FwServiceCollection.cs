using AutoMapper;
using Core.Interfaces;

namespace Service.Base
{
  public class FwServiceCollection
  {
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _uow;
    private ProductService _productSrv;

    public FwServiceCollection(IMapper mapper, IUnitOfWork uow)
    {
      _uow = uow;
      _mapper = mapper;
    }

    public ProductService ProductSrv => _productSrv = _productSrv ?? new ProductService(_mapper, _uow);
  }
}
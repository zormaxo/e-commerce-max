using AutoMapper;
using Shop.Core.HelperTypes;
using Shop.Persistence;

namespace Shop.Application.ApplicationServices;

public class BaseAppService
{
    protected IMapper Mapper { get; }

    protected StoreContext StoreContext { get; }

    protected CachedItems CachedItems { get; }

    public BaseAppService(IMapper mapper) { Mapper = mapper; }

    public BaseAppService(IMapper mapper, StoreContext context) : this(mapper) { StoreContext = context; }
    public BaseAppService(IMapper mapper, StoreContext context, CachedItems cachedItems) : this(mapper, context)
    { CachedItems = cachedItems; }
}
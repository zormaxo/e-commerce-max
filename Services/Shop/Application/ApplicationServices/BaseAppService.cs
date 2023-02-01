using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Shop.Core.HelperTypes;
using Shop.Persistence;

namespace Shop.Application.ApplicationServices;

public class BaseAppService
{
    protected IMapper Mapper { get; }

    protected StoreContext StoreContext { get; }

    protected CachedItems CachedItems { get; }

    public BaseAppService(IServiceProvider serviceProvider)
    {
        StoreContext = serviceProvider.GetService<StoreContext>();
        Mapper = serviceProvider.GetService<IMapper>();
        CachedItems = serviceProvider.GetService<CachedItems>();
    }
}
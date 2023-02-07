using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Shop.Application.Common.Interfaces.Repository;
using Shop.Core.HelperTypes;
using System.Security.Claims;

namespace Shop.Application.ApplicationServices;

public class BaseAppService
{
    protected IMapper Mapper { get; }

    protected IStoreContext StoreContext { get; }

    protected CachedItems CachedItems { get; }

    protected HttpContext HttpContext { get; }

    protected int? UserId { get; }

    public BaseAppService(IServiceProvider serviceProvider)
    {
        StoreContext = serviceProvider.GetService<IStoreContext>();
        Mapper = serviceProvider.GetService<IMapper>();
        CachedItems = serviceProvider.GetService<CachedItems>();
        HttpContext = serviceProvider.GetService<IHttpContextAccessor>().HttpContext;

        if (int.TryParse(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value, out int result))
        {
            UserId = result;
        }
    }
}
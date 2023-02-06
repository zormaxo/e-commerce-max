using System.Diagnostics.CodeAnalysis;

// This file is used by Code Analysis to maintain SuppressMessage
// attributes that are applied to this project.
// Project-level suppressions either have no target or are given
// a specific target and scoped to a namespace, type, member, etc.
[assembly: SuppressMessage(
    "Major Code Smell",
    "S125:Sections of code should not be commented out",
    Justification = "<Pending>",
    Scope = "member",
    Target = "~M:Shop.Application.CacheService.FillCacheItemsAsync(Shop.Persistence.StoreContext,Microsoft.Extensions.Logging.ILoggerFactory,AutoMapper.IMapper,Shop.Core.HelperTypes.CachedItems,RestSharp.RestClient)~System.Threading.Tasks.Task")]
[assembly: SuppressMessage(
    "Major Code Smell",
    "S3358:Ternary operators should not be nested",
    Justification = "<Pending>",
    Scope = "member",
    Target = "~M:Shop.Application.ApplicationServices.ProductBaseService`1.GetProducts``1(Shop.Core.HelperTypes.ProductSpecParams)~System.Threading.Tasks.Task{Shop.Core.Shared.Pagination{``0}}")]

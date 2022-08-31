using Application.Interfaces;
using AutoMapper;
using Core.Dtos;
using Core.Errors;

namespace Application;

public class UserAppService : BaseAppService
{
    private readonly IUserRepository _userRepository;

    public UserAppService(IUserRepository userRepository, IMapper mapper) : base(mapper)
    {
        _userRepository = userRepository;
    }

    public async Task<ApiResponse<IEnumerable<MemberDto>>> GetUsers()
    {
        var users = await _userRepository.GetMembersAsync();
        return new ApiResponse<IEnumerable<MemberDto>>(users);
    }

    public async Task<ApiResponse<MemberDto>> GetUser(int id)
    {
        var user = await _userRepository.GetMemberAsync(id);
        return new ApiResponse<MemberDto>(user);
    }
}
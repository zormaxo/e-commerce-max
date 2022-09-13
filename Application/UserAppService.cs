using Application.Interfaces;
using AutoMapper;
using Core.Dtos;
using System.Security.Claims;

namespace Application;

public class UserAppService : BaseAppService
{
    private readonly IUserRepository _userRepository;

    public UserAppService(IUserRepository userRepository, IMapper mapper) : base(mapper)
    {
        _userRepository = userRepository;
    }

    public async Task<IEnumerable<MemberDto>> GetUsers()
    {
        return await _userRepository.GetMembersAsync();
    }

    public async Task<MemberDto> GetUser(int id)
    {
        return await _userRepository.GetMemberAsync(id);
    }
 
    public async Task UpdateUser(MemberUpdateDto memberUpdateDto, string username)
    {
        var user = await _userRepository.GetUserByUsernameAsync(username);
        _mapper.Map(memberUpdateDto, user);
        await _userRepository.SaveAllAsync();
    }
}
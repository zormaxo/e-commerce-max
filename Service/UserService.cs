using API.Errors;
using AutoMapper;
using Core.DTOs;
using Core.Interfaces;

namespace Service
{
    public class UserService : BaseService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository, IMapper mapper) : base(mapper)
        {
            _userRepository = userRepository;
        }

        public async Task<ApiResponse<IEnumerable<MemberDto>>> GetUsers()
        {
            var users = await _userRepository.GetMembersAsync();
            return new ApiResponse<IEnumerable<MemberDto>>(users);
        }

        public async Task<ApiResponse<MemberDto>> GetUser(string username)
        {
            var user = await _userRepository.GetMemberAsync(username);
            return new ApiResponse<MemberDto>(user);
        }
    }
}
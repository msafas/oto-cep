using CarApp.DTOs;
using CarApp.DTOs.User;

namespace CarApp.Services.Interfaces;

public interface IUserService
{
    Task<Response<UserDto>> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<Response<List<UserDto>>> SearchAsync(SearchUserDto searchUserDto,CancellationToken cancellationToken);
    Task<Response<UserDto>> CreateAsync(CreateUserDto userDto, CancellationToken cancellationToken);
    Task<Response<UserDto>> UpdateAsync(Guid id,UpdateUserDto userDto, CancellationToken cancellationToken);
    Task<Response<NoContent>> DeleteAsync(Guid id, CancellationToken cancellationToken);
}
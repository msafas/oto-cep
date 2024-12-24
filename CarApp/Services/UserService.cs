using System.Net;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using CarApp.DTOs;
using CarApp.DTOs.User;
using CarApp.Entities;
using CarApp.Extensions;
using CarApp.Services.Interfaces;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace CarApp.Services;

public class UserService(IMongoDatabase database, IMapper mapper, ISharedIdentityService identityService) : IUserService
{
    private readonly IMongoCollection<User> _users = database.GetCollection<User>("User");

    private readonly IQueryable<User> _usersQueryable =
        database.GetCollection<User>("User").AsQueryable().Where(x => x.IsDeleted == false);

    public async Task<Response<UserDto>> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var user = await _usersQueryable.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

        if (user == null)
        {
            return Response<UserDto>.Fail("User not found");
        }

        return Response<UserDto>.Success(mapper.Map<UserDto>(user));
    }

    public async Task<Response<List<UserDto>>> SearchAsync(SearchUserDto searchUserDto,
        CancellationToken cancellationToken)
    {
        var query = _usersQueryable;

        if (!string.IsNullOrEmpty(searchUserDto.Name))
        {
            query = query.Where(x => x.Name.Contains(searchUserDto.Name));
        }

        if (!string.IsNullOrEmpty(searchUserDto.Surname))
        {
            query = query.Where(x => x.Surname.Contains(searchUserDto.Surname));
        }

        if (!string.IsNullOrEmpty(searchUserDto.Phone))
        {
            query = query.Where(x => x.Phone.Contains(searchUserDto.Phone));
        }

        if (!string.IsNullOrEmpty(searchUserDto.Email))
        {
            query = query.Where(x => x.Email.Contains(searchUserDto.Email));
        }

        query = query.Paginate(searchUserDto);

        var users = await query.ProjectTo<UserDto>(mapper.ConfigurationProvider).ToListAsync(cancellationToken);

        return Response<List<UserDto>>.Success(users);
    }

    public async Task<Response<UserDto>> CreateAsync(CreateUserDto userDto, CancellationToken cancellationToken)
    {
        var session = identityService.GetSession;

        var userExist = await _users.AsQueryable().Where(x => x.Phone == userDto.Phone || x.Email == userDto.Email)
            .AnyAsync(cancellationToken);

        if (userExist)
        {
            return Response<UserDto>.Fail("User with this email or phone number already exists",
                HttpStatusCode.BadRequest);
        }

        var user = mapper.Map<User>(userDto);
        user.SetBaseProperties(session);

        await _users.InsertOneAsync(user, cancellationToken: cancellationToken);

        return Response<UserDto>.Success(mapper.Map<UserDto>(user));
    }

    public async Task<Response<UserDto>> UpdateAsync(Guid id, UpdateUserDto userDto,
        CancellationToken cancellationToken)
    {
        var session = identityService.GetSession;

        var user = await _usersQueryable.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

        if (user == null)
        {
            return Response<UserDto>.Fail("User not found", HttpStatusCode.NotFound);
        }

        if (user.Email != userDto.Email)
        {
            var userExist = await _users.AsQueryable().Where(x => x.Email == userDto.Email)
                .AnyAsync(cancellationToken);

            if (userExist)
            {
                return Response<UserDto>.Fail("User with this email already exists", HttpStatusCode.BadRequest);
            }
        }

        if (user.Phone != userDto.Phone)
        {
            var userExist = await _users.AsQueryable().Where(x => x.Phone == userDto.Phone)
                .AnyAsync(cancellationToken);

            if (userExist)
            {
                return Response<UserDto>.Fail("User with this phone number already exists", HttpStatusCode.BadRequest);
            }
        }

        user = mapper.Map(userDto, user);

        user.SetBaseUpdateProperties(session);

        await _users.ReplaceOneAsync(x => x.Id == id, user, cancellationToken: cancellationToken);

        return Response<UserDto>.Success(mapper.Map<UserDto>(user));
    }

    public async Task<Response<NoContent>> DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        var session = identityService.GetSession;

        var user = await _usersQueryable.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

        if (user == null)
        {
            return Response<NoContent>.Fail("User not found", HttpStatusCode.NotFound);
        }

        user.SetBaseUpdateProperties(session);

        user.IsDeleted = true;

        await _users.ReplaceOneAsync(x => x.Id == id, user, cancellationToken: cancellationToken);

        return Response<NoContent>.Success(HttpStatusCode.NoContent);
    }
}
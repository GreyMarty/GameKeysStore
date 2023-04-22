using Application.Services;
using AutoMapper;
using Domain.Entities;
using FluentValidation;
using Infrastructure.Security.Helpers;
using Microsoft.Extensions.DependencyInjection;
using Application.Models.WriteModels;
using Application;
using Microsoft.EntityFrameworkCore;
using FluentValidation.Results;

namespace Infrastructure.Security.Services;

public interface IIdentityService
{
    Task AssignUserToRoleAsync(User user, RoleFlags flags);
    Task<User?> FindUserByLoginAsync(string login);
    IEnumerable<string> GetRolesFromFlags(RoleFlags flags);
    bool HasUserRoles(User user, RoleFlags roles);
    Task<User> RegisterUserAsync(UserWriteModel model);
    Task RemoveUserFromRoleAsync(User user, RoleFlags flags);
    Task SetUserRolesAsync(User user, RoleFlags flags);
}

[Service(ServiceLifetime.Scoped)]
public class IdentityService : IIdentityService
{
    private readonly IApplicationDbContext _db;
    private readonly IPasswordHelper _passwordHelper;
    private readonly IMapper _mapper;
    private readonly IValidator<UserWriteModel> _userValidator;

    public IdentityService(IPasswordHelper passwordHelper, IApplicationDbContext applicationDbContext, IMapper mapper,
        IValidator<UserWriteModel> userValidator)
    {
        _db = applicationDbContext;
        _passwordHelper = passwordHelper;
        _mapper = mapper;
        _userValidator = userValidator;
    }

    public async Task<User?> FindUserByLoginAsync(string login)
    {
        return await _db.Users.FirstOrDefaultAsync(x => x.UserName == login || x.Email == login);
    }

    public async Task<User> RegisterUserAsync(UserWriteModel model)
    {
        var validationResult = _userValidator.Validate(model);

        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }

        var errors = new List<ValidationFailure>();

        if (_db.Users.Any(e => e.UserName == model.UserName))
            errors.Add(new ValidationFailure(nameof(model.UserName), "User with the same username already exists"));

        if (_db.Users.Any(e => e.Email == model.Email))
            errors.Add(new ValidationFailure(nameof(model.Email), "User with the same e-mail already exists"));

        if (errors.Count > 0)
        {
            throw new ValidationException(errors);
        }

        var salt = _passwordHelper.GenerateSalt();
        var passwordHash = _passwordHelper.ComputeHash(model.Password!, salt);

        var user = _mapper.Map<User>(model);
        user.Salt = salt;
        user.Password = passwordHash;
        user.RoleFlags = (ulong)RoleFlags.User;

        await _db.Users.AddAsync(user);
        await _db.SaveChangesAsync();

        return user;  
    }

    public async Task AssignUserToRoleAsync(User user, RoleFlags flags)
    {
        var userRoles = (RoleFlags)user.RoleFlags;
        userRoles |= flags;

        user.RoleFlags = (ulong)userRoles;
        _db.Users.Update(user);
        await _db.SaveChangesAsync();
    }

    public async Task RemoveUserFromRoleAsync(User user, RoleFlags flags)
    {
        var userRoles = (RoleFlags)user.RoleFlags;
        userRoles &= ~flags;

        user.RoleFlags = (ulong)userRoles;
        _db.Users.Update(user);
        await _db.SaveChangesAsync();
    }

    public async Task SetUserRolesAsync(User user, RoleFlags flags)
    {
        user.RoleFlags = (ulong)flags;
        _db.Users.Update(user);
        await _db.SaveChangesAsync();
    }

    public bool HasUserRoles(User user, RoleFlags roles)
    {
        return (user.RoleFlags & (ulong)roles) != 0;
    }

    public IEnumerable<string> GetRolesFromFlags(RoleFlags flags)
    {
        foreach (var flag in Enum.GetValues<RoleFlags>())
        {
            if (flags.HasFlag(flag))
            {
                yield return Enum.GetName(flag)!.ToLower();
            }
        }
    }
}
using Application.DTOs;
using Application.Results;
using Application.Services;
using Application.Validation;
using AutoMapper;
using Domain.Entities;
using Domain.Repositories;
using FluentValidation;
using Infrastructure.Security.Helpers;
using Microsoft.Extensions.DependencyInjection;
using OneOf;
using OneOf.Types;

namespace Infrastructure.Security.Services;

public interface IIdentityService
{
    OneOf<User, NotFound> FindUserByLogin(string login);
    OneOf<User, ValidationFailed> RegisterUser(RegistrationUserDto model);
    OneOf<Success, NotFound> AssignUserToRole(string login, RoleFlags flags);
    OneOf<Success, NotFound> RemoveUserFromRole(string login, RoleFlags flags);
    OneOf<Success, NotFound> SetUserRoles(string login, RoleFlags flags);
    public OneOf<Success, NotFound> SetUserRoles(int userId, RoleFlags flags);
    bool HasUserRoles(User user, RoleFlags roles);
    public IEnumerable<string> GetRolesFromFlags(RoleFlags flags);
}

[Service(ServiceLifetime.Scoped)]
public class IdentityService : IIdentityService
{
    private readonly IPasswordHelper _passwordHelper;
    private readonly IUsersRepo _usersRepo;
    private readonly IMapper _mapper;
    private readonly IValidator<RegistrationUserDto> _registrationUserValidator;

    public IdentityService(IPasswordHelper passwordHelper, IUsersRepo usersRepo, IMapper mapper,
        IValidator<RegistrationUserDto> registrationUserValidator)
    {
        _passwordHelper = passwordHelper;
        _usersRepo = usersRepo;
        _mapper = mapper;
        _registrationUserValidator = registrationUserValidator;
    }

    public OneOf<User, NotFound> FindUserByLogin(string login)
    {
        var user = _usersRepo.Get(e => e.UserName == login || e.Email == login);
        return user is null ? new NotFound() : user;
    }

    public OneOf<User, ValidationFailed> RegisterUser(RegistrationUserDto model)
    {
        var validationResult = _registrationUserValidator.Validate(model);

        if (!validationResult.IsValid)
            return new ValidationFailed(_mapper.Map<IEnumerable<ValidationError>>(validationResult.Errors));

        var errors = new List<ValidationError>();

        if (_usersRepo.Any(e => e.UserName == model.UserName))
            errors.Add(new ValidationError(nameof(model.UserName), "User with the same username already exists"));

        if (_usersRepo.Any(e => e.Email == model.Email))
            errors.Add(new ValidationError(nameof(model.Email), "User with the same e-mail already exists"));

        if (errors.Count > 0) return new ValidationFailed(errors);

        var salt = _passwordHelper.GenerateSalt();
        var passwordHash = _passwordHelper.ComputeHash(model.Password, salt);

        var user = _mapper.Map<User>(model);
        user.Salt = salt;
        user.Password = passwordHash;
        user.RoleFlags = (ulong)RoleFlags.User;
        return _usersRepo.Add(user);
    }

    public OneOf<Success, NotFound> AssignUserToRole(string login, RoleFlags flags)
    {
        var findResult = FindUserByLogin(login);

        if (findResult.IsT1) return new NotFound();

        var user = findResult.AsT0;

        var userRoles = (RoleFlags)user.RoleFlags;
        userRoles |= flags;

        user.RoleFlags = (ulong)userRoles;
        _usersRepo.Update(user);

        return new Success();
    }

    public OneOf<Success, NotFound> RemoveUserFromRole(string login, RoleFlags flags)
    {
        var findResult = FindUserByLogin(login);

        if (findResult.IsT1) return new NotFound();

        var user = findResult.AsT0;

        var userRoles = (RoleFlags)user.RoleFlags;
        userRoles |= ~flags;

        user.RoleFlags = (ulong)userRoles;
        _usersRepo.Update(user);

        return new Success();
    }

    public OneOf<Success, NotFound> SetUserRoles(string login, RoleFlags flags)
    {
        var findResult = FindUserByLogin(login);

        if (findResult.IsT1) return new NotFound();

        var user = findResult.AsT0;

        user.RoleFlags = (ulong)flags;
        _usersRepo.Update(user);

        return new Success();
    }

    public OneOf<Success, NotFound> SetUserRoles(int userId, RoleFlags flags)
    {
        var user = _usersRepo.Get(userId);

        if (user is null) return new NotFound();

        user.RoleFlags = (ulong)flags;
        _usersRepo.Update(user);

        return new Success();
    }

    public bool HasUserRoles(User user, RoleFlags roles)
    {
        return (user.RoleFlags & (ulong)roles) != 0;
    }

    public IEnumerable<string> GetRolesFromFlags(RoleFlags flags)
    {
        foreach (var flag in Enum.GetValues<RoleFlags>())
            if (flags.HasFlag(flag))
                yield return Enum.GetName(flag)!.ToLower();
    }
}
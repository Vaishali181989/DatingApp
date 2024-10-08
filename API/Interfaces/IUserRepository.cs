using System;
using API.DTOs;
using API.Entities;

namespace API.Interfaces;

public interface IUserRepository
{
    void Update(AppUsers user);

    Task<bool>SaveAllAsync();
    Task<IEnumerable<AppUsers>> GetUsersAsync();
    Task<AppUsers?> GetUserByIdAsync(int id);
    Task<AppUsers?> GetUserByUsernameAsync(string username);
    Task<MemberDto?> GetMemberAsync(string username);
    Task<IEnumerable<MemberDto?>> GetmembersAsync();
}

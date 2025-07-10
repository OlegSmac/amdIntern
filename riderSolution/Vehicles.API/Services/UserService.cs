using MediatR;
using Vehicles.Application.Users.Admins.Queries;
using Vehicles.Application.Users.Companies.Queries;
using Vehicles.Application.Users.Users.Queries;
using Microsoft.AspNetCore.Identity;
using Vehicles.API.Models.DTOs.Users;
using Vehicles.API.Models.Requests.Users;
using Vehicles.Application.Users.Admins.Commands;
using Vehicles.Application.Users.Companies.Commands;
using Vehicles.Application.Users.Users.Commands;
using Vehicles.Domain.Users.Models;

namespace Vehicles.API.Services;

public static class UserService
{
    public static async Task<object?> GetUserByIdAsync(IMediator mediator, UserManager<ApplicationUser> userManager, string id)
    {
        var user = await userManager.FindByIdAsync(id);
        if (user == null) return null;

        var roles = await userManager.GetRolesAsync(user);
        var role = roles.FirstOrDefault();

        if (role == "Admin")
        {
            var command = new GetAdminById(id);
            var domain = await mediator.Send(command);
            
            return new AdminDTO
            {
                FirstName = domain.FirstName,
                LastName = domain.LastName,
                Email = user.Email ?? "",
                Phone = user.PhoneNumber ?? ""
            };
        }
        else if (role == "Company")
        {
            var command = new GetCompanyById(id);
            var domain = await mediator.Send(command);
            
            return new CompanyDTO
            {
                Id = domain.Id,
                Name = domain.Name,
                Email = user.Email ?? "",
                Phone = user.PhoneNumber ?? "",
                Description = domain.Description
            };
        }
        else
        {
            var command = new GetUserById(id);
            var domain = await mediator.Send(command);
            
            return new UserDTO
            {
                FirstName = domain.FirstName,
                LastName = domain.LastName,
                Email = user.Email ?? "",
                Phone = user.PhoneNumber ?? ""
            };
        }
    }

    public static async Task<string> UpdateUserAsync(IMediator mediator, UserManager<ApplicationUser> userManager, UpdateUserRequest request)
    {
        var user = await userManager.FindByIdAsync(request.Id);
        if (user == null) return "User not found";

        var roles = await userManager.GetRolesAsync(user);
        var role = roles.FirstOrDefault();

        if (role == "Admin")
        {
            var command = new UpdateAdmin(request.Id, request.FirstName, request.LastName);
            await mediator.Send(command);
        }
        else if (role == "User")
        {
            var command = new UpdateUser(request.Id, request.FirstName, request.LastName);
            await mediator.Send(command);
        }
        else if (role == "Company") {
            var command = new UpdateCompany(request.Id, request.CompanyName, request.Description);
            await mediator.Send(command);
        }
        
        user.PhoneNumber = request.Phone ?? user.PhoneNumber;
        
        if (!string.IsNullOrWhiteSpace(request.CurrentPassword) &&
            !string.IsNullOrWhiteSpace(request.NewPassword))
        {
            var result = await userManager.ChangePasswordAsync(user, request.CurrentPassword, request.NewPassword);
            if (!result.Succeeded) return "Password change failed: " + string.Join(", ", result.Errors.Select(e => e.Description));
        }
        
        var updateResult = await userManager.UpdateAsync(user);
        if (!updateResult.Succeeded) return "Update failed: " + string.Join(", ", updateResult.Errors.Select(e => e.Description));
        
        return "Successfully updated";
    }
}

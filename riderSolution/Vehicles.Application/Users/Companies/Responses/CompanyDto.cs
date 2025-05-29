using Vehicles.Domain.Users.Models;

namespace Vehicles.Application.Users.Companies.Responses;

public class CompanyDto
{
    public int Id { get; init; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string Description { get; set; }

    public static CompanyDto FromCompany(Company company)
    {
        return new CompanyDto()
        {
            Id = company.Id,
            Name = company.Name,
            Email = company.Email,
            Password = company.Password,
            Description = company.Description
        };
    }
}
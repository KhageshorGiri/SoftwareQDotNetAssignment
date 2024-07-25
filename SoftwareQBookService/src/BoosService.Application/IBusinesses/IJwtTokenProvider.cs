using BookService.Domain.Entities;

namespace BookService.Application.IBusinesses;

public interface IJwtTokenProvider
{
    string GenerateToken(ApplicationUser applicationUser, IEnumerable<string> roles);
}

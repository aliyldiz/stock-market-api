using webAPI.Models;

namespace webAPI.Interfaces;

public interface ITokenService
{
    string CreateToken(AppUser user);
}
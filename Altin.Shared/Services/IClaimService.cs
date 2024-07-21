namespace Altin.Shared;

public interface IClaimService
{
    string GetUserId();

    string GetClaim(string key);
}

namespace backend.Contracts
{

    public record class AuthResponce(
    Guid Id,
    string Email,
    string AccessJwt
    );
}

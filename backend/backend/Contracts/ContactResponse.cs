namespace backend.Contracts
{
    public record class ContactResponse(
        Guid Id,
        Guid? UserId,
        bool isGlobal,
        string Name,
        string Number,
        string Description 
    );
}

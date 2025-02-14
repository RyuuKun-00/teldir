namespace backend.Contracts
{
    public record class ContactResponse(
        Guid Id,
        string Name,
        string Number,
        string Description 
    );
}

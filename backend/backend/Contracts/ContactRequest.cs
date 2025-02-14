namespace backend.Contracts
{
    public record class ContactRequest(
        string Name,
        string Number,
        string Description
        );
}

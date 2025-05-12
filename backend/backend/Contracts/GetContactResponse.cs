namespace backend.Contracts
{
    public record class GetContactResponse
    (
        IEnumerable<ContactResponse> contacts,
        int count

    );
}

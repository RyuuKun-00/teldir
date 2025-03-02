namespace backend.Core.Models
{
    public class Contact
    {
        public Guid Id { get; }
        public string Name { get; } = string.Empty;
        public string Number { get; } = string.Empty;
        public string Description { get; } = string.Empty;

        public Contact(Guid id, string name, string number, string description)
        {
            Id = id;
            Name = name;
            Number = number;
            Description = description;
        }
    }
}

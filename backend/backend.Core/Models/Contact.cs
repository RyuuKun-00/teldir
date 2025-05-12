namespace backend.Core.Models
{
    public class Contact
    {
        public Guid Id { get; }
        public Guid? UserId { get; }

        public bool IsGlobal { get; } = false;
        public string Name { get; } = string.Empty;
        public string Number { get; } = string.Empty;
        public string Description { get; } = string.Empty;

        public Contact(Guid id, Guid? userId,bool isGlobal, string name, string number, string description)
        {
            Id = id;
            UserId = userId;
            IsGlobal = isGlobal;
            Name = name;
            Number = number;
            Description = description;
        }
    }
}

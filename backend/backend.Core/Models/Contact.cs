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

        public static (Contact Contact,string Error) Create( string name, string number, string description)
        {
            var error = string.Empty;

            if (string.IsNullOrEmpty(name))
            {
                error = "Contact name cannot be empty.";
            }
            if (string.IsNullOrEmpty(number))
            {
                error = $"Contact{(string.IsNullOrEmpty(name)? "name and":"")} number cannot be empty.";
            }

            var contact = new Contact(new Guid(), name, number, description);

            return (contact, error);
        }
    }
}

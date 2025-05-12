

namespace backend.DataAccess.Entities
{
    public class ContactEntity
    {
        public Guid Id { get; set; }
        public Guid? UserEntityId { get; set; }
        public UserEntity? User {  get; set; }
        public bool IsGlobal { get; set; } = false;
        public string Name { get; set; } = string.Empty;
        public string Number { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }
}

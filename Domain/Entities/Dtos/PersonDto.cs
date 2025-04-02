namespace Domain.Entities.Dtos
{
    public class PersonDto(Person person)
    {
        public Guid Id { get; set; } = person.Id;
        public string Name { get; set; } = person.Name;
        public string Email { get; set; } = person.Email;
        public bool Active { get; set; } = person.Active;

        public static implicit operator PersonDto(Person person)
            => new PersonDto(person);
    }
}

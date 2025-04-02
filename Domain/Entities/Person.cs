namespace Domain.Entities
{
    public class Person : BaseEntity
    {
        protected Person() { }
        private Person(string name, string email, bool active, int age, string address, string otherInformation, string interests, string feelings, string values)
        {
            Name = name;
            Email = email;
            Active = active;
            Age = age;
            Address = address;
            OtherInformation = otherInformation;
            Interests = interests;
            Feelings = feelings;
            Values = values;
        }

        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public bool Active { get; set; }
        public int? Age { get; set; }
        public string? Address { get; set; }
        public string? OtherInformation { get; set; }
        public string? Interests { get; set; }
        public string? Feelings { get; set; }
        public string? Values { get; set; }

        public static class Factory
        {
            public static Person Create(string name, string email, bool active, int age, string address, string otherInformation, string interests, string feelings, string values)
                => new Person(name, email, active, age, address, otherInformation, interests, feelings, values);
        }

    }
}

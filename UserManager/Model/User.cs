using Newtonsoft.Json;

namespace UserManager.Model
{
    public enum Gender
    {
        Male,
        Female
    }

    public class User
    {
        private const string ACTIVE = "active";
        private const string INACTIVE = "inactive";
        private const string MALE = "male";
        private const string FEMALE = "female";

        public int id { get; set; }
        public string? name { get; set; }
        public string? email { get; set; }
        public string? gender
        {
            get => UserGender == Gender.Male ? MALE : FEMALE;
            set => UserGender = value == MALE ? Gender.Male : Gender.Female;
        }
        public string? status
        {
            get => IsActive ? ACTIVE : INACTIVE;
            set => IsActive = value == ACTIVE;
        }

        [JsonIgnore]
        public bool IsActive { get; set; }

        [JsonIgnore]
        public Gender UserGender
        {
            get;
            set;
        }

        public User Clone()
        {
            var user = new User
            {
                id = id,
                name = name,
                email = email,
                gender = gender,
                status = status
            };

            return user;
        }

    }
}
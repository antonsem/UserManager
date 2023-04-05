using Newtonsoft.Json;
using System;

namespace UserManager
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

        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Gender
        {
            get => UserGender == UserManager.Gender.Male ? MALE : FEMALE;
            set => UserGender = value == MALE ? UserManager.Gender.Male : UserManager.Gender.Female;
        }
        public string? Status
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
                Id = Id,
                Name = Name,
                Email = Email,
                Gender = Gender,
                Status = Status
            };

            return user;
        }

    }
}
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace UserManager
{
    internal static class UserHelper
    {
        private const string USERS = "users";

        public static async Task<IList<User>?> GetUsers(HttpClient client)
        {
            using var result = await client.GetAsync(USERS);

            if (!result.IsSuccessStatusCode)
            {
                throw new Exception(result.ReasonPhrase);
            }

            var json = await result.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<List<User>>(json);
        }

        public static async Task<User?> UpdateUser(HttpClient client, User user)
        {
            var json = JsonConvert.SerializeObject(user);

            var buffer = Encoding.UTF8.GetBytes(json);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            using var result = await client.PutAsync($"{USERS}/{user.Id}", byteContent);

            if (result.IsSuccessStatusCode)
            {
                var updatedUserJson = await result.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<User>(updatedUserJson);
            }

            MessageBox.Show($"Could not update the user!\n{result.ReasonPhrase}", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            return null;
        }

        public static async Task<User?> DeleteUser(HttpClient client, User user)
        {
            var result = await client.DeleteAsync($"{USERS}/{user.Id}");

            if (result.IsSuccessStatusCode)
            {
                return user;
            }

            MessageBox.Show($"Could not delete the user!\n{result.ReasonPhrase}", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            return null;
        }

        public static string GetCSV(IList<User> users)
        {
            var csv = new StringBuilder("id;name;email;gender;status\n");

            foreach (var user in users)
            {
                csv.AppendLine($"{user.Id};{user.Name};{user.Email};{user.Gender};{user.Status}");
            }

            return csv.ToString();
        }
    }
}

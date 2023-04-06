using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using UserManager.Model;

namespace UserManager.Helpers
{
    internal static class UserHelper
    {
        private const string USERS = "users";

        public static async Task<GetRequestResult<IList<User>>> GetUsers(HttpClient client, int page = 0, int perPage = 10, string filter = "", string search = "")
        {
            var call = new StringBuilder($"{USERS}?");
            if (!string.IsNullOrWhiteSpace(filter) && !string.IsNullOrWhiteSpace(search))
            {
                call.Append($"{filter}={search}&");
            }
            call.Append($"page={page}&per_page={perPage}");

            using var result = await client.GetAsync(call.ToString());
            var requestResult = new GetRequestResult<IList<User>>(result);

            return requestResult;
        }

        public static async Task<RequestResult<User>> UpdateUser(HttpClient client, User user)
        {
            var byteContent = GetByteArrayContent(user);

            using var result = await client.PutAsync($"{USERS}/{user.id}", byteContent);

            var requestResult = new RequestResult<User>(result);

            return requestResult;
        }

        public static async Task<RequestResult<User>> DeleteUser(HttpClient client, User user)
        {
            var result = await client.DeleteAsync($"{USERS}/{user.id}");

            var requestResult = new RequestResult<User>(result)
            {
                Request = user
            };

            return requestResult;
        }

        public static async Task<RequestResult<User>> CreateUser(HttpClient client, User user)
        {
            var byteContent = GetByteArrayContent(user);

            using var result = await client.PostAsync(USERS, byteContent);

            var requestResult = new RequestResult<User>(result);

            return requestResult;
        }

        public static string GetCSV(IList<User> users)
        {
            var csv = new StringBuilder("id;name;email;gender;status\n");

            foreach (var user in users)
            {
                csv.AppendLine($"{user.id};{user.name};{user.email};{user.gender};{user.status}");
            }

            return csv.ToString();
        }

        private static ByteArrayContent GetByteArrayContent(object obj)
        {
            var json = JsonConvert.SerializeObject(obj);

            var buffer = Encoding.UTF8.GetBytes(json);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            return byteContent;
        }
    }
}

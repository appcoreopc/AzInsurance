using Newtonsoft.Json;
using System.IO;
using System.Threading.Tasks;

namespace AzCore.Shared.JsonSerializer
{
    public class MessageConverter
    {
        public static async Task<T> Deserialize<T>(Stream body)
        {
            string requestBody = await new StreamReader(body).ReadToEndAsync();
            return Deserialize<T>(requestBody);
        }

        public static T Deserialize<T>(string body)
        {
            T data = JsonConvert.DeserializeObject<T>(body);
            return data;
        }
    }
}

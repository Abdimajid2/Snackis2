using System.Text.Json;
using System.Text.Json.Serialization;

namespace Snackis2.DAL
{
    public class CategoryManager
    {
        private static Uri BasedAddress = new Uri("https://activepeopleapi.azurewebsites.net/");

        public static async Task<List<Models.Category>> GetAllCategoriesFromAPI()
        {
            List<Models.Category> categories = new List<Models.Category>();

            using (var client = new HttpClient())
            {
                client.BaseAddress = BasedAddress;
                HttpResponseMessage response = await client.GetAsync("api/Category");
                if (response.IsSuccessStatusCode)
                {
                    string responsestring = await response.Content.ReadAsStringAsync();
                    categories = JsonSerializer.Deserialize<List<Models.Category>>(responsestring);
                }
                return categories;
            }
        }
    }
}


using API.Model;
using API.ViewModel;
using Client.Base.Urls;
using Client.Repositories;
using Newtonsoft.Json;
using System.Text;

namespace Client.Repositories.Data
{
    public class EmployeeRepository : GeneralRepository<Employee, string>
    {
        private readonly Address address;
        private readonly string request;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly HttpClient httpClient;


        public EmployeeRepository(Address address, string request = "Employees/") : base(address, request)
        {
            this.address = address;
            this.request = request;
            _contextAccessor = new HttpContextAccessor();
            httpClient = new HttpClient
            {
                BaseAddress = new Uri(address.Link)
            };
        }

        public async Task<List<RegisterVMClient>> GetRegister()
        {
            List<RegisterVMClient> entities = new List<RegisterVMClient>();

            using (var response = await httpClient.GetAsync(request+"Register"))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                entities = JsonConvert.DeserializeObject<List<RegisterVMClient>>(apiResponse);
            }
            return entities;
        }

        //public async Task<LoginVM> Login()
        //{
        //    LoginVM entities = new LoginVM();

        //    using (var response = await httpClient.GetAsync(request + "Login"))
        //    {
        //        string apiResponse = await response.Content.ReadAsStringAsync();
        //        entities = JsonConvert.DeserializeObject<LoginVM>(apiResponse);
        //    }
        //    return entities;
        //}

        public async Task<JWTokenVM> Auth(LoginVM login)
        {
            JWTokenVM token = null;

            StringContent content = new StringContent(JsonConvert.SerializeObject(login), Encoding.UTF8, "application/json");
            var result = await httpClient.PostAsync(  request + "Login", content);

            string apiResponse = await result.Content.ReadAsStringAsync();
            token = JsonConvert.DeserializeObject<JWTokenVM>(apiResponse);

            return token;
        }

    }
}
using AutoBogus;
using CrudApiDotnet;
using CrudApiDotnet.Models.Usuarios;
using CrudTestsDotnet.Configurations;
using CrudWebDotnet.Models.Usuario;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace CrudTestsDotnet.Integrations.Controllers
{
    public class UsuarioControllerTests : IClassFixture<WebApplicationFactory<Startup>>, IAsyncLifetime
    {
        private readonly WebApplicationFactory<Startup> _factory;
        protected readonly ITestOutputHelper _output;
        protected readonly HttpClient _httpClient;
        protected RegistroViewModelInput _registroViewModelInput;
        protected LoginViewModelOutput _loginViewModelOutput;

        public UsuarioControllerTests(WebApplicationFactory<Startup> factory, ITestOutputHelper output)
        {
            _factory = factory;
            _output = output;
            _httpClient = _factory.CreateClient();
        }

        [Fact]
        public async Task Registrar_InformandoUsuarioSenhaExistentes_DeveRetornarSucesso()
        {
            _registroViewModelInput = new AutoFaker<RegistroViewModelInput>(AutoBogusConfiguration.LOCATE)
                .RuleFor(p => p.Email, faker => faker.Person.Email);

            var content = new StringContent(JsonConvert.SerializeObject(_registroViewModelInput), Encoding.UTF8, "application/json");

            var httpClientRequest = await _httpClient.PostAsync("api/v1/usuario/registrar", content);
            
            _output.WriteLine(await httpClientRequest.Content.ReadAsStringAsync());

            Assert.Equal(HttpStatusCode.Created, httpClientRequest.StatusCode);
        }

        [Fact]
        public async Task Logar_InformandoUsuarioSenhaExistentes_DeveRetornarSucesso()
        {
            var loginViewModelInput = new CrudApiDotnet.Models.Usuarios.LoginViewModelInput
            { 
                Login = _registroViewModelInput.Login,
                Senha = _registroViewModelInput.Senha
            };

            var content = new StringContent(JsonConvert.SerializeObject(loginViewModelInput), Encoding.UTF8, "application/json");
            
            var httpClientRequest = await _httpClient.PostAsync("api/v1/usuario/logar", content);

            _loginViewModelOutput = JsonConvert.DeserializeObject<LoginViewModelOutput>(await httpClientRequest.Content.ReadAsStringAsync());

            Assert.Equal(HttpStatusCode.OK, httpClientRequest.StatusCode);
            Assert.NotNull(_loginViewModelOutput.Token);
            Assert.Equal(loginViewModelInput.Login, _loginViewModelOutput.Usuario.Login);
            _output.WriteLine($"{nameof(UsuarioControllerTests)} : {nameof(Logar_InformandoUsuarioSenhaExistentes_DeveRetornarSucesso)} : { await httpClientRequest.Content.ReadAsStringAsync()}");
        }

        public async Task InitializeAsync()
        {
            await Registrar_InformandoUsuarioSenhaExistentes_DeveRetornarSucesso();
            await Logar_InformandoUsuarioSenhaExistentes_DeveRetornarSucesso();
        }

        public async Task DisposeAsync()
        {
            _factory.Dispose();
        }
    }
}

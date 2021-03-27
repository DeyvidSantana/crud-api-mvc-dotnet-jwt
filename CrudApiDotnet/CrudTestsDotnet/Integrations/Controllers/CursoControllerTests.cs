using AutoBogus;
using CrudApiDotnet;
using CrudApiDotnet.Models.Cursos;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace CrudTestsDotnet.Integrations.Controllers
{
    public class CursoControllerTests : UsuarioControllerTests
    {
        public CursoControllerTests(WebApplicationFactory<Startup> factory, ITestOutputHelper output) : base(factory, output)
        {
        }

        [Fact]
        public async Task Registrar_InformandoCursoValidoUsuarioAutenticado_DeveRetornarSucesso()
        {
            var cursoViewModelInput = new AutoFaker<CursoViewModelInput>();

            var content = new StringContent(JsonConvert.SerializeObject(cursoViewModelInput.Generate()), Encoding.UTF8, "application/json");

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _loginViewModelOutput.Token);

            var httpClientRequest = await _httpClient.PostAsync("api/v1/cursos", content);
            
            _output.WriteLine($"{nameof(CursoControllerTests)} : {nameof(Registrar_InformandoCursoValidoUsuarioAutenticado_DeveRetornarSucesso)} : { await httpClientRequest.Content.ReadAsStringAsync()}");
            Assert.Equal(HttpStatusCode.Created, httpClientRequest.StatusCode);
        }

        [Fact]
        public async Task Registrar_InformandoCursoValidoUsuarioNaoAutenticado_DeveRetornarSucesso()
        {
            var cursoViewModelInput = new AutoFaker<CursoViewModelInput>();

            var content = new StringContent(JsonConvert.SerializeObject(cursoViewModelInput.Generate()), Encoding.UTF8, "application/json");

            var httpClientRequest = await _httpClient.PostAsync("api/v1/cursos", content);

            _output.WriteLine($"{nameof(CursoControllerTests)} : {nameof(Registrar_InformandoCursoValidoUsuarioNaoAutenticado_DeveRetornarSucesso)} : { await httpClientRequest.Content.ReadAsStringAsync()}");
            Assert.Equal(HttpStatusCode.Unauthorized, httpClientRequest.StatusCode);
        }

        [Fact]
        public async Task Obter_InformandoUsuarioAutenticadp_DeveRetornarSucesso()
        {
            await Registrar_InformandoCursoValidoUsuarioAutenticado_DeveRetornarSucesso();

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _loginViewModelOutput.Token);

            var httpClientRequest = await _httpClient.GetAsync("api/v1/cursos"); 
            
            _output.WriteLine($"{nameof(CursoControllerTests)} : {nameof(Obter_InformandoUsuarioAutenticadp_DeveRetornarSucesso)}");
            var cursos = JsonConvert.DeserializeObject<IList<CursoViewModelOutput>>(await httpClientRequest.Content.ReadAsStringAsync());

            Assert.NotNull(cursos);
            Assert.Equal(HttpStatusCode.Created, httpClientRequest.StatusCode);
        }
    }
}

using CrudApiDotnet.Business.Entidades;
using CrudApiDotnet.Business.Repositories;
using CrudApiDotnet.Configurations;
using CrudApiDotnet.Filters;
using CrudApiDotnet.Models;
using CrudApiDotnet.Models.Usuarios;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Threading.Tasks;

namespace CrudApiDotnet.Controllers
{
    [Route("api/v1/usuario")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IAuthenticationService _authenticationService;

        public UsuarioController(
            IUsuarioRepository usuarioRepository, 
            IAuthenticationService authenticationService)
        {
            _usuarioRepository = usuarioRepository;
            _authenticationService = authenticationService;
        }


        /// <summary>
        /// Este serviço permite autenticar um usuário cadastrado e ativo
        /// </summary>
        /// <param name="loginViewModelInput"></param>
        /// <returns>Retorna status ok, dados de usuario e o token em caso de sucesso</returns>
        [SwaggerResponse(statusCode: 200, description: "Sucesso ao autenticar", Type = typeof(LoginViewModelInput))]
        [SwaggerResponse(statusCode: 400, description: "Campos obrigatórios", Type = typeof(ValidaCampoViewModelOutput))]
        [SwaggerResponse(statusCode: 500, description: "Erro interno", Type = typeof(ErroGenericoViewModel))]
        [HttpPost]
        [Route("logar")]
        [ValidacaoModelStateCustomizado]
        public async Task<IActionResult> Logar(LoginViewModelInput loginViewModelInput)
        {
            var usuario = await _usuarioRepository.ObterUsuarioAsync(loginViewModelInput.Login);

            if (usuario == null)
                return BadRequest("Houve um erro ao tentar acessar");

            //if(usuario.Senha != loginViewModelInput.Senha.GerarSenhaCriptografada())
            //    return BadRequest("Houve um erro ao tentar acessar");

            var usuarioViewModelOutput = new UsuarioViewModelOutput
            {
                Codigo = usuario.Codigo,
                Login = loginViewModelInput.Login,
                Email = usuario.Email
            };

            var token = _authenticationService.GerarToken(usuarioViewModelOutput);

            return Ok(new 
            { 
                Token = token,
                Usuario = usuarioViewModelOutput
            });
        }

        /// <summary>
        /// Este método permite cadastrar um usuário não existente
        /// </summary>
        /// <param name="registroViewModelInput">View model de registro de login</param>
        /// <returns></returns>
        [HttpPost]
        [Route("registrar")]
        [ValidacaoModelStateCustomizado]      
        [SwaggerResponse(statusCode: 200, description: "Sucesso ao autenticar", Type = typeof(LoginViewModelInput))]
        [SwaggerResponse(statusCode: 400, description: "Campos obrigatórios", Type = typeof(ValidaCampoViewModelOutput))]
        [SwaggerResponse(statusCode: 500, description: "Erro interno", Type = typeof(ErroGenericoViewModel))]
        public async Task<IActionResult> Registrar(RegistroViewModelInput registroViewModelInput)
        {
            var usuario = await _usuarioRepository.ObterUsuarioAsync(registroViewModelInput.Login);

            if(usuario != null)
            {
                return BadRequest("Usuário já cadastrado");
            }

            usuario = new Usuario
            {
                Login = registroViewModelInput.Login,
                Email = registroViewModelInput.Email,
                Senha = registroViewModelInput.Senha
            };

            _usuarioRepository.Adicionar(usuario);
            _usuarioRepository.Commit();

            return Created("", registroViewModelInput);
        }
    }
}

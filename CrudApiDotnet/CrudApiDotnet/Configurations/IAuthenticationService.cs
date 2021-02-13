using CrudApiDotnet.Models.Usuarios;

namespace CrudApiDotnet.Configurations
{
    public interface IAuthenticationService
    {
        string GerarToken(UsuarioViewModelOutput usuarioViewModelOutput);
    }
}

using CrudApiDotnet.Business.Entidades;
using System.Threading.Tasks;

namespace CrudApiDotnet.Business.Repositories
{
    public interface IUsuarioRepository
    {
        void Adicionar(Usuario usuario);
        void Commit();
        Task<Usuario> ObterUsuarioAsync(string login);
    }
}

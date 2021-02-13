using CrudApiDotnet.Business.Entidades;
using System.Collections.Generic;

namespace CrudApiDotnet.Business.Repositories
{
    public interface ICursoRepository
    {
        void Adicionar(Curso curso);
        void Commit();
        IList<Curso> ObterPorUsuario(int codigoUsuario);
    }
}

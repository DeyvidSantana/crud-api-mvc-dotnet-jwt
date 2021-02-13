using CrudWebDotnet.Models.Curso;
using Refit;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CrudWebDotnet.Services
{
    public interface ICursoService
    {
        [Post("/api/v1/cursos")]
        [Headers("Authorization: Bearer")]
        Task<RegistrarCursoViewModelOutput> Registrar(RegistrarCursoViewModelInput registrarCursoViewModelInput);

        [Get("/api/v1/cursos")]
        [Headers("Authorization: Bearer")]
        Task<IList<ListarCursoViewModelOutput>> Obter();
    }
}

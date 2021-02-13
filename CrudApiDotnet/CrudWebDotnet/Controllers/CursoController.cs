using CrudWebDotnet.Models.Curso;
using CrudWebDotnet.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace CrudWebDotnet.Controllers
{
    public class CursoController : Controller
    {
        private readonly ICursoService _cursoService;

        public CursoController(ICursoService cursoService)
        {
            _cursoService = cursoService;
        }

        public IActionResult Cadastrar()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Cadastrar(RegistrarCursoViewModelInput registrarCursoViewModelInput)
        {
            try
            {
                var curso = await _cursoService.Registrar(registrarCursoViewModelInput);

                ModelState.AddModelError("", $"O curso foi cadastrado com sucesso {curso.Nome}");
            }
            catch (Refit.ApiException ex)
            {
                ModelState.AddModelError("", ex.Message);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
            }

            return View();
        }

        [Authorize]
        public async Task<IActionResult> Listar()
        {
            var cursos = await _cursoService.Obter();

            return View(cursos);
        }
    }
}

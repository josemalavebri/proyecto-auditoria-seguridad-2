using front_auditoria.Respository.Encuesta;
using front_auditoria.Respository.Lugares;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace proyecto_auditoria_seguridad.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IServiceProvider _serviceProvider;
        public IndexModel(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        public async Task<JsonResult> OnGetSugerenciasDepartamentoAsync(string termino)
        {
            if (string.IsNullOrWhiteSpace(termino))
                return new JsonResult(new List<string>());

            var repoDepartamentos = _serviceProvider.GetRequiredService<IRepositoryDepartamentos>();
            var resultados = await repoDepartamentos.BuscarDepartamentosAsync(termino);
            return new JsonResult(resultados);
        }

        public async Task<JsonResult> OnGetSugerenciasFacultadAsync(string termino)
        {
            if (string.IsNullOrWhiteSpace(termino))
                return new JsonResult(new List<string>());

            var repoFacultades = _serviceProvider.GetRequiredService<IRepositoryFacultades>();
            var resultados = await repoFacultades.BuscarFacultadesAsync(termino);
            return new JsonResult(resultados);
        }

        public async Task<JsonResult> OnGetSugerenciasDireccionAsync(string termino)
        {
            if (string.IsNullOrWhiteSpace(termino))
                return new JsonResult(new List<string>());

            var repoDirecciones = _serviceProvider.GetRequiredService<IRepositoryDirecciones>();
            var resultados = await repoDirecciones.BuscarDireccionesAsync(termino);
            return new JsonResult(resultados);
        }

        public async Task<IActionResult> OnGetUltimaEjecucionFiltradaAsync(
       string nombreDepartamento,
       string nombreDireccion,
       string nombreFacultad = null)
        {
            // Validar parámetros obligatorios
            if (string.IsNullOrWhiteSpace(nombreDepartamento) || string.IsNullOrWhiteSpace(nombreDireccion))
            {
                return new JsonResult(new { mensaje = "Parámetros departamento y dirección son obligatorios." });
            }

            var repo = _serviceProvider.GetRequiredService<IRepositoryGet>();
            var resultado = await repo.GetUltimaEjecucionFiltradaAsync(
                nombreDepartamento.Trim(),
                nombreDireccion.Trim(),
                string.IsNullOrWhiteSpace(nombreFacultad) ? null : nombreFacultad.Trim()
            );

            if (resultado == null)
            {
                return new JsonResult(new { mensaje = "No se encontraron datos para los filtros proporcionados." });
            }

            return new JsonResult(resultado);
        }

    }
}

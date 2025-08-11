using front_auditoria.Respository.Encuesta;
using front_auditoria.Respository.Lugares;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using proyecto_auditoria_seguridad.Repository.EncuestaEjecucion;

namespace front_auditoria.Pages
{
    public class CreadorEncuestasModel : PageModel
    {
        private readonly IServiceProvider _serviceProvider;
        public CreadorEncuestasModel(IServiceProvider serviceProvider)
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

        public async Task<JsonResult> OnGetPreguntasItemsAsync(int idEncuesta)
        {
            if (idEncuesta <= 0)
                return new JsonResult(new { success = false, message = "ID inválido" });

            var repository = _serviceProvider.GetRequiredService<IRepositoryAuditoriaDetalles>();

            var datos = await repository.ObtenerUltimaAuditoriaDetallesAsync(idEncuesta);

            return new JsonResult(new { success = true, data = datos });
        }
    }
}

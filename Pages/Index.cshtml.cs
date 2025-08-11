using front_auditoria.Respository.Encuesta;
using front_auditoria.Respository.Lugares;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using proyecto_auditoria_seguridad.Repository.EncuestaEjecucion;
using static front_auditoria.Respository.Encuesta.RepositoryEncuestaEjecucion;

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
            // Validar par�metros obligatorios
            if (string.IsNullOrWhiteSpace(nombreDepartamento) || string.IsNullOrWhiteSpace(nombreDireccion))
            {
                return new JsonResult(new { mensaje = "Par�metros departamento y direcci�n son obligatorios." });
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

        public async Task<IActionResult> OnGetResumenAsync(
      string nombreDepartamento,
      string nombreDireccion,
      string nombreFacultad = null)
        {
            var repo = _serviceProvider.GetRequiredService<IRepositoryResumenAuditoriaEncuesta>();

            // Pasar los par�metros al m�todo async
            var resumen = await repo.ObtenerResumenAuditoriasAsync(
                nombreDepartamento, nombreDireccion, nombreFacultad);

            return new JsonResult(resumen);
        }


        public async Task<IActionResult> OnGetDetalleEncuestaAsync(int idEncuesta)
        {
            var repo = _serviceProvider.GetRequiredService<IDetalleEncuestaRepository>();

            var detalle = await repo.ObtenerPreguntasPorEncuestaAsync(idEncuesta);

            return new JsonResult(detalle);
        }

        public async Task<IActionResult> OnGetBuscarEncuestasAsync(string nombreDepartamento, string nombreDireccion, string nombreFacultad)
        {
            if (string.IsNullOrEmpty(nombreDepartamento) || string.IsNullOrEmpty(nombreDireccion))
            {
                return BadRequest("Faltan par�metros requeridos.");
            }

            // Obtener el repositorio dentro del m�todo usando _serviceProvider
            var repo = _serviceProvider.GetRequiredService<IRepositoryGet>();

            // Llamar al m�todo del repositorio
            var resultado = await repo.GetUltimaEjecucionFiltradaAsync(nombreDepartamento, nombreDireccion, nombreFacultad);

            if (resultado == null)
                return new JsonResult(new List<EncuestaEjecucion>()); // lista vac�a

            return new JsonResult(new[] { resultado });
        }


    }
}

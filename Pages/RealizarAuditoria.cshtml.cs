using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using proyecto_auditoria_seguridad.Repository.EncuestaEjecucion;

namespace proyecto_auditoria_seguridad.Pages
{
    public class RealizarAuditoriaModel : PageModel
    {
        private readonly IServiceProvider _serviceProvider;

        public RealizarAuditoriaModel(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task<IActionResult> OnGetResumenProgramadasAsync()
        {
            try
            {
                // Obtener la instancia del repositorio
                var repo = _serviceProvider.GetRequiredService<IRepositoryResumenAuditoriaProgramada>();

                // Llamar al m�todo que obtiene todos los registros sin filtros
                var resumen = await repo.ObtenerResumenAuditoriasProgramadasAsync();

                // Devolver los datos como JSON
                return new JsonResult(resumen);
            }
            catch (Exception ex)
            {
                // Esto es �til para la depuraci�n
                Console.WriteLine($"Error en OnGetResumenProgramadasAsync: {ex.Message}");
                return new JsonResult(new { error = "Ocurri� un error al cargar los datos." });
            }
        }


    }
}

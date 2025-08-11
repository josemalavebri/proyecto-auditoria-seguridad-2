using Microsoft.EntityFrameworkCore;
using proyecto_auditoria_seguridad.Models;
using System.Text.Json;
using System.Threading.Tasks;
using static proyecto_auditoria_seguridad.Repository.EncuestaEjecucion.RepositoryResumenAuditoriaEncuesta;

namespace proyecto_auditoria_seguridad.Repository.EncuestaEjecucion
{
    interface IRepositoryResumenAuditoriaEncuesta
    {
        Task<List<ResumenAuditoriaEncuestaDTO>> ObtenerResumenAuditoriasAsync(string nombreDepartamento, string nombreDireccion, string nombreFacultad = null);
    }

    public class RepositoryResumenAuditoriaEncuesta : IRepositoryResumenAuditoriaEncuesta
    {
        private readonly EncuestaDBContext _context;

        public RepositoryResumenAuditoriaEncuesta(EncuestaDBContext context)
        {
            _context = context;
        }

        public async Task<List<ResumenAuditoriaEncuestaDTO>> ObtenerResumenAuditoriasAsync( // Cambiado a Task<List<...>>
            string nombreDepartamento,
            string nombreDireccion,
            string nombreFacultad = null)
        {
            var query = from a in _context.Auditorias
                        join pa in _context.Personas on a.idPersonaAuditada equals pa.idPersona
                        join au in _context.Personas on a.idAuditor equals au.idPersona
                        join e in _context.Encuestas on a.idEncuesta equals e.idEncuesta

                        join ui in _context.UbicacionesInstitucionales on e.idUbicacionInstitucional equals ui.idUbicacionInstitucional
                        join d in _context.Departamentos on ui.idDepartamento equals d.idDepartamento
                        join dir in _context.Direcciones on ui.idDireccion equals dir.idDireccion
                        join f in _context.Facultades on ui.idFacultad equals f.idFacultad into facultadGroup
                        from f in facultadGroup.DefaultIfEmpty() // LEFT JOIN facultad

                        join p in _context.Preguntas on e.idEncuesta equals p.idEncuesta into preguntasGroup
                        from p in preguntasGroup.DefaultIfEmpty()

                        join pi in _context.PreguntasItems on p.idPregunta equals pi.idPregunta into itemsGroup
                        from pi in itemsGroup.DefaultIfEmpty()

                        where d.nombre == nombreDepartamento
                              && dir.nombre == nombreDireccion
                              && (string.IsNullOrEmpty(nombreFacultad) || (f != null && f.nombre == nombreFacultad))

                        group new { p, pi } by new
                        {
                            a.fechaAuditoria,
                            PersonaAuditada = pa.nombre + " " + pa.apellido,
                            Auditor = au.nombre + " " + au.apellido,
                            e.idEncuesta,
                            e.descripcion
                        } into g
                        select new ResumenAuditoriaEncuestaDTO
                        {
                            fechaAuditoria = g.Key.fechaAuditoria,
                            PersonaAuditada = g.Key.PersonaAuditada,
                            Auditor = g.Key.Auditor,
                            idEncuesta = g.Key.idEncuesta,
                            DescripcionEncuesta = g.Key.descripcion,
                            CantidadPreguntas = g.Select(x => x.p != null ? x.p.idPregunta : (int?)null).Where(id => id != null).Distinct().Count(),
                            CantidadItems = g.Select(x => x.pi != null ? x.pi.idItem : (int?)null).Where(id => id != null).Distinct().Count()
                        };

            var lista = await query.OrderByDescending(r => r.fechaAuditoria).ToListAsync();
            return lista; 
        }

        public class ResumenAuditoriaEncuestaDTO
        {
            public int idEncuesta { get; set; }
            public DateTime fechaAuditoria { get; set; }
            public string PersonaAuditada { get; set; } = null!;
            public string Auditor { get; set; } = null!;
            public string DescripcionEncuesta { get; set; } = null!;
            public int CantidadPreguntas { get; set; }
            public int CantidadItems { get; set; }
        }
    }
}
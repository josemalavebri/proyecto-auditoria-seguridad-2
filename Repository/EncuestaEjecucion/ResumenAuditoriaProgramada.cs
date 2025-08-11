using Microsoft.EntityFrameworkCore;
using proyecto_auditoria_seguridad.Models;
using static proyecto_auditoria_seguridad.Repository.EncuestaEjecucion.RepositoryResumenAuditoriaEncuesta;

namespace proyecto_auditoria_seguridad.Repository.EncuestaEjecucion
{
    interface IRepositoryResumenAuditoriaProgramada
    {
        Task<List<ResumenAuditoriaProgramadaDTO>> ObtenerResumenAuditoriasProgramadasAsync();
    }

    public class RepositoryResumenAuditoriaProgramada : IRepositoryResumenAuditoriaProgramada
    {
        private readonly EncuestaDBContext _context;

        public RepositoryResumenAuditoriaProgramada(EncuestaDBContext context)
        {
            _context = context;
        }

        public async Task<List<ResumenAuditoriaProgramadaDTO>> ObtenerResumenAuditoriasProgramadasAsync()
        {
            var query = from ap in _context.AuditoriasProgramadas
                        join e in _context.Encuestas on ap.idEncuesta equals e.idEncuesta into eGroup
                        from e in eGroup.DefaultIfEmpty()

                        join ui in _context.UbicacionesInstitucionales on e.idUbicacionInstitucional equals ui.idUbicacionInstitucional into uiGroup
                        from ui in uiGroup.DefaultIfEmpty()

                        join d in _context.Departamentos on ui.idDepartamento equals d.idDepartamento into dGroup
                        from d in dGroup.DefaultIfEmpty()

                        join dir in _context.Direcciones on ui.idDireccion equals dir.idDireccion into dirGroup
                        from dir in dirGroup.DefaultIfEmpty()

                        join f in _context.Facultades on ui.idFacultad equals f.idFacultad into fGroup
                        from f in fGroup.DefaultIfEmpty() // LEFT JOIN facultad

                        select new ResumenAuditoriaProgramadaDTO
                        {
                            Direccion = dir != null ? dir.nombre : null,
                            Departamento = d != null ? d.nombre : null,
                            Facultad = f != null ? f.nombre : null,
                            IdEncuesta = e != null ? e.idEncuesta : 0, // Añadir manejo de nulos
                            FechaProgramada = ap.fechaProgramada,
                            Descripcion = e != null ? e.descripcion : null // Añadir manejo de nulos
                        };

            return await query
                .OrderByDescending(r => r.FechaProgramada)
                .ToListAsync();
        }
    }


    public class ResumenAuditoriaProgramadaDTO
    {
        public string Direccion { get; set; }
        public string Departamento { get; set; }
        public string Facultad { get; set; } // Puede ser null si no hay facultad
        public int IdEncuesta { get; set; }
        public DateTime FechaProgramada { get; set; }
        public string Descripcion { get; set; }
    }
}

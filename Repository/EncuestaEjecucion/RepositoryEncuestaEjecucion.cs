using Microsoft.EntityFrameworkCore;
using proyecto_auditoria_seguridad.Models;
using System.Text.Json;

namespace front_auditoria.Respository.Encuesta
{
    public class RepositoryEncuestaEjecucion : IRepositoryGet
    {

        public RepositoryEncuestaEjecucion(EncuestaDBContext _context)
        {
            Context = _context;
        }

        public EncuestaDBContext Context { get; }

        public async Task<EncuestaEjecucion> GetUltimaEjecucionFiltradaAsync(
    string nombreDepartamento,
    string nombreDireccion,
    string nombreFacultad = null)
        {
            var query = from ee in Context.Auditorias
                        join e in Context.Encuestas on ee.idEncuesta equals e.idEncuesta
                        join ui in Context.UbicacionesInstitucionales on e.idUbicacionInstitucional equals ui.idUbicacionInstitucional
                        join dep in Context.Departamentos on ui.idDepartamento equals dep.idDepartamento
                        join d in Context.Direcciones on ui.idDireccion equals d.idDireccion
                        join f in Context.Facultades on ui.idFacultad equals f.idFacultad into facultadJoin
                        from f in facultadJoin.DefaultIfEmpty()
                        where (nombreDepartamento == null || dep.nombre == nombreDepartamento)
                           && (nombreDireccion == null || d.nombre == nombreDireccion)
                           && (string.IsNullOrEmpty(nombreFacultad) || (f != null && f.nombre == nombreFacultad))
                        orderby ee.fechaAuditoria descending
                        select new EncuestaEjecucion
                        {
                            IdEncuesta = e.idEncuesta,
                            FechaEjecucion = ee.fechaAuditoria,
                            Descripcion = e.descripcion
                        };

            return await query.FirstOrDefaultAsync();
        }


        public class EncuestaEjecucion
        {
            public int IdEncuesta { get; set; }   // nuevo
            public string Descripcion { get; set; }
            public DateTime FechaEjecucion { get; set; }
        }
    }
}

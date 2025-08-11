using Microsoft.EntityFrameworkCore;
using proyecto_auditoria_seguridad.Models;

namespace proyecto_auditoria_seguridad.Repository.EncuestaEjecucion
{
    public class RepositoryAuditoriaDetalles : IRepositoryAuditoriaDetalles
    {
        private readonly EncuestaDBContext _context;

        public RepositoryAuditoriaDetalles(EncuestaDBContext context)
        {
            _context = context;
        }

        public async Task<List<EncuestaAuditadaDto>> ObtenerUltimaAuditoriaDetallesAsync(int idEncuesta)
        {
            // Obtener la fecha máxima de auditoría para esa encuesta
            var ultimaFechaAuditoria = await _context.Auditorias
                .Where(a => a.idEncuesta == idEncuesta)
                .MaxAsync(a => (DateTime?)a.fechaAuditoria);

            if (ultimaFechaAuditoria == null)
            {
                // No hay auditorías para esa encuesta
                return new List<EncuestaAuditadaDto>();
            }

            var resultado = await (
                from a in _context.Auditorias
                join e in _context.Encuestas on a.idEncuesta equals e.idEncuesta
                join p in _context.Preguntas on e.idEncuesta equals p.idEncuesta
                join pi in _context.PreguntasItems on p.idPregunta equals pi.idPregunta
                join i in _context.Items on pi.idItem equals i.idItem
                where a.idEncuesta == idEncuesta && a.fechaAuditoria == ultimaFechaAuditoria.Value
                orderby p.idPregunta
                select new EncuestaAuditadaDto
                {
                    IdEncuesta = e.idEncuesta,
                    DescripcionEncuesta = e.descripcion,
                    FechaAuditoria = a.fechaAuditoria,
                    Pregunta = p.descripcion,
                    Item = i.descripcion
                }).ToListAsync();

            return resultado;
        }
    }

    public class EncuestaAuditadaDto
    {
        public int IdEncuesta { get; set; }
        public string DescripcionEncuesta { get; set; } = null!;
        public DateTime FechaAuditoria { get; set; }
        public string Pregunta { get; set; } = null!;
        public string Item { get; set; } = null!;
    }

}

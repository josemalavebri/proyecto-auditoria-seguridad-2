using Microsoft.EntityFrameworkCore;
using proyecto_auditoria_seguridad.Models;

namespace proyecto_auditoria_seguridad.Repository.EncuestaEjecucion
{
    public interface IDetalleEncuestaRepository
    {
        Task<List<DetalleEncuestaRepository.PreguntaResultadoDto>> ObtenerPreguntasPorEncuestaAsync(int idEncuesta);
    }

    public class DetalleEncuestaRepository : IDetalleEncuestaRepository
    {
        private readonly EncuestaDBContext _context;

        public DetalleEncuestaRepository(EncuestaDBContext context)
        {
            _context = context;
        }

        public async Task<List<PreguntaResultadoDto>> ObtenerPreguntasPorEncuestaAsync(int idEncuesta)
        {
            var resultado = await (from e in _context.Encuestas
                                   join a in _context.Auditorias on e.idEncuesta equals a.idEncuesta
                                   join r in _context.RespuestasItems on a.idAuditoria equals r.idAuditoria
                                   join pi in _context.PreguntasItems on r.idPreguntaItem equals pi.idPreguntaItem
                                   join p in _context.Preguntas on pi.idPregunta equals p.idPregunta
                                   join i in _context.Items on pi.idItem equals i.idItem
                                   where e.idEncuesta == idEncuesta
                                   select new PreguntaResultadoDto
                                   {
                                       Pregunta = p.descripcion,
                                       Titulo = i.titulo,
                                       Codigo = i.codigo,
                                       Descripcion = i.descripcion,
                                       PorcentajeCumplimiento = r.porcentajeCumplimiento,
                                       Comentario = r.comentario
                                   }).ToListAsync();

            return resultado;
        }


        public class PreguntaResultadoDto
        {
            public string Pregunta { get; set; }
            public string Titulo { get; set; }
            public string Codigo { get; set; }
            public string Descripcion { get; set; }
            public decimal? PorcentajeCumplimiento { get; set; }
            public string Comentario { get; set; }
        }
    }
}

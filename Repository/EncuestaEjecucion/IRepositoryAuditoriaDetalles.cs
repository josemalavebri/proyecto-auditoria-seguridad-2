namespace proyecto_auditoria_seguridad.Repository.EncuestaEjecucion
{
    public interface IRepositoryAuditoriaDetalles
    {
        Task<List<EncuestaAuditadaDto>> ObtenerUltimaAuditoriaDetallesAsync(int idEncuesta);
    }
}

using static front_auditoria.Respository.Encuesta.RepositoryEncuestaEjecucion;

namespace front_auditoria.Respository.Encuesta
{
    public interface IRepositoryGet
    {
        Task<EncuestaEjecucion> GetUltimaEjecucionFiltradaAsync(
            string nombreDepartamento,
            string nombreDireccion,
            string nombreFacultad = null);
    }

}

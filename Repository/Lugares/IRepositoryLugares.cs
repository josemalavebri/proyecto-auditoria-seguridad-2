namespace front_auditoria.Respository.Lugares
{
    public interface IRepositoryDepartamentos
    {
        Task<List<string>> BuscarDepartamentosAsync(string termino);
    }

    public interface IRepositoryFacultades
    {
        Task<List<string>> BuscarFacultadesAsync(string termino);
    }

    public interface IRepositoryDirecciones
    {
        Task<List<string>> BuscarDireccionesAsync(string termino);
    }

}
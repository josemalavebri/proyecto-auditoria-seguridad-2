using Microsoft.EntityFrameworkCore;
using proyecto_auditoria_seguridad.Models;

namespace front_auditoria.Respository.Lugares
{
        public class RepositoryDepartamentos : IRepositoryDepartamentos
        {
            private readonly EncuestaDBContext _context;

            public RepositoryDepartamentos(EncuestaDBContext context)
            {
                _context = context;
            }

            public async Task<List<string>> BuscarDepartamentosAsync(string termino)
            {
                return await _context.Departamentos
                    .Where(d => d.nombre.StartsWith(termino))
                    .Select(d => d.nombre)
                    .ToListAsync();
            }
        }

        public class RepositoryFacultades : IRepositoryFacultades
        {
            private readonly EncuestaDBContext _context;

            public RepositoryFacultades(EncuestaDBContext context)
            {
                _context = context;
            }

            public async Task<List<string>> BuscarFacultadesAsync(string termino)
            {
                return await _context.Facultades
                    .Where(f => f.nombre.StartsWith(termino))
                    .Select(f => f.nombre)
                    .ToListAsync();
            }
        }

        public class RepositoryDirecciones : IRepositoryDirecciones
        {
            private readonly EncuestaDBContext _context;

            public RepositoryDirecciones(EncuestaDBContext context)
            {
                _context = context;
            }

            public async Task<List<string>> BuscarDireccionesAsync(string termino)
            {
                return await _context.Direcciones
                    .Where(dir => dir.nombre.StartsWith(termino))
                    .Select(dir => dir.nombre)
                    .ToListAsync();
            }
        }
    }

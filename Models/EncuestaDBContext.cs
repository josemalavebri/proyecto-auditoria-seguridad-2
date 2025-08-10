using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace proyecto_auditoria_seguridad.Models;

public partial class EncuestaDBContext : DbContext
{
    public EncuestaDBContext(DbContextOptions<EncuestaDBContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Departamentos> Departamentos { get; set; }

    public virtual DbSet<Direcciones> Direcciones { get; set; }

    public virtual DbSet<EjecucionesEncuesta> EjecucionesEncuesta { get; set; }

    public virtual DbSet<Encuestas> Encuestas { get; set; }

    public virtual DbSet<Facultades> Facultades { get; set; }

    public virtual DbSet<Items> Items { get; set; }

    public virtual DbSet<ItemsEncuesta> ItemsEncuesta { get; set; }

    public virtual DbSet<Personas> Personas { get; set; }

    public virtual DbSet<PreguntasBase> PreguntasBase { get; set; }

    public virtual DbSet<PreguntasEncuesta> PreguntasEncuesta { get; set; }

    public virtual DbSet<PreguntasEncuestaItems> PreguntasEncuestaItems { get; set; }

    public virtual DbSet<RespuestasItems> RespuestasItems { get; set; }

    public virtual DbSet<Roles> Roles { get; set; }

    public virtual DbSet<Secciones> Secciones { get; set; }

    public virtual DbSet<UbicacionesInstitucionales> UbicacionesInstitucionales { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Departamentos>(entity =>
        {
            entity.HasKey(e => e.idDepartamento).HasName("PK__Departam__C225F98DCC123A94");

            entity.Property(e => e.nombre).HasMaxLength(150);
        });

        modelBuilder.Entity<Direcciones>(entity =>
        {
            entity.HasKey(e => e.idDireccion).HasName("PK__Direccio__B49878C9C9563C5F");

            entity.Property(e => e.nombre).HasMaxLength(150);
        });

        modelBuilder.Entity<EjecucionesEncuesta>(entity =>
        {
            entity.HasKey(e => e.idEjecucion).HasName("PK__Ejecucio__BC52C2F0E9349094");

            entity.Property(e => e.fechaEjecucion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.idEncuestaNavigation).WithMany(p => p.EjecucionesEncuesta)
                .HasForeignKey(d => d.idEncuesta)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Ejecucion__idEnc__4CA06362");
        });

        modelBuilder.Entity<Encuestas>(entity =>
        {
            entity.HasKey(e => e.idEncuesta).HasName("PK__Encuesta__C03F985769F083BD");

            entity.Property(e => e.descripcion).HasMaxLength(300);
            entity.Property(e => e.fechaCreacion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.idEncuestaAnteriorNavigation).WithMany(p => p.InverseidEncuestaAnteriorNavigation)
                .HasForeignKey(d => d.idEncuestaAnterior)
                .HasConstraintName("FK__Encuestas__idEnc__47DBAE45");

            entity.HasOne(d => d.idUbicacionInstitucionalNavigation).WithMany(p => p.Encuestas)
                .HasForeignKey(d => d.idUbicacionInstitucional)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Encuestas__idUbi__48CFD27E");
        });

        modelBuilder.Entity<Facultades>(entity =>
        {
            entity.HasKey(e => e.idFacultad).HasName("PK__Facultad__B57E5B20F3219F3C");

            entity.Property(e => e.nombre).HasMaxLength(150);
        });

        modelBuilder.Entity<Items>(entity =>
        {
            entity.HasKey(e => e.idItem).HasName("PK__Items__AD1942682CFEED3B");

            entity.Property(e => e.activo).HasDefaultValue(true);
            entity.Property(e => e.codigo).HasMaxLength(50);
            entity.Property(e => e.descripcion).HasMaxLength(300);
            entity.Property(e => e.titulo).HasMaxLength(150);
            entity.Property(e => e.version).HasDefaultValue(1);
        });

        modelBuilder.Entity<ItemsEncuesta>(entity =>
        {
            entity.HasKey(e => e.idItemsEncuesta).HasName("PK__ItemsEnc__E38BFEBC40378C44");

            entity.HasIndex(e => new { e.idItem, e.idEncuesta }, "UQ__ItemsEnc__C11ABBEC6BF4D525").IsUnique();

            entity.Property(e => e.porcentajeCumplimiento).HasColumnType("decimal(5, 2)");

            entity.HasOne(d => d.idEncuestaNavigation).WithMany(p => p.ItemsEncuesta)
                .HasForeignKey(d => d.idEncuesta)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ItemsEncu__idEnc__60A75C0F");

            entity.HasOne(d => d.idItemNavigation).WithMany(p => p.ItemsEncuesta)
                .HasForeignKey(d => d.idItem)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ItemsEncu__idIte__5FB337D6");
        });

        modelBuilder.Entity<Personas>(entity =>
        {
            entity.HasKey(e => e.idPersona).HasName("PK__Personas__A478814194867ECB");

            entity.Property(e => e.apellido).HasMaxLength(100);
            entity.Property(e => e.nombre).HasMaxLength(100);

            entity.HasOne(d => d.idRolNavigation).WithMany(p => p.Personas)
                .HasForeignKey(d => d.idRol)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Personas__idRol__398D8EEE");
        });

        modelBuilder.Entity<PreguntasBase>(entity =>
        {
            entity.HasKey(e => e.idPreguntaBase).HasName("PK__Pregunta__2F1652A030A591A1");

            entity.Property(e => e.activo).HasDefaultValue(true);
            entity.Property(e => e.descripcion).HasMaxLength(300);
            entity.Property(e => e.fechaCreacion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.version).HasDefaultValue(1);
        });

        modelBuilder.Entity<PreguntasEncuesta>(entity =>
        {
            entity.HasKey(e => e.idPreguntaEncuesta).HasName("PK__Pregunta__58BFE1E4C7B51E9C");

            entity.Property(e => e.descripcion).HasMaxLength(300);

            entity.HasOne(d => d.idEncuestaNavigation).WithMany(p => p.PreguntasEncuesta)
                .HasForeignKey(d => d.idEncuesta)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Preguntas__idEnc__5629CD9C");

            entity.HasOne(d => d.idPreguntaBaseNavigation).WithMany(p => p.PreguntasEncuesta)
                .HasForeignKey(d => d.idPreguntaBase)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Preguntas__idPre__571DF1D5");

            entity.HasOne(d => d.idSeccionNavigation).WithMany(p => p.PreguntasEncuesta)
                .HasForeignKey(d => d.idSeccion)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Preguntas__idSec__5812160E");
        });

        modelBuilder.Entity<PreguntasEncuestaItems>(entity =>
        {
            entity.HasKey(e => e.idPreguntasEncuestaItems).HasName("PK__Pregunta__F1946E0DE7786BC6");

            entity.HasOne(d => d.idItemNavigation).WithMany(p => p.PreguntasEncuestaItems)
                .HasForeignKey(d => d.idItem)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Preguntas__idIte__6477ECF3");

            entity.HasOne(d => d.idPreguntaEncuestaNavigation).WithMany(p => p.PreguntasEncuestaItems)
                .HasForeignKey(d => d.idPreguntaEncuesta)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Preguntas__idPre__6383C8BA");
        });

        modelBuilder.Entity<RespuestasItems>(entity =>
        {
            entity.HasKey(e => e.idRespuestaItem).HasName("PK__Respuest__B77D05C83BF0C170");

            entity.Property(e => e.comentario).HasMaxLength(500);
            entity.Property(e => e.fechaRespuesta)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.idEjecucionNavigation).WithMany(p => p.RespuestasItems)
                .HasForeignKey(d => d.idEjecucion)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Respuesta__idEje__693CA210");

            entity.HasOne(d => d.idPreguntasEncuestaItemsNavigation).WithMany(p => p.RespuestasItems)
                .HasForeignKey(d => d.idPreguntasEncuestaItems)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Respuesta__idPre__68487DD7");
        });

        modelBuilder.Entity<Roles>(entity =>
        {
            entity.HasKey(e => e.idRol).HasName("PK__Roles__3C872F76495A5AE6");

            entity.Property(e => e.nombre).HasMaxLength(100);
        });

        modelBuilder.Entity<Secciones>(entity =>
        {
            entity.HasKey(e => e.idSeccion).HasName("PK__Seccione__94B87A7CC56295A9");

            entity.Property(e => e.descripcion).HasMaxLength(200);
        });

        modelBuilder.Entity<UbicacionesInstitucionales>(entity =>
        {
            entity.HasKey(e => e.idUbicacionInstitucional).HasName("PK__Ubicacio__686A1E04960F65D5");

            entity.HasOne(d => d.idDepartamentoNavigation).WithMany(p => p.UbicacionesInstitucionales)
                .HasForeignKey(d => d.idDepartamento)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Ubicacion__idDep__440B1D61");

            entity.HasOne(d => d.idDireccionNavigation).WithMany(p => p.UbicacionesInstitucionales)
                .HasForeignKey(d => d.idDireccion)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Ubicacion__idDir__4316F928");

            entity.HasOne(d => d.idFacultadNavigation).WithMany(p => p.UbicacionesInstitucionales)
                .HasForeignKey(d => d.idFacultad)
                .HasConstraintName("FK__Ubicacion__idFac__4222D4EF");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

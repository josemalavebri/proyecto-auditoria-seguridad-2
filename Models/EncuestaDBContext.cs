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

    public virtual DbSet<Auditorias> Auditorias { get; set; }

    public virtual DbSet<AuditoriasProgramadas> AuditoriasProgramadas { get; set; }

    public virtual DbSet<Departamentos> Departamentos { get; set; }

    public virtual DbSet<Direcciones> Direcciones { get; set; }

    public virtual DbSet<Encuestas> Encuestas { get; set; }

    public virtual DbSet<Facultades> Facultades { get; set; }

    public virtual DbSet<Items> Items { get; set; }

    public virtual DbSet<Personas> Personas { get; set; }

    public virtual DbSet<Preguntas> Preguntas { get; set; }

    public virtual DbSet<PreguntasItems> PreguntasItems { get; set; }

    public virtual DbSet<RespuestasItems> RespuestasItems { get; set; }

    public virtual DbSet<Roles> Roles { get; set; }

    public virtual DbSet<UbicacionesInstitucionales> UbicacionesInstitucionales { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Auditorias>(entity =>
        {
            entity.HasKey(e => e.idAuditoria).HasName("PK__Auditori__F1F30701C1F0ED96");

            entity.HasIndex(e => e.idEncuesta, "UQ__Auditori__C03F9856F0A7C75E").IsUnique();

            entity.Property(e => e.fechaAuditoria)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.idAuditorNavigation).WithMany(p => p.AuditoriasidAuditorNavigation)
                .HasForeignKey(d => d.idAuditor)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Auditoria__idAud__4F7CD00D");

            entity.HasOne(d => d.idEncuestaNavigation).WithOne(p => p.Auditorias)
                .HasForeignKey<Auditorias>(d => d.idEncuesta)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Auditoria__idEnc__4D94879B");

            entity.HasOne(d => d.idPersonaAuditadaNavigation).WithMany(p => p.AuditoriasidPersonaAuditadaNavigation)
                .HasForeignKey(d => d.idPersonaAuditada)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Auditoria__idPer__4E88ABD4");
        });

        modelBuilder.Entity<AuditoriasProgramadas>(entity =>
        {
            entity.HasKey(e => e.idProgramacion).HasName("PK__Auditori__EA62461D04790AD3");

            entity.Property(e => e.fechaProgramada).HasColumnType("datetime");

            entity.HasOne(d => d.idEncuestaNavigation).WithMany(p => p.AuditoriasProgramadas)
                .HasForeignKey(d => d.idEncuesta)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Auditoria__idEnc__5FB337D6");
        });

        modelBuilder.Entity<Departamentos>(entity =>
        {
            entity.HasKey(e => e.idDepartamento).HasName("PK__Departam__C225F98D841D6156");

            entity.Property(e => e.nombre).HasMaxLength(150);
        });

        modelBuilder.Entity<Direcciones>(entity =>
        {
            entity.HasKey(e => e.idDireccion).HasName("PK__Direccio__B49878C9B8D9731C");

            entity.Property(e => e.nombre).HasMaxLength(150);
        });

        modelBuilder.Entity<Encuestas>(entity =>
        {
            entity.HasKey(e => e.idEncuesta).HasName("PK__Encuesta__C03F98573BFF40DF");

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
            entity.HasKey(e => e.idFacultad).HasName("PK__Facultad__B57E5B20FE1F6772");

            entity.Property(e => e.nombre).HasMaxLength(150);
        });

        modelBuilder.Entity<Items>(entity =>
        {
            entity.HasKey(e => e.idItem).HasName("PK__Items__AD194268C2DDF7FC");

            entity.Property(e => e.codigo).HasMaxLength(50);
            entity.Property(e => e.descripcion).HasMaxLength(300);
            entity.Property(e => e.titulo).HasMaxLength(150);
        });

        modelBuilder.Entity<Personas>(entity =>
        {
            entity.HasKey(e => e.idPersona).HasName("PK__Personas__A47881417207335D");

            entity.Property(e => e.apellido).HasMaxLength(100);
            entity.Property(e => e.nombre).HasMaxLength(100);

            entity.HasOne(d => d.idRolNavigation).WithMany(p => p.Personas)
                .HasForeignKey(d => d.idRol)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Personas__idRol__398D8EEE");
        });

        modelBuilder.Entity<Preguntas>(entity =>
        {
            entity.HasKey(e => e.idPregunta).HasName("PK__Pregunta__623EEC422E0A3E6D");

            entity.Property(e => e.descripcion).HasMaxLength(300);

            entity.HasOne(d => d.idEncuestaNavigation).WithMany(p => p.Preguntas)
                .HasForeignKey(d => d.idEncuesta)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Preguntas__idEnc__52593CB8");
        });

        modelBuilder.Entity<PreguntasItems>(entity =>
        {
            entity.HasKey(e => e.idPreguntaItem).HasName("PK__Pregunta__01F57902E2B761D8");

            entity.HasOne(d => d.idItemNavigation).WithMany(p => p.PreguntasItems)
                .HasForeignKey(d => d.idItem)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Preguntas__idIte__5812160E");

            entity.HasOne(d => d.idPreguntaNavigation).WithMany(p => p.PreguntasItems)
                .HasForeignKey(d => d.idPregunta)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Preguntas__idPre__571DF1D5");
        });

        modelBuilder.Entity<RespuestasItems>(entity =>
        {
            entity.HasKey(e => e.idRespuestaItem).HasName("PK__Respuest__B77D05C8EFC4BCB8");

            entity.Property(e => e.comentario).HasMaxLength(500);
            entity.Property(e => e.fechaRespuesta)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.porcentajeCumplimiento).HasColumnType("decimal(5, 2)");

            entity.HasOne(d => d.idAuditoriaNavigation).WithMany(p => p.RespuestasItems)
                .HasForeignKey(d => d.idAuditoria)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Respuesta__idAud__5CD6CB2B");

            entity.HasOne(d => d.idPreguntaItemNavigation).WithMany(p => p.RespuestasItems)
                .HasForeignKey(d => d.idPreguntaItem)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Respuesta__idPre__5BE2A6F2");
        });

        modelBuilder.Entity<Roles>(entity =>
        {
            entity.HasKey(e => e.idRol).HasName("PK__Roles__3C872F76507599EB");

            entity.Property(e => e.nombre).HasMaxLength(100);
        });

        modelBuilder.Entity<UbicacionesInstitucionales>(entity =>
        {
            entity.HasKey(e => e.idUbicacionInstitucional).HasName("PK__Ubicacio__686A1E04B045C2A3");

            entity.HasOne(d => d.idDepartamentoNavigation).WithMany(p => p.UbicacionesInstitucionales)
                .HasForeignKey(d => d.idDepartamento)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Ubicacion__idDep__4316F928");

            entity.HasOne(d => d.idDireccionNavigation).WithMany(p => p.UbicacionesInstitucionales)
                .HasForeignKey(d => d.idDireccion)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Ubicacion__idDir__440B1D61");

            entity.HasOne(d => d.idFacultadNavigation).WithMany(p => p.UbicacionesInstitucionales)
                .HasForeignKey(d => d.idFacultad)
                .HasConstraintName("FK__Ubicacion__idFac__4222D4EF");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DL;

public partial class JwtexamenBibliotecaContext : DbContext
{
    public JwtexamenBibliotecaContext()
    {
    }

    public JwtexamenBibliotecaContext(DbContextOptions<JwtexamenBibliotecaContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Area> Areas { get; set; }

    public virtual DbSet<Autor> Autors { get; set; }

    public virtual DbSet<Colonium> Colonia { get; set; }

    public virtual DbSet<Direccion> Direccions { get; set; }

    public virtual DbSet<Editorial> Editorials { get; set; }

    public virtual DbSet<Estado> Estados { get; set; }

    public virtual DbSet<Idioma> Idiomas { get; set; }

    public virtual DbSet<Medio> Medios { get; set; }

    public virtual DbSet<Municipio> Municipios { get; set; }

    public virtual DbSet<Rol> Rols { get; set; }

    public virtual DbSet<TipoMedio> TipoMedios { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    /*protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=.;Database=JWTExamenBiblioteca;Integrated Security=True;TrustServerCertificate=True");*/

    public virtual DbSet<UsuarioGetAllSP> UsuarioGetAllSP { get; set; }
    public virtual DbSet<AutorGetAllSP> AutorGetAllSP { get; set; }
    public virtual DbSet<MedioGetAllSP> MedioGetAllSP { get; set; }
    public virtual DbSet<MedioGetByIdSP> MedioGetByIdSP { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.UseCollation("SQL_Latin1_General_CP1_CI_AS");

        //Agregando los DTOs
        modelBuilder.Entity<UsuarioGetAllSP>(entity =>
        {
            entity.HasNoKey();
        });

        modelBuilder.Entity<MedioGetAllSP>(entity =>
        {
            entity.HasNoKey();
        });

        modelBuilder.Entity<AutorGetAllSP>(entity =>
        {
            entity.HasNoKey();
        });

        modelBuilder.Entity<MedioGetByIdSP>(entity =>
        {
            entity.HasNoKey();
        });



        modelBuilder.Entity<Area>(entity =>
        {
            entity.HasKey(e => e.IdArea).HasName("PK__Area__2FC141AADA4CE70E");

            entity.ToTable("Area");

            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false)
                .UseCollation("Latin1_General_BIN");
        });

        modelBuilder.Entity<Autor>(entity =>
        {
            entity.HasKey(e => e.IdAutor).HasName("PK__Autor__DD33B031616097F4");

            entity.ToTable("Autor");

            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Colonium>(entity =>
        {
            entity.HasKey(e => e.IdColonia).HasName("PK__Colonia__A1580F6698DB679D");

            entity.HasIndex(e => e.CodigoPostal, "idx_CP_Colonia");

            entity.Property(e => e.IdColonia).ValueGeneratedNever();
            entity.Property(e => e.CodigoPostal)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.IdMunicipioNavigation).WithMany(p => p.Colonia)
                .HasForeignKey(d => d.IdMunicipio)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Colonia__IdMunic__2F10007B");
        });

        modelBuilder.Entity<Direccion>(entity =>
        {
            entity.HasKey(e => e.IdDireccion).HasName("PK__Direccio__1F8E0C76B46036AA");

            entity.ToTable("Direccion");

            entity.Property(e => e.Calle)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.NumeroExterior)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.NumeroInterior)
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.HasOne(d => d.IdColoniaNavigation).WithMany(p => p.Direccions)
                .HasForeignKey(d => d.IdColonia)
                .HasConstraintName("FK__Direccion__IdCol__35BCFE0A");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.Direccions)
                .HasForeignKey(d => d.IdUsuario)
                .HasConstraintName("FK__Direccion__IdUsu__36B12243");
        });

        modelBuilder.Entity<Editorial>(entity =>
        {
            entity.HasKey(e => e.IdEditorial).HasName("PK__Editoria__EF838671E46B168E");

            entity.ToTable("Editorial");

            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false)
                .UseCollation("Latin1_General_BIN");
        });

        modelBuilder.Entity<Estado>(entity =>
        {
            entity.HasKey(e => e.IdEstado).HasName("PK__Estado__FBB0EDC10A846AD8");

            entity.ToTable("Estado");

            entity.HasIndex(e => e.Nombre, "idx_NombreEstado");

            entity.Property(e => e.IdEstado).ValueGeneratedNever();
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Idioma>(entity =>
        {
            entity.HasKey(e => e.IdIdioma).HasName("PK__Idioma__C867BD362D44E282");

            entity.ToTable("Idioma");

            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .UseCollation("Latin1_General_BIN");
        });

        modelBuilder.Entity<Medio>(entity =>
        {
            entity.HasKey(e => e.IdMedio).HasName("PK__Medio__EF80186098B6359D");

            entity.ToTable("Medio");

            entity.Property(e => e.ArchivoPdf).HasColumnName("ArchivoPDF");
            entity.Property(e => e.Descripcion).HasColumnType("text");
            entity.Property(e => e.Titulo)
                .HasMaxLength(150)
                .IsUnicode(false);

            entity.HasOne(d => d.IdAreaNavigation).WithMany(p => p.Medios)
                .HasForeignKey(d => d.IdArea)
                .HasConstraintName("FK__Medio__IdArea__21B6055D");

            entity.HasOne(d => d.IdEditorialNavigation).WithMany(p => p.Medios)
                .HasForeignKey(d => d.IdEditorial)
                .HasConstraintName("FK__Medio__IdEditori__20C1E124");

            entity.HasOne(d => d.IdIdiomaNavigation).WithMany(p => p.Medios)
                .HasForeignKey(d => d.IdIdioma)
                .HasConstraintName("FK__Medio__IdIdioma__22AA2996");

            entity.HasOne(d => d.IdTipoMedioNavigation).WithMany(p => p.Medios)
                .HasForeignKey(d => d.IdTipoMedio)
                .HasConstraintName("FK__Medio__IdTipoMed__1FCDBCEB");

            entity.HasMany(d => d.IdAutors).WithMany(p => p.IdMedios)
                .UsingEntity<Dictionary<string, object>>(
                    "MedioAutor",
                    r => r.HasOne<Autor>().WithMany()
                        .HasForeignKey("IdAutor")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__MedioAuto__IdAut__267ABA7A"),
                    l => l.HasOne<Medio>().WithMany()
                        .HasForeignKey("IdMedio")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__MedioAuto__IdMed__25869641"),
                    j =>
                    {
                        j.HasKey("IdMedio", "IdAutor").HasName("PK__MedioAut__E25323639E73F9FB");
                        j.ToTable("MedioAutor");
                    });
        });

        modelBuilder.Entity<Municipio>(entity =>
        {
            entity.HasKey(e => e.IdMunicipio).HasName("PK__Municipi__6100597848BF5F05");

            entity.ToTable("Municipio");

            entity.HasIndex(e => e.Nombre, "idx_NombreMunicipio");

            entity.Property(e => e.IdMunicipio).ValueGeneratedNever();
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.IdEstadoNavigation).WithMany(p => p.Municipios)
                .HasForeignKey(d => d.IdEstado)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Municipio__IdEst__2C3393D0");
        });

        modelBuilder.Entity<Rol>(entity =>
        {
            entity.HasKey(e => e.IdRol).HasName("PK__Rol__2A49584CC13D2C41");

            entity.ToTable("Rol");

            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false)
                .UseCollation("Latin1_General_BIN");
        });

        modelBuilder.Entity<TipoMedio>(entity =>
        {
            entity.HasKey(e => e.IdTipoMedio).HasName("PK__TipoMedi__7A9964B2437EEE33");

            entity.ToTable("TipoMedio");

            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false)
                .UseCollation("Latin1_General_BIN");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.IdUsuario).HasName("PK__Usuario__5B65BF978A54A2E1");

            entity.ToTable("Usuario");

            entity.HasIndex(e => e.Email, "UQ__Usuario__A9D1053467853D48").IsUnique();

            entity.Property(e => e.Contrasena)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.NombreCompleto)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.IdRolNavigation).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.IdRol)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Usuario__IdRol__32E0915F");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

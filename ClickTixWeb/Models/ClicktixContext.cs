using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ClickTixWeb.Models;

public partial class ClicktixContext : DbContext
{
    public ClicktixContext()
    {
    }

    public ClicktixContext(DbContextOptions<ClicktixContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Asiento> Asientos { get; set; }

    public virtual DbSet<CategoriaCandy> CategoriaCandies { get; set; }

    public virtual DbSet<Categorium> Categoria { get; set; }

    public virtual DbSet<Clasificacion> Clasificacions { get; set; }

    public virtual DbSet<Dimension> Dimensions { get; set; }

    public virtual DbSet<Funcion> Funcions { get; set; }

    public virtual DbSet<Idioma> Idiomas { get; set; }

    public virtual DbSet<Pelicula> Peliculas { get; set; }

    public virtual DbSet<ProductoCandy> ProductoCandies { get; set; }

    public virtual DbSet<Qr> Qrs { get; set; }

    public virtual DbSet<Sala> Salas { get; set; }

    public virtual DbSet<Sucursal> Sucursals { get; set; }

    public virtual DbSet<Ticket> Tickets { get; set; }

    public virtual DbSet<TicketCandy> TicketCandies { get; set; }

    public virtual DbSet<Turno> Turnos { get; set; }

    public virtual DbSet<UsuarioSistema> UsuarioSistemas { get; set; }

    public virtual DbSet<UsuarioWeb> UsuarioWebs { get; set; }

    //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder);
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
//        => optionsBuilder.UseMySql("server=localhost;port=3306;database=clicktix;uid=root", Microsoft.EntityFrameworkCore.ServerVersion.Parse("10.4.24-mariadb"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8_general_ci")
            .HasCharSet("utf8");

        modelBuilder.Entity<Asiento>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("asiento");

            entity.HasIndex(e => e.IdFuncion, "fk_asiento_funcion1_idx");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.Columna)
                .HasColumnType("int(11)")
                .HasColumnName("columna");
            entity.Property(e => e.Disponible)
                .HasColumnType("tinyint(4)")
                .HasColumnName("disponible");
            entity.Property(e => e.Fila)
                .HasColumnType("int(11)")
                .HasColumnName("fila");
            entity.Property(e => e.IdFuncion)
                .HasColumnType("int(11)")
                .HasColumnName("id_funcion");

            entity.HasOne(d => d.IdFuncionNavigation).WithMany(p => p.Asientos)
                .HasForeignKey(d => d.IdFuncion)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_asiento_funcion1");
        });

        modelBuilder.Entity<CategoriaCandy>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("categoria_candy");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.Categoria)
                .HasMaxLength(45)
                .HasColumnName("categoria");
        });

        modelBuilder.Entity<Categorium>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("categoria");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.Nombre)
                .HasMaxLength(45)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<Clasificacion>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("clasificacion");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.Clasificacion1)
                .HasMaxLength(45)
                .HasColumnName("clasificacion");
        });

        modelBuilder.Entity<Dimension>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("dimension");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.Dimension1)
                .HasMaxLength(45)
                .HasColumnName("dimension");
            entity.Property(e => e.Precio).HasColumnName("precio");
        });

        modelBuilder.Entity<Funcion>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("funcion");

            entity.HasIndex(e => e.IdDimension, "fk_funcion_dimension1_idx");

            entity.HasIndex(e => e.IdPelicula, "fk_funcion_pelicula1_idx");

            entity.HasIndex(e => e.IdSala, "fk_funcion_sala1_idx");

            entity.HasIndex(e => e.TurnoId, "fk_funcion_turno1_idx");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.Fecha).HasColumnName("fecha");
            entity.Property(e => e.IdDimension)
                .HasColumnType("int(11)")
                .HasColumnName("id_dimension");
            entity.Property(e => e.IdPelicula)
                .HasColumnType("int(11)")
                .HasColumnName("id_pelicula");
            entity.Property(e => e.IdSala)
                .HasColumnType("int(11)")
                .HasColumnName("id_sala");
            entity.Property(e => e.IdiomaFuncion)
                .HasMaxLength(45)
                .HasColumnName("idioma_funcion");
            entity.Property(e => e.TurnoId)
                .HasColumnType("int(11)")
                .HasColumnName("turno_id");

            entity.HasOne(d => d.IdDimensionNavigation).WithMany(p => p.Funcions)
                .HasForeignKey(d => d.IdDimension)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_funcion_dimension1");

            entity.HasOne(d => d.IdPeliculaNavigation).WithMany(p => p.Funcions)
                .HasForeignKey(d => d.IdPelicula)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_funcion_pelicula1");

            entity.HasOne(d => d.IdSalaNavigation).WithMany(p => p.Funcions)
                .HasForeignKey(d => d.IdSala)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_funcion_sala1");

            entity.HasOne(d => d.Turno).WithMany(p => p.Funcions)
                .HasForeignKey(d => d.TurnoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_funcion_turno1");
        });

        modelBuilder.Entity<Idioma>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("idioma");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.Idioma1)
                .HasMaxLength(45)
                .HasColumnName("idioma");
        });

        modelBuilder.Entity<Pelicula>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("pelicula");

            entity.HasIndex(e => e.IdCategoria, "fk_pelicula_categoria1_idx");

            entity.HasIndex(e => e.IdClasificacion, "fk_pelicula_clasificacion1_idx");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(500)
                .HasColumnName("descripcion");
            entity.Property(e => e.Director)
                .HasMaxLength(45)
                .HasColumnName("director");
            entity.Property(e => e.Duracion)
                .HasColumnType("int(11)")
                .HasColumnName("duracion");
            entity.Property(e => e.EstaActiva)
                .HasColumnType("tinyint(4)")
                .HasColumnName("esta_activa");
            entity.Property(e => e.FechaEstreno).HasColumnName("fecha_estreno");
            entity.Property(e => e.IdCategoria)
                .HasColumnType("int(11)")
                .HasColumnName("id_categoria");
            entity.Property(e => e.IdClasificacion)
                .HasColumnType("int(11)")
                .HasColumnName("id_clasificacion");
            entity.Property(e => e.Portada)
                .HasMaxLength(45)
                .HasColumnName("portada");
            entity.Property(e => e.Titulo)
                .HasMaxLength(45)
                .HasColumnName("titulo");

            entity.HasOne(d => d.IdCategoriaNavigation).WithMany(p => p.Peliculas)
                .HasForeignKey(d => d.IdCategoria)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_pelicula_categoria1");

            entity.HasOne(d => d.IdClasificacionNavigation).WithMany(p => p.Peliculas)
                .HasForeignKey(d => d.IdClasificacion)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_pelicula_clasificacion1");

            entity.HasMany(d => d.IdIdiomas).WithMany(p => p.IdPeliculas)
                .UsingEntity<Dictionary<string, object>>(
                    "PeliculaIdioma",
                    r => r.HasOne<Idioma>().WithMany()
                        .HasForeignKey("IdIdioma")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("fk_pelicula_has_idioma_idioma1"),
                    l => l.HasOne<Pelicula>().WithMany()
                        .HasForeignKey("IdPelicula")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("fk_pelicula_has_idioma_pelicula1"),
                    j =>
                    {
                        j.HasKey("IdPelicula", "IdIdioma")
                            .HasName("PRIMARY")
                            .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });
                        j.ToTable("pelicula_idioma");
                        j.HasIndex(new[] { "IdIdioma" }, "fk_pelicula_has_idioma_idioma1_idx");
                        j.HasIndex(new[] { "IdPelicula" }, "fk_pelicula_has_idioma_pelicula1_idx");
                    });
        });

        modelBuilder.Entity<ProductoCandy>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("producto_candy");

            entity.HasIndex(e => e.IdCategoria, "fk_catalogo_candy_categoria_candy1_idx");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(45)
                .HasColumnName("descripcion");
            entity.Property(e => e.IdCategoria)
                .HasColumnType("int(11)")
                .HasColumnName("id_categoria");
            entity.Property(e => e.Imagen)
                .HasMaxLength(45)
                .HasColumnName("imagen");
            entity.Property(e => e.Nombre)
                .HasMaxLength(45)
                .HasColumnName("nombre");
            entity.Property(e => e.Precio).HasColumnName("precio");

            entity.HasOne(d => d.IdCategoriaNavigation).WithMany(p => p.ProductoCandies)
                .HasForeignKey(d => d.IdCategoria)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_catalogo_candy_categoria_candy1");
        });

        modelBuilder.Entity<Qr>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("qr");

            entity.HasIndex(e => e.IdTicket, "fk_qr_ticket1_idx");

            entity.HasIndex(e => e.IdTicketcandy, "fk_qr_ticket_candy1_idx");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.Codigo)
                .HasMaxLength(45)
                .HasColumnName("codigo");
            entity.Property(e => e.IdTicket)
                .HasColumnType("int(11)")
                .HasColumnName("id_ticket");
            entity.Property(e => e.IdTicketcandy)
                .HasColumnType("int(11)")
                .HasColumnName("id_ticketcandy");

            entity.HasOne(d => d.IdTicketNavigation).WithMany(p => p.Qrs)
                .HasForeignKey(d => d.IdTicket)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_qr_ticket1");

            entity.HasOne(d => d.IdTicketcandyNavigation).WithMany(p => p.Qrs)
                .HasForeignKey(d => d.IdTicketcandy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_qr_ticket_candy1");
        });

        modelBuilder.Entity<Sala>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("sala");

            entity.HasIndex(e => e.IdSucursal, "fk_sala_sucursal1_idx");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.Capacidad)
                .HasColumnType("int(11)")
                .HasColumnName("capacidad");
            entity.Property(e => e.Columnas)
                .HasColumnType("int(11)")
                .HasColumnName("columnas");
            entity.Property(e => e.Filas)
                .HasColumnType("int(11)")
                .HasColumnName("filas");
            entity.Property(e => e.IdSucursal)
                .HasColumnType("int(11)")
                .HasColumnName("id_sucursal");
            entity.Property(e => e.NroSala)
                .HasColumnType("int(11)")
                .HasColumnName("nro_sala");

            entity.HasOne(d => d.IdSucursalNavigation).WithMany(p => p.Salas)
                .HasForeignKey(d => d.IdSucursal)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_sala_sucursal1");
        });

        modelBuilder.Entity<Sucursal>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("sucursal");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.Cuit)
                .HasMaxLength(45)
                .HasColumnName("cuit");
            entity.Property(e => e.Direccion)
                .HasMaxLength(45)
                .HasColumnName("direccion");
            entity.Property(e => e.Nombre)
                .HasMaxLength(45)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<Ticket>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("ticket");

            entity.HasIndex(e => e.IdFuncion, "fk_ticket_funcion1_idx");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.Columna)
                .HasColumnType("int(11)")
                .HasColumnName("columna");
            entity.Property(e => e.Fecha)
                .HasColumnType("timestamp")
                .HasColumnName("fecha");
            entity.Property(e => e.Fila)
                .HasColumnType("int(11)")
                .HasColumnName("fila");
            entity.Property(e => e.IdFuncion)
                .HasColumnType("int(11)")
                .HasColumnName("id_funcion");
            entity.Property(e => e.PrecioAlMomento).HasColumnName("precio_al_momento");

            entity.HasOne(d => d.IdFuncionNavigation).WithMany(p => p.Tickets)
                .HasForeignKey(d => d.IdFuncion)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_ticket_funcion1");
        });

        modelBuilder.Entity<TicketCandy>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("ticket_candy");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.Fecha)
                .HasColumnType("timestamp")
                .HasColumnName("fecha");

            entity.HasMany(d => d.IdProductos).WithMany(p => p.IdTickets)
                .UsingEntity<Dictionary<string, object>>(
                    "PedidoCandy",
                    r => r.HasOne<ProductoCandy>().WithMany()
                        .HasForeignKey("IdProducto")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("fk_ticket_candy_has_producto_candy_producto_candy1"),
                    l => l.HasOne<TicketCandy>().WithMany()
                        .HasForeignKey("IdTicket")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("fk_ticket_candy_has_producto_candy_ticket_candy1"),
                    j =>
                    {
                        j.HasKey("IdTicket", "IdProducto")
                            .HasName("PRIMARY")
                            .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });
                        j.ToTable("pedido_candy");
                        j.HasIndex(new[] { "IdProducto" }, "fk_ticket_candy_has_producto_candy_producto_candy1_idx");
                        j.HasIndex(new[] { "IdTicket" }, "fk_ticket_candy_has_producto_candy_ticket_candy1_idx");
                    });
        });

        modelBuilder.Entity<Turno>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("turno");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.Hora)
                .HasColumnType("time")
                .HasColumnName("hora");
        });

        modelBuilder.Entity<UsuarioSistema>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("usuario_sistema");

            entity.HasIndex(e => e.IdSucursal, "fk_empleado_sucursal_idx");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.Apellido)
                .HasMaxLength(45)
                .HasColumnName("apellido");
            entity.Property(e => e.IdSucursal)
                .HasColumnType("int(11)")
                .HasColumnName("id_sucursal");
            entity.Property(e => e.IsAdmin)
                .HasColumnType("tinyint(4)")
                .HasColumnName("is_admin");
            entity.Property(e => e.Nombre)
                .HasMaxLength(45)
                .HasColumnName("nombre");
            entity.Property(e => e.Pass)
                .HasMaxLength(45)
                .HasColumnName("pass");
            entity.Property(e => e.Usuario)
                .HasMaxLength(45)
                .HasColumnName("usuario");

            entity.HasOne(d => d.IdSucursalNavigation).WithMany(p => p.UsuarioSistemas)
                .HasForeignKey(d => d.IdSucursal)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_empleado_sucursal");
        });

        modelBuilder.Entity<UsuarioWeb>(entity =>
        {
            entity.HasKey(e => e.IdUsuario).HasName("PRIMARY");

            entity.ToTable("usuario_web");

            entity.Property(e => e.IdUsuario)
                .ValueGeneratedNever()
                .HasColumnType("int(11)")
                .HasColumnName("id_usuario");
            entity.Property(e => e.Apellido)
                .HasMaxLength(45)
                .HasColumnName("apellido");
            entity.Property(e => e.Nombre)
                .HasMaxLength(45)
                .HasColumnName("nombre");
            entity.Property(e => e.Pass)
                .HasMaxLength(45)
                .HasColumnName("pass");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

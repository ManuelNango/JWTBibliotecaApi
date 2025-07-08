using DL;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Medio : IMedio
    {
        //Conexión
        private readonly DL.JwtexamenBibliotecaContext _connection;

        public Medio(JwtexamenBibliotecaContext connection)
        {
            _connection = connection;
        }

        public ML.Result GetAll(ML.Medio medioObj)
        {
            ML.Result result = new ML.Result();

            try
            {
                var query = _connection.MedioGetAllSP.FromSqlRaw($"MedioGetAll '{medioObj.Titulo}'").ToList();

                if (query.Count > 0)
                {
                    result.Objects = new List<object>();

                    foreach (var item in query)
                    {
                        ML.Medio medio = new ML.Medio();

                        medio.Autor = new ML.Autor();
                        medio.TipoMedio = new ML.TipoMedio();
                        medio.Editorial = new ML.Editorial();
                        medio.Area = new ML.Area();
                        medio.Idioma = new ML.Idioma();

                        medio.IdMedio = item.IdMedio;
                        medio.Titulo = item.Titulo;
                        medio.Descripcion = item.Descripcion;
                        medio.FechaPublicacion = item.FechaPublicacion.ToString();
                        medio.ArchivoPDF = item.ArchivoPDF;
                        medio.ImagenPortada = item.ImagenPortada;

                        //Relaciones
                        medio.Autor.Nombre = item.Autor;
                        medio.TipoMedio.Nombre = item.TipoMedio;
                        medio.Editorial.Nombre = item.Editorial;
                        medio.Area.Nombre = item.Area;
                        medio.Idioma.Nombre = item.Idioma;

                        result.Objects.Add(medio);
                    }

                    result.Correct = true;
                }
                else
                {
                    result.Correct = false;
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }

            return result;
        }

        public ML.Result GetById(int idMedio)
        {
            ML.Result result = new ML.Result();

            try
            {
                var query = _connection.Medios.FromSqlRaw($"MedioGetById {idMedio}").AsEnumerable().FirstOrDefault();

                if (query != null)
                {
                    ML.Medio medio = new ML.Medio();

                    medio.TipoMedio = new ML.TipoMedio();
                    medio.Editorial = new ML.Editorial();
                    medio.Area = new ML.Area();
                    medio.Idioma = new ML.Idioma();

                    medio.IdMedio = query.IdMedio;
                    medio.Titulo = query.Titulo;
                    medio.Descripcion = query.Descripcion;
                    medio.FechaPublicacion = query.FechaPublicacion.ToString("dd/MM/yyyy");
                    medio.ArchivoPDF = query.ArchivoPdf;
                    medio.ImagenPortada = query.ImagenPortada;

                    //Relaciones
                    medio.TipoMedio.IdTipoMedio = query.IdTipoMedio.Value;
                    medio.Editorial.IdEditorial = query.IdEditorial.Value;
                    medio.Area.IdArea = query.IdArea.Value;
                    medio.Idioma.IdIdioma = query.IdIdioma.Value;


                    result.Object = medio;

                    result.Correct = true;
                }
                else
                {
                    result.Correct = false;
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }

            return result;
        }

        public ML.Result Delete(int idMedio)
        {
            ML.Result result = new ML.Result();

            try
            {                                           //ExecuteSqlRaw => INSERT, UPDATE, DELETE
                int filasAfectadas = _connection.Database.ExecuteSqlRaw($"MedioDelete {idMedio}");

                if (filasAfectadas > 0)
                {
                    result.Correct = true;
                }
                else
                {
                    result.Correct = false;
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }

            return result;
        }
        public ML.Result Add(ML.Medio medio)
        {
            ML.Result result = new ML.Result();

            try
            {
                var imagen = new SqlParameter("@Imagen", SqlDbType.VarBinary);

                if (medio.ImagenPortada != null)
                {
                    imagen.Value = medio.ImagenPortada;
                }
                else
                {
                    imagen.Value = DBNull.Value;
                }

                var pdf = new SqlParameter("@Pdf", SqlDbType.VarBinary);

                if (medio.ArchivoPDF != null)
                {
                    pdf.Value = medio.ArchivoPDF;
                }
                else
                {
                    pdf.Value = DBNull.Value;
                }

                //ExecuteSqlRaw => INSERT, UPDATE, DELETE
                var filasAfectadas = _connection.Database.ExecuteSqlRaw($"MedioAdd " +
                    $"'{medio.Titulo}', '{medio.Descripcion}', '{medio.FechaPublicacion}', @Pdf, @Imagen, {medio.TipoMedio.IdTipoMedio}, " +
                    $"{medio.Editorial.IdEditorial}, {medio.Area.IdArea}, {medio.Idioma.IdIdioma}, {medio.Autor.IdAutor}", pdf, imagen);

                if (filasAfectadas > 0)
                {
                    result.Correct = true;
                }
                else
                {
                    result.Correct = false;
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }

            return result;
        }
        public ML.Result Update(ML.Medio medio)
        {
            ML.Result result = new ML.Result();

            try
            {
                var imagen = new SqlParameter("@Imagen", SqlDbType.VarBinary);

                if (medio.ImagenPortada != null)
                {
                    imagen.Value = medio.ImagenPortada;
                }
                else
                {
                    imagen.Value = DBNull.Value;
                }

                var pdf = new SqlParameter("@Pdf", SqlDbType.VarBinary);

                if (medio.ArchivoPDF != null)
                {
                    pdf.Value = medio.ArchivoPDF;
                }
                else
                {
                    pdf.Value = DBNull.Value;
                }

                //ExecuteSqlRaw => INSERT, UPDATE, DELETE
                var filasAfectadas = _connection.Database.ExecuteSqlRaw($"MedioUpdate " +
                    $"{medio.IdMedio}, '{medio.Titulo}', '{medio.Descripcion}', '{medio.FechaPublicacion}', @Pdf, @Imagen, {medio.TipoMedio.IdTipoMedio}, " +
                    $"{medio.Editorial.IdEditorial}, {medio.Area.IdArea}, {medio.Idioma.IdIdioma}, {medio.Autor.IdAutor}", pdf, imagen);

                if (filasAfectadas > 0)
                {
                    result.Correct = true;
                }
                else
                {
                    result.Correct = false;
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }

            return result;
        }

    }
}

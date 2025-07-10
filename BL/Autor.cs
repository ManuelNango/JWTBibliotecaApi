using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Autor : IAutor
    {
        readonly DL.JwtexamenBibliotecaContext _connection;
        
        public Autor(DL.JwtexamenBibliotecaContext connection)
        {
            _connection = connection;
        }

        public ML.Result GetAll(ML.Autor autorObj)
        {
            ML.Result result = new ML.Result();

            try
            {
                var query = _connection.AutorGetAllSP.FromSqlRaw($"AutorGetAll '{autorObj.Nombre}'").ToList();

                if (query.Count > 0)
                {
                    result.Objects = new List<object>();

                    foreach(var item in query)
                    {
                        ML.Autor autor = new ML.Autor();

                        autor.IdAutor = item.IdAutor;
                        autor.Nombre = item.Nombre;

                        result.Objects.Add(autor);
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

        public ML.Result GetById(int idAutor)
        {
            ML.Result result = new ML.Result();

            try
            {
                var query = _connection.Autors.FromSqlInterpolated($"AutorGetById {idAutor}").AsEnumerable().FirstOrDefault();

                if(query != null)
                {
                    ML.Autor autor = new ML.Autor();

                    autor.IdAutor = query.IdAutor;
                    autor.Nombre = query.Nombre;

                    result.Object = autor;
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

        public ML.Result Add(ML.Autor autor)
        {
            ML.Result result = new ML.Result();

            try
            {
                var filasAfectadas = _connection.Database.ExecuteSqlRaw($"AutorAdd '{autor.Nombre}'");

                if(filasAfectadas > 0)
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
        public ML.Result Update(ML.Autor autor)
        {
            ML.Result result = new ML.Result();

            try
            {
                var filasAfectadas = _connection.Database.ExecuteSqlRaw($"AutorUpdate {autor.IdAutor}, '{autor.Nombre}'");

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

        public ML.Result Delete(int idAutor)
        {
            ML.Result result = new ML.Result();

            try
            {
                var filasAfectadas = _connection.Database.ExecuteSqlRaw($"AutorDelete {idAutor}");

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

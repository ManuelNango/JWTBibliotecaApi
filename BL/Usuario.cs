using DL;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace BL
{
    public class Usuario : IUsuario
    {
        private readonly JwtexamenBibliotecaContext _context;

        public Usuario (JwtexamenBibliotecaContext context)
        {
            _context = context;
        }

        public ML.Result GetAll(ML.Usuario usuarioObj)
        {
            ML.Result result = new ML.Result();

            try
            {
                var query = _context.UsuarioGetAllSP.FromSqlInterpolated($"RestauranteGetAll '{usuarioObj.NombreCompleto}', {usuarioObj?.Rol?.IdRol}").ToList();

                if (query.Count > 0)
                {
                    result.Objects = new List<object>();

                    foreach (var item in query)
                    {
                        ML.Usuario usuario = new ML.Usuario();

                        usuario.Rol = new ML.Rol();

                        usuario.Direccion = new ML.Direccion();
                        usuario.Direccion.Colonia = new ML.Colonia();
                        usuario.Direccion.Colonia.Municipio = new ML.Municipio();
                        usuario.Direccion.Colonia.Municipio.Estado = new ML.Estado();

                        //Usuario
                        usuario.IdUsuario = item.IdUsuario;
                        usuario.NombreCompleto = item.NombreCompleto;
                        usuario.Email = item.Email;
                        usuario.Contrasena = item.Contrasena;

                        //Rol
                        usuario.Rol.IdRol = item.IdRol;
                        usuario.Rol.Nombre = item.NombreRol;

                        //Dirección
                        usuario.Direccion.Calle = item.Calle;
                        usuario.Direccion.NumeroInterior = item.NumeroInterior;
                        usuario.Direccion.NumeroExterior = item.NumeroExterior;

                        //Colonia
                        usuario.Direccion.Colonia.Nombre = item.NombreColonia;
                        usuario.Direccion.Colonia.CodigoPostal = item.CodigoPostal;

                        //Municipio
                        usuario.Direccion.Colonia.Municipio.Nombre = item.NombreMunicipio;

                        //Estado
                        usuario.Direccion.Colonia.Municipio.Estado.Nombre = item.NombreEstado;

                        result.Objects.Add(usuario);
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

        public ML.Result GetById(int idUsuario)
        {
            ML.Result result = new ML.Result();

            try
            {
                var query = _context.UsuarioGetAllSP.FromSqlInterpolated($"UsuarioGetById {idUsuario}").AsEnumerable().FirstOrDefault();

                if(query != null)
                {
                    ML.Usuario usuario = new ML.Usuario();

                    usuario.Rol = new ML.Rol();

                    usuario.Direccion = new ML.Direccion();
                    usuario.Direccion.Colonia = new ML.Colonia();
                    usuario.Direccion.Colonia.Municipio = new ML.Municipio();
                    usuario.Direccion.Colonia.Municipio.Estado = new ML.Estado();

                    //Usuario
                    usuario.IdUsuario = query.IdUsuario;
                    usuario.NombreCompleto = query.NombreCompleto;
                    usuario.Email = query.Email;
                    usuario.Contrasena = query.Contrasena;

                    //Rol
                    usuario.Rol.IdRol = query.IdRol;
                    usuario.Rol.Nombre = query.NombreRol;

                    //Dirección
                    usuario.Direccion.IdDireccion = query.IdDireccion;
                    usuario.Direccion.Calle = query.Calle;
                    usuario.Direccion.NumeroInterior = query.NumeroInterior;
                    usuario.Direccion.NumeroExterior = query.NumeroExterior;

                    //Colonia
                    usuario.Direccion.Colonia.IdColonia = query.IdColonia;
                    usuario.Direccion.Colonia.Nombre = query.NombreColonia;
                    usuario.Direccion.Colonia.CodigoPostal = query.CodigoPostal;

                    //Municipio
                    usuario.Direccion.Colonia.Municipio.IdMunicipio = query.IdMunicipio;
                    usuario.Direccion.Colonia.Municipio.Nombre = query.NombreMunicipio;

                    //Estado
                    usuario.Direccion.Colonia.Municipio.Estado.IdEstado = query.IdEstado;
                    usuario.Direccion.Colonia.Municipio.Estado.Nombre = query.NombreEstado;

                    result.Object = usuario;

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

        public ML.Result Add(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();

            try
            {
                var filasAfectadas = _context.Database.ExecuteSqlRaw
                    ($"UsuarioAdd '{usuario.NombreCompleto}', '{usuario.Email}', '{usuario.Contrasena}', " +
                    $"{usuario.Rol.IdRol}, '{usuario.Direccion.Calle}', '{usuario.Direccion.NumeroInterior}', " +
                    $"'{usuario.Direccion.NumeroExterior}', {usuario.Direccion.Colonia.IdColonia}");

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

        public ML.Result Update(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();

            try
            {
                var filasAfectadas = _context.Database.ExecuteSqlRaw
                    ($"UsuarioAdd '{usuario.NombreCompleto}', '{usuario.Email}', '{usuario.Contrasena}', " +
                    $"{usuario.Rol.IdRol}, '{usuario.Direccion.Calle}', '{usuario.Direccion.NumeroInterior}', " +
                    $"'{usuario.Direccion.NumeroExterior}', {usuario.Direccion.Colonia.IdColonia}, {usuario.IdUsuario}");

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
        public ML.Result Delete(int idUsuario)
        {
            ML.Result result = new ML.Result();

            try
            {
                int filasAfectadas = _context.Database.ExecuteSqlRaw($"UsuarioDelete {idUsuario}");

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

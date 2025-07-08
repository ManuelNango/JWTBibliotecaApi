using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public interface IUsuario
    {
        ML.Result GetAll(ML.Usuario usuario);
        ML.Result GetById(int idUsuario);
        ML.Result Add(ML.Usuario usuario);
        ML.Result Update(ML.Usuario usuario);
        ML.Result Delete(int idUsuario);
    }
}

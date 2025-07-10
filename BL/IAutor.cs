using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public interface IAutor
    {
        ML.Result GetAll(ML.Autor autor);
        ML.Result GetById(int idAutor);
        ML.Result Delete(int idAutor);
        ML.Result Add(ML.Autor autor);
        ML.Result Update(ML.Autor autor);
    }
}

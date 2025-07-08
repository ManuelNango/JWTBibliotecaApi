using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public interface IMedio
    {
        ML.Result GetAll(ML.Medio medio);
        ML.Result GetById(int idMedio);
        ML.Result Delete(int idMedio);
        ML.Result Add(ML.Medio medio);
        ML.Result Update(ML.Medio medio);
    }
}

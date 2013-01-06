using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PiHeatingController.Services
{
    public interface IStateFullBoilerHeaterService : IBoilerHeaterService
    {
        double GetLevel();

        bool IsOn();
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PiHeatingController.Services
{
    public enum StartSwitchPosition
    {
        AllOff = 0,
        WaterOn = 30,
        HeaterOn = 60
    }

    public interface IBoilerHeaterService
    {
        void TurnOn();

        void TurnOff();

        void SetHeatLevel(int level);
    }
}

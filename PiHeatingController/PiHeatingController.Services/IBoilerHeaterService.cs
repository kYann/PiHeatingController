using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PiHeatingController.Services
{
    public enum StartSwitchPosition
    {
        AllOff = 90,
        WaterOn = 120,
        HeaterOn = 150
    }

    public interface IBoilerHeaterService
    {
        void TurnOn();

        void TurnOff();

        void SetHeatLevel(double level);
    }
}

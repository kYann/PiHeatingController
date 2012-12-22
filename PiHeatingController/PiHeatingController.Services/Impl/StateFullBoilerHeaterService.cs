using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PiHeatingController.Services.Impl
{
    public class StateFullBoilerHeaterService : IStateFullBoilerHeaterService
    {
        IBoilerHeaterService service;
        static StartSwitchPosition currentPosition;
        static int currentLevel;

        public StateFullBoilerHeaterService(IBoilerHeaterService service)
        {
            this.service = service;
        }

        public void TurnOn()
        {
            this.service.TurnOn();
            currentPosition = StartSwitchPosition.HeaterOn;
        }

        public void TurnOff()
        {
            this.service.TurnOff();
            currentPosition = StartSwitchPosition.WaterOn;
        }

        public void SetHeatLevel(int level)
        {
            this.service.SetHeatLevel(level);
            currentLevel = level;
        }

        public int GetLevel()
        {
            return currentLevel;
        }

        public bool IsOn()
        {
            return currentPosition == StartSwitchPosition.HeaterOn;
        }
    }
}

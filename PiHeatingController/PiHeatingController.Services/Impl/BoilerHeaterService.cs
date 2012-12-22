using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PiHeatingController.Services.Impl
{
    public class BoilerHeaterService : IBoilerHeaterService
    {
        IServoMotorService servoMotorService;
        const int StartSwitchServo = 0;
        const int LevelSwitchServo = 1;

        const int MaxLevel = 5;

        public BoilerHeaterService(IServoMotorService servoMotorService)
        {
            this.servoMotorService = servoMotorService;
        }

        public void TurnOn()
        {
            this.servoMotorService.RotateServo(StartSwitchServo, (int)StartSwitchPosition.HeaterOn);
        }

        public void TurnOff()
        {
            this.servoMotorService.RotateServo(StartSwitchServo, (int)StartSwitchPosition.WaterOn);
        }

        protected int GetAngleFromLevel(double level)
        {
            return (int)((MaxLevel - 1) / 180.0 * level);
        }

        public void SetHeatLevel(double level)
        {
            if (level < 0.9)
                this.TurnOff();
            else if (level < MaxLevel)
            {
                var angle = this.GetAngleFromLevel(level);
                this.servoMotorService.RotateServo(LevelSwitchServo, angle);
            }
        }
    }
}

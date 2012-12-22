using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PiHeatingController.Services.Impl
{
    public class ServoMotorService : IServoMotorService
    {
        const int MinPulse = 50;
        const int MaxPulse = 249;

        protected virtual void ExecuteCommand(string command)
        {
            File.WriteAllText("/dev/servoblaster", command, Encoding.ASCII);
        }

        protected string PrepareCommand(int servorPort, int steps)
        {
            return string.Format("{0}={1}\n");
        }

        public void RotateServo(int servoPort, int angle)
        {
            var step = (MaxPulse - MinPulse) / 180.0;

            var cmd = this.PrepareCommand(servoPort, (int)(step * angle));
            this.ExecuteCommand(cmd);
        }
    }
}

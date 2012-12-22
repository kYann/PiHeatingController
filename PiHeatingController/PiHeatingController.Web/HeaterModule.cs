using Nancy;
using PiHeatingController.Services.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PiHeatingController.Web
{
    public class HeaterModule : NancyModule
    {
        public HeaterModule()
        {
            var heaterService = new StateFullBoilerHeaterService(
                new BoilerHeaterService(new ServoMotorService())
                );

            Get["heater"] = parameters =>
            {
                var model = new { 
                    status = heaterService.IsOn() ? "on" : "off",
                    level = heaterService.GetLevel()
                };

                return View[model];
            };

            Post["heater"] = parameters =>
            {
                var model = new
                {
                    status = (string)this.Request.Form.status,
                    level = (int)this.Request.Form.level
                };

                heaterService.SetHeatLevel(model.level);
                switch (model.status)
                {
                    case "on":
                        heaterService.TurnOn();
                        break;
                    case "off":
                        heaterService.TurnOff();
                        break;
                }

                return View[model];
            };
        }
    }
}

using Nancy;
using PiHeatingController.Services.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PiHeatingController.Web
{
    public class HeaterServiceModule : NancyModule
    {
        public HeaterServiceModule()
        {
            var heaterService = new StateFullBoilerHeaterService(
                new BoilerHeaterService(new ServoMotorService())
                );

            Get["api/v1/heater/info"] = parameters =>
            {
                var model = new
                {
                    status = heaterService.IsOn() ? "on" : "off",
                    level = heaterService.GetLevel()
                };

                return Response.AsJson(model);
            };

            Get["api/v1/heater/powerswitch"] = parameters => {
                var model = new
                {
                    status = heaterService.IsOn() ? "on" : "off",
                };

                return Response.AsJson(model);
            };

            Post["api/v1/heater/powerswitch"] = parameters =>
            {
                var model = new { status = (string)this.Context.Request.Form.status };
                switch (model.status)
                {
                    case "on":
                        heaterService.TurnOn();
                        break;
                    case "off":
                        heaterService.TurnOff();
                        break;
                    default:
                        return Response.AsJson(model, HttpStatusCode.BadRequest);
                }
                return Response.AsJson(model);
            };

            Get["api/v1/heater/level"] = parameters =>
            {
                var model = new
                {
                    level = heaterService.GetLevel()
                };
                return Response.AsJson(model);
            };

            Post["api/v1/heater/level"] = parameters =>
            {
                int level = this.Context.Request.Form.level;
                var model = new { value = level };
                heaterService.SetHeatLevel(level);
                return Response.AsJson(model);
            };
        }
    }
}

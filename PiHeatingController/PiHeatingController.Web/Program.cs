using Nancy;
using Nancy.Hosting.Self;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PiHeatingController.Web
{
    public class CustomRootPathProvider : IRootPathProvider
    {
        internal static string Path = null;

        public string GetRootPath()
        {
            return Path;
        }
    }

    public class CustomBootstrapper : DefaultNancyBootstrapper
    {
        protected override Type RootPathProvider
        {
            get { return typeof(CustomRootPathProvider); }
        }
    }

    class Program
    {
        static NancyHost host;

        public static string GetHostUrl()
        {
            return "http://localhost:8080";
        }

        public static void StartServer(string path)
        {
            CustomRootPathProvider.Path = path;
            host = new Nancy.Hosting.Self.NancyHost(new CustomBootstrapper(), new Uri(GetHostUrl()));
            host.Start();
            //TinyIoC.TinyIoCContainer.Current.Register<RazorViewEngine>();
        }

        public static void StopServer()
        {
            host.Stop();
        }

        static void Main(string[] args)
        {
            var path = AppDomain.CurrentDomain.BaseDirectory.TrimEnd(Path.DirectorySeparatorChar);
#if DEBUG
            path = Path.GetDirectoryName(Path.GetDirectoryName(path));
#endif
            StartServer(path);
            Console.ReadLine();
            StopServer();
        }
    }
}

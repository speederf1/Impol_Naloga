using Microsoft.Owin.Hosting;
using System;

namespace Impol_SelfHostedAPI
{
    class Program
    {
        static void Main(string[] args)
        {
            string baseAddress = "http://localhost:9000/";

            // Start OWIN host 
            using (WebApp.Start<Startup>(url: baseAddress))
            {
                Console.WriteLine("Impol self hosted Web API");
                Console.ReadLine();
            }
        }
    }
}

using Controller.Services;
using Controller.Services.Admin;
using MEFLoader;
using Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Test;

namespace Controller
{
    class Program
    {
        // This is the main class that controls logic flow of the server (see comments below for process description)
        // It is sufficient to run this controller to start full-functional server
        static void Main(string[] args)
        {
            Console.WriteLine("Starting server initialization...");

            // Initializes MEF loader class and triggers loading of dll files
            // All classes loaded with MEF are contained in the loader object, and are accessible from it
            Console.WriteLine("\tStarting MEF catalog...");
            Loader loader = new Loader();
            loader.LoadCatalog();

            // Basically just re-writing loaded classes from dynamic loader object to static lists (see 00_DataObjects/Objects/)
            // Note: it was decided to keep 4 lists instead on one generic because of 2 reasons:
            // Resource shortage, due to several people dropped out from the program
            // To keep the code cleaner and to ensure type-safe behavior (however this has made the code larger and more redundant)
            Console.WriteLine("\tRegistering connectors...");
            foreach (var i in loader.Test) { TestObject.Add(i); }
            foreach (var i in loader.Weather) { WeatherObject.Add(i); }
            foreach (var i in loader.Kodi) { KodiObject.Add(i); }
            foreach (var i in loader.GenericInfo) { GenericInfoObject.Add(i); }

            // This piece of code does 2 things:
            // Initializes DB connection and loads the rules from the database
            // Starts a daemon which executes business rules (once per minute)
            Console.WriteLine("\tLoading business rules...");
            BusinessRules.BusinessRules.Initialize();

            // This part goes through all loaded dll classes and collects their definition (methods exposed for administration, i.e. staring with A and AGet)
            // This information is then wrapped into the objects and is exposed to Admin section of a client
            Console.WriteLine("\tCollecting admin methods...");
            BusinessRules.DataCollector.Collector.Collect();

            // Just some hosting of web services
            // There is a separate web service exposed for each source (was done to reduce code complexity, provide type safety and comply to required architecture)
            // Note that web services are added to this project, since they are registered in static classes, which are not visible from other projects
            Console.WriteLine("\tHosting web services...");
            new ServiceHost(typeof(TestService)).Open();
            new ServiceHost(typeof(KodiService)).Open();
            new ServiceHost(typeof(WeatherService)).Open();
            new ServiceHost(typeof(GenericInfoService)).Open();
            new ServiceHost(typeof(AdminService)).Open();

            Console.WriteLine("Server initialization complete");
            Console.WriteLine("------------------------------\n");
            Console.ReadLine();
        }
    }
}

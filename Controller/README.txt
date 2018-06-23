@Developer

When testing locally, you have to disable the DB connection part in the controller.

For that, comment the following two lines in Controller/Program.cs:

            Console.WriteLine("\tLoading business rules...");
            //BusinessRules.BusinessRules.Initialize(); //comment this when testing locally

            Console.WriteLine("\tCollecting admin methods...");
            //BusinessRules.DataCollector.Collector.Collect(); //comment this when testing locally
using System;
using System.Globalization;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

namespace Nhs
{
    class Program
    {
        private const string PracticePath = "../../T201202ADD REXT.csv";
        private const string PrescriptionPath = "../../T201109PDP IEXT.csv";
        private const string PrescriptionCostPath = "../../pres-cost-anal-eng-2014-data.csv";

        static void Main()
        {
            var container = new WindsorContainer();
            container.Install(new ServicesInstaller());

            var nhs = container.Resolve<Nhs>();
            var viewModel = nhs.Execute(PracticePath, PrescriptionPath, PrescriptionCostPath);

            var nfi = (NumberFormatInfo)CultureInfo.CurrentCulture.NumberFormat.Clone();
            nfi.CurrencySymbol = "";
            Console.WriteLine($"Number of practices in London: {viewModel.PracticeCount}");

            Console.WriteLine("---------------------------------------------------------------");
            Console.WriteLine($"Average actual cost of all peppermint oil prescriptions: {viewModel.AverageCost:C}");

            Console.WriteLine("---------------------------------------------------------------");
            Console.WriteLine("Top-spending post codes:");
            foreach (var postCode in viewModel.PostCodeSpents)
            {
                Console.WriteLine(string.Format(nfi, $"{postCode.Name,-9} {postCode.Spent:C}"));
            }

            Console.WriteLine("---------------------------------------------------------------");
            Console.WriteLine("Average price per prescription of Flucloxacillin:");
            foreach (var region in viewModel.RegionPrescripctions)
            {
                Console.WriteLine(string.Format(nfi, $"{region.Region,-25} {region.Spent:C} {region.Diff,8:P0}"));
            }

            Console.WriteLine("---------------------------------------------------------------");
            Console.WriteLine("Most commonly used drug types:");
            foreach (var drugType in viewModel.DrugTypes)
            {
                Console.WriteLine($"{drugType.Name,-36} {drugType.Count:P0}");
            }

            Console.ReadLine();
        }
    }

    class ServicesInstaller: IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Component.For<Nhs>(),
                Component.For<IFileStorage>().ImplementedBy<FileStorage>(),
                Component.For<INhsProcessor>().ImplementedBy<NhsProcessor>(), 
                Component.For<ICsvSerializer>().ImplementedBy<CsvSerializer>()
            );
        }
    }
}
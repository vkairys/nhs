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
            Console.WriteLine("Number of practises in London: {0}", viewModel.PracticeCount);

            Console.WriteLine("---------------------------------------------------------------");
            Console.WriteLine("Average actual cost of all peppermint oil prescriptions: {0:C}", viewModel.AverageCost);

            Console.WriteLine("---------------------------------------------------------------");
            Console.WriteLine("Top-spending post codes:");
            foreach (var postCode in viewModel.PostCodeSpents)
            {
                Console.WriteLine(string.Format(nfi, "{0,-9} {1:C}", postCode.Name, postCode.Spent));
            }

            Console.WriteLine("---------------------------------------------------------------");
            Console.WriteLine("Average price per prescription of Flucloxacillin:");
            foreach (var region in viewModel.RegionPrescripctions)
            {
                Console.WriteLine(string.Format(nfi, "{0,-25} {1:C} {2,8:P0}", region.Region, region.Spent, region.Diff));
            }

            Console.WriteLine("---------------------------------------------------------------");
            Console.WriteLine("Most commonly used drug types:");
            foreach (var drugType in viewModel.DrugTypes)
            {
                Console.WriteLine("{0,-36} {1:P0}", drugType.Name, drugType.Count);
            }

            Console.ReadLine();
        }
    }

    class ServicesInstaller: IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Component.For<Nhs>()
            );
        }
    }
}
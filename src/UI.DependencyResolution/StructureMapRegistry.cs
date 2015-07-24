using ClearMeasure.Bootcamp.Core;
using ClearMeasure.Bootcamp.Core.Model;
using ClearMeasure.Bootcamp.DataAccess;
using ClearMeasure.Bootcamp.DataAccess.Mappings;
using StructureMap.Configuration.DSL;
using UI;

namespace ClearMeasure.Bootcamp.UI.DependencyResolution
{
    public class StructureMapRegistry : Registry
    {
        public StructureMapRegistry()
        {
            Scan(scanner =>
            {
                scanner.AssemblyContainingType<Employee>();
                scanner.AssemblyContainingType<DataContext>();
                scanner.AssemblyContainingType<Startup>();
                scanner.WithDefaultConventions();
                scanner.ConnectImplementationsToTypesClosing(typeof (IRequestHandler<,>));
            });
            For<SingleInstanceFactory>().Use<SingleInstanceFactory>(ctx => t => ctx.GetInstance(t));
        }
    }
}
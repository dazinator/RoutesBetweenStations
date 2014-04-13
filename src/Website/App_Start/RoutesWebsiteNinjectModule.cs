using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.Web;
using Ninject.Extensions.Wcf;
using Ninject.Modules;
using Ninject.Syntax;
using RoutesBetweenStations.DataAccess;
using RoutesBetweenStations.Model.Factory;
using RoutesBetweenStations.Model.Provider;

namespace RoutesBetweenStations.Website
{
    public class RoutesWebsiteNinjectModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IFileStreamProvider>().To<FileStreamProvider>();
            Bind<INodeFactory>().To<TrainStationFactory>();
            Bind<INodeConnectionFactory>().To<TrainConnectionFactory>();
            Bind<INodesRepository>().To<CsvFileNodesRepository>();
            Bind<IRouteProvider>().To<DijkstraRouteProvider>();
        }
    }
}
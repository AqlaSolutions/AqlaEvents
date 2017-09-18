using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Facebook;

namespace AqlaEvents
{
    public class DiMainModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterType<CsvStorage>().AsSelf().AsImplementedInterfaces().SingleInstance();
            builder.RegisterType<EventDescriptionParser>().AsSelf().AsImplementedInterfaces().SingleInstance();
            builder.RegisterType<MinMaxPriceExtractor>().AsSelf().AsImplementedInterfaces().SingleInstance();
            builder.RegisterType<FacebookEventRetriever>().AsSelf().AsImplementedInterfaces().SingleInstance();
            builder.RegisterType<FacebookEventFormat>().AsSelf().AsImplementedInterfaces().SingleInstance();
            builder.RegisterType<CityEventFromFacebookImporter>().AsSelf().AsImplementedInterfaces().SingleInstance();
            builder.RegisterInstance(new FacebookClient(Settings.Default.FacebookAccessToken)).AsSelf().AsImplementedInterfaces();
        }
    }
}
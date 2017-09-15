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

            builder.RegisterType<EventDescriptionParser>().AsSelf().AsImplementedInterfaces();
            builder.RegisterType<MinMaxPriceExtractor>().AsSelf().AsImplementedInterfaces();
            builder.RegisterType<FacebookEventRetriever>().AsSelf().AsImplementedInterfaces();
            builder.RegisterType<FacebookEventParser>().AsSelf().AsImplementedInterfaces();
            builder.RegisterType<CityEventFromFacebookImporter>().AsSelf().AsImplementedInterfaces();
            builder.RegisterInstance(new FacebookClient(Settings.Default.FacebookAccessToken)).AsSelf().AsImplementedInterfaces();
        }
    }
}
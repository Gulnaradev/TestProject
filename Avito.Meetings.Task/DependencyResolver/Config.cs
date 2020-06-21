using MeetingsBLL;
using MeetingsDAO;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DependencyResolver
{
    public static class Config
    {
        public static void RegisterServices(IKernel kernel)
        {
            kernel
                .Bind<IMeetingDAO>()
                .To<MeetingDAO>();
            kernel
                .Bind<IMeetingBLL>()
                .To<MeetingBLL>();
        }
    }
}

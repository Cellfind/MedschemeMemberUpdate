﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace MedschemeMemberUpdateSMSService
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
#if (DEBUG)
            Service1 service = new Service1();
            service.OnDebug();
            System.Threading.Thread.Sleep(System.Threading.Timeout.Infinite);
#else

                                        ServiceBase[] ServicesToRun;
                                        ServicesToRun = new ServiceBase[]
                                        {
                                                                new Service1()
                                        };
                                        ServiceBase.Run(ServicesToRun);
#endif
        }
    }
}

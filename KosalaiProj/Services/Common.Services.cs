using System;
using System.Collections.Generic;
using System.Linq;
using Common.Models;

namespace Common.Services
{
    public class CommonService
    {
        public Dictionary<string, List<string>> GetDataSourceTypes()
        {
            Dictionary<string, List<string>> master =
                new Dictionary<string, List<string>>();
            master
                .Add(nameof(sponserType),
                Enum.GetNames(typeof (sponserType)).ToList());

            master
                .Add(nameof(eventType),
                Enum.GetNames(typeof (eventType)).ToList());

            master
                .Add(nameof(tamilYr), Enum.GetNames(typeof (tamilYr)).ToList());

            master
                .Add(nameof(tamilMon),
                Enum.GetNames(typeof (tamilMon)).ToList());

            master.Add(nameof(star), Enum.GetNames(typeof (star)).ToList());

            master.Add(nameof(patcam), Enum.GetNames(typeof (patcam)).ToList());

            return master;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using Common.Models;

namespace Sharedmodels
{
    public class MasterList
    {
        public MasterList()
        {
        }

        public List<string> sponserList
        {
            get
            {
                return Enum.GetNames(typeof (sponserType)).ToList();
            }
        }

        public List<string> eventList
        {
            get
            {
                return Enum.GetNames(typeof (eventType)).ToList();
            }
        }

        public List<string> tamilYearList
        {
            get
            {
                return Enum.GetNames(typeof (tamilYr)).ToList();
            }
        }

        public List<string> tamilMonthList
        {
            get
            {
                return Enum.GetNames(typeof (tamilMon)).ToList();
            }
        }

        public List<string> starList
        {
            get
            {
                return Enum.GetNames(typeof (star)).ToList();
            }
        }

        public List<string> patcamList
        {
            get
            {
                return Enum.GetNames(typeof (patcam)).ToList();
            }
        }
    }
}

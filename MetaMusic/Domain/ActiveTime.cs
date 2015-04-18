using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MetaMusic.Domain
{
    /// <summary>
    /// Period of active time
    /// </summary>
    public class ActiveTime
    {
        public ActiveTime()
        {
            YearFrom = 0;
            YearTo = 1;
        }
        /// <summary>
        /// Start of active time
        /// </summary>
        public int YearFrom { get; set; }
        /// <summary>
        /// End of active time
        /// </summary>
        public int YearTo { get; set; }
        /// <summary>
        /// Duration of active years
        /// </summary>
        public int Duration
        {
            get
            {
                if (YearTo <= YearFrom)
                    return 1;

                return YearTo - YearFrom;
            }
        }
    }
}

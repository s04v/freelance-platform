using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Core.Common
{
    public static class Helpers
    {
        public static ProposalsRange GetJobProposalsRange(int proposals)
        {
            if (proposals >= 0 && proposals <= 5)
            {
                return ProposalsRange.From0To5;
            } 
            else if (proposals > 5 && proposals <= 1)
            {
                return ProposalsRange.From5To10;
            }
            else if (proposals > 10 && proposals <= 15)
            {
                return ProposalsRange.From10To15;
            }
            else 
            {
                return ProposalsRange.More15;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Common
{
    public enum JobType : byte
    {
        OnTime,
        LongTerm
    }

    public enum CandidateLevel : byte
    {
        Beginner,
        Intermediate,
        Expert
    }
    public enum ProposalsRange : byte
    {
        From0To5,
        From5To10,
        From10To15,
        More15,
    }
}

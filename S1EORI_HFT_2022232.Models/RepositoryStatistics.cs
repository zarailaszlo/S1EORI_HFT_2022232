using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S1EORI_HFT_2022232.Models
{
    public class RepositoryStatistics
    {
        public string RepositoryName { get; set; }
        public string UserName { get; set; }
        public int CommitCount { get; set; }
        public int AverageCommitLength { get; set; }
    }

}

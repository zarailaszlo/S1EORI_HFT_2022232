using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S1EORI_HFT_2022232.Models.Statistics
{
    public class RepositoryStatistics
    {
        public string RepositoryName { get; set; }
        public string UserId { get; set; }
        public int CommitCount { get; set; }
        public int AverageCommitLength { get; set; }
        public override string ToString()
        {
            return $"Repository: {RepositoryName}\t|UserId: {UserId}\t|Commit Count: {CommitCount}\t|Average Commit Length: {AverageCommitLength}";
        }
    }

}

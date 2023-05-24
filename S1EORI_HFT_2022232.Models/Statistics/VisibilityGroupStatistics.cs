using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S1EORI_HFT_2022232.Models.Statistics
{
    public class VisibilityGroupStatistics
    {
        public string Visibility { get; set; }
        public int RepositoryCount { get; set; }
        public override string ToString()
        {
            return $"Visibility: {Visibility} \tCount: {RepositoryCount}";
        }
    }
}

using System.Collections.Generic;

namespace SolarTally.Application.Sites.Queries.Dtos
{
    public class SiteDetailsList
    {
        public IList<SiteDetail> SiteDetails { get; set; }

        public int Count { get; set; }
    }
}
using System.Collections.Generic;

namespace SolarTally.Application.Sites.Queries.Dtos
{
    public class SitesListVm
    {
        public IList<SiteDetailVm> SiteDtos { get; set; }

        public int Count { get; set; }
    }
}
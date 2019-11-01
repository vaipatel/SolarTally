using System.Collections.Generic;
using SolarTally.Application.Sites.Queries.Dtos;

namespace SolarTally.Application.Sites.Queries.GetSitesList
{
    public class SitesListVm
    {
        public IList<SiteDto> Sites { get; set; }

        public int Count { get; set; }
    }
}
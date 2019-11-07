using System.Collections.Generic;

namespace SolarTally.Application.Sites.Queries.Dtos
{
    public class SitesListDto
    {
        public IList<SiteDto> Sites { get; set; }

        public int Count { get; set; }
    }
}
using System.Collections.Generic;

namespace SolarTally.Application.Sites.Queries.Dtos
{
    public class SiteDtosListVm
    {
        public IList<SiteDto> SiteDtos { get; set; }

        public int Count { get; set; }
    }
}
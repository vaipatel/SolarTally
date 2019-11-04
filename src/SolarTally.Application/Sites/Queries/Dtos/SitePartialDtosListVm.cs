using System.Collections.Generic;

namespace SolarTally.Application.Sites.Queries.Dtos
{
    public class SitePartialDtosListVm
    {
        public IList<SitePartialDto> Sites { get; set; }

        public int Count { get; set; }
    }
}
using System.Collections.Generic;

namespace SolarTally.Application.Common.Dtos
{
    public abstract class BaseListDto<T>
    {
        public IList<T> Items { get; set; }
    }
}
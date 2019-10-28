using System;
using AutoMapper;
using SolarTally.Application.Common.Mappings;
using SolarTally.Persistence;
using Xunit;

namespace SolarTally.Application.UnitTests.Common
{
    public class QueryTestFixture : IDisposable
    {
        public SolarTallyDbContext Context { get; private set; }
        public IMapper Mapper { get; private set; }

        public QueryTestFixture()
        {
            Context = SolarTallyContextFactory.Create();

            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });

            Mapper = configurationProvider.CreateMapper();
        }

        public void Dispose()
        {
            SolarTallyContextFactory.Destroy(Context);
        }
    }

    [CollectionDefinition("QueryCollection")]
    public class QueryCollection : ICollectionFixture<QueryTestFixture> {}
}
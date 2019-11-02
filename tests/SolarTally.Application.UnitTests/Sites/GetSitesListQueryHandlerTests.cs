﻿using AutoMapper;
using SolarTally.Application.Sites.Queries.GetSitesList;
using SolarTally.Application.UnitTests.Common;
using SolarTally.Persistence;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace SolarTally.Application.UnitTests.Sites
{
    [Collection("QueryCollection")]
    public class GetSitesListQueryHandlerTests
    {
        private readonly SolarTallyDbContext _context;
        private readonly IMapper _mapper;

        public GetSitesListQueryHandlerTests(QueryTestFixture fixture)
        {
            _context = fixture.Context;
            _mapper = fixture.Mapper;
        }

        [Fact]
        public async Task GetSitesTest()
        {
            var handler = new GetSitesListQueryHandler(
                _context, _mapper);

            var result = await handler.Handle(new GetSitesListQuery(),
                CancellationToken.None);
            
            Assert.IsType<SitesListVm>(result);
            Assert.Equal(1, result.Sites.Count);
            Assert.Equal(1, result.Count);
            Assert.Equal("PetroCanada Station", result.Sites.Last().Name);
        }

    }
}
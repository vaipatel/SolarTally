using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SolarTally.Application.Common.Interfaces;
using SolarTally.Application.Sites.Queries.Dtos;

namespace SolarTally.Application.Sites.Queries.GetSitesList
{
    /// <summary>
    /// Request DTO for getting all the sites in a SitesListVm.
    /// </summary>
    /// <remarks>
    /// The handler is placed after the request.
    /// </remarks>
    public class GetSitesListQuery : IRequest<SitesListVm>
    {
        // No props in our Query DTO at the moment. Maybe later we add UserId.
    }

    /// <summary>
    /// Handler for handling the GetSitesListsQuery request.
    /// </summary>
    public class GetSitesListQueryHandler :
        IRequestHandler<GetSitesListQuery, SitesListVm>
    {
        private readonly ISolarTallyDbContext _context;
        private readonly IMapper _mapper;

        public GetSitesListQueryHandler(ISolarTallyDbContext context,
            IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<SitesListVm> Handle(GetSitesListQuery request,
            CancellationToken cancellationToken)
        {
            // Previous
            // --------------------------------------------------------------
            // Note: I'm adding AsNoTracking() below because without it I get
            // the following error
            // ------
            // System.InvalidOperationException : A tracking query projects 
            // owned entity without corresponding owner in result. Owned 
            // entities cannot be tracked without their owner. Either include 
            // the owner entity in the result or make query non-tracking using 
            // AsNoTracking().
            // ------
            // Note (cont.): This might be a bug in EF Core 3.0, and might be
            // resolved in 3.1.0, 3.1.0 preview.
            // See https://github.com/aspnet/EntityFrameworkCore/issues/18024

            // var sites = await _context.Sites
            //     .AsNoTracking()
            //     .ProjectTo<SiteDto>(_mapper.ConfigurationProvider)
            //     .ToListAsync(cancellationToken);
            // --------------------------------------------------------------

            // Edit
            // --------------------------------------------------------------
            // So it seems that while the exclusion of AsNoTracking() causes
            // buggy behavior related to owned types, it also seems that the
            // AsNoTracking() was killing all the relational information (0 ids
            // being a clear sign) in the returned entities.
            // So, for example, the operation would fail to get any of the
            // associated ApplianceUsages.
            // I'm deciding to get all the tracked entities instead and just map
            // each to its Dto myself.
            // --------------------------------------------------------------
            
            var sites = await _context.Sites.ToListAsync(cancellationToken);
            var siteDtos = new List<SiteDto>();
            foreach( var site in sites)
            {
                var siteDto = _mapper.Map<SiteDto>(site);
                siteDtos.Add(siteDto);
            }
            
            var vm = new SitesListVm
            {
                Sites = siteDtos,
                Count = siteDtos.Count
            };

            return vm;
        }
    }
}
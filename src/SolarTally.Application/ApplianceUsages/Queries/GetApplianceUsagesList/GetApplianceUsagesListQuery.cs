using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SolarTally.Application.ApplianceUsages.Queries.Dtos;
using SolarTally.Application.Common.Interfaces;

namespace SolarTally.Application.ApplianceUsages.Queries.GetApplianceUsagesList
{
    /// <summary>
    /// Request DTO for getting all the appliances usages in a
    /// ApplianceUsagesListVm.
    /// </summary>
    /// <remarks>
    /// The handler is placed after the request.
    /// </remarks>
    public class GetApplianceUsagesListQuery : IRequest<ApplianceUsagesListVm>
    {
    }

    /// <summary>
    /// Handler for handling the GetApplianceUsagesListQuery request.
    /// </summary>
    public class GetApplianceUsagesListQueryHandler :
        IRequestHandler<GetApplianceUsagesListQuery, ApplianceUsagesListVm>
    {
        private readonly ISolarTallyDbContext _context;
        private readonly IMapper _mapper;

        public GetApplianceUsagesListQueryHandler(ISolarTallyDbContext context,
            IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ApplianceUsagesListVm> Handle(
            GetApplianceUsagesListQuery request,
            CancellationToken cancellationToken)
        {
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
            var applianceUsagesDtos = await _context.ApplianceUsages
                .AsNoTracking()
                .ProjectTo<ApplianceUsageDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
            
            var vm = new ApplianceUsagesListVm
            {
                ApplianceUsages = applianceUsagesDtos
            };

            return vm;
        }
    }
}
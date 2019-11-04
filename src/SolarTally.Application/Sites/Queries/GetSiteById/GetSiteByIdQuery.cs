using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SolarTally.Application.Common.Interfaces;
using SolarTally.Application.Sites.Queries.Dtos;

namespace SolarTally.Application.Sites.Queries.GetSiteById
{
    /// <summary>
    /// Request DTO for getting single site by its Id.
    /// </summary>
    /// <remarks>
    /// The handler is placed after the request.
    /// </remarks>
    public class GetSiteByIdQuery : IRequest<SiteDto>
    {
        /// Id of the site
        public int Id { get; set; }
    }

    /// <summary>
    /// Handler for handling the GetSiteByIdQuery request.
    /// </summary>
    public class GetSiteByIdQueryHandler :
        IRequestHandler<GetSiteByIdQuery, SiteDto>
    {
        private readonly ISolarTallyDbContext _context;
        private readonly IMapper _mapper;

        public GetSiteByIdQueryHandler(ISolarTallyDbContext context,
            IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<SiteDto> Handle(GetSiteByIdQuery request,
            CancellationToken cancellationToken)
        {
            var site = await _context.Sites
                .Where(s => s.Id == request.Id)
                .SingleAsync(cancellationToken);
            
            var siteDto = _mapper.Map<SiteDto>(site);

            return siteDto;
        }
    }
}
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SolarTally.Application.Common.Interfaces;
using SolarTally.Application.Consumptions.Queries.Dtos;

namespace SolarTally.Application.Consumptions.Queries.GetConsumptionById
{
    /// <summary>
    /// Request DTO for getting single consumption by its Id (the site id).
    /// </summary>
    /// <remarks>
    /// The handler is placed after the request.
    /// </remarks>
    public class GetConsumptionByIdQuery : IRequest<ConsumptionDto>
    {
        /// Id of the consumption ( == site id )
        public int Id { get; set; }
    }

    /// <summary>
    /// Handler for handling the GetConsumptionByIdQuery request.
    /// </summary>
    public class GetConsumptionByIdQueryHandler :
        IRequestHandler<GetConsumptionByIdQuery, ConsumptionDto>
    {
        private readonly ISolarTallyDbContext _context;
        private readonly IMapper _mapper;

        public GetConsumptionByIdQueryHandler(ISolarTallyDbContext context,
            IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ConsumptionDto> Handle(
            GetConsumptionByIdQuery request,
            CancellationToken cancellationToken)
        {
            var consumption = await _context.Consumptions
                .Where(s => s.Id == request.Id)
                .SingleAsync(cancellationToken);
            
            var consumptionDto = _mapper.Map<ConsumptionDto>(consumption);

            return consumptionDto;
        }
    }
}
using System.Collections.Generic;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SolarTally.Application.Common.Interfaces;
using SolarTally.Domain.Entities;

namespace SolarTally.Application.ApplianceUsages.Commands.AddApplianceUsage
{
    public class AddApplianceUsageCommand : IRequest
    {
        public int ConsumptionId { get; set; }
        public int ApplianceId { get; set; }
        public int Quantity { get; set; }
        public decimal PowerConsumption { get; set; }
        public int NumHoursOnSolar { get; set; } 
        public int NumHoursOffSolar { get; set; } 
        // public bool Enabled { get; set; }
    }

    public class AddApplianceUsageCommandHandler :
        IRequestHandler<AddApplianceUsageCommand, Unit>
    {
        private readonly ISolarTallyDbContext _context;
        // private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public AddApplianceUsageCommandHandler(ISolarTallyDbContext context,
            IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }

        public async Task<Unit> Handle(AddApplianceUsageCommand command,
            CancellationToken cancellationToken)
        {
            var applianceQuery = from a in _context.Appliances
                where a.Id == command.ApplianceId
                select a;
            
            var appliance = await applianceQuery.SingleOrDefaultAsync(cancellationToken);
            
            var siteQuery = _context.Sites
                .Where(s => s.Id == command.ConsumptionId)
                .Include(s => s.Consumption)
                    .ThenInclude(c => c.ApplianceUsages)
                        .ThenInclude(au => au.Appliance)
                ;
            
            var site = await
                siteQuery.SingleOrDefaultAsync(cancellationToken);
            
            // Some logging
            Console.WriteLine($"\nSiteId is {site.Id}\n");
            Console.WriteLine($"\nConsumptionId is {site.Consumption.Id}\n");

            site.Consumption.AddApplianceUsage(appliance);
            var au = site.Consumption.ApplianceUsages.Last();
            au.SetQuantity(command.Quantity);
            au.SetPowerConsumption(command.PowerConsumption);
            au.SetNumHoursOnSolar(command.NumHoursOnSolar);
            au.SetNumHoursOffSolar(command.NumHoursOffSolar);

            _context.ApplianceUsages.Add(au);

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
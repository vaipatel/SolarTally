using System.Collections.Generic;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SolarTally.Application.Common.Interfaces;
using SolarTally.Domain.Entities;
using SolarTally.Application.ApplianceUsages.Queries.Dtos;
using SolarTally.Application.Consumptions.Queries.Dtos;
using AutoMapper;
using SolarTally.Domain.ValueObjects;
using SolarTally.Domain.Enumerations;

namespace SolarTally.Application.ApplianceUsages.Commands.UpdateApplianceUsage
{
    public class UpdateApplianceUsageCommand : IRequest<ConsumptionDto>
    {
        public int ConsumptionId { get; set; }
        public int ApplianceUsageId { get; set; }
        // public int ApplianceId { get; set; }
        public int Quantity { get; set; }
        public decimal PowerConsumption { get; set; }
        public IList<UsageTimeIntervalAbrv> UsageIntervals { get; set; }
        // public bool Enabled { get; set; }
    }

    public class UpdateApplianceUsageCommandHandler :
        IRequestHandler<UpdateApplianceUsageCommand, ConsumptionDto>
    {
        private readonly ISolarTallyDbContext _context;
        // private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public UpdateApplianceUsageCommandHandler(ISolarTallyDbContext context,
            IMediator mediator, IMapper mapper)
        {
            _context = context;
            _mediator = mediator;
            _mapper = mapper;
        }

        public async Task<ConsumptionDto> Handle(
            UpdateApplianceUsageCommand command,
            CancellationToken cancellationToken)
        {
            // var applianceQuery = from a in _context.Appliances
            //     where a.Id == command.ApplianceId
            //     select a;
            
            // var appliance = await applianceQuery.SingleOrDefaultAsync(cancellationToken);
            
            var consumptionQuery = _context.Consumptions
                .Where(c => c.Id == command.ConsumptionId)
                .Include(c => c.ApplianceUsages)
                    .ThenInclude(au => au.ApplianceUsageSchedule)
                        // .ThenInclude(aus => aus.UsageIntervals)
                        // .ThenInclude(au => au.Appliance)
                ;
            
            var consumption = await
                consumptionQuery.SingleOrDefaultAsync(cancellationToken);
            
            var au = consumption.ApplianceUsages
                .Where(au => au.Id == command.ApplianceUsageId)
                .First();

            // Some logging
            Console.WriteLine($"\nApplianceUsageId is {au.Id}\n");
            Console.WriteLine($"Num usage intervals: {command.UsageIntervals.Count}\n");

            au.ApplianceUsageSchedule.ClearUsageIntervals();
            foreach(var ui in command.UsageIntervals)
            {
                var ti = ui.TimeInterval;
                var startTI = ui.TimeInterval.Start;
                var startHr = startTI.Hours; var startMin = startTI.Minutes;
                var endTI = ui.TimeInterval.End;
                var endHr = endTI.Hours; var endMin = endTI.Minutes;
                au.ApplianceUsageSchedule.AddUsageInterval(startHr, startMin,
                    endHr, endMin, ui.UsageKind);
            }

            au.SetQuantity(command.Quantity);
            au.SetPowerConsumption(command.PowerConsumption);
            consumption.Recalculate();

            await _context.SaveChangesAsync(cancellationToken);

            var consumptionDto = _mapper.Map<ConsumptionDto>(consumption);

            return consumptionDto;
        }
    }
}
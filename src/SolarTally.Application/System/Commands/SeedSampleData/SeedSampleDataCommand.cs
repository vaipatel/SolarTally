using MediatR;
using SolarTally.Application.Common.Interfaces;
//using SolarTally.Persistence;
using System.Threading;
using System.Threading.Tasks;

namespace SolarTally.Application.System.Commands.SeedSampleData
{
    public class SeedSampleDataCommand : IRequest
    {
        // No data in DTO, this just triggers the Handler.
    }

    public class SeedSampleDataCommandHandler : IRequestHandler<SeedSampleDataCommand>
    {
        private readonly ISolarTallyDbContext _context;
        private readonly IUserManager _userManager;

        public SeedSampleDataCommandHandler(ISolarTallyDbContext context, IUserManager userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<Unit> Handle(SeedSampleDataCommand request, CancellationToken cancellationToken)
        {
            var seeder = new SampleDataSeeder(_context, _userManager);

            await seeder.SeedAllAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
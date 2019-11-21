using SolarTally.Application.Common.Interfaces;
using SolarTally.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SolarTally.Application.System.Commands.SeedSampleData
{
    public class SampleDataSeeder
    {
        private readonly ISolarTallyDbContext _context;
        private readonly IUserManager _userManager;

        private readonly Dictionary<int, Site> Sites =
            new Dictionary<int, Site>();
        private readonly Dictionary<int, Consumption> Consumptions =
            new Dictionary<int, Consumption>();
        private readonly Dictionary<int, ApplianceUsage> ApplianceUsages =
            new Dictionary<int, ApplianceUsage>();
        private readonly Dictionary<int, Appliance> Appliances =
            new Dictionary<int, Appliance>();

        public SampleDataSeeder(ISolarTallyDbContext context, IUserManager userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        
        public async Task SeedAllAsync(CancellationToken cancellationToken)
        {
            MakeAppliances();
            MakeSites();
            MakeConsumptions();
            MakeApplianceUsages();

            foreach(var appliance in Appliances.Values)
            {
                _context.Appliances.Add(appliance);
            }
            await _context.SaveChangesAsync(cancellationToken);

            foreach(var site in Sites.Values)
            {
                _context.Sites.Add(site);
                _context.Consumptions.Add(site.Consumption);
                foreach(var consumption in Consumptions.Values)
                {
                    foreach(var au in consumption.ApplianceUsages)
                    {
                        _context.ApplianceUsages.Add(au);
                    }
                }
            }
            await _context.SaveChangesAsync(cancellationToken);
        }

        public void MakeAppliances()
        {
            Appliances.Add(1, new Appliance("LED", "An LED bulb", 20, 20));
            Appliances.Add(2, new Appliance("TV", "A 32 inch LCD TV", 80, 80));
            Appliances.Add(3, new Appliance("Heater", "", 300, 500));
        }

        public void MakeSites()
        {
            Sites.Add(1, new Site("Campbell Residence"));
            Sites.Add(2, new Site("St. Mary School"));
            Sites.Add(3, new Site("OTI Imaging Center"));
        }

        public void MakeConsumptions()
        {
            foreach(var site in Sites)
            {
                Consumptions.Add(site.Key, site.Value.Consumption);
            }
        }

        public void MakeApplianceUsages()
        {
            Consumptions[1].AddApplianceUsage(Appliances[1]);
            Consumptions[1].AddApplianceUsage(Appliances[2]);

            Consumptions[2].AddApplianceUsage(Appliances[2]);
            Consumptions[2].ApplianceUsages.Last().SetQuantity(10);
            Consumptions[2].ApplianceUsages.Last().SetNumHoursOnSolar(5);
            Consumptions[2].ApplianceUsages.Last().SetNumHoursOffSolar(3);
            Consumptions[2].AddApplianceUsage(Appliances[3]);

            Consumptions[3].AddApplianceUsage(Appliances[1]);
            Consumptions[3].AddApplianceUsage(Appliances[2]);
            Consumptions[3].AddApplianceUsage(Appliances[3]);
        }

    }
}
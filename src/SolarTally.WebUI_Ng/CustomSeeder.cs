using SolarTally.Application.Common.Interfaces;
using SolarTally.Domain.Entities;
using SolarTally.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SolarTally.WebUI_Ng
{
    public class CustomSeeder
    {
        private readonly ISolarTallyDbContext _context;

        private readonly Dictionary<int, Site> Sites =
            new Dictionary<int, Site>();
        private readonly Dictionary<int, Consumption> Consumptions =
            new Dictionary<int, Consumption>();
        private readonly Dictionary<int, ApplianceUsage> ApplianceUsages =
            new Dictionary<int, ApplianceUsage>();
        private readonly Dictionary<int, Appliance> Appliances =
            new Dictionary<int, Appliance>();

        public CustomSeeder(ISolarTallyDbContext context)
        {
            _context = context;
        }
        
        public void SeedAll()
        {
            MakeAppliances();
            MakeSites();
            MakeConsumptions();
            MakeApplianceUsages();

            foreach(var appliance in Appliances.Values)
            {
                _context.Appliances.Add(appliance);
            }

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
            _context.SaveChanges(true);
        }

        public void MakeAppliances()
        {
            Appliances.Add(1, new Appliance("LED", "An LED bulb", 10, 10));
            Appliances.Add(2, new Appliance("TV", "55 inch 4K OLED TV", 350,
                350));
            Appliances.Add(3, new Appliance("Heater", "Ceramic Heater", 1000,
                1500));
            Appliances.Add(4, new Appliance("Refrigerator", "", 200, 300));
            Appliances.Add(5, new Appliance("Furnace", "1/4 hp", 600, 1000));
            Appliances.Add(6, new Appliance("Microwave", "FoodNukem", 1500,
                1500));
        }

        public void MakeSites()
        {
            Sites.Add(1, new Site("Campbell Residence", 7));
            Sites[1].MainAddress = new Address("0 Bloor St.",
            "Toronto", "Ontario", "Canada", "M1N2O3");
            Sites.Add(2, new Site("St. Mary School", 9));
            Sites[2].MainAddress = new Address("0 Major Mackenzie St.",
            "Richmond Hill", "Ontario", "Canada", "L3M4N5");
            Sites.Add(3, new Site("OTI Imaging Center", 8));
            Sites[3].MainAddress = new Address("0 Sandalwood St.",
            "Brampton", "Ontario", "Canada", "L6M7N8");
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
            Consumptions[1].ApplianceUsages.Last().SetQuantity(20);
            Consumptions[1].AddApplianceUsage(Appliances[2]);
            Consumptions[1].ApplianceUsages.Last().SetQuantity(2);

            Consumptions[2].AddApplianceUsage(Appliances[2]);
            Consumptions[2].ApplianceUsages.Last().SetQuantity(5);
            Consumptions[2].ApplianceUsages.Last().SetNumHoursOnSolar(5);
            Consumptions[2].ApplianceUsages.Last().SetNumHoursOffSolar(3);
            Consumptions[2].AddApplianceUsage(Appliances[3]);

            Consumptions[3].AddApplianceUsage(Appliances[1]);
            Consumptions[3].ApplianceUsages.Last().SetQuantity(30);
            Consumptions[3].AddApplianceUsage(Appliances[2]);
            Consumptions[3].ApplianceUsages.Last().SetQuantity(4);
            Consumptions[3].AddApplianceUsage(Appliances[3]);
            Consumptions[3].ApplianceUsages.Last().SetQuantity(10);
        }

    }
}
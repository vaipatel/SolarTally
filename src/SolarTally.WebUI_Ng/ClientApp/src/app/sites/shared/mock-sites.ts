import { Site } from '../../shared/dtos/site';

export const SITES: Site[] = [
    { 
        id: 1, name: "Great Lakes Vet", numSolarHours: 6, mainAddress: { 
            street: "0 Sandalwood Pkwy", 
            city: "Brampton", 
            state: "ON", 
            country: "CAN", 
            zipCode: "L6N8P0" },
        consumptionTotal: { 
            totalPowerConsumption: 100, 
            totalEnergyConsumption: 500
        }
    },
    { 
        id: 1, name: "First Choice", numSolarHours: 9, mainAddress: { 
            street: "0 Front St", 
            city: "Toronto", 
            state: "ON", 
            country: "CAN", 
            zipCode: "M6N8O0" },
        consumptionTotal: { 
            totalPowerConsumption: 320, 
            totalEnergyConsumption: 1600
        }
    },
    { 
        id: 1, name: "Aberfoyle Mill", numSolarHours: 5, mainAddress: { 
            street: "0 Brock Rd", 
            city: "Guelph", 
            state: "ON", 
            country: "CAN", 
            zipCode: "N0O1P2" },
        consumptionTotal: { 
            totalPowerConsumption: 1040, 
            totalEnergyConsumption: 4160
        }
    }
]

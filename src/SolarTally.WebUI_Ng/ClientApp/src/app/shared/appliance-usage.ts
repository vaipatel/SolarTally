import { Appliance } from './appliance';

export class ApplianceUsage {
    id: number;
    applianceDto: Appliance;
    quantity: number;
    powerConsumption: number;
    numHours: number;
    percentHrsOnSolar: number;
    enabled: boolean;
    consumptionId: number;
}

export class ApplianceUsageLst {
    items: ApplianceUsage[];
}

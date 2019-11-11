import { Appliance } from './appliance';
import { ApplianceUsageTotal } from './appliance-usage-total';

export class ApplianceUsage {
    id: number;
    applianceDto: Appliance;
    quantity: number;
    powerConsumption: number;
    numHoursOnSolar: number;
    numHoursOffSolar: number;
    numHours: number;
    enabled: boolean;
    consumptionId: number;
    applianceUsageTotal: ApplianceUsageTotal;
}

export class ApplianceUsageLst {
    items: ApplianceUsage[];
}

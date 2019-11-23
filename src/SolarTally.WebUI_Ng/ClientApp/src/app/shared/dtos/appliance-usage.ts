import { Appliance } from './appliance';
import { ApplianceUsageTotal } from './appliance-usage-total';
import { ApplianceUsageSchedule } from './appliance-usage-schedule';

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
    applianceUsageScheduleDto: ApplianceUsageSchedule;
}

export class ApplianceUsageLst {
    items: ApplianceUsage[];
}

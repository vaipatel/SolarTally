import { Appliance } from './appliance';
import { ApplianceUsageTotal } from './appliance-usage-total';
import { ApplianceUsageSchedule, ApplianceUsageScheduleStr } from './appliance-usage-schedule';

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

export class ApplianceUsageStr {
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
    applianceUsageScheduleDto: ApplianceUsageScheduleStr;
}

export class ApplianceUsageLstStr {
    items: ApplianceUsageStr[];
}

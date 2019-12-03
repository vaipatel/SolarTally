import { UsageTimeInterval, UsageTimeIntervalStr } from './usage-time-interval';

export class UpdateApplianceUsageCommand {
    consumptionId: number;
    applianceUsageId: number;
    quantity: number;
    powerConsumption: number;
    usageIntervals: Array<UsageTimeInterval>;
}

export class UpdateApplianceUsageCommandStr {
    consumptionId: number;
    applianceUsageId: number;
    quantity: number;
    powerConsumption: number;
    usageIntervals: Array<UsageTimeIntervalStr>;
}
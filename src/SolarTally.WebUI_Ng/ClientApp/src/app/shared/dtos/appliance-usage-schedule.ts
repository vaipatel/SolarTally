import { UsageTimeInterval } from './usage-time-interval';
import { TimeSpan } from './time-span';

export class ApplianceUsageSchedule
{
    usageIntervals: UsageTimeInterval[];
    totalTimeOnSolar: TimeSpan;
    totalTimeOffSolar: TimeSpan;
    hoursOnSolar: number;
    hoursOffSolar: number;
    hours: number;
}
import { TimeInterval, TimeIntervalAbrv, TimeIntervalStr } from './time-interval';
import { UsageKind } from '../enums/usage-kind.enum';

export class UsageTimeInterval
{
    timeInterval: TimeInterval;
    usageKind: UsageKind;
}

export class UsageTimeIntervalAbrv
{
    timeInterval: TimeIntervalAbrv;
    usageKind: UsageKind;
}

export class UsageTimeIntervalStr
{
    timeInterval: TimeIntervalStr;
    usageKind: UsageKind;
}
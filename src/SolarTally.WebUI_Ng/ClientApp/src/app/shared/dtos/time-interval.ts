import { TimeSpan, TimeSpanAbrv } from './time-span';

export class TimeInterval
{
    start: TimeSpan;
    end: TimeSpan;
    difference: TimeSpan;
}

export class TimeIntervalAbrv
{
    start: TimeSpanAbrv;
    end: TimeSpanAbrv;
    difference: TimeSpanAbrv;
}
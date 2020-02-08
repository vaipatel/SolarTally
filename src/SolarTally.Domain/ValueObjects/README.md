# ValueObjects

This folder will contain all the Enterprise value objects. These objects
* do not have an Id, and 
* are immutable

These objects will typically belong to an Entity, either by themselves or inside
a collection.

# Don'ts

## Don't Share ValueObjects

You have a `TimeInterval` value object that is used in `Site` as
`PeakSolarInterval`, and also in `UsageTimeInterval` as just `TimeInterval`.

You want to inform each `UsageTimeInterval` in the
`ApplianceUsageSchedule.UsageTimeIntervals` collection that the `Site`'s
`PeakSolarInterval` has changed.

You just pass the `PeakSolarInterval` from `Site` down to
`ApplianceUsageSchedule`.

You then erroneously use it directly inside a new `UsageTimeInterval`, so 
that it is now shared between two entities. You then obliviously append to the
`UsageTimeIntervals` collection.

Because of the erroneous sharing, you'll first see the warning:
```
The same entity is being tracked as different weak entity types 'Site.PeakSolarInterval#TimeInterval' and 'UsageTimeInterval.TimeInterval#TimeInterval'. If a property value changes it will result in two store changes, which might not be the desired outcome.
```

It'll be followed by the `Microsoft.EntityFrameworkCore.Update` `fail` message:
```
An exception occurred in the database while saving changes for context type 'SolarTally.Persistence.SolarTallyDbContext'.
      System.InvalidOperationException: The entity of type 'Site.PeakSolarInterval#TimeInterval' is sharing the table 'Sites' with entities of type 'Site', but there is no entity of this type with the same key value that has been marked as 'Added'. Consider using 'DbContextOptionsBuilder.EnableSensitiveDataLogging' to see the key values.
```

### Alternative

Just pass the requisite value object info to whoever needs it, and then they
should make the value object anew.


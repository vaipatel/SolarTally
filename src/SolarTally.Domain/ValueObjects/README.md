# ValueObjects

This folder will contain all the Enterprise value objects. These objects
* do not have an Id, and 
* are immutable

These objects will typically belong to an Entity, either by themselves or inside
a collection.

# Don'ts

## Don't Share ValueObjects

You have a `TimeInterval` value object that is used in `Site` called
`PeakSolarInterval`, and also in `TimeIntervalWithKind` which appear in the
collection `ApplianceUsageSchedule.TimeIntervalsWithKind`.

You want to inform each `TimeIntervalWithKind` in 
`ApplianceUsageSchedule.TimeIntervalsWithKind` that the `Site`'s
`PeakSolarInterval` has changed.

You just pass the `PeakSolarInterval` from `Site` down to
`ApplianceUsageSchedule`.

You then erroneously use it directly inside a new `TimeIntervalWithKind`, so 
that it is now shared between two entities, and you then append to the
`TimeIntervalsWithKind` collection.

Because of the erroneous sharing, you'll first see the warning:
```
The same entity is being tracked as different weak entity types 'Site.PeakSolarInterval#TimeInterval' and 'TimeIntervalWithKind.TimeInterval#TimeInterval'. If a property value changes it will result in two store changes, which might not be the desired outcome.
```

It'll be followed by the `Microsoft.EntityFrameworkCore.Update` `fail` message:
```
An exception occurred in the database while saving changes for context type 'SolarTally.Persistence.SolarTallyDbContext'.
      System.InvalidOperationException: The entity of type 'Site.PeakSolarInterval#TimeInterval' is sharing the table 'Sites' with entities of type 'Site', but there is no entity of this type with the same key value that has been marked as 'Added'. Consider using 'DbContextOptionsBuilder.EnableSensitiveDataLogging' to see the key values.
```

### Alternative

Just pass the requisite value object info to whoever needs it, and then they
should make the value object anew.


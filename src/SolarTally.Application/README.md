# Application Layer

[The Application layer, per Jason Taylor](https://youtu.be/Zygw4UAxCdg?t=55),
should represent the "business logic" of our software system.

This logic is not enterprise-wide, like domain logic, but rather restricted to
the current application. 

So for example, per Jason's talk above, [if we want to implement the Repository 
pattern, we should make an IRepository interface in the Application layer and
implement the interface in the Persistence layer](https://youtu.be/Zygw4UAxCdg?t=105).
This makes us much more independent of any specific Persistence technologies.

## Naming Conventions

### DTOs

For now, I'm suffixing `Dto` to a Data Transfer Object.

If it's a class representing a collection of `Dto`s, I go: {pluralized object
name}{collection type}`Dto`.

So, for example, `SiteDto` and `SitesListDto`,
`ApplianceUsageDto` and `ApplianceUsagesListDto` etc.

The hope is the the `Dto` will give it away. I don't want to use 
`ViewModel`/`VM` because [apparently those can have behavior](https://stackoverflow.com/questions/1982042/dto-viewmodel).

It seems this is [a rather contentious topic](https://stackoverflow.com/questions/1724774/java-data-transfer-object-naming-convention/35341664).

My take for now is that I would like to distinguish various mapped entities 
that all have to coexist in the same monolithic space but not be too pedantic
about it, so I'm using the **an** approach that has **some** consistency
(**to me :)**).

Perhaps if the structure of the entire project (as in the undertaking, not the
csprojs) is fundamentally different, it might be ok or even better to use the
same names.

For example, I might still `ApplianceUsage` in the angular app because there is
enough of a physical separation in my mind between where the client-side JS runs
vs where the server-side .NET runs.

### Queries

With the above approach for DTOs, I feel it best to do:

{name}`Dto` -> `Get`{name}`Query` and `Get`{name}`QueryHandler`.

Examples:
* `SiteDto` -> `GetSiteQuery` and `GetSiteQueryHandler`
* `ApplianceUsagesListDto` -> `GetApplianceUsagesListQuery` and `GetApplianceUsagesListQueryHandler`

# References
* Mostly stealing, mostly verbatim, from [Jason's talk linked 
  above](https://youtu.be/Zygw4UAxCdg).
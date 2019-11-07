# SolarTally (WIP Under Development!)
[![Build Status](https://dev.azure.com/vaipatel/SolarTallyProject/_apis/build/status/vaipatel.SolarTally_AspDotNetCore?branchName=master)](https://dev.azure.com/vaipatel/SolarTallyProject/_build/latest?definitionId=2&branchName=master)

An open-source app to tally electricity needs and solar equipment costs for 
off-grid solar setups.

## Key technologies

* ASP .NET Core 3 with EF Core 3
* Angular 8

## Configuring Secrets for Development enviroment

When working in the Development environment, I use the
[Secret Manager tool](https://docs.microsoft.com/en-us/aspnet/core/security/app-secrets?view=aspnetcore-3.0&tabs=linux#secret-manager)
to configure user secrets in the connection strings.

Since we're using postgres for our database, I've assigned the secret id to be
`postgres` (but perhaps later I can generalize this).

In order to configure the secrets on your machine, you should:

1. `cd src/SolarTally.WebUI_Ng`
2. `dotnet user-secrets init --id postgres`
   
   Unless you've already used the `postgres` id for a previous project, this
   will create an empty `secrets.json` under
   [`{user_secrets_path}`/](https://docs.microsoft.com/en-us/aspnet/core/security/app-secrets?view=aspnetcore-3.0&tabs=windows#how-the-secret-manager-tool-works)`postgres`.
   
   It normally also adds a `<UserSecretsId>postgres</UserSecretsId>` to the
   csproj, but in our case the `SolarTally.WebUI_Ng.csproj` already has this
   attribute.
   
   Nevertheless, depending on your platform, it might cause some harmless
   formatting changes to the csproj xml, which *should* be ok to commit.
3. `dotnet user-secrets set "PostgresUsername" "YOUR_POSTGRES_USERNAME"`
4. `dotnet user-secrets set "PostgresPassword" "YOUR_POSTGRES_PASSWORD"`

Now the secrets can be accessed from both the WebUI_Ng and Persistence projects.

## Architectural Concerns

### Monolithic Architecture

This app (inclusive of all the different projects in the solution) has a 
[monolithic architecture](https://docs.microsoft.com/en-us/dotnet/architecture/modern-web-apps-azure/common-web-application-architectures),
as opposed to, say, a [microservices architecture](https://azure.microsoft.com/en-us/blog/microservices-an-application-revolution-powered-by-the-cloud).

There are quite a few pros and cons of each architecural style.
* The monolithic architecture is considerably simpler and faster to develop than
  the microservices architecture.
* On the other hand, the microservices architecture offers much greater and much
  more granular scalability.

### Domain-Driven Design or DDD

The entire architecure tries to adhere to the Domain-Driven design or DDD. In
DDD, the Core layers contain:
* a representation of business-wide entities at the foundation of the entire
  project, called the Domain layer, and
* the surrounding application-wide logic for interfacing with those entities,
  called the Application layer

Specific technologies for Persistence and Web access must adhere to the
data + logic imposed by the Core layers. But the Core layers must not rely on
anything implemented in the Persistence and Web layers.

In fact, I feel like a stronger adherent would make sure that Core does not rely
on the specific technologies used in Persistence and Web. That would make it
easy to swap out different ORMs etc. So, for example, you should not have the
Domain or Application referencing, say, EF Core, because that is an ORM that
deals with Persistence. Anyway, maybe that's too ignorant of the vastly more
common cases.

## Resources

Here were some resources I referenced when making the app.

#### Julie Lerman

* YouTube
  * [Mapping DDD Domain Models with EF Core 2.1 @ Update Conference Prague 2018
  ](https://youtu.be/Z62cbp61Bb8)

#### Jason Taylor

* Github
  * [NorthwindTraders](https://github.com/JasonGT/NorthwindTraders)

* YouTube
  * [Clean Architecture with ASP.NET Core 2.2](https://youtu.be/Zygw4UAxCdg)


#### Steve Smith

* Github
  * [eShopOnWeb](https://github.com/dotnet-architecture/eShopOnWeb)

* YouTube
  * [Tour of Microsoft's Reference ASP NET Core App eShopOnWeb
  ](https://www.youtube.com/watch?v=rSpF1s8wcyA)
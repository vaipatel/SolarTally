# Persistence Layer

This layer will leverage available persistence technologies to persist data to
various databases. It will use our Domain entities and specific database
technologies (like drivers) to implement data-touching interfaces in the
Application layer.

# References

* DesignTimeDbContextFactoryBase based on [JasonGT's DesignTimeDbContextFactoryBase](https://github.com/JasonGT/NorthwindTraders/blob/master/Src/Persistence/DesignTimeDbContextFactoryBase.cs)
* SolarDbContext based on [JasonGT's NorthwindDbContext](https://github.com/JasonGT/NorthwindTraders/blob/master/Src/Persistence/NorthwindDbContext.cs)
* SolarDbContextFactory based on [JasonGT's NorthwindDbContextFactory](https://github.com/JasonGT/NorthwindTraders/blob/master/Src/Persistence/NorthwindDbContextFactory.cs)
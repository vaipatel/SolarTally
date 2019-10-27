# Domain Layer

This folder should contain all the strictly "domain-related" classes and 
functions - it represents the [innermost circle, excluding the "Application"
layer in the "onion architecture"](https://youtu.be/Zygw4UAxCdg?t=55).

It should encapsulate a deep, application-independent, enterprise-wide level 
representation of the shape of our data and how it wishes to be manipulated IMO.

[According to Jimmy Bogard](https://lostechies.com/jimmybogard/2010/02/04/strengthening-your-domain-a-primer/),
"\[a\] strong domain means that our objects become more behavioral, and less as 
solely data-holders".

This folder structure is based on a combination of:
* [NorthwindTraders' Src/Domain folder](https://github.com/JasonGT/NorthwindTraders/tree/master/Src/Domain)
* [eShopOnWeb's src/ApplicationCore folder](https://github.com/dotnet-architecture/eShopOnWeb/tree/master/src/ApplicationCore)
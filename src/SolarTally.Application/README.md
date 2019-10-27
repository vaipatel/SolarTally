# Application Layer

[The Application layer, per Jason Taylor](https://youtu.be/Zygw4UAxCdg?t=55),
should represent the "business logic" of our software system.

This logic is not enterprise-wide, like domain logic, but rather restricted to
the current application. 

So for example, [if we want to implement the Repository pattern, we should make
an IRepository interface in the Application layer and implement the interface in
the Persistence layer](https://youtu.be/Zygw4UAxCdg?t=105). This makes us much
more independent of any specific Persistence technologies.

# References
* Mostly stealing, mostly verbatim, from [Jason's talk linked 
  above](https://youtu.be/Zygw4UAxCdg).
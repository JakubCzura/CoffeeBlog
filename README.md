# CoffeeBlog

### Rules to write code for this project:
Prefer FluentValidation over DataAnnotations. <br/>
Prefer explicit FluentValidation in controller over auto-validation. <br/>
Prefer FluentApi over DataAnnotations or default Entity Framework behaviour to configure entities. <br/>
Always specify entities' details like IsRequired(), ToTable() despite default Entity Framework behaviour for better readability and understanding the code (even if it requires writing some more code lines - it's worth :) ). <br/>

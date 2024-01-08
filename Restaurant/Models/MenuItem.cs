using ErrorOr;

public class MenuItem
{
  
  public Guid Id { get;  }
  public string Name { get;  }
  public string Category { get;  }
  public decimal Price { get;  }
  public bool IsAvailable { get;  }

  private MenuItem(
    Guid id,
    string name,
    string category,
    decimal price,
    bool isAvailable
  ){
    Id = id;
    Name = name;
    Category = category;
    Price = ValidatePrice(price);
    IsAvailable = isAvailable;
  }

  public static ErrorOr<MenuItem> Create (
    string name,
    string category,
    decimal price,
    bool isAvailable,
    Guid? id = null
  ){     
    return new MenuItem(
      id ?? Guid.NewGuid(),
      name,
      category,
      price,
      isAvailable
    );
  }

   private static decimal ValidatePrice(decimal price)
    {
        // Enforce two decimal places for the price
        decimal validatedPrice = Math.Round(price, 2, MidpointRounding.AwayFromZero);
        return validatedPrice;
    }
}
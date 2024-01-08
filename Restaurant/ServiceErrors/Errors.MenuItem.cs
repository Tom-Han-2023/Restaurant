using ErrorOr;

public static class Errors 
{
  public static class MenuItem
  {
    public static Error NotFound => Error.NotFound(
      code: "MenuItem.NotFound",
      description: "Menu item not found"
    );
  }
}
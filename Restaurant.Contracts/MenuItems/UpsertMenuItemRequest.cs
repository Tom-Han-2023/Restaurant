public record UpsertMenuItemsRequest(
  string Name,
  string Category,
  decimal Price,
  bool IsAvailable
);

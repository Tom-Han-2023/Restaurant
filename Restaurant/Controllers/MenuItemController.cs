using ErrorOr;
using Microsoft.AspNetCore.Mvc;
using Restaurant.Controllers;
using Restaurant.Services.MenuItems;


public class MenuItemController : ApiController
{
  private readonly IMenuItemService _menuItemService;

  public MenuItemController(IMenuItemService menuItemService)
  {
    _menuItemService = menuItemService;
  }

  [HttpPost]
   public IActionResult CreateMenuItem(CreateMenuItemRequest request)
   {
    ErrorOr<MenuItem> requestToMenuItemResult =  MenuItem.Create(
      
      request.Name,
      request.Category,
      request.Price,
      request.IsAvailable    
    );
    if (requestToMenuItemResult.IsError)
    {
      return Problem(requestToMenuItemResult.Errors);
    }

    var menuItem = requestToMenuItemResult.Value;
    
    ErrorOr<Created> createMenuItemResult = _menuItemService.CreateMenuItem(menuItem);
    return createMenuItemResult.Match(
      created=> CreateAsGetMenuItem(menuItem),
      errors => Problem(errors)
    );}
  
   [HttpGet("{id:guid}")]
   public IActionResult GetMenuItem(Guid id)
   {
    ErrorOr<MenuItem> getMenuItemResult = _menuItemService.GetMenuItem(id);

    return getMenuItemResult.Match(
      menuItem => Ok(MapMenuItemResponse(menuItem)),
      errors => Problem(errors)
    );   
   }
 
   [HttpPut("{id:guid}")]
   public IActionResult UpsertMenuItem(Guid id, UpsertMenuItemsRequest request)
   {
    ErrorOr<MenuItem>requestToMenuItemResult = MenuItem.Create(
      
      request.Name,
      request.Category,
      request.Price, 
      request.IsAvailable,
      id
      );
    if (requestToMenuItemResult.IsError)
    {
     return Problem(requestToMenuItemResult.Errors);
    }

    var menuItem = requestToMenuItemResult.Value;

    ErrorOr<UpsertedMenuItem> upsertMenuItemResult = _menuItemService.UpsertMenuItem(menuItem);

   
    return upsertMenuItemResult.Match(
      upserted => upserted.IsNewlyCreated ? CreateAsGetMenuItem(menuItem) : NoContent(),
      errors => Problem(errors)
    );

   }
   [HttpDelete("{id:guid}")]
   public IActionResult DeleteMenuItem(Guid id)
   {
   ErrorOr<Deleted> deleteMenuItemResult =  _menuItemService.DeleteMenuItem(id);
   return deleteMenuItemResult.Match(
    deleted => NoContent(),
    errors => Problem(errors) 
   );
 
   }
  private MenuItemResponse MapMenuItemResponse(MenuItem menuItem)
  {
   return new MenuItemResponse(
      menuItem.Id,
      menuItem.Name,
      menuItem.Category,
      menuItem.Price,
      menuItem.IsAvailable);
  }

  private CreatedAtActionResult CreateAsGetMenuItem(MenuItem menuItem)
  {
    return CreatedAtAction(actionName:nameof(GetMenuItem), routeValues: new {id = menuItem.Id}, value: MapMenuItemResponse(menuItem));
  }

} 



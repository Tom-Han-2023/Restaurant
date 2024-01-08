
using ErrorOr;

namespace Restaurant.Services.MenuItems
{
    public class MenuItemService : IMenuItemService
    {
      private static readonly Dictionary<Guid, MenuItem> _menuItems = new();
        public ErrorOr<Created> CreateMenuItem(MenuItem menuItem)
        {
          _menuItems.Add(menuItem.Id, menuItem);
          return Result.Created ;
        }
        public ErrorOr<MenuItem> GetMenuItem(Guid id)
        {
          if(_menuItems.TryGetValue(id, out var menuItem))
          {
            return menuItem;
          }
          return Errors.MenuItem.NotFound;
        }
        public ErrorOr<Deleted> DeleteMenuItem( Guid id)
        {
         _menuItems.Remove(id);
         return Result.Deleted; 
        }
        public ErrorOr<UpsertedMenuItem> UpsertMenuItem(MenuItem menuItem)
        {
          var IsNewlyCreated = !_menuItems.ContainsKey(menuItem.Id);
          _menuItems[menuItem.Id] = menuItem;
          return new UpsertedMenuItem(IsNewlyCreated);
        }
    }
}
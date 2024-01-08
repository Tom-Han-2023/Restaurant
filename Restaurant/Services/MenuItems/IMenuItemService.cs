

using ErrorOr;

namespace Restaurant.Services.MenuItems
{
    public interface IMenuItemService
    {
        ErrorOr<Created> CreateMenuItem(MenuItem menuItem);
        ErrorOr<MenuItem> GetMenuItem(Guid id);
        ErrorOr<UpsertedMenuItem> UpsertMenuItem(MenuItem menuItem);
        ErrorOr<Deleted> DeleteMenuItem(Guid id);


    }
}
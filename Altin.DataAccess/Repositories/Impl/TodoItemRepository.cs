using Altin.Core;

namespace Altin.DataAccess;

public class TodoItemRepository : BaseRepository<TodoItem>, ITodoItemRepository
{
    public TodoItemRepository(DatabaseContext context) : base(context)
    {
    }
}

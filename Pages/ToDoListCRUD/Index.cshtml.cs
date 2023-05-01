using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ToDoList.Data;
using ToDoList.Models;

namespace ToDoList.Pages.ToDoListCRUD
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly ToDoList.Data.ToDoDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public IndexModel(ToDoList.Data.ToDoDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IList<ToDoItem> ToDoItem { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.ToDoItems != null)
            {
                ToDoItem = await _context.ToDoItems.Where(x=> x.UserId == _userManager.GetUserId(User)).OrderBy(x => x.IsCompleted).ToListAsync();
            }
        }
    }
}

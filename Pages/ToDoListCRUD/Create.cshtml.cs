using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ToDoList.Data;
using ToDoList.Models;

namespace ToDoList.Pages.ToDoListCRUD
{
    [Authorize]
    public class CreateModel : PageModel
    {
        private readonly ToDoList.Data.ToDoDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public CreateModel(ToDoList.Data.ToDoDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public ToDoItem ToDoItem { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            /*if (!ModelState.IsValid || _context.ToDoItems == null || ToDoItem == null)
            {
                return Page();
            }*/

            ToDoItem.UserId = _userManager.GetUserId(User);
            _context.ToDoItems.Add(ToDoItem);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}

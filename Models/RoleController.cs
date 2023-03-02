using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApp.Models.DB;

namespace WebApp.Models
{
    public class RoleController : Controller
    {
        private readonly ComercializadoraContext _context;

        public RoleController(ComercializadoraContext context)
        {
            _context = context;
        }

        public async Task <IActionResult>  Index()
        {


            return View(await _context.Roles.ToListAsync());
        }
    }
}

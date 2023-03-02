using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApp.Models.DB;

namespace WebApp.Controllers
{
    public class OrdersController : Controller
    {
        private readonly ComercializadoraContext _context;

        public OrdersController(ComercializadoraContext context)
        {
            _context = context;
        }

        public async Task <IActionResult>  Index()
        {
            var orders = _context.Orderscostumers.Include(b => b.Customer); //Obtengo el valor de la llave foránea
            return View(await orders.ToListAsync());
        }
    }
}

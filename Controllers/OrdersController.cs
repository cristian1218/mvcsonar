using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApp.Models.DB;
using WebApp.Models.ViewModels;

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

        public IActionResult Create()
        {
            /* Es como un diccionario que accedes desde la vista, paso la tabla, campo que quiere que traiga como lista desplegable (fk) y cómo quiero que se vea*/
            ViewData["CustomerId"] = new SelectList(_context.Customers,"Id", "NameCustomer");
            ViewData["UsersId"] = new SelectList(_context.Users, "Id", "Name");
            return View();
        }

        [HttpPost] //para recibir por POST )todo lo que se mueva es post
        [ValidateAntiForgeryToken] //Seguridad para indicar que solo reciba del formulario del mismo dominio
        public async Task<IActionResult> Create(OrdersViewModels model)
        {
            if(ModelState.IsValid)
            {
                var Order = new Orderscostumer()
                {
                    CustomerId = model.CustomerId,
                    Date = model.Date,
                    Estado = model.Estado,
                    UsersId = model.UsuarioId
                };
                _context.Add(Order);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            
            
            ViewData["CustomerId"] = new SelectList(_context.Customers, "Id", "NameCustomer", model.CustomerId);
            ViewData["UsersId"] = new SelectList(_context.Users, "Id", "Name", model.UsuarioId);

            return View(model);
        }

        // GET: Customers/Edit/5
        public async Task<IActionResult> Edit(uint? id)
        {
            if (id == null || _context.Orderscostumers == null)
            {
                return NotFound();
            }

            var orderscostumer = await _context.Orderscostumers.FindAsync(id);
            if (orderscostumer == null)
            {
                return NotFound();
            }
            ViewData["CustomerId"] = new SelectList(_context.Customers, "Id", "NameCustomer", orderscostumer.CustomerId);
            ViewData["UsersId"] = new SelectList(_context.Users, "Id", "Name", orderscostumer.UsersId);
            return View(orderscostumer);
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(uint id, [Bind("IdOrder,CustomerId,Date,Estado,UsersId")] Orderscostumer orderscostumer)
        {
            if (id != orderscostumer.IdOrder)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(orderscostumer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderscostumerExists(orderscostumer.IdOrder))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CustomerId"] = new SelectList(_context.Customers, "Id", "NameCustomer", orderscostumer.CustomerId);
            ViewData["UsersId"] = new SelectList(_context.Users, "Id", "Name", orderscostumer.UsersId);
            return View(orderscostumer);
        }

        private bool OrderscostumerExists(uint idOrder)
        {
            throw new NotImplementedException();
        }
    }
}   

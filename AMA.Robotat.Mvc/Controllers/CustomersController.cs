using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AMA.Robotat.Entities.Customers;
using AMA.Robotat.Mvc.Data;
using AutoMapper;
using AMA.Robotat.Mvc.Models.Customers;

namespace AMA.Robotat.Mvc.Controllers
{
    public class CustomersController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public CustomersController(ApplicationDbContext context ,IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var customer = await _context
                                    .Customers
                                        .ToListAsync();

            var customerVMs = _mapper.Map<List<CustomerViewModel>>(customer);
            return View(customerVMs);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _context
                                        .Customers
                                            .FindAsync(id);
            if (customer == null)
            {
                return NotFound();
            }
            var customerVM = _mapper.Map<CustomerDetailsViewModel>(customer);

            return View(customerVM);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create( CreateUpdateCustomerViewModel createUpdateCustomerVM)
        {
            if (ModelState.IsValid)
            {
                var customer = _mapper.Map<Customer>(createUpdateCustomerVM);

                _context.Add(customer);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            return View(createUpdateCustomerVM);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _context
                                        .Customers
                                            .FindAsync(id);
            if (customer == null)
            {
                return NotFound();
            }
            var createUpdateCustomerVM = _mapper.Map<CreateUpdateCustomerViewModel>(customer);
            return View(createUpdateCustomerVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,CreateUpdateCustomerViewModel createUpdateCustomerVM)
        {
            if (id != createUpdateCustomerVM.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var customer = await _context
                                        .Customers
                                            .FindAsync(id);

                if (customer ==null)
                {
                    return NotFound();
                }

                _mapper.Map(createUpdateCustomerVM, customer);

                _context.Update(customer);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            return View(createUpdateCustomerVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var customer = await _context.Customers.FindAsync(id);
            if (customer != null)
            {
                _context.Customers.Remove(customer);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CustomerExists(int id)
        {
            return _context.Customers.Any(e => e.Id == id);
        }
    }
}

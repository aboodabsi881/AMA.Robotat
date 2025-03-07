using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AMA.Robotat.Entities.Orders;
using AMA.Robotat.Mvc.Data;
using AutoMapper;
using AMA.Robotat.Mvc.Models.Orders;
using AMA.Robotat.Entities.Robots;
using AMA.Robotat.Entities.Customers;
using AMA.Robotat.Mvc.Models.Customers;

namespace AMA.Robotat.Mvc.Controllers
{
    public class OrdersController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public OrdersController(ApplicationDbContext context,IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var orders =  await _context
                                    .Orders
                                        .Include(order => order.Customer)
                                        .Include(order => order.Robots)
                                        .ToListAsync();


            var orderVMs = _mapper.Map<List<OrderViewModel>>(orders);
            return View(orderVMs);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                                        .Include(o => o.Customer)
                                            .Include(o => o.Robots)
                                                .Where(m => m.Id == id)
                                                    .SingleOrDefaultAsync();
            if (order == null)
            {
                return NotFound();
            }
            var orderVM = _mapper.Map<OrderDetailsViewModel>(order);

            return View(orderVM);
        }

        public IActionResult Create()
        {
            var order = new CreateUpdateOrderViewModel
            {
                CustomersLookup = new SelectList(_context.Customers, "Id", "FullName"),
                RobotLookup = new MultiSelectList(_context.Robots, "Id", "Name")
            };
            return View(order);
        }
        

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateUpdateOrderViewModel createUpdateOrderVM)
        {
            if (ModelState.IsValid) 
            {
                var order = _mapper.Map<Order>(createUpdateOrderVM);

                order.OrderTime = DateTime.Now;

                await UpdateOrderRobots(order, createUpdateOrderVM.RobotsIds);


                order.TotalPrice = GetOrderPrice(order.Robots);

                _context.Orders.Add(order);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            createUpdateOrderVM.CustomersLookup = new SelectList(_context.Customers, "Id", "FullName");
            createUpdateOrderVM.RobotLookup = new MultiSelectList(_context.Robots, "Id", "Name");

            return View(createUpdateOrderVM);
        }



        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                                            .Include(o => o.Customer)
                                            .Include(o => o.Robots)
                                                .Where(m => m.Id == id)
                                                    .SingleOrDefaultAsync();

            if (order == null)
            {
                return NotFound();
            }

            var createUpdateOrderVM = _mapper.Map<CreateUpdateOrderViewModel>(order);
            createUpdateOrderVM.RobotsIds = order.Robots.Select(robot => robot.Id).ToList();
            createUpdateOrderVM.CustomersLookup = new SelectList(_context.Customers, "Id", "FullName");
            createUpdateOrderVM.RobotLookup = new MultiSelectList(_context.Robots, "Id", "Name");

            return View(createUpdateOrderVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CreateUpdateOrderViewModel createUpdateOrderVM)
        {
            if (id != createUpdateOrderVM.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var order = await _context.Orders
                                          .Include(o => o.Customer)
                                            .Include(o => o.Robots)
                                                .Where(m => m.Id == id)
                                                    .SingleOrDefaultAsync();

                if (order == null)
                {
                    return NotFound();
                }

                _mapper.Map(createUpdateOrderVM, order);

                order.OrderTime = DateTime.Now;

                await UpdateOrderRobots(order, createUpdateOrderVM.RobotsIds);

                order.TotalPrice = GetOrderPrice(order.Robots);

                _context.Update(order);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            createUpdateOrderVM.CustomersLookup = new SelectList(_context.Customers, "Id", "FullName", createUpdateOrderVM.CustomerId);
            createUpdateOrderVM.RobotLookup = new MultiSelectList(_context.Robots, "Id", "Name", createUpdateOrderVM.RobotsIds);

            return View(createUpdateOrderVM);


        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order != null)
            {
                _context.Orders.Remove(order);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderExists(int id)
        {
            return _context.Orders.Any(e => e.Id == id);
        }

        private async Task UpdateOrderRobots(Order order , List<int> robotsIds)
        {
            //Clear Robot Components
            order.Robots.Clear();
            //Get Components from the DB
            var robots = await _context
                                        .Robots
                                        .Where(robots => robotsIds.Contains(robots.Id))
                                        .ToListAsync();
            //Add Components to the Robot
            order.Robots.AddRange(robots);
        }
        private async Task UpdateOrderCustomers(Order order,int customerId)
        {
            //Clear Robot Components
            order.Customer = null;
            //Get Components from the DB
            var customer = await _context
                                        .Customers
                                        .Where(c => c.Id == customerId)
                                        .SingleOrDefaultAsync();
            //Add Components to the Robot
            order.Customer = customer;
        }

        private decimal GetOrderPrice(List<Robot> robots)
        {
            var subPrice = robots.Sum(robots => robots.Price);
            decimal totalPrice =+ subPrice ;
            return totalPrice;
        }

    }
}
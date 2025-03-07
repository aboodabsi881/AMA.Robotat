using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AMA.Robotat.Entities.Robots;
using AMA.Robotat.Mvc.Data;
using AutoMapper;
using AMA.Robotat.Mvc.Models.Robots;
using Microsoft.AspNetCore.Mvc.Rendering;
using AMA.Robotat.Entities.Components;
using System.Numerics;

namespace AMA.Robotat.Mvc.Controllers
{
    public class RobotsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public RobotsController(ApplicationDbContext context,IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var robots = await _context
                                    .Robots
                                        .ToListAsync();

            var robotVMs = _mapper.Map<List<RobotViewModel>>(robots);

            return View(robotVMs);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }


            var robot = await _context.Robots
                                        .Include(robot => robot.Components)
                                        .Where(robot => robot.Id == id)
                                        .SingleOrDefaultAsync();
            if (robot == null)
            {
                return NotFound();
            }

            var robotDetailsVM = _mapper.Map<RobotDetailsViewModel>(robot);

            return View(robotDetailsVM);
        }

        public IActionResult Create()
        {
            var createUpdateRobotVM = new CreateUpdateRobotViewModel();
            createUpdateRobotVM.ComponentLookup = new MultiSelectList(_context.Components,"Id","Name");
            return View(createUpdateRobotVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateUpdateRobotViewModel createUpdateRobotVM)
        {
            if (ModelState.IsValid)
            {
                var robot = _mapper.Map<Robot>(createUpdateRobotVM);

                //UpdateRobotComponents
                await UpdateRobotComponents(robot,createUpdateRobotVM.ComponentsIds);
                //Set Robot Price
                robot.Price = GetRobotPrice(robot.Components);

                _context.Add(robot);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            createUpdateRobotVM.ComponentLookup = new MultiSelectList(_context.Components, "Id", "Name");
            return View(createUpdateRobotVM);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var robot = await _context
                                        .Robots
                                        .Include(robot => robot.Components)
                                        .Where(robot => robot.Id == id)
                                        .SingleOrDefaultAsync();

            if (robot == null)
            {
                return NotFound();
            }

            var createUpdateRobotVM = _mapper.Map<CreateUpdateRobotViewModel>(robot);
            createUpdateRobotVM.ComponentsIds = robot.Components.Select(component => component.Id).ToList();
            createUpdateRobotVM.ComponentLookup = new MultiSelectList(_context.Components, "Id", "Name");

            return View(createUpdateRobotVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CreateUpdateRobotViewModel createUpdateRobotVM)
        {
            if (id != createUpdateRobotVM.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {

                //Get Component from DB
                var robot = await _context
                                    .Robots
                                    .Include(robot => robot.Components)
                                        .Where(robot => robot.Id == id)
                                        .SingleOrDefaultAsync();

                if (robot == null)
                {
                    return NotFound();
                }

                //Patch (Copy) createUpdateComponentVM in to the component
                _mapper.Map(createUpdateRobotVM, robot);

                //Update Robot Components
                await UpdateRobotComponents(robot,createUpdateRobotVM.ComponentsIds);

                //Sell Robot Price
                robot.Price = GetRobotPrice(robot.Components);

                _context.Update(robot);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            createUpdateRobotVM.ComponentLookup = new MultiSelectList(_context.Components, "Id", "Name");
            return View(createUpdateRobotVM);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var robot = await _context.Robots.FindAsync(id);
            if (robot != null)
            {
                _context.Robots.Remove(robot);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RobotExists(int id)
        {
            return _context.Robots.Any(e => e.Id == id);
        }

        private async Task UpdateRobotComponents(Robot robot, List<int> componentsIds)
        {
            //Clear Robot Components
            robot.Components.Clear();
            //Get Components from the DB
            var components = await _context
                                        .Components
                                        .Where(components => componentsIds.Contains(components.Id))
                                        .ToListAsync();
            //Add Components to the Robot
            robot.Components.AddRange(components);
        }

        private decimal GetRobotPrice(List<Component> components)
        {
            var subPrice = components.Sum(component => component.Price);
            decimal totalPrice = subPrice * 1.5m;
            return totalPrice; 
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AMA.Robotat.Entities.Components;
using AMA.Robotat.Mvc.Data;
using AutoMapper;
using AMA.Robotat.Mvc.Models.Components;

namespace AMA.Robotat.Mvc.Controllers
{
    public class ComponentsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public ComponentsController(ApplicationDbContext context,IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var components = await _context.
                                        Components.
                                        ToListAsync();

            var componentVMs = _mapper.Map < List < ComponentViewModel >> (components);
            return View(componentVMs);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var component = await _context
                                    .Components
                                        .FindAsync(id);
            if (component == null)
            {
                return NotFound();
            }
            var componentVM = _mapper.Map<ComponentDetailsViewModel>(component);
            return View(component);
        }

        public IActionResult Create()
        {

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateUpdateComponentViewModel createUpdateComponentVM)
        {
            if (ModelState.IsValid)
            {
                var component = _mapper.Map<Component>(createUpdateComponentVM);


                _context.Add(component);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            return View(createUpdateComponentVM);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var component = await _context
                                    .Components
                                        .FindAsync(id);

            if (component == null)
            {
                return NotFound();
            }

            var createUpdateComponentVM = _mapper.Map<CreateUpdateComponentViewModel>(component);

            return View(createUpdateComponentVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CreateUpdateComponentViewModel createUpdateComponentVM)
        {
            if (id != createUpdateComponentVM.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                //Get Component from DB
                var component = await _context
                                    .Components
                                        .FindAsync(id);

                if(component==null)
                {
                    return NotFound();
                }

                //Patch (Copy) createUpdateComponentVM in to the component
                _mapper.Map(createUpdateComponentVM, component);

                //Add to update in the context and save
                _context.Update(component);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));


            }
            return View(createUpdateComponentVM);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id) // 1 => Arduino Uno
        {
            var component = await _context
                                        .Components
                                        .FindAsync(id); //Get Arduino Uno

            if (component == null)
            {
                return RedirectToAction(nameof(Index));
                
            } //If Arduino is not  in the DB return to Index Page

            _context.Components.Remove(component); //Remove  Arduino Uno memory

            await _context.SaveChangesAsync(); //Confirm deleting Arduino frem DB

            return RedirectToAction(nameof(Index)); // Return to Index page
        }

        private bool ComponentExists(int id)
        {
            return _context.Components.Any(e => e.Id == id);
        }
    }
}

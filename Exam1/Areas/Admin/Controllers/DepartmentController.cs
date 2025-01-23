using Exam1.DAL;
using Exam1.Models;
using Exam1.ViewModels.Departments;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Exam1.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DepartmentController(AppDbContext _context) : Controller
    {
        public async Task<IActionResult> Index()
        {
            return View(await _context.Departments.ToListAsync());
            
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        { 
            return View();
        }
        [HttpPost]  
        public async Task<IActionResult> Create(DepartmentCreateVM vm)
        {
            if (!ModelState.IsValid) 
                return View(vm);
            if(await _context.Departments.AnyAsync(d=>d.Name==vm.Name))
            {
                ModelState.AddModelError("Name", "This Name is exists");
                    return View(vm);                   
            }
            var department= new Department
            {
                Name=vm.Name
            };
            _context.Departments.Add(department);
           await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        
        public async Task<IActionResult> Delete(int? id)
        {
            if (!id.HasValue)
                return BadRequest();

            var data = await _context.Departments.FindAsync(id.Value);
            if (data == null)
            return NotFound();

            _context.Departments.Remove(data);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public async Task<IActionResult> Update(int? id)
        {
            if (!id.HasValue)
                return BadRequest();

            await _context.Departments
                .Where(d => d.Id == id)
                .Select(new DepartmentUpdateVM
                {
                    Name = d.Name

                }).ToListAsync();

            return View();
                
            
        }
        [HttpPost]
        public async Task<IActionResult> Update(DepartmentUpdateVM vm,int? id)
        {
            if (!id.HasValue)
                return BadRequest();
            if(ModelState.IsValid) 
            return View(vm);

            var data = await _context.Departments.FindAsync(id.Value);
            if (data == null)
                return NotFound();

            vm.Name=data.Name;

            _context.Departments.Remove(data);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }

    }
}

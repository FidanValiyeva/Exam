using Exam1.DAL;
using Exam1.Models;
using Exam1.ViewModels.Departments;
using Exam1.ViewModels.Teachers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Exam1.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class TeacherController(AppDbContext _context,IWebHostEnvironment _environment) : Controller
    {
        
        public async Task<IActionResult> Index()
        {
            return View(await _context.Teachers.ToListAsync());
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }
        public async Task<IActionResult> Create(TeacherCreateVM vm)
        {
            if (!ModelState.IsValid)
                return View(vm);
            if (await _context.Teachers.AnyAsync(d => d.FullName == vm.FullName))
            {
                ModelState.AddModelError("Name", "This Name is exists");
                return View(vm);
            }
            var teacher = new Teacher
            {
                FullName = vm.FullName
            };
            _context.Departments.Add(teacher);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (!id.HasValue)
                return BadRequest();

            var data = await _context.Teachers.FindAsync(id.Value);
            if (data == null)
                return NotFound();

            _context.Departments.Remove(data);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Update(int? id)
        {
            if (!id.HasValue)
                return BadRequest();

            await _context.Teachers
                .Where(d => d.Id == id)
                .Select(new DepartmentUpdateVM
                {
                    Name = d.Name

                }).ToListAsync();

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Update(TeacherUpdateVM vm, int? id)
        {
            if (!id.HasValue)
                return BadRequest();
            if (ModelState.IsValid)
                return View(vm);

            var data = await _context.Departments.FindAsync(id.Value);
            if (data == null)
                return NotFound();

            vm.FullName = data.Name;

            _context.Departments.Remove(data);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }


    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProjectManagerMVC.Data;
using ProjectManagerMVC.Models;

namespace ProjectManagerMVC.Controllers
{
    public class EmployeeProjectsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EmployeeProjectsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: EmployeeProjects
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.EmployeeProject.Include(e => e.Employee).Include(e => e.Project);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: EmployeeProjects/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.EmployeeProject == null)
            {
                return NotFound();
            }

            var employeeProject = await _context.EmployeeProject
                .Include(e => e.Employee)
                .Include(e => e.Project)
                .FirstOrDefaultAsync(m => m.EmployeeID == id);
            if (employeeProject == null)
            {
                return NotFound();
            }

            return View(employeeProject);
        }

        // GET: EmployeeProjects/Create
        public IActionResult Create()
        {
            ViewData["EmployeeID"] = new SelectList(_context.Employee, "EmployeeID", "EmployeeID");
            ViewData["ProjectID"] = new SelectList(_context.Project, "ProjectID", "ProjectID");
            return View();
        }
        // назначение сотрудников
        public ActionResult CreateProjectEmployee()
        {
            ViewBag.ProjectID = new SelectList(_context.Project, "ProjectID", "ProjectName");
            ViewBag.EmployeeID = _context.Employee;
            return View();
        }

        [HttpPost]

        public ActionResult CreateProjectEmployee(int ProjectID, int []EmployeeID)
        {


            foreach (int employeeID in EmployeeID)
            {
                EmployeeProject employeeProject = new EmployeeProject();
                employeeProject.ProjectID = ProjectID;
                employeeProject.EmployeeID = employeeID;
                _context.EmployeeProject.Add(employeeProject);
                if (ModelState.IsValid)
                {
                    _context.SaveChanges();
                }
            }
            return RedirectToAction("Index");
        }

        // POST: EmployeeProjects/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProjectID,EmployeeID")] EmployeeProject employeeProject)
        {
            if (ModelState.IsValid)
            {
                _context.Add(employeeProject);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EmployeeID"] = new SelectList(_context.Employee, "EmployeeID", "EmployeeID", employeeProject.EmployeeID);
            ViewData["ProjectID"] = new SelectList(_context.Project, "ProjectID", "ProjectID", employeeProject.ProjectID);
            return View(employeeProject);
        }

        // GET: EmployeeProjects/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.EmployeeProject == null)
            {
                return NotFound();
            }

            var employeeProject = await _context.EmployeeProject.FindAsync(id);
            if (employeeProject == null)
            {
                return NotFound();
            }
            ViewData["EmployeeID"] = new SelectList(_context.Employee, "EmployeeID", "EmployeeID", employeeProject.EmployeeID);
            ViewData["ProjectID"] = new SelectList(_context.Project, "ProjectID", "ProjectID", employeeProject.ProjectID);
            return View(employeeProject);
        }

        // POST: EmployeeProjects/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProjectID,EmployeeID")] EmployeeProject employeeProject)
        {
            if (id != employeeProject.EmployeeID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(employeeProject);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeeProjectExists(employeeProject.EmployeeID))
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
            ViewData["EmployeeID"] = new SelectList(_context.Employee, "EmployeeID", "EmployeeID", employeeProject.EmployeeID);
            ViewData["ProjectID"] = new SelectList(_context.Project, "ProjectID", "ProjectID", employeeProject.ProjectID);
            return View(employeeProject);
        }

        // GET: EmployeeProjects/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.EmployeeProject == null)
            {
                return NotFound();
            }

            var employeeProject = await _context.EmployeeProject
                .Include(e => e.Employee)
                .Include(e => e.Project)
                .FirstOrDefaultAsync(m => m.EmployeeID == id);
            if (employeeProject == null)
            {
                return NotFound();
            }

            return View(employeeProject);
        }

        // POST: EmployeeProjects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id, int id2)
        {
            if (_context.EmployeeProject == null)
            {
                return Problem("Entity set 'ApplicationDbContext.EmployeeProject'  is null.");
            }
            var employeeProject = await _context.EmployeeProject.FindAsync(id, id2);
            if (employeeProject != null)
            {
                _context.EmployeeProject.Remove(employeeProject);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmployeeProjectExists(int id)
        {
          return (_context.EmployeeProject?.Any(e => e.EmployeeID == id)).GetValueOrDefault();
        }
    }
}

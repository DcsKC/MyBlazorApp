using Microsoft.EntityFrameworkCore;
using MyBlazorApp.Data;
using MyBlazorApp.Models;

namespace MyBlazorApp.Services
{
    public class TimeTrackingService
    {
        private readonly AppDbContext _context;

        public TimeTrackingService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Employee>> GetEmployeesAsync()
        {
            return await _context.Employees.Include(e => e.TimeEntries).ToListAsync();
        }

        public async Task<Employee> GetEmployeeByIdAsync(int id)
        {
            return await _context.Employees.Include(e => e.TimeEntries)
                .FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task AddEmployeeAsync(Employee employee)
        {
            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();
        }

        public async Task CheckInAsync(int employeeId)
        {
            var timeEntry = new TimeEntry
            {
                EmployeeId = employeeId,
                CheckInTime = DateTime.Now
            };
            _context.TimeEntries.Add(timeEntry);
            await _context.SaveChangesAsync();
        }

        public async Task CheckOutAsync(int employeeId)
        {
            var timeEntry = await _context.TimeEntries
                .Where(te => te.EmployeeId == employeeId && te.CheckOutTime == null)
                .FirstOrDefaultAsync();
            if (timeEntry != null)
            {
                timeEntry.CheckOutTime = DateTime.Now;
                await _context.SaveChangesAsync();
            }
        }
    }
}
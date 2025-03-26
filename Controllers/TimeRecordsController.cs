using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyBlazorApp.Data;
using MyBlazorApp.Models;

namespace MyBlazorApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TimeRecordsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TimeRecordsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<TimeEntry>>> GetTimeRecords()
        {
            return await _context.TimeEntries.ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<TimeEntry>> PostTimeRecord(TimeEntry record)
        {
            _context.TimeEntries.Add(record);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetTimeRecords), new { id = record.Id }, record);
        }
    }
}

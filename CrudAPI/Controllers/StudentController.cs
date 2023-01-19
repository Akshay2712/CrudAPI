using CrudAPI.Model;
using Microsoft.AspNetCore.Components.Web.Virtualization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.EntityFrameworkCore;

namespace CrudAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        
        private readonly DataContext _context;

        public StudentController(DataContext context)
        {
            _context= context;
        }
        [HttpGet]
        [EnableQuery]
        public async Task<ActionResult<List<Student>>> Get()
        {
            return Ok(await _context.Students.ToListAsync());
        }

        [HttpGet("id")]
        [EnableQuery]
        public async Task<ActionResult<List<Student>>> Get(int id)
        {
            var student = await _context.Students.FindAsync(id);
            if (student == null)
                return BadRequest("Student Not found");
            return Ok(student);
        }

        [HttpPost]
        [EnableQuery]
        public async Task<ActionResult<List<Student>>> AddStudent([FromBody] Student student)
        {
            _context.Students.Add(student);
            await _context.SaveChangesAsync();
            return Ok(await _context.Students.ToListAsync());
        }
        [HttpPut]
        [EnableQuery]
        public async Task<ActionResult<List<Student>>> UpdateStudent(Student request)
        {
            var student_put = await _context.Students.FindAsync(request.Id);
            if (student_put == null)
                return BadRequest("Student Not found");
  
            student_put.FirstName = request.FirstName;
            student_put.LastName = request.LastName;
            student_put.Place = request.Place;
            await _context.SaveChangesAsync();
            return Ok(await _context.Students.ToListAsync());
        }

        [HttpDelete("{id}")]
        [EnableQuery]
        public async Task<ActionResult<List<Student>>> Delete (int id)
        {
            var student = await _context.Students.FindAsync(id);
            if (student == null)
                return BadRequest("Student Not found");
           
            _context.Students.Remove(student);
            await _context.SaveChangesAsync();

            return Ok(await _context.Students.ToListAsync());
        }

    }
}

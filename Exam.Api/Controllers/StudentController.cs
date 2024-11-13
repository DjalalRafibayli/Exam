using Container.Base;
using Container.Models.Student;
using Container.Request.Student;
using Exam.Application.Services.Student;
using Microsoft.AspNetCore.Mvc;

namespace Exam.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : BaseController
    {
        private readonly IStudentService _studentService;

        public StudentController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add([FromBody] AddStudentRequest request)
        {
            await _studentService.AddStudent(request);
            return CreateActionResultInstance(ResponseModel<NoContent>.SuccessResponse());
        }
        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete(int Id)
        {
            await _studentService.DeleteStudent(Id);
            return CreateActionResultInstance(ResponseModel<NoContent>.SuccessResponse());
        }
        [HttpPut("edit")]
        public async Task<IActionResult> Update([FromBody] EditStudentRequest request)
        {
            await _studentService.UpdateStudent(request);
            return CreateActionResultInstance(ResponseModel<NoContent>.SuccessResponse());
        }
        [HttpGet("get/{Id}")]
        public async Task<IActionResult> Get(int Id)
        {
            var response = await _studentService.GetStudent(Id);
            return CreateActionResultInstance(ResponseModel<StudentListGetModel>.SuccessResponse(response));
        }
        [HttpGet("get/{page}/{limit}")]
        public async Task<IActionResult> GetAll([FromRoute] int Page, [FromRoute] int Limit)
        {
            var response = await _studentService.GetStudents(Page, Limit);
            return CreateActionResultInstance(ResponseModel<StudentsList>.SuccessResponse(response));
        }
        [HttpGet("getselect")]
        public async Task<IActionResult> GetSelect()
        {
            var response = await _studentService.GetStudentSelect();
            return CreateActionResultInstance(ResponseModel<List<StudentSelectModel>>.SuccessResponse(response));
        }

    }
}

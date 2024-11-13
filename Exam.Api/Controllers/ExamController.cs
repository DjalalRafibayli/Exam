using Container.Base;
using Container.Models.Exam;
using Container.Request.Exam;
using Exam.Application.Services.Exam;
using Microsoft.AspNetCore.Mvc;

namespace Exam.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExamController : BaseController
    {
        private readonly IExamService _examService;

        public ExamController(IExamService examService)
        {
            _examService = examService;
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add([FromBody] AddExamRequest request)
        {
            await _examService.AddExam(request);
            return CreateActionResultInstance(ResponseModel<NoContent>.SuccessResponse());
        }
        [HttpDelete("Delete/{Id}")]
        public async Task<IActionResult> Delete(int Id)
        {
            await _examService.DeleteExam(Id);
            return CreateActionResultInstance(ResponseModel<NoContent>.SuccessResponse());
        }
        [HttpPut("edit")]
        public async Task<IActionResult> Update([FromBody] EditExamRequest request)
        {
            await _examService.UpdateExam(request);
            return CreateActionResultInstance(ResponseModel<NoContent>.SuccessResponse());
        }
        [HttpGet("get/{Id}/")]
        public async Task<IActionResult> Get(int Id)
        {
            var response = await _examService.GetExam(Id);
            return CreateActionResultInstance(ResponseModel<ExamListGetModel>.SuccessResponse(response));
        }
        [HttpGet("get/{page}/{limit}")]
        public async Task<IActionResult> GetAll([FromRoute] int Page, [FromRoute] int Limit)
        {
            var response = await _examService.GetExams(Page, Limit);
            return CreateActionResultInstance(ResponseModel<ExamList>.SuccessResponse(response));
        }
    }
}

using Container.Base;
using Container.Models.Lesson;
using Container.Request.Lesson;
using Exam.Application.Services.Lesson;
using Microsoft.AspNetCore.Mvc;

namespace Exam.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LessonController : BaseController
    {
        private readonly ILessonService _lessonService;

        public LessonController(ILessonService lessonService)
        {
            _lessonService = lessonService;
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add([FromBody] AddLessonRequest request)
        {
            await _lessonService.AddLesson(request);
            return CreateActionResultInstance(ResponseModel<NoContent>.SuccessResponse());
        }
        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete(int Id)
        {
            await _lessonService.DeleteLesson(Id);
            return CreateActionResultInstance(ResponseModel<NoContent>.SuccessResponse());
        }
        [HttpPut("edit")]
        public async Task<IActionResult> Update([FromBody] EditLessonRequest request)
        {
            await _lessonService.UpdateLesson(request);
            return CreateActionResultInstance(ResponseModel<NoContent>.SuccessResponse());
        }
        [HttpGet("get/{Id}/")]
        public async Task<IActionResult> Get(int Id)
        {
            var response = await _lessonService.GetLesson(Id);
            return CreateActionResultInstance(ResponseModel<LessonListGetModel>.SuccessResponse(response));
        }
        [HttpGet("get/{page}/{limit}")]
        public async Task<IActionResult> GetAll([FromRoute] int Page, [FromRoute] int Limit)
        {
            var response = await _lessonService.GetLessons(Page, Limit);
            return CreateActionResultInstance(ResponseModel<LessonsList>.SuccessResponse(response));
        }
        [HttpGet("getselect")]
        public async Task<IActionResult> GetSelect()
        {
            var response = await _lessonService.GetLessonSelect();
            return CreateActionResultInstance(ResponseModel<List<LessonSelectModel>>.SuccessResponse(response));
        }
    }
}

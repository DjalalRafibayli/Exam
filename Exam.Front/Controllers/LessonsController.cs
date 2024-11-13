using Container.Base;
using Container.Models.Lesson;
using Container.Request.Lesson;
using Exam.Front.Getaways;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Exam.Front.Controllers
{
    public class LessonsController : Controller
    {
        private readonly IResponseGetaway _responseGetaway;

        public LessonsController(IResponseGetaway responseGetaway)
        {
            _responseGetaway = responseGetaway;
        }

        public async Task<IActionResult> Index(int? page = 1)
        {

            var resp = await _responseGetaway.GetAsync($"api/Lesson/get/{page}/{5}");
            var responseModel = JsonConvert.DeserializeObject<ResponseModel<LessonsList>>(resp);

            return View(responseModel);
        }
        [HttpGet]
        public async Task<IActionResult> Add()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Add(AddLessonRequest model)
        {
            var resp = await _responseGetaway.PostAsync(model, $"api/Lesson/add");
            var responseModel = JsonConvert.DeserializeObject<ResponseModel<NoContent>>(resp);
            if (responseModel.IsSuccess)
            {
                TempData["success"] = "Ok";
            }
            return View();
        }
        [HttpGet("Lesson/edit/{Id}")]
        public async Task<IActionResult> Edit(int Id)
        {
            var resp = await _responseGetaway.GetAsync($"api/Lesson/get/{Id}");
            var responseModel = JsonConvert.DeserializeObject<ResponseModel<LessonListGetModel>>(resp);
            return View(responseModel);
        }
        [HttpPost("Lesson/edit/{Id}")]
        public async Task<IActionResult> Edit(EditLessonRequest model)
        {
            var resp = await _responseGetaway.PutAsync(model, $"api/Lesson/edit");
            var responseModel = JsonConvert.DeserializeObject<ResponseModel<NoContent>>(resp);
            if (responseModel.IsSuccess)
            {
                TempData["success"] = "Ok";
            }
            resp = await _responseGetaway.GetAsync($"api/Lesson/get/{model.Id}");
            var Lesson = JsonConvert.DeserializeObject<ResponseModel<LessonListGetModel>>(resp);
            return View(Lesson);
        }
    }
}

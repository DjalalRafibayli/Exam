using Container.Base;
using Container.Models.Exam;
using Container.Models.Lesson;
using Container.Models.Student;
using Container.Request.Exam;
using Exam.Front.Getaways;
using Exam.Front.Models.ViewModel.Exam;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Exam.Front.Controllers
{
    public class ExamsController : Controller
    {
        private readonly IResponseGetaway _responseGetaway;

        public ExamsController(IResponseGetaway responseGetaway)
        {
            _responseGetaway = responseGetaway;
        }

        public async Task<IActionResult> Index(int? page = 1)
        {

            var resp = await _responseGetaway.GetAsync($"api/Exam/get/{page}/{5}");
            var responseModel = JsonConvert.DeserializeObject<ResponseModel<ExamList>>(resp);

            return View(responseModel);
        }
        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var resp = await _responseGetaway.GetAsync($"api/lesson/getselect");
            var responseModel = JsonConvert.DeserializeObject<ResponseModel<List<LessonSelectModel>>>(resp);

            var studentresp = await _responseGetaway.GetAsync($"api/student/getselect");
            var studentresponseModel = JsonConvert.DeserializeObject<ResponseModel<List<StudentSelectModel>>>(studentresp);

            return View(new ExamVM
            {
                lessons = responseModel.Data,
                students = studentresponseModel.Data
            });
        }
        [HttpPost]
        public async Task<IActionResult> Add(AddExamRequest model)
        {
            var resp = await _responseGetaway.PostAsync(model, $"api/Exam/add");
            var responseModel = JsonConvert.DeserializeObject<ResponseModel<NoContent>>(resp);

            var lessonresp = await _responseGetaway.GetAsync($"api/lesson/getselect");
            var lessonresponseModel = JsonConvert.DeserializeObject<ResponseModel<List<LessonSelectModel>>>(lessonresp);

            var studentresp = await _responseGetaway.GetAsync($"api/student/getselect");
            var studentresponseModel = JsonConvert.DeserializeObject<ResponseModel<List<StudentSelectModel>>>(studentresp);


            if (responseModel.IsSuccess)
            {
                TempData["success"] = "Ok";
            }
            else
            {
                TempData["error"] = "Ok";
                TempData["message"] = responseModel.Errors.FirstOrDefault();
            }
            return View(new ExamVM
            {
                lessons = lessonresponseModel.Data,
                students = studentresponseModel.Data
            });
        }
        [HttpGet("Exam/edit/{Id}")]
        public async Task<IActionResult> Edit(int Id)
        {
            var resp = await _responseGetaway.GetAsync($"api/Exam/get/{Id}");
            var responseModel = JsonConvert.DeserializeObject<ResponseModel<ExamListGetModel>>(resp);

            var lessonresp = await _responseGetaway.GetAsync($"api/lesson/getselect");
            var lessonresponseModel = JsonConvert.DeserializeObject<ResponseModel<List<LessonSelectModel>>>(lessonresp);

            var studentresp = await _responseGetaway.GetAsync($"api/student/getselect");
            var studentresponseModel = JsonConvert.DeserializeObject<ResponseModel<List<StudentSelectModel>>>(studentresp);

            return View(new ExamEditVM
            {
                exam = responseModel.Data,
                lessons = lessonresponseModel.Data,
                students = studentresponseModel.Data
            });
        }
        [HttpPost("Exam/edit/{Id}")]
        public async Task<IActionResult> Edit(EditExamRequest model)
        {
            var resp = await _responseGetaway.PutAsync(model, $"api/Exam/edit");
            var responseModel = JsonConvert.DeserializeObject<ResponseModel<NoContent>>(resp);
            if (responseModel.IsSuccess)
            {
                TempData["success"] = "Ok";
            }
            else
            {
                TempData["error"] = "Ok";
                TempData["message"] = responseModel.Errors.FirstOrDefault();
            }
            resp = await _responseGetaway.GetAsync($"api/Exam/get/{model.Id}");
            var Exam = JsonConvert.DeserializeObject<ResponseModel<ExamListGetModel>>(resp);

            var lessonresp = await _responseGetaway.GetAsync($"api/lesson/getselect");
            var lessonresponseModel = JsonConvert.DeserializeObject<ResponseModel<List<LessonSelectModel>>>(lessonresp);

            var studentresp = await _responseGetaway.GetAsync($"api/student/getselect");
            var studentresponseModel = JsonConvert.DeserializeObject<ResponseModel<List<StudentSelectModel>>>(studentresp);

            return View(new ExamEditVM
            {
                exam = Exam.Data,
                lessons = lessonresponseModel.Data,
                students = studentresponseModel.Data
            });
        }
    }
}

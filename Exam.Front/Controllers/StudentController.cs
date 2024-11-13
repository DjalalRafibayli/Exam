using Container.Base;
using Container.Models.Student;
using Container.Request.Student;
using Exam.Front.Getaways;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Exam.Front.Controllers
{
    public class StudentController : Controller
    {
        private readonly IResponseGetaway _responseGetaway;

        public StudentController(IResponseGetaway responseGetaway)
        {
            _responseGetaway = responseGetaway;
        }

        public async Task<IActionResult> Index(int? page = 1)
        {

            var resp = await _responseGetaway.GetAsync($"api/student/get/{page}/{5}");
            var responseModel = JsonConvert.DeserializeObject<ResponseModel<StudentsList>>(resp);

            return View(responseModel);
        }
        [HttpGet]
        public async Task<IActionResult> Add()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Add(AddStudentRequest model)
        {
            var resp = await _responseGetaway.PostAsync(model, $"api/student/add");
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
            return View();
        }
        [HttpGet("student/edit/{Id}")]
        public async Task<IActionResult> Edit(int Id)
        {
            var resp = await _responseGetaway.GetAsync($"api/student/get/{Id}");
            var responseModel = JsonConvert.DeserializeObject<ResponseModel<StudentListGetModel>>(resp);
            return View(responseModel);
        }
        [HttpPost("student/edit/{Id}")]
        public async Task<IActionResult> Edit(EditStudentRequest model)
        {
            var resp = await _responseGetaway.PutAsync(model, $"api/student/edit");
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
            resp = await _responseGetaway.GetAsync($"api/student/get/{model.Id}");
            var student = JsonConvert.DeserializeObject<ResponseModel<StudentListGetModel>>(resp);
            return View(student);
        }
    }
}

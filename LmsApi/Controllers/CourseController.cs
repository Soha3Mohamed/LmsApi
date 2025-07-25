using LmsApi.Helpers;
using LmsApi.Models.DTOs.Course;
using LmsApi.Models.DTOs.Lesson;
using LmsApi.Models.DTOs.User;
using LmsApi.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace LmsApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly ICourseService _courseService;

        public CourseController(ICourseService courseService)
        {
            _courseService = courseService;
        }

        private int GetUserId()
        {
            return int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult GetAllCourses()
        {
            var result = _courseService.GetAllCourses();
            if (!result.Success)
                return NotFound(result.ErrorMessage);
            return Ok(result.Data);
        }

        [AllowAnonymous]
        [HttpGet("search/{name}")]
        public IActionResult GetCourseByName(string name)
        {
            var result = _courseService.SearchCourseByName(name);
            if (!result.Success)
                return NotFound(result.ErrorMessage);
            return Ok(result.Data);
        }

        [Authorize(Roles = "Student")]
        [HttpGet("my-courses")]
        public IActionResult GetMyCourses()
        {
            var studentId = GetUserId();
            var result = _courseService.GetStudentCourses(studentId);
            if (!result.Success)
                return NotFound(result.ErrorMessage);
            return Ok(result.Data);
        }

        [Authorize]
        [HttpGet("{courseId}/lessons")]
        public IActionResult GetCourseLessons(int courseId)
        {
            var result = _courseService.GetCourseLessons(courseId);
            if (!result.Success)
                return NotFound(result.ErrorMessage);
            return Ok(result.Data);
        }

        [Authorize(Roles = "Student")]
        [HttpPost("{courseId}/enroll")]
        public IActionResult Enroll(int courseId)
        {
            var studentId = GetUserId();
            var result = _courseService.EnrollInCourse(courseId, studentId);
            if (!result.Success)
                return BadRequest(result.ErrorMessage);
            return Ok(result.Data);
        }

        [Authorize(Roles = "Student")]
        [HttpPost("{courseId}/unroll")]
        public IActionResult Unroll(int courseId)
        {
            var studentId = GetUserId();
            var result = _courseService.UnrollFromCourse(courseId, studentId);
            if (!result.Success)
                return BadRequest(result.ErrorMessage);
            return Ok(result.Data);
        }

        [Authorize(Roles = "Student")]
        [HttpGet("{courseId}/is-enrolled")]
        public IActionResult IsEnrolled(int courseId)
        {
            var studentId = GetUserId();
            var result = _courseService.IsUserEnrolled(courseId, studentId);
            return Ok(result);
        }

        [Authorize(Roles = "Student")]
        [HttpGet("{courseId}/is-completed")]
        public IActionResult IsCompleted(int courseId)
        {
            var studentId = GetUserId();
            var result = _courseService.IsCourseCompleted(courseId, studentId);
            return Ok(result);
        }

        [Authorize(Roles = "Instructor")]
        [HttpPost("add")]
        public IActionResult AddCourse([FromBody] AddCourseDTO courseDTO)
        {
            var instructorId = GetUserId();
            var result = _courseService.AddCourse(courseDTO, instructorId);
            if (!result.Success)
                return BadRequest(result.ErrorMessage);
            return Ok(result.Data);
        }

        [Authorize(Roles = "Instructor")]
        [HttpGet("my-courses/instructor")]
        public IActionResult GetInstructorCourses()
        {
            var instructorId = GetUserId();
            var result = _courseService.GetInstructorCourses(instructorId);
            if (!result.Success)
                return NotFound(result.ErrorMessage);
            return Ok(result.Data);
        }

        [Authorize(Roles = "Instructor")]
        [HttpPut("{courseId}/update")]
        public IActionResult UpdateCourse(int courseId, [FromBody] AddCourseDTO courseDTO)
        {
            var result = _courseService.UpdateCourse(courseDTO, courseId);
            if (!result.Success)
                return BadRequest(result.ErrorMessage);
            return Ok(result.Data);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{courseId}/delete")]
        public IActionResult DeleteCourse(int courseId)
        {
            var result = _courseService.DeleteCourse(courseId);
            if (!result.Success)
                return NotFound(result.ErrorMessage);
            return Ok(result.Data);
        }

        [Authorize(Roles = "Admin,Instructor")]
        [HttpPut("{courseId}/publish")]
        public IActionResult PublishCourse(int courseId)
        {
            var result = _courseService.PublishCourse(courseId);
            if (!result.Success)
                return BadRequest(result.ErrorMessage);
            return Ok(result.Data);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("{courseId}/students")]
        public IActionResult GetCourseStudents(int courseId)
        {
            var result = _courseService.GetCourseStudents(courseId);
            if (!result.Success)
                return NotFound(result.ErrorMessage);
            return Ok(result.Data);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("{courseId}/instructor-name")]
        public IActionResult GetCourseInstructorName(int courseId)
        {
            var result = _courseService.GetCourseInstructorName(courseId);
            if (!result.Success)
                return NotFound(result.ErrorMessage);
            return Ok(result.Data);
        }
    }
}

using LmsApi.Models.DTOs.Lesson;
using LmsApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

[Route("api/courses/{courseId}/lessons")]
[ApiController]
public class LessonController : ControllerBase
{
    private readonly ILessonService _lessonService;

    public LessonController(ILessonService lessonService)
    {
        _lessonService = lessonService;
    }

    [HttpGet]
    public IActionResult GetAllLessons(int courseId)
    {
        var result = _lessonService.GetAllLessons(courseId);
        if (!result.Success) return NotFound(result.ErrorMessage);
        return Ok(result.Data);
    }

    [HttpGet("{lessonId}")]
    public IActionResult GetLesson(int courseId, int lessonId)
    {
        var result = _lessonService.GetLesson(courseId, lessonId);
        if (!result.Success) return NotFound(result.ErrorMessage);
        return Ok(result.Data);
    }

    [HttpPost]
    public IActionResult AddLesson(int courseId, [FromBody] AddLessonDTO lessonDto)
    {
        var result = _lessonService.AddLesson(courseId, lessonDto);
        if (!result.Success) return BadRequest(result.ErrorMessage);
        return Ok(result.Data);
    }

    [HttpPut("{lessonId}")]
    public IActionResult UpdateLesson(int courseId, int lessonId, [FromBody] AddLessonDTO lessonDto)
    {
        var result = _lessonService.UpdateLesson(courseId, lessonId, lessonDto);
        if (!result.Success) return NotFound(result.ErrorMessage);
        return Ok(result.Data);
    }

    [HttpDelete("{lessonId}")]
    public IActionResult DeleteLesson(int courseId, int lessonId)
    {
        var result = _lessonService.DeleteLesson(courseId, lessonId);
        if (!result.Success) return NotFound(result.ErrorMessage);
        return Ok(result.Data);
    }

    [HttpGet("{lessonId}/is-completed")]
    public IActionResult IsCompleted(int courseId, int lessonId)
    {
        var result = _lessonService.IsCompleted(courseId, lessonId);
        if (!result.Success) return NotFound(result.ErrorMessage);
        return Ok(result.Data);
    }

    [HttpPut("{lessonId}/complete")]
    public IActionResult CompleteLesson(int courseId, int lessonId)
    {
        var result = _lessonService.CompleteLesson(courseId, lessonId);
        if (!result.Success) return NotFound(result.ErrorMessage);
        return Ok(result.Data);
    }
}

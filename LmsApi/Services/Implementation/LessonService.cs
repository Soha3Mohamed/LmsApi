using LmsApi.Helpers;
using LmsApi.Models.Data;
using LmsApi.Models.DTOs.Lesson;
using LmsApi.Models.Entities;
using LmsApi.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

public class LessonService : ILessonService
{
    private readonly ApplicationDbContext _dbContext;

    public LessonService(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public ServiceResult<List<GetLessonDTO>> GetAllLessons(int courseId)
    {
        var course = _dbContext.Courses.Include(c => c.Lessons).FirstOrDefault(c => c.Id == courseId);
        if (course == null || course.Lessons.Count == 0)
        {
            return ServiceResult<List<GetLessonDTO>>.Fail("No lessons found in this course.");
        }

        var lessonsDto = course.Lessons.Select(l => new GetLessonDTO
        {
            Title = l.Title,
            Content = l.Content,
        }).ToList();

        return ServiceResult<List<GetLessonDTO>>.Ok(lessonsDto);
    }

    public ServiceResult<GetLessonDTO> GetLesson(int courseId, int lessonId)
    {
        var lesson = _dbContext.Lessons.FirstOrDefault(l => l.Id == lessonId && l.CourseId == courseId);
        if (lesson == null)
        {
            return ServiceResult<GetLessonDTO>.Fail("Lesson not found.");
        }

        var dto = new GetLessonDTO
        {
            Title = lesson.Title,
            Content = lesson.Content,
        };

        return ServiceResult<GetLessonDTO>.Ok(dto);
    }

    public ServiceResult<GetLessonDTO> AddLesson(int courseId, AddLessonDTO lessonDto)
    {
        var course = _dbContext.Courses.Find(courseId);
        if (course == null)
        {
            return ServiceResult<GetLessonDTO>.Fail("Course not found.");
        }

        var lesson = new Lesson
        {
            Title = lessonDto.Title,
            Content = lessonDto.Content,
            CourseId = courseId,
            IsCompleted = false
        };

        _dbContext.Lessons.Add(lesson);
        _dbContext.SaveChanges();

        var dto = new GetLessonDTO
        {
            Title = lesson.Title,
            Content = lesson.Content,
        };

        return ServiceResult<GetLessonDTO>.Ok(dto);
    }

    public ServiceResult<GetLessonDTO> UpdateLesson(int courseId, int lessonId, AddLessonDTO lessonDto)
    {
        var lesson = _dbContext.Lessons.FirstOrDefault(l => l.Id == lessonId && l.CourseId == courseId);
        if (lesson == null)
        {
            return ServiceResult<GetLessonDTO>.Fail("Lesson not found.");
        }

        lesson.Title = lessonDto.Title;
        lesson.Content = lessonDto.Content;
        _dbContext.SaveChanges();

        var dto = new GetLessonDTO
        {
            Title = lesson.Title,
            Content = lesson.Content
        };

        return ServiceResult<GetLessonDTO>.Ok(dto);
    }

    public ServiceResult<string> DeleteLesson(int courseId, int lessonId)
    {
        var lesson = _dbContext.Lessons.FirstOrDefault(l => l.Id == lessonId && l.CourseId == courseId);
        if (lesson == null)
        {
            return ServiceResult<string>.Fail("Lesson not found.");
        }

        _dbContext.Lessons.Remove(lesson);
        _dbContext.SaveChanges();
        return ServiceResult<string>.Ok("Lesson deleted successfully.");
    }

    public ServiceResult<bool> IsCompleted(int courseId, int lessonId)
    {
        var lesson = _dbContext.Lessons.FirstOrDefault(l => l.Id == lessonId && l.CourseId == courseId);
        if (lesson == null)
        {
            return ServiceResult<bool>.Fail("Lesson not found.");
        }

        return ServiceResult<bool>.Ok(lesson.IsCompleted);
    }

    public ServiceResult<bool> CompleteLesson(int courseId, int lessonId)
    {
        var lesson = _dbContext.Lessons.FirstOrDefault(l => l.Id == lessonId && l.CourseId == courseId);
        if (lesson == null)
        {
            return ServiceResult<bool>.Fail("Lesson not found.");
        }

        lesson.IsCompleted = true;
        _dbContext.SaveChanges();
        return ServiceResult<bool>.Ok(true);
    }
}

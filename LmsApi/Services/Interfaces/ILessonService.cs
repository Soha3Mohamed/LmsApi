using LmsApi.Helpers;
using LmsApi.Models.DTOs.Lesson;

namespace LmsApi.Services.Interfaces
{
    public interface ILessonService
    {
        ServiceResult<List<GetLessonDTO>> GetAllLessons(int courseId);
        ServiceResult<GetLessonDTO> GetLesson(int courseId, int lessonId);

        ServiceResult<GetLessonDTO> AddLesson(int courseId, AddLessonDTO lessonDto);

        ServiceResult<GetLessonDTO> UpdateLesson(int courseId, int lessonId, AddLessonDTO lessonDto);

        ServiceResult<string> DeleteLesson(int courseId, int lessonId);

        ServiceResult<bool> IsCompleted(int courseId, int lessonId);

        public ServiceResult<bool> CompleteLesson(int courseId, int lessonId);
    }
}

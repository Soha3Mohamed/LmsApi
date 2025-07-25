using LmsApi.Helpers;
using LmsApi.Models.DTOs.Course;
using LmsApi.Models.DTOs.Lesson;
using LmsApi.Models.DTOs.User;
using LmsApi.Models.Entities;

namespace LmsApi.Services.Interfaces
{
    public interface ICourseService
    {
        // Student
        ServiceResult<List<GetCourseDTO>> GetAllCourses();
        ServiceResult<GetCourseDTO> SearchCourseByName(string title);
        ServiceResult<List<GetCourseDTO>> GetStudentCourses(int studentId);
        ServiceResult<List<GetLessonDTO>> GetCourseLessons(int courseId);
        ServiceResult<bool> EnrollInCourse(int courseId, int userId);
        ServiceResult<bool> UnrollFromCourse(int courseId, int userId);
        bool IsUserEnrolled(int courseId, int userId);
        bool IsCourseCompleted(int courseId, int userId);



        // Instructor / Admin
        ServiceResult<List<GetCourseDTO>> GetInstructorCourses(int instructorId);
        ServiceResult<List<GetUserDto>> GetCourseStudents(int courseId);
        ServiceResult<GetCourseDTO> AddCourse(AddCourseDTO courseDTO, int instructorId);
        ServiceResult<GetCourseDTO> UpdateCourse(AddCourseDTO courseDTO, int courseId);
        ServiceResult<string> DeleteCourse(int courseId);
        ServiceResult<string> GetCourseInstructorName(int courseId); // optional
        ServiceResult<bool> PublishCourse(int courseId);

    }
}

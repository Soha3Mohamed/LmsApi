using LmsApi.Helpers;
using LmsApi.Mappings;
using LmsApi.Models.Data;
using LmsApi.Models.DTOs.Course;
using LmsApi.Models.DTOs.Lesson;
using LmsApi.Models.DTOs.User;
using LmsApi.Models.Entities;
using LmsApi.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace LmsApi.Services.Implementation
{
    public class CourseService : ICourseService
    {
        private readonly ApplicationDbContext _dbContext;

        public CourseService(ApplicationDbContext dbContext)
        {
            this._dbContext = dbContext;
        }
        public ServiceResult<List<GetCourseDTO>> GetAllCourses()
        {
            var courses = _dbContext.Courses.Include(c => c.Lessons).ToList();
            if (courses.Count == 0)
            {
                return ServiceResult<List<GetCourseDTO>>.Fail("No Courses found");
            }
            var coursesDto = courses.Select(c => new GetCourseDTO
            {
                Id = c.Id,
                Title = c.Title,
                Description = c.Description,
                CreatedAt = c.CreatedAt,
                UpdatedAt = c.UpdatedAt,
                InstructorId = c.InstructorId,
                LessonsCount = c.Lessons.Count
            }).ToList();
            return ServiceResult<List<GetCourseDTO>>.Ok(coursesDto);
        } //done


        public ServiceResult<string> GetCourseInstructorName(int courseId)
        {
            var course = _dbContext.Courses.Find(courseId);
            if (course == null)
            {
                return ServiceResult<string>.Fail($"The course with id : {courseId} doesn't exist.");
            }
            var instructor = _dbContext.Users.Find(course.InstructorId);
            if (instructor == null)
            {
                return ServiceResult<string>.Fail($"The course with id : {courseId} doesn't have an instructor.");//weird case
            }
            return ServiceResult<string>.Ok(instructor.Name);
        }//done

        public ServiceResult<List<GetLessonDTO>> GetCourseLessons(int courseId)
        {
            var course = _dbContext.Courses.Include(c => c.Lessons).FirstOrDefault(c => c.Id == courseId);
            if (course == null)
            {
                return ServiceResult<List<GetLessonDTO>>.Fail($"The curse with id: {courseId} doesn't exist");
            }
            var courseLessonsDto = course.Lessons.Select(l => new GetLessonDTO
            {
                Title = l.Title,
                Content = l.Content,
            }).ToList();
            return ServiceResult<List<GetLessonDTO>>.Ok(courseLessonsDto);
        } //done


        public ServiceResult<List<GetUserDto>> GetCourseStudents(int courseId) //admin.......................................................
        {
            var course = _dbContext.Courses.Include(c => c.Enrollments).FirstOrDefault(c => c.Id == courseId);
            if (course == null)
            {
                return ServiceResult<List<GetUserDto>>.Fail($"course with this id: {courseId} doesn't exist");
            }
            //i want to get all enrollemnts that has the courseid and then get the userid and return list of theit getuserdtos
            var students = _dbContext.Users
                 .Where(u => course.Enrollments.Select(e => e.UserId).Contains(u.Id))
                 .Select(u => u.ToDto())
                 .ToList();
            return ServiceResult<List<GetUserDto>>.Ok(students);
        }

        public ServiceResult<List<GetCourseDTO>> GetInstructorCourses(int instructorId)
        {
            var courses = _dbContext.Courses
                .Where(c => c.InstructorId == instructorId)
                .Include(c => c.Lessons)
                .ToList();

            if (!courses.Any())
            {
                return ServiceResult<List<GetCourseDTO>>.Fail("No courses found for this instructor.");
            }

            var courseDtos = courses.Select(c => new GetCourseDTO
            {
                Id = c.Id,
                Title = c.Title,
                Description = c.Description,
                CreatedAt = c.CreatedAt,
                UpdatedAt = c.UpdatedAt,
                InstructorId = c.InstructorId,
                LessonsCount = c.Lessons.Count
            }).ToList();

            return ServiceResult<List<GetCourseDTO>>.Ok(courseDtos);
        }


        public ServiceResult<List<GetCourseDTO>> GetStudentCourses(int studentId)
        {
            var courses = _dbContext.Enrollments
                .Where(e => e.UserId == studentId)
                .Include(e => e.Course).ThenInclude(c => c.Lessons)
                .Select(e => e.Course)
                .ToList();

            if (!courses.Any())
            {
                return ServiceResult<List<GetCourseDTO>>.Fail("No enrolled courses found.");
            }

            var dtos = courses.Select(c => new GetCourseDTO
            {
                Id = c.Id,
                Title = c.Title,
                Description = c.Description,
                CreatedAt = c.CreatedAt,
                UpdatedAt = c.UpdatedAt,
                InstructorId = c.InstructorId,
                LessonsCount = c.Lessons.Count
            }).ToList();

            return ServiceResult<List<GetCourseDTO>>.Ok(dtos);
        }

        public ServiceResult<GetCourseDTO> SearchCourseByName(string title)
        {
            var course = _dbContext.Courses
                .FirstOrDefault(c => c.Title.ToLower() == title.ToLower());
            if (course == null)
            {
                return ServiceResult<GetCourseDTO>.Fail($"course with this title: {title} couldn't be found");
            }
            var courseDto = new GetCourseDTO
            {
                Title = course.Title,
                Description = course.Description,
                CreatedAt = course.CreatedAt,
                UpdatedAt = course.UpdatedAt,
                InstructorId = course.InstructorId,
                LessonsCount = course.Lessons.Count
            };
            return ServiceResult<GetCourseDTO>.Ok(courseDto);

        }//done

       
        public ServiceResult<GetCourseDTO> AddCourse(AddCourseDTO courseDTO, int instructorId)
        {
            var instructor = _dbContext.Users.FirstOrDefault(u => u.Id == instructorId && u.UserRole == UserRole.Instructor);
            if (instructor == null)
            {
                return ServiceResult<GetCourseDTO>.Fail("Only instructors can create courses.");
            }

            var course = new Course
            {
                Title = courseDTO.Title,
                Description = courseDTO.Description,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                InstructorId = instructorId,
                IsPublished = false // default false
            };

            _dbContext.Courses.Add(course);
            _dbContext.SaveChanges();

            var getCourseDto = new GetCourseDTO
            {
                Id = course.Id,
                Title = course.Title,
                Description = course.Description,
                CreatedAt = course.CreatedAt,
                UpdatedAt = course.UpdatedAt,
                InstructorId = course.InstructorId,
                LessonsCount = 0
            };

            return ServiceResult<GetCourseDTO>.Ok(getCourseDto);
        }

        public ServiceResult<GetCourseDTO> UpdateCourse(AddCourseDTO courseDTO, int courseId)
        {
            var course = _dbContext.Courses.Include(c => c.Lessons).FirstOrDefault(c => c.Id == courseId);
            if (course == null)
            {
                return ServiceResult<GetCourseDTO>.Fail($"Course with this id: {courseId} doesn't exist");
            }
            course.Title = courseDTO.Title;
            course.Description = courseDTO.Description;
            course.UpdatedAt = DateTime.Now;
            _dbContext.SaveChanges();

            var getCourseDto = new GetCourseDTO
            {
                Id = course.Id,
                Title = course.Title,
                Description = courseDTO.Description,
                CreatedAt = course.CreatedAt,
                InstructorId = course.InstructorId,
                LessonsCount = course.Lessons.Count
            };
            return ServiceResult<GetCourseDTO>.Ok(getCourseDto);
        }//admin or instructor? --->done
        public ServiceResult<string> DeleteCourse(int courseId)
        {
            var course = _dbContext.Courses.Find(courseId);
            if (course == null)
            {
                return ServiceResult<string>.Fail($"Course with this id: {courseId} doesn't exist");
            }
            _dbContext.Courses.Remove(course);
            _dbContext.SaveChanges();
            return ServiceResult<string>.Ok($"Course with id: {courseId} deleted successfully");
        } //admin --->done

        //only for authenticated so ensuring user exist is not necessary here, i don't know
        //i need the userId though to add an enrollment row
        public ServiceResult<bool> EnrollInCourse(int courseId, int userId)
        {
            var course = _dbContext.Courses.Find(courseId);
            if (course == null)
            {
                return ServiceResult<bool>.Fail($"Course with id: {courseId} doesn't exist");
            }
            var user = _dbContext.Users.Find(userId);
            if (user == null || user.UserRole != UserRole.Student)
            {
                return ServiceResult<bool>.Fail("Only students can enroll in courses.");
            }

            var enrollment = new Enrollment
            {
                CourseId = courseId,
                EnrolledAt = DateTime.Now,
                UserId = userId,
                IsCompleted = false,
            };
            _dbContext.Enrollments.Add(enrollment);
            _dbContext.SaveChanges();
            return ServiceResult<bool>.Ok(true);

        }//done

        public bool IsUserEnrolled(int courseId, int userId)
        {
            var enrollment = _dbContext.Enrollments.
                FirstOrDefault(enrollment => enrollment.CourseId == courseId && enrollment.UserId == userId);
            return enrollment != null; //genius, autocomplete suggested this but it is short and effective
            //if enrollment is null then the expression results in false and returns it
            //if enrollment is not null then the expression results in true and also returns it!
        } //done
        public ServiceResult<bool> UnrollFromCourse(int courseId, int userId)
        {
            var course = _dbContext.Courses.Find(courseId);
            if (course == null)
            {
                return ServiceResult<bool>.Fail($"Course with id: {courseId} doesn't exist");
            }
            var user = _dbContext.Users.Find(userId);
            if (user == null || user.UserRole != UserRole.Student)
            {
                return ServiceResult<bool>.Fail("Only students can enroll in courses.");
            }

            var enrollment = _dbContext.Enrollments.
                FirstOrDefault(enrollment => enrollment.CourseId == courseId && enrollment.UserId == userId);

            if (enrollment == null)
            {
                return ServiceResult<bool>.Fail("Something went wrong, you are nor enrolled in this course");
            }

            _dbContext.Enrollments.Remove(enrollment);
            _dbContext.SaveChanges();
            return ServiceResult<bool>.Ok(true);
        }//done
        public ServiceResult<bool> PublishCourse(int courseId)
        {
            var course = _dbContext.Courses.Find(courseId);
            if (course == null)
            {
                return ServiceResult<bool>.Fail($"Course with id {courseId} not found.");
            }

            course.IsPublished = true;
            course.UpdatedAt = DateTime.UtcNow;
            _dbContext.SaveChanges();

            return ServiceResult<bool>.Ok(true);
        }


        public bool IsCourseCompleted(int courseId, int userId)
        {
            var enrollment = _dbContext.Enrollments.
                FirstOrDefault(enrollment => enrollment.CourseId == courseId && enrollment.UserId == userId);
            if (enrollment == null)
            {
                return false;
            }
            return enrollment.IsCompleted;
        }//done
    }
}

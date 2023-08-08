using System.ComponentModel.DataAnnotations;

namespace CUni.Shared
{
    public record CreateStudentData
    {
        [MinLength(2)]
        public string FirstMidName { get; set; }

        [MinLength(2)]
        public string LastName { get; set; }

        [RegularExpression(@"^\d{4}-(0[1-9]|1[0-2])-(0[1-9]|[1-2]\d|3[0-1])$")]
        public string EnrollmentDate { get; set; }
    }

    public record UpdateStudentData
    {
        [MinLength(2)]
        public string? FirstMidName { get; set; }

        [MinLength(2)]
        public string? LastName { get; set; }
    }

    public record StudentResponse
    {
        public int Id { get; set; }
        public string FirstMidName { get; set; }
        public string LastName { get; set; }
        public DateTime EnrollmentDate { get; set; }

        public ICollection<EnrollmentResponse> Enrollments { get; set; }
    }

    public record CreateEnrollmentRequest
    {
        public int CourseId { get; set; }
        public int StudentId { get; set; }
        public Grade? Grade { get; set; }
    }

    public record EnrollmentResponse
    {
        public int Id { get; set; }
        public int CourseId { get; set; }
        public int StudentId { get; set; }
        public Grade? Grade { get; set; }
    }

    public record CourseResponse
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int Credits { get; set; }
        public int DepartmentId { get; set; }
        public ICollection<InstructorResponse> Instructors { get; set; }
        public ICollection<EnrollmentResponse> Enrollments { get; set; }
    }

    public record OfficeAssignmentResponse
    {
        public int InstructorId { get; set; }
        public string Location { get; set; }
    }

    public record InstructorResponse
    {
        public int Id { get; set; }
        public string LastName { get; set; }
        public string FirstMidName { get; set; }
        public DateTime HireDate { get; set; }
        public string FullName { get; set; }
        public OfficeAssignmentResponse OfficeAssignment { get; set; }
    }

    public record DepartmentResponse
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public decimal Budget { get; set; }

        public DateTime StartDate { get; set; }

        public int? InstructorId { get; set; }

        public InstructorResponse Administrator { get; set; }
        public ICollection<CourseResponse> Courses { get; set; }
    }


}

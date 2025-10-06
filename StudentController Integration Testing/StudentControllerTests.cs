using Xunit;
using LMS.Presentation.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts;


namespace StudentController_Integration_Testing
{
    public class StudentControllerTests
    {
        StudentController studentController;

        [Fact]
        public void GetCourseForStudent_NotFound_When_InvalidStudentID()
        {
            // Arrange
            Guid studentId = Guid.Empty;

            Task<IActionResult> actual = studentController.GetCourseForStudent(studentId);
            Task<IActionResult> expected = studentController.GetCourseForStudent(studentId);

            // Assert
            //Assert.Equal(expectedMsg, (IAsyncEnumerable<char>?)actualMsg);
            Assert.Equal(expected, actual);
        }
        //skapa en factory med test databas, custom web application factory 


        [Fact]
         public async Task GetCourseForStudent_NotFound_When_InvalidStudentID_2()
         {
              // Arrange
             Guid studentId = Guid.Empty;
             string expectedMsg = "Invalid student ID.";

             // Act
             string? actualMsg = studentController.GetCourseForStudent(studentId).ToString();
             IActionResult result = await studentController.GetCourseForStudent(studentId);

             // Assert
             Assert.Equal(expectedMsg, actualMsg);
         }


        [Fact]
        public async Task GetCourseForStudent_NotFound_When_InvalidStudentID_3()
        {
            // Arrange
            Guid studentId = Guid.Empty;
            string expectedMsg = "Invalid student ID.";

            // Act
            string? actualMsg = studentController.GetCourseForStudent(studentId).ToString();
            IActionResult result = await studentController.GetCourseForStudent(studentId);

            // Assert
            Assert.Equal(expectedMsg, actualMsg);
        }
    }
}

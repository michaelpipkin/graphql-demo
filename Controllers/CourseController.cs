using GraphQLDemoBase.Authorization;
using GraphQLDemoBase.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GraphQLDemoBase.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class CourseController
    {
        private AppDBContext dbContext;

        private TokenInfo Token { get; set; }
        private ITokenManager tokenManager { get; set; }

        public CourseController(AppDBContext _dbContext, TokenInfo tokeninfo, ITokenManager _tokenManager) {
            dbContext = _dbContext;
            Token = tokeninfo;
            tokenManager = _tokenManager;
        }

        /// <summary>
        /// This returns the same data as the GraphQL method GetAllStudents
        /// </summary>
        /// <returns></returns>
        /// 
        [HttpGet]
        [Route("GetAuthToken")]
        [AllowAnonymous]
        public string GetAuthToken(int userId) {
            return tokenManager.GenerateToken(userId);
        }

        [HttpGet]
        [Route("GetAllStudentsByRest")]
        public IQueryable<Student> GetAllStudentsByRest() {
            return dbContext.Students;
        }

        [HttpGet]
        [Route("GetAllCoursesByRest")]
        public IQueryable<Course> GetAllCoursesByRest() {
            return dbContext.Courses;
        }
    }
}
using GraphQLDemoBase.Authorization;
using GraphQLDemoBase.Models;

namespace GraphQLDemoBase.Queries
{
    [ExtendObjectType("Query")]
    public class StudentQueries
    {
        private readonly TokenInfo _token;
        private readonly ITokenManager _tokenManager;
        public StudentQueries(TokenInfo tokeninfo, ITokenManager tokenManager) {
            _token = tokeninfo;
            _tokenManager = tokenManager;
        }

        [UseDbContext(typeof(AppDBContext))]
        [UseProjection, UseFiltering]
        public IQueryable<Student> GetStudentByName(string firstName, [ScopedService] AppDBContext context) {
            return context.Students.Where(s => s.FirstMidName.Contains(firstName));
        }

        [GraphQLDescription("This accepts Where filters and allows for drilldown")]
        [UseDbContext(typeof(AppDBContext))]
        [UseProjection, UseFiltering]
        public IQueryable<Student> GetStudents([ScopedService] AppDBContext context) {
            return context.Students;
        }

        [UseDbContext(typeof(AppDBContext))]
        [UseProjection, UseFiltering]
        public IQueryable<Course> GetCourses([ScopedService] AppDBContext context) {
            return context.Courses;
        }
    }
}

//[GraphQLDescription("This might accept Where filters and should allows for drilldown")]
//[UseDbContext(typeof(AppDBContext))]
//[UseProjection]
//public  async Task<IAsyncEnumerable<ClientApi.EnrollmentServices.EnrollmentAPI>> GetStudentsByAPI()
//{
//    string serviceURL = "http://localhost:19531/";
//    CancellationTokenSource source = new CancellationTokenSource();
//    CancellationToken token = source.Token;
//    var httpClient = new HttpClient();
//    var client = new EnrollmentAPIServices(
//        serviceURL,
//         httpClient);
//
//    //var webCall = (
//    //    serviceURL,
//    //     httpClient);
//    //var result = await client.GetEnrollmentsParentAsync(); 
//     var result = await client.GetEnrollmentsByJoinAsync(token);
//     return result.ToAsyncEnumerable<ClientApi.EnrollmentServices.EnrollmentAPI>();
//    //The source IQueryable doesn't implement IAsyncEnumerable.
//    //Only sources that implement IAsyncEnumerable can be used for Entity Framework asynchronous operations.
//}

//[GraphQLDescription("This accepts a course ID")]
//[UseProjection]
//public async Task<IAsyncEnumerable<TimeslotByCourse.Course>> GetCourseByAPI([Service] TimeslotByCourse.swaggerTimeslot swagSvc, int? courseID)
//{
//    //string serviceURL = "http://localhost:19531/";
//    CancellationTokenSource source = new CancellationTokenSource();
//    CancellationToken token = source.Token;
//    //var httpClient = new HttpClient();
//    //var client = new TimeslotByCourse.swaggerTimeslot(
//    //    serviceURL,
//    //     httpClient);
//
//    ////var webCall = (
//    ////    serviceURL,
//    ////     httpClient);
//    ////var result = await client.GetEnrollmentsParentAsync(); 
//    //var result = await client.GetCourseTimeSlotsAsync(courseID);
//    var result = await swagSvc.GetCourseTimeSlotsAsync(courseID, token);
//    return (IAsyncEnumerable<TimeslotByCourse.Course>)result.ToAsyncEnumerable();
//    //The source IQueryable doesn't implement IAsyncEnumerable.
//    //Only sources that implement IAsyncEnumerable can be used for Entity Framework asynchronous operations.
//}
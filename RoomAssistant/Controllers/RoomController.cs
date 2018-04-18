using System.Web.Http;
using RoomAssistant.Services;
using Swashbuckle.Swagger.Annotations;

namespace RoomAssistant.Controllers
{
    public class RoomController : ApiController
    {
        // GET api/values
        [SwaggerOperation("GetAll")]
        public IHttpActionResult Get()
        {
            RoomService roomService = new RoomService();
            var result = roomService.FindRooms();
            return Ok(result);
        }
    }
}

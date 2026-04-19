using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using PJATK_APBD_Cw5_s33613.DTO;
using PJATK_APBD_Cw5_s33613.Objects;

namespace PJATK_APBD_Cw5_s33613.Controllers;
[ApiController]
[Route("api/[controller]")]

public class RoomsController : ControllerBase
{
    public static List<Room> rooms = [
    new Room()
    {
        id = 1,
        name = "Room VIP",
        buildingCode = "a1",
        floor = 3,
        capacity = 5,
        hasProjector = true,
        isActive = true
    },
    new Room(){
        id = 2,
        name = "Room 101",
        buildingCode = "a1",
        floor = 1,
        capacity = 5,
        hasProjector = false,
        isActive = true
    },
    new Room()
    {
        id = 3,
        name = "Room 102",
        buildingCode = "a1",
        floor = 1,
        capacity = 5,
        hasProjector = false,
        isActive = true
    }];
    [HttpGet]
    public IActionResult GetAllRooms()
    {
        return Ok(rooms.Select(e => new RoomDTO()
        {
            id = e.id,
            name = e.name,
            buildingCode = e.buildingCode,
            floor = e.floor,
            capacity = e.capacity,
            hasProjector = e.hasProjector,
            isActive = e.isActive
        }));
    }
}
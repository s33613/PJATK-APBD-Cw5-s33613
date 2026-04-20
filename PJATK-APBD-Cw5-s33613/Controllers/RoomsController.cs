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
        Id = 1,
        Name = "Room VIP",
        BuildingCode = "a1",
        Floor = 3,
        Capacity = 5,
        HasProjector = true,
        IsActive = true
    },
    new Room(){
        Id = 2,
        Name = "Room 101",
        BuildingCode = "a1",
        Floor = 1,
        Capacity = 5,
        HasProjector = false,
        IsActive = true
    },
    new Room()
    {
        Id = 3,
        Name = "Room 102",
        BuildingCode = "a2",
        Floor = 1,
        Capacity = 5,
        HasProjector = false,
        IsActive = true
    }];
    [HttpGet]
    public IActionResult GetAllRooms([FromQuery] int? minCapacity, [FromQuery] bool? hasProjector, [FromQuery] bool? isActive)
    {
        var selected = rooms.Where(e => (e.HasProjector == hasProjector || hasProjector is null)
                                     && (e.IsActive == isActive || isActive is null)
                                     && (e.Capacity >= minCapacity || minCapacity is null))
            .Select(e => new RoomDTO(e));
        if (selected.Any())
            return Ok(selected);
        else
        {
            return NotFound($"No rooms found");
        }
    }
    [HttpGet("{id:int}")]
    public IActionResult GetRoom(int id)
    {
        var room = rooms.FirstOrDefault(e => e.Id == id);

        if (room is null)
        {
            return NotFound($"room with id {id} not found");
        }
    
        return Ok(new RoomDTO(room));
    }
    [HttpGet("building/{buildingCode}")]
    public IActionResult GetRoom2(string? buildingCode)
    {
        var room = rooms.FirstOrDefault(e => e.BuildingCode == buildingCode);
        if (room is null)
        {
            return NotFound($"No rooms found in building {buildingCode}");
        }
    
        return Ok(rooms.Where(e => e.BuildingCode == buildingCode).Select(e => new RoomDTO(e)));
    }
}
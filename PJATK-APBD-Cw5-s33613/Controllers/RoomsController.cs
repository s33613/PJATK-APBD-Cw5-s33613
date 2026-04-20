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
    },new Room()
    {
        Id = 4,
        Name = "Room inactive",
        BuildingCode = "a5",
        Floor = 0,
        Capacity = 5,
        HasProjector = false,
        IsActive = false
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
    public IActionResult GetRoomsByFloor(string? buildingCode)
    {
        var room = rooms.FirstOrDefault(e => e.BuildingCode == buildingCode);
        if (room is null)
        {
            return NotFound($"No rooms found in building {buildingCode}");
        }
    
        return Ok(rooms.Where(e => e.BuildingCode == buildingCode).Select(e => new RoomDTO(e)));
    }
    [HttpPost]
    public IActionResult Add(ChangeRoomDTO dto)
    {
        if (dto.Floor < 0)
            return BadRequest($"Floor {dto.Floor} is invalid");
        if (dto.Capacity < 0)
            return BadRequest($"Capacity {dto.Capacity} must be greater than zero");
        if (dto.Name.Length == 0)
            return BadRequest($"Name is required");
        if (dto.BuildingCode.Length == 0)
            return BadRequest($"BuildingCode is required");
        var room = new Room
        {
            Id = rooms.Max(e => e.Id) + 1,
            Name = dto.Name,
            BuildingCode = dto.BuildingCode,
            Floor = dto.Floor,
            Capacity = dto.Capacity,
            HasProjector = dto.HasProjector,
            IsActive = true
        };
    
        rooms.Add(room);
    
        // return Created($"students/{student.Id}", student);
        return CreatedAtAction(nameof(GetRoom), new { id = room.Id }, room);
    }
    [HttpPut("{id:int}")]
    public IActionResult Update(int id, ChangeRoomDTO updateDto)
    {
        var room = rooms.FirstOrDefault(e => e.Id == id);
        if (room is null)
        {
            return NotFound($"room with id {id} not found");
        }
        if (updateDto is null)
            return BadRequest($"Provided values are incorrect");
        if (updateDto.Floor < 0)
            return BadRequest($"Floor {updateDto.Floor} is invalid");
        if (updateDto.Capacity < 0)
            return BadRequest($"Capacity {updateDto.Capacity} must be greater than zero");
        if (updateDto.Name.Length == 0)
            return BadRequest($"Name is required");
        if (updateDto.BuildingCode.Length == 0)
            return BadRequest($"BuildingCode is required");
        
        room.Name = updateDto.Name;
        room.BuildingCode = updateDto.BuildingCode;
        room.Floor = updateDto.Floor;
        room.Capacity = updateDto.Capacity;
        room.HasProjector = updateDto.HasProjector;
    
        return NoContent();
    }
    [HttpDelete("{id:int}")]
    public IActionResult Delete(int id)
    {
        var room = rooms.FirstOrDefault(e => e.Id == id);

        if (room is null)
        {
            return NotFound($"room with id {id} not found");
        }
        if (ReservationController.reservations.Any(e => e.RoomId == id))
            return Conflict($"Room with id {id} has at least one reservation");
    
        rooms.Remove(room);
        return NoContent();
    }
    
}
using System.Runtime.InteropServices.JavaScript;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using PJATK_APBD_Cw5_s33613.DTO;
using PJATK_APBD_Cw5_s33613.Objects;

namespace PJATK_APBD_Cw5_s33613.Controllers;
[ApiController]
[Route("api/[controller]")]

public class ReservationController : ControllerBase
{
    public static List<Reservation> reservations = [
    new Reservation()
    {
        Id = 1,
        RoomId = 1,
        OrganizerName = "Micheal Jordan",
        Topic = "Dear Basketball",
        Date = new DateOnly(2025,10,2),
        StartTime = new TimeOnly(12,30),
        EndTime = new TimeOnly(13,30),
        Status = States.planned
    },
    new Reservation()
    {
        Id = 2,
        RoomId = 2,
        OrganizerName = "Micheal Jordan",
        Topic = "Dear Basketball",
        Date = new DateOnly(2025,10,2),
        StartTime = new TimeOnly(14,30),
        EndTime = new TimeOnly(15,30),
        Status = States.confirmed
    },
    new Reservation()
    {
    Id = 3,
    RoomId = 3,
    OrganizerName = "Vegeta",
    Topic = "The importance of pride",
    Date = new DateOnly(2025,10,2),
    StartTime = new TimeOnly(12,30),
    EndTime = new TimeOnly(13,30),
    Status = States.confirmed
    }];
    [HttpGet]
    public IActionResult GetAllReservations([FromQuery] DateOnly? date, [FromQuery] int? roomId, [FromQuery] States? status)
    {
        var selected = reservations.Where(e => (e.Date.Equals(date) || date is null)
                                     && (e.RoomId == roomId || roomId is null)
                                     && (e.Status == status || status is null))
            .Select(e => new ReservationDTO(e));
        if (selected.Any())
            return Ok(selected);
        else
        {
            if(RoomsController.rooms.FirstOrDefault(e => e.Id.Equals(roomId)) is null)
                return NotFound($"room with id {roomId} does not exist"); 
            return NotFound($"No reservations found");
        }
    }
    [HttpGet("{id:int}")]
    public IActionResult GetReservation(int id)
    {
        var reservation = reservations.FirstOrDefault(e => e.Id == id);

        if (reservation is null)
        {
            return NotFound($"reservation with id {id} not found");
        }
    
        return Ok(new ReservationDTO(reservation));
    }
    [HttpPost]
    public IActionResult Add(ChangeReservationDTO dto)
    {
        if (dto.OrganizerName.Length == 0)
            return BadRequest($"Organizer Name is required");
        if (dto.Topic.Length == 0)
            return BadRequest($"Topic is required");
        if (dto.StartTime > dto.EndTime)
            return BadRequest($"Start time must be earlier than End time");
        if(RoomsController.rooms.FirstOrDefault(e => e.Id.Equals(dto.RoomId)) is null)
            return BadRequest($"Room with id {dto.RoomId} not found");
        if(RoomsController.rooms.FirstOrDefault(e => e.Id.Equals(dto.RoomId)).IsActive == false)
            return BadRequest($"Room with id {dto.RoomId} is not active");
        if(reservations.FirstOrDefault(e => e.RoomId.Equals(dto.RoomId) && e.Date.Equals(dto.Date) 
                                && (e.StartTime < (dto.EndTime) || e.EndTime > (dto.StartTime))) is null)
            return Conflict($"Reservation in room with id {dto.RoomId} overlaps with another reservation");
        
        var reservation = new Reservation()
        {
            Id = reservations.Max(e => e.Id) + 1,
            OrganizerName =  dto.OrganizerName,
            Topic = dto.Topic,
            Date = dto.Date,
            StartTime = dto.StartTime,
            EndTime = dto.EndTime,
            Status = dto.Status
        };
    
        reservations.Add(reservation);
    
        // return Created($"students/{student.Id}", student);
        return CreatedAtAction(nameof(GetReservation), new { id = reservation.Id }, reservation);
    }
    [HttpPut("{id:int}")]
    public IActionResult Update(int id, ChangeReservationDTO updateDto)
    {
        var reservation = reservations.FirstOrDefault(e => e.Id == id);
        if (reservation is null)
        {
            return NotFound($"reservation with id {id} not found");
        }
        if (updateDto.OrganizerName.Length == 0)
            return BadRequest($"Organizer Name is required");
        if (updateDto.Topic.Length == 0)
            return BadRequest($"Topic is required");
        if (updateDto.StartTime > updateDto.EndTime)
            return BadRequest($"Start time must be earlier than End time");
        if(RoomsController.rooms.FirstOrDefault(e => e.Id.Equals(updateDto.RoomId)) is null)
            return BadRequest($"Room with id {updateDto.RoomId} not found");
        
        reservation.RoomId = updateDto.RoomId;
        reservation.OrganizerName = updateDto.OrganizerName;
        reservation.Topic = updateDto.Topic;
        reservation.Date = updateDto.Date;
        reservation.StartTime = updateDto.StartTime;
        reservation.EndTime = updateDto.EndTime;
        reservation.Status = updateDto.Status;
    
        return NoContent();
    }
    [HttpDelete("{id:int}")]
    public IActionResult Delete(int id)
    {
        var reservation = reservations.FirstOrDefault(e => e.Id == id);
    
        if (reservation is null)
        {
            return NotFound($"room with id {id} not found");
        }
    
        reservations.Remove(reservation);
        return NoContent();
    }
    
}
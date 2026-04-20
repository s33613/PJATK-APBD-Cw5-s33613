using PJATK_APBD_Cw5_s33613.Objects;

namespace PJATK_APBD_Cw5_s33613.DTO;

public class ReservationDTO
{
    public int RoomId { get; set; }
    public string OrganizerName { get; set; }
    public string Topic { get; set; }
    public DateOnly Date { get; set; }
    public TimeOnly StartTime { get; set; }
    public TimeOnly EndTime  { get; set; }
    public string Status { get; set; } // todo: make an enum

    public ReservationDTO(Reservation reservation)
    {
        RoomId = reservation.RoomId;
        OrganizerName = reservation.OrganizerName;
        Topic = reservation.Topic;
        Date = reservation.Date;
        StartTime = reservation.StartTime;
        EndTime = reservation.EndTime;
        Status = reservation.Status;
    }
}
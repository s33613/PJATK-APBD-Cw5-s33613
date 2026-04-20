namespace PJATK_APBD_Cw5_s33613.Objects;

public class Reservation
{
    public int Id { get; set; }
    public int RoomId { get; set; }
    public string OrganizerName { get; set; }
    public string Topic { get; set; }
    public DateOnly Date { get; set; }
    public TimeOnly StartTime { get; set; }
    public TimeOnly EndTime  { get; set; }
    public States Status { get; set; } // todo: make an enum
}
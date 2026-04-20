using PJATK_APBD_Cw5_s33613.Objects;

namespace PJATK_APBD_Cw5_s33613.DTO;
using System.ComponentModel.DataAnnotations;
public class ChangeReservationDTO
{
    public int RoomId { get; set; }
    [MaxLength(20),Required]
    public string OrganizerName { get; set; }
    [MaxLength(100),Required]
    public string Topic { get; set; }
    public DateOnly Date { get; set; }
    public TimeOnly StartTime { get; set; }
    public TimeOnly EndTime  { get; set; }
    public States Status { get; set; } // todo: make an enum
}
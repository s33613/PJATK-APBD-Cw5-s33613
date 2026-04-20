using System.ComponentModel.DataAnnotations;

namespace PJATK_APBD_Cw5_s33613.DTO;

public class ChangeRoomDTO
{
    [MaxLength(20),Required]
    public string Name { get; set; }
    [MaxLength(4),Required]
    public string BuildingCode { get; set; }
    [Required]
    public int Floor { get; set; }
    [Required]
    public int Capacity { get; set; }
    [Required]
    public bool HasProjector  { get; set; }
}
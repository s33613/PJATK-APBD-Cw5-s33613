namespace PJATK_APBD_Cw5_s33613.DTO;

public class CreateRoomDto
{
    public string Name { get; set; }
    public string BuildingCode { get; set; }
    public int Floor { get; set; }
    public int Capacity { get; set; }
    public bool HasProjector  { get; set; }
}
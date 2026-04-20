namespace PJATK_APBD_Cw5_s33613.Objects;

public class Room
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string BuildingCode { get; set; }
    public int Floor { get; set; }
    public int Capacity { get; set; }
    public bool HasProjector  { get; set; }
    public bool IsActive { get; set; }
}
namespace PJATK_APBD_Cw5_s33613.Objects;
using System.Text.Json.Serialization;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum States
{
    planned,confirmed,cancelled,completed
}
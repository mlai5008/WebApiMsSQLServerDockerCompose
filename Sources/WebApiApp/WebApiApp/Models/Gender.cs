using System.Text.Json.Serialization;

namespace WebApiApp.Models
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum Gender
    {
        Male = 0,
        Female = 1,
        Other = 2,
        Unknow = 3
    }
}

using System.Text.Json.Serialization;

namespace dotnet_api_first.models
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum RpgClass
    {
        Paladin = 1,
        Rouge = 2,
        DeathKnight = 3,
        Hunter = 4,
    }
}
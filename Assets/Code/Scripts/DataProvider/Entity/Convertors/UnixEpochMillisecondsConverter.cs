using System;
using Newtonsoft.Json;

public class UnixEpochMillisecondsConverter : JsonConverter<DateTime>
{
    public override DateTime ReadJson(
        JsonReader reader,
        Type objectType,
        DateTime existingValue,
        bool hasExistingValue,
        JsonSerializer serializer
    )
    {
        if (reader.TokenType == JsonToken.Integer)
        {
            long milliseconds = (long)reader.Value;
            return DateTimeOffset.FromUnixTimeMilliseconds(milliseconds).DateTime;
        }

        return DateTime.MinValue;
    }

    public override void WriteJson(JsonWriter writer, DateTime value, JsonSerializer serializer)
    {
        writer.WriteValue(new DateTimeOffset(value).ToUnixTimeMilliseconds());
    }
}

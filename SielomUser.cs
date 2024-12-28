using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace UristBot
{
    internal class SielomUser
    {
        [BsonId]  // Указывает, что это поле является уникальным идентификатором в MongoDB
        [BsonElement("_id")]  // Указывает на соответствующее поле в базе данных MongoDB
        public ObjectId Id { get; set; }
        [JsonPropertyName("tg_id")]
        public long TgId { get; set; }
        [JsonPropertyName("user_id")]
        public string UserId { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("group")]
        public string Group { get; set; }

        [JsonPropertyName("group_id")]
        public string GroupId { get; set; }

        [JsonPropertyName("status")]
        public string Status { get; set; }

        public static SielomUser? FromJson(string json)
        {
            try
            {
                return JsonSerializer.Deserialize<SielomUser>(json);
            }
            catch { return null; }
        }
    }
}

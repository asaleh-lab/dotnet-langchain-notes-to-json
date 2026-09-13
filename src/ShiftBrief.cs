using System.Text.Json;
using System.Text.Json.Serialization;

internal sealed class ShiftBrief
{
    [JsonPropertyName("title")]
    public string Title { get; set; } = "";

    [JsonPropertyName("actions")]
    public List<string> Actions { get; set; } = [];

    [JsonPropertyName("owners")]
    public List<string> Owners { get; set; } = [];

    public static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNameCaseInsensitive = true,
        WriteIndented = true,
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
    };

    public const string FormatInstructions =
        """
        The output should be a JSON object that matches this schema:
        {
          "title": string (One-line summary of the shift),
          "actions": string[] (Tasks that still need to happen),
          "owners": string[] (People responsible)
        }
        """;

    public static ShiftBrief Parse(string text)
    {
        string json = text.Trim();
        if (json.StartsWith("```", StringComparison.Ordinal))
        {
            int start = json.IndexOf('{');
            int end = json.LastIndexOf('}');
            if (start < 0 || end < start)
            {
                throw new InvalidOperationException("Model reply did not contain a JSON object.");
            }

            json = json[start..(end + 1)];
        }

        return JsonSerializer.Deserialize<ShiftBrief>(json, JsonOptions)
            ?? throw new InvalidOperationException("Model reply was not a shift brief.");
    }

    public override string ToString() => JsonSerializer.Serialize(this, JsonOptions);
}

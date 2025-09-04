using System.Text.Json.Serialization;

namespace allspice.Models;

public class Account
{
  [JsonPropertyName("sub")]
  public string Id { get; set; }
  [JsonPropertyName("name")]
  public string Name { get; set; }
  [JsonPropertyName("email")]
  public string Email { get; set; }
  [JsonPropertyName("picture")]
  public string Picture { get; set; }
}

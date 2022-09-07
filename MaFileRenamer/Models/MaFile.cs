using Newtonsoft.Json;

namespace MaFileRenamer.Models;

public class MaFile
{
    [JsonProperty("account_name")]
    public string AccountName { get; set; }
}
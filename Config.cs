using System.IO;
using Newtonsoft.Json;

namespace TwitchChatLogger
{
  public class Config
  {
    public string Name { get; set; }
    public string AuthToken { get; set; }
    public string Password { get; set; }

    public void SetConfig(string path)
    {
      string json = File.ReadAllText(path);
      Config config = JsonConvert.DeserializeObject<Config>(json);

      Name = config.Name;
      AuthToken = config.AuthToken;
      Password = config.Password;
    }
  }
}

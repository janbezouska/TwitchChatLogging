using System;
using System.Threading;
using System.Data.SqlClient;
using Dapper;
using TwitchLib;
using TwitchLib.Client;
using TwitchLib.Client.Models;
using TwitchLib.Communication.Clients;
using TwitchLib.Communication.Models;

namespace TwitchChatLogger
{
  static class Program
  {
    static void Main(string[] args)
    {
      Bot bot = new("Epousek");
      Bot bot2 = new("Herdyn");
      Console.ReadLine();
    }
  }

  class Bot
  {
    TwitchClient client;
    private Config config;

    public Bot(string channel)
    {
      config = new();
      config.SetConfig("config.txt");

      ConnectionCredentials credentials = new ConnectionCredentials(config.Name, config.AuthToken);
      //var clientOptions = new ClientOptions
      //{
      //  MessagesAllowedInPeriod = 200,
      //  ThrottlingPeriod = TimeSpan.FromSeconds(30)
      //};
      //WebSocketClient wsClient = new WebSocketClient(clientOptions);
      client = new();
      client.Initialize(credentials, channel);

      client.OnJoinedChannel += Client_OnJoinedChannel;
      client.OnMessageReceived += Client_OnMessageReceived;

      client.Connect();
    }

    private void Client_OnJoinedChannel(object sender, TwitchLib.Client.Events.OnJoinedChannelArgs e) => Console.WriteLine("funguje to");
    private void Client_OnMessageReceived(object sender, TwitchLib.Client.Events.OnMessageReceivedArgs e)
    {
      Console.WriteLine($"Channel: {e.ChatMessage.Channel}; " +
      $"DISPLAY NAME: {e.ChatMessage.DisplayName}; MESSAGE: {e.ChatMessage.Message}");

      ChatMessage msg = new()
      {
        When = DateTime.Now,
        Channel = e.ChatMessage.Channel,
        Name = e.ChatMessage.DisplayName,
        Message = e.ChatMessage.Message
      };

      ToDb(msg, config.Password);
    }

    private async void ToDb(ChatMessage msg, string pass)
    {
      SqlConnectionStringBuilder builder = new();

      builder.ConnectionString = $"Server=tcp:sql-twitchchatlogger.database.windows.net,1433;Initial Catalog=ChatLogs;Persist Security Info=False;User ID=Epousek;Password={pass};MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

      using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
      {
        await connection.ExecuteAsync("dbo.WriteMessage @Channel, @Username, @ChatMessage, @TimeStamp",
          new { Channel = msg.Channel, Username = msg.Name, ChatMessage = msg.Message, TimeStamp = msg.When});
      }
    }
  }
}

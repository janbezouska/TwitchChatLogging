using System;
using TwitchLib;
using TwitchLib.Client;
using TwitchLib.Client.Models;
using TwitchLib.Communication.Clients;
using TwitchLib.Communication.Models;

namespace TwitchChatLogger
{
  class Program
  {
    static void Main(string[] args)
    {
      Bot bot = new();
      Console.ReadLine();
    }
  }

  class Bot
  {
    TwitchClient client;
    private Config config;

    public Bot()
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
      client.Initialize(credentials, "Epousek");

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
    }
  }
}

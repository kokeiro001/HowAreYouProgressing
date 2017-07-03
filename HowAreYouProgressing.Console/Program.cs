using HowAreYouProgressing.Service;

namespace HowAreYouProgressing.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            // 即時実行のテスト用。
            var config = new NotifyConfig
            {
                //DiscordWebhookId = 0,
                //DiscordWebhookToken = @"",
                //GitLabHostUrl = @"https://gitlab.com",
                //GitLabApiToken = @"",
                //GitLabTargetProjectId = 0,
            };

            new NotifyService(config).NotifyAsync().GetAwaiter().GetResult();
        }
    }
}

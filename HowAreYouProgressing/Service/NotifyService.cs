using System;
using System.Linq;
using System.Threading.Tasks;
using Discord.Webhook;
using NGitLab;

namespace HowAreYouProgressing.Service
{
    public class NotifyConfig
    {
        public ulong DiscordWebhookId { get; set; }
        public string DiscordWebhookToken { get; set; }
        public string GitLabHostUrl { get; set; }
        public string GitLabApiToken { get; set; }
        public int GitLabTargetProjectId { get; set; }
    }

    public class NotifyService
    {
        readonly NotifyConfig config;

        public NotifyService(NotifyConfig config) => this.config = config;

        public async Task NotifyAsync()
        {
            var client = new DiscordWebhookClient(config.DiscordWebhookId, config.DiscordWebhookToken);

            int comitNumSince24h = GetCommitNumSince24h();
            int remainDate = GetRemainDate();

            var text = $"C93まであと{remainDate}日！24時間以内のコミット数は{comitNumSince24h}！！進　捗　ど　う　で　す　か　？　？　？";

            await client.SendMessageAsync(text);
        }

        int GetCommitNumSince24h()
        {
            var gitlabClinet = GitLabClient.Connect(config.GitLabHostUrl, config.GitLabApiToken);
            var repository = gitlabClinet.GetRepository(config.GitLabTargetProjectId);
            var commits = repository.Commits.ToList();

            var now = DateTime.UtcNow;
            var before24h = now.AddHours(-24);
            return commits.Where(x => x.CreatedAt >= before24h).Count();
        }

        int GetRemainDate()
        {
            var now = DateTime.UtcNow;
            var comicMarketDate = new DateTime(2017, 12, 29);
            return (comicMarketDate - now).Days;
        }
    }
}

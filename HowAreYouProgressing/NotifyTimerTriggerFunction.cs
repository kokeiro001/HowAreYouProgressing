using System;
using System.Threading.Tasks;
using HowAreYouProgressing.Service;
using Microsoft.Azure;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;

namespace HowAreYouProgressing
{
    public static class NotifyTimerTriggerFunction
    {
        // 時差考える。21時に通知したいので、21-9=12
        [FunctionName("NotifyTimerTrigger")]
        public static async Task Run([TimerTrigger("0 0 12 * * *")]TimerInfo myTimer, TraceWriter log)
        {
            log.Info($"C# Timer trigger function executed at: {DateTime.Now}");

            var config = new NotifyConfig
            {
                DiscordWebhookId = ulong.Parse(CloudConfigurationManager.GetSetting("DiscordWebhookId")),
                DiscordWebhookToken = CloudConfigurationManager.GetSetting("DiscordWebhookToken"),
                GitLabHostUrl = CloudConfigurationManager.GetSetting("GitLabHostUrl"),
                GitLabApiToken = CloudConfigurationManager.GetSetting("GitLabGitLabApiToken"),
                GitLabTargetProjectId = int.Parse(CloudConfigurationManager.GetSetting("GitLabTargetProjectId")),
            };

            await new NotifyService(config).NotifyAsync();
        }
    }
}
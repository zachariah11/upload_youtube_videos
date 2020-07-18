using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Upload;
using Google.Apis.Util.Store;
using Google.Apis.YouTube.v3;
using Google.Apis.YouTube.v3.Data;
using System.Reflection;
using System.Threading;
using Upload_Video_To_Youtube.Properties;

namespace Upload_Video_To_Youtube
{
    class UpVideo
    {
        
        public void videoUp(string videoPath, string titleText, string descripText,string privacy , string[]tags)
        {
            new UpVideo().Run(videoPath, titleText, descripText, privacy, tags).Wait();
            Console.WriteLine("End Uploading Video");
        }
        
        async Task Run( string videoPath, string titleText, string descripText, string privacy, string[] tags)
        {
            ClientSecrets clientSecrets = new ClientSecrets();
            clientSecrets.ClientId= Resources.client_id;
            clientSecrets.ClientSecret = Resources.client_secret;
            Console.WriteLine("Getting Credential Path");
            UserCredential credential;
            credential = await GoogleWebAuthorizationBroker.AuthorizeAsync(
                clientSecrets,
                new[] { YouTubeService.Scope.YoutubeUpload },
                "user",
                CancellationToken.None
            );
            Console.WriteLine("End of Getting Credential Path");
            Console.WriteLine("Getting Youtube Service");
            var youtubeService = new YouTubeService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = Assembly.GetExecutingAssembly().GetName().Name
            });
            youtubeService.HttpClient.Timeout = TimeSpan.FromMinutes(60);
            Console.WriteLine("End of Getting Youtube Service");
            Console.WriteLine("Start Uploading Video");
            var video = new Video();
            video.Snippet = new VideoSnippet();
            video.Snippet.Title = titleText;
            video.Snippet.Description = descripText;
            video.Snippet.Tags = tags;
            video.Snippet.CategoryId = "22"; // See https://developers.google.com/youtube/v3/docs/videoCategories/list
            video.Status = new VideoStatus();
            video.Status.PrivacyStatus = privacy; // or "private" or "public"

            var filePath = videoPath;
            
            using (var fileStream = new FileStream(filePath, FileMode.Open))
            {
                VideoInsertRequest vir = new VideoInsertRequest();
                var videosInsertRequest = youtubeService.Videos.Insert(video, "snippet,status", fileStream, "video/*");
                videosInsertRequest.ProgressChanged += vir.videosInsertRequest_ProgressChanged;
                videosInsertRequest.ResponseReceived += vir.videosInsertRequest_ResponseReceived;
                await videosInsertRequest.UploadAsync();
            }            
        }
        
    }
}

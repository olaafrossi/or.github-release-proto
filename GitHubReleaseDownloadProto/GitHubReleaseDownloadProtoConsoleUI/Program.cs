using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Octokit;
using System.Net;
using System.IO;
using System.Net.Http;
using Newtonsoft;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using RestSharp;
using Newtonsoft.Json.Linq;
using Markdig;
using System.Text.RegularExpressions;

namespace GitHubReleaseDownloadProtoConsoleUI
{
    class Program
    {

        public static void Main(string[] args)
        {
            string token = "ad3d79eb08dfe0b07cd3c245a4b7065d577b8123"; // Olaaf's personal token
            string clientAppName = "LPWatchdog";
            string accountOwner = "olaafrossi";
            string workingRepo = "or.github-release-proto";

            // builds a connection to GitHub with Olaaf's Token
            var client = GetGitHubConnection(token, clientAppName);

            // connects to an Account , using Olaaf's account right now
            var myRepos = ConnectGitHubAccount(client, accountOwner);

            // connects to a specific repo
            var myRepo = ConnectGitHubRepo(client, accountOwner, workingRepo);

            // connects to a specific repo/release
            var myRelease = ConnectGitHubRepoRelease(client, accountOwner, workingRepo);

            Thread.Sleep(5000);
            // hold here
            Console.ReadLine();


        }

        public static GitHubClient GetGitHubConnection(string token, string clientAppName)
        {
            var tokenAuth = new Credentials(token); 
            var output = new GitHubClient(new Octokit.ProductHeaderValue(clientAppName));
            output.Credentials = tokenAuth;
            return output;
        }

        public static async Task<IList<string>> ConnectGitHubAccount(GitHubClient myRepos, string accountOwner)
        {
            var account = await myRepos.User.Get(accountOwner);

            // create a list of info in the account and then iterate through it
            IList<string> accountInfoList = new List<string>();
            accountInfoList.Add($"Account Avatar URL {account.AvatarUrl}");
            accountInfoList.Add(account.Url);
            accountInfoList.Add(account.TotalPrivateRepos.ToString());
            accountInfoList.Add(account.PublicRepos.ToString());
            accountInfoList.Add(account.Company);
            accountInfoList.Add(account.HtmlUrl);
            accountInfoList.Add(account.Name);

            foreach (var item in accountInfoList)
            {
                Console.WriteLine($"account info: {item}");
            }

            Console.ReadLine();

            return accountInfoList;
        }

        public static async Task<IList<string>> ConnectGitHubRepo(GitHubClient myRepos, string accountOwner, string workingRepo)
        {
            var repoInfo = await myRepos.Repository.Get(accountOwner, workingRepo);
            var branch = await myRepos.Repository.Branch.Get(accountOwner, workingRepo, "master");

            // create a list of info in the account and then iterate through it
            IList<string> repoInfoList = new List<string>();
            //repoInfoList.Add(repoInfo.ToString());
            repoInfoList.Add(repoInfo.PushedAt.ToString());
            repoInfoList.Add(repoInfo.Url);
            repoInfoList.Add(repoInfo.Size.ToString());
            repoInfoList.Add(repoInfo.Name);
            repoInfoList.Add(repoInfo.Language);

            foreach (var item in repoInfoList)
            {
                Console.WriteLine($"repo info: {item}");
            }

            return repoInfoList;
        }

        public static async Task<IList<string>> ConnectGitHubRepoRelease(GitHubClient myRepo, string accountOwner, string workingRepo)
        {
            var latestAsset = await myRepo.Repository.Release.GetLatest(accountOwner, workingRepo);

            IList<string> latestAssetInfo = new List<string>();
            latestAssetInfo.Add(latestAsset.TagName);
            latestAssetInfo.Add(latestAsset.TargetCommitish);
            latestAssetInfo.Add(latestAsset.Name);
            latestAssetInfo.Add(latestAsset.Body);
            latestAssetInfo.Add(latestAsset.CreatedAt.ToString());
            latestAssetInfo.Add(latestAsset.Author.ToString());
            latestAssetInfo.Add(latestAsset.Draft.ToString());

            foreach (var item in latestAssetInfo)
            {
                Console.WriteLine($"latestasset info: {item}");
            }

            Console.WriteLine("printing the body again");
            Console.WriteLine(latestAsset.Body);


            string text = latestAsset.Body;

            // text to array of words
            string[] words = text.Split('(').ToArray();

            // printing
            Console.WriteLine("Output:");
            string str1 = "";
            foreach (string str in words)
            {
                if (IsUrl(str))
                {
                    str1 = str.Remove(str.Length - 1);
                    Console.WriteLine(str1);
                }
            }

            using (WebClient wc1 = new WebClient())
            {

                wc1.DownloadFile(firstAsset.BrowserDownloadUrl, sb.ToString());

            }

            Console.WriteLine();


            var firstAsset = latestAsset.Assets[0];

            // create a list of info in the account and then iterate through it
            IList<string> firstAssetInfo = new List<string>();
            firstAssetInfo.Add(firstAsset.BrowserDownloadUrl);
            firstAssetInfo.Add(firstAsset.ContentType);
            firstAssetInfo.Add(firstAsset.CreatedAt.ToString());
            firstAssetInfo.Add(firstAsset.DownloadCount.ToString());
            firstAssetInfo.Add(firstAsset.Id.ToString());
            firstAssetInfo.Add($"this is the label {firstAsset.Label}");
            firstAssetInfo.Add(firstAsset.Name);
            firstAssetInfo.Add(firstAsset.NodeId);
            firstAssetInfo.Add(firstAsset.Size.ToString());
            firstAssetInfo.Add(firstAsset.UpdatedAt.ToString());
            firstAssetInfo.Add(firstAsset.Uploader.ToString());
            firstAssetInfo.Add(firstAsset.Url);

            foreach (var item in firstAssetInfo)
            {
                Console.WriteLine($"asset info: {item}");
            }
            Console.WriteLine("whattt");
            string assetFilePath = @"c:\temp\";
            string assetFileName = firstAsset.Name;
            StringBuilder sb = new StringBuilder();
            sb.Append(assetFilePath);
            sb.Append(assetFileName);

            Console.WriteLine(sb);
            Console.WriteLine("trying the web client");

            using (WebClient wc = new WebClient())
            {
                wc.DownloadProgressChanged += wc_DownloadProgressChanged;
                //wc.DownloadFileCompleted += wc_DownloadFileCompleted;
                wc.DownloadFile(firstAsset.BrowserDownloadUrl, sb.ToString());

            }

            return firstAssetInfo;
        }

        private static bool IsUrl(string url)
        {
            string pattern = @"((https|ftp|file)\://|www.)[A-Za-z0-9\.\-]+(/[A-Za-z0-9\?\&\=;\+!'\(\)\*\-\._~%]*)*";
            Regex reg = new Regex(pattern, RegexOptions.Compiled | RegexOptions.IgnoreCase);
            return reg.IsMatch(url);
        }

        private static void wc_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {

            Console.WriteLine("trying thr event handler");
            Console.WriteLine(e.ProgressPercentage);

        }

    }
}

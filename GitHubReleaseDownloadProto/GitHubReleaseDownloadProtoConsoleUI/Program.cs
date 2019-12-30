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

namespace GitHubReleaseDownloadProtoConsoleUI
{
    class Program
    {
        public static void Main(string[] args)
        {
            GetGitHubContent3();

            Console.ReadLine();
        }

        public static async Task GetGitHubContent3()
        {
            var tokenAuth = new Credentials("ee7ff072da7d1a3bb3ff54288731f4ceaa3f9517"); // Olaaf's personal token
            var client = new GitHubClient(new ProductHeaderValue("my-cool-app"));
            client.Credentials = tokenAuth;

            var user = await client.User.Get("olaafrossi");

            string userName = user.Name;
            int publicRepoCount = user.PublicRepos;
            int privateRepoCount = user.OwnedPrivateRepos;
            string userURL = user.Url;

            Console.WriteLine($"{userName} has {publicRepoCount} public repos at {userURL}");
            Console.WriteLine($"{userName} has {privateRepoCount} private repos");

            var request = client.Repository.Release.GetAll("olaafrossi", "or.github-release-proto");
            var releases = await request;
            var latest = releases[0];


            var latestAsset = await client.Repository.Release.GetLatest("olaafrossi", "or.github-release-proto");
            Console.WriteLine("writing out the asset ID");
            Console.WriteLine(latestAsset.Id);
            Console.WriteLine("writing out the asset url");
            Console.WriteLine(latestAsset.AssetsUrl);
            Console.WriteLine("writing out the HTML url");
            Console.WriteLine(latestAsset.HtmlUrl);
            Console.WriteLine("writing out the url");
            Console.WriteLine(latestAsset.Url);

            //var asset = latestAsset.First(n => n.Name == assetname);

            if (latestAsset.Url.Contains("browser_download_url") == true)
            {
                Console.WriteLine("ddd");
            }
            else
            {
                Console.WriteLine("does not containt");
            }

            List<string> myList = new List<string>();
            //var s = JsonConvert.ToString(@"a\b");
            //Console.WriteLine(s);

            foreach (var item in latestAsset.Url)
            {
                myList.Add(item.ToString());
                Console.WriteLine(item);
            }

            

            


            var myAsset = await client.Repository.Release.GetAsset("olaafrossi", "or.github-release-proto", 17044496);
            var myAsset2 = await client.Repository.Release.GetAsset("olaafrossi", "or.github-release-proto", 17044496);
            Console.WriteLine(myAsset.DownloadCount.ToString());
            Console.WriteLine(myAsset);
            WebClient wc = new WebClient();
            Console.WriteLine("new webclinet, will try to download assetURL");
            wc.DownloadFile(new Uri(myAsset.BrowserDownloadUrl), @"c:\temp\somefile");


            //ReleaseAsset myRelease = new ReleaseAsset();
            //Asset myAsset = new Asset();

            //ReleaseAsset myRelease = new ReleaseAsset();
            //myRelease = latestAsset.Url
            //var ReleaseAsset = Newtonsoft.Json.JsonConvert.DeserializeObject<ReleaseAsset>(latestAsset.AssetsUrl);
            Console.WriteLine("yeah right");
            //Console.WriteLine(ReleaseAsset.
        

            


            //OK, goddammit. Read this JSON into a class and pick out the download URL
            //Console.WriteLine("i'm dumping the asseturl into the class");

            
           // myAsset = Newtonsoft.Json.JsonConvert.DeserializeObject<Asset>(latestAsset.AssetsUrl);

            //Console.WriteLine("i'm writing the download url");
            //Console.WriteLine(myAsset.BrowserDownloadUrl);

            //Asset myAsset = new Asset();
            

            var releaseAsset = await client.Repository.Release.GetAsset("olaafrossi", "or.github-release-proto", 17044496);
            Console.WriteLine("writing out the asset it");

            Console.WriteLine(releaseAsset.Id);
            var response = await client.Repository.Release.GetAsset("olaafrossi", "or.github-release-proto", 17044496);

            //WebClient wc = new WebClient();
            //Console.WriteLine("new webclinet, will try to download assetURL");
            //wc.DownloadFile(new Uri(response.BrowserDownloadUrl), @"c:\temp\somefile");
        }

        public static async Task GetGitHubContent4()
        {

            var tokenAuth = new Credentials("ee7ff072da7d1a3bb3ff54288731f4ceaa3f9517"); // Olaaf's personal token
            var client = new GitHubClient(new ProductHeaderValue("my-cool-app"));
            client.Credentials = tokenAuth;

            var user = await client.User.Get("olaafrossi");

            string userName = user.Name;
            int publicRepoCount = user.PublicRepos;
            int privateRepoCount = user.OwnedPrivateRepos;
            string userURL = user.Url;
            Console.WriteLine($"{userName} has {publicRepoCount} public repos at {userURL}");
            Console.WriteLine($"{userName} has {privateRepoCount} private repos");

            var request = client.Repository.Release.GetAll("olaafrossi", "or.github-release-proto");
            var releases = await request;
            var latest = releases[0];
            var latestAsset = await client.Repository.Release.GetAllAssets("olaafrossi", "or.github-release-proto", latest.Id);
            Console.WriteLine("write the latest asset");
            Console.WriteLine(latestAsset.ToString());

            //Download Release.zip here
            //var response = await client.Connection.Get<object>(new Uri(latestAsset[0].Url), new Dictionary<string, string>(), "application/octet-stream");
            //var response = await client.Repository.Release.GetAsset(new Uri(latest), new Dictionary<string, string>(), "application/octet-stream");
            //byte[] bytes = Encoding.ASCII.GetBytes(response.HttpResponse.Body.ToString());
            //File.WriteAllBytes(@"c:\temp\Release.zip", bytes);
        }

        public static async Task GetGitHubContent2()
        {
            var tokenAuth = new Credentials("ee7ff072da7d1a3bb3ff54288731f4ceaa3f9517"); // Olaaf's personal token
            var client = new GitHubClient(new ProductHeaderValue("my-cool-app"));
            client.Credentials = tokenAuth;

            var user = await client.User.Get("olaafrossi");

            string userName = user.Name;
            int publicRepoCount = user.PublicRepos;
            int privateRepoCount = user.OwnedPrivateRepos;
            string userURL = user.Url;
            Console.WriteLine($"{userName} has {publicRepoCount} public repos at {userURL}");
            Console.WriteLine($"{userName} has {privateRepoCount} private repos");

            var request = client.Repository.Release.GetLatest("olaafrossi", "or.github-release-proto");
            var releases = await request;
            var latest = releases;
            var assets = await client.Repository.Release.GetAllAssets("olaafrossi", "or.github-release-proto", latest.Id);
            //var myAsset_zipFile = assets;
            Console.WriteLine("write assets to string");
            Console.WriteLine(assets.ToString());

            WebClient wc = new WebClient();
            Console.WriteLine("new webclinet, will try to download assetURL");
            wc.DownloadFile(new Uri(assets.ToString()), @"c:\temp\somefile.zip");


            //var resp = await client.Connection.Get<byte[]>(new Uri(myAsset_zipFile.Url),
            //          new Dictionary<string, string>(),
            //          null); //NullReference Here

            //var data = resp.Body;
            //var responseData = resp.HttpResponse.Body;
            //System.IO.File.WriteAllBytes(@"c:\temp\myZip.zip", (byte[])responseData);
        }








        public static async Task GetGitHubContent()
        {
            var tokenAuth = new Credentials("ee7ff072da7d1a3bb3ff54288731f4ceaa3f9517"); // Olaaf's personal token
            var client = new GitHubClient(new ProductHeaderValue("my-cool-app"));
            client.Credentials = tokenAuth;

            var user = await client.User.Get("olaafrossi");

            string userName = user.Name;
            int publicRepoCount = user.PublicRepos;
            int privateRepoCount = user.OwnedPrivateRepos;
            string userURL = user.Url;

            Console.WriteLine($"{userName} has {publicRepoCount} public repos at {userURL}");
            Console.WriteLine($"{userName} has {privateRepoCount} private repos");

            var latestAsset = await client.Repository.Release.GetLatest("olaafrossi", "or.github-release-proto");
            Console.WriteLine(latestAsset.Body);
            Console.WriteLine(latestAsset.ZipballUrl.ToString());
            Console.WriteLine("write the latest ID");
            Console.WriteLine(latestAsset.Id);
            
            Console.WriteLine(latestAsset.NodeId);
            Console.WriteLine("writing the tag name");
            Console.WriteLine(latestAsset.TagName);
            Console.WriteLine("writing the assets URL name");
            Console.WriteLine(latestAsset.AssetsUrl);

            Console.WriteLine("i will now try to get an asset by it's tag string");
            var myAsset = await client.Repository.Release.Get("olaafrossi", "or.github-release-proto", "1.0.0");

            //WebClient wc = new WebClient();
            Console.WriteLine("print my asset url");

            Console.WriteLine(myAsset.AssetsUrl);
            Console.WriteLine("print my html url");
            Console.WriteLine(myAsset.HtmlUrl);

            var releaseAsset = new ReleaseAsset();


            //releaseAsset = latestAsset;

            Console.WriteLine("print the download url");
            Console.WriteLine(releaseAsset.BrowserDownloadUrl);
            
            
            
            //Console.WriteLine(myAsset.browser_download_url);

            //wc.DownloadFile(new Uri(myAsset.Url), @"c:\temp\somefile.zip");

            //var allAsset = await client.Repository.Release.GetAsset("olaafrossi", "or.github-release-proto", 17044496);



            //var assetID = await client.Repository.Release.Get("olaafrossi", "or.github-release-proto", 17044496);
            //Console.WriteLine("get asset 1");

            //var assetURL = await client.Repository.Release.GetLatest("olaafrossi", "or.github-release-proto");

            //Console.WriteLine("get latest- print url");

            //WebClient wc = new WebClient();
            //Console.WriteLine("new webclinet, will try to download assetURL");
            //wc.DownloadFile(new Uri(assetURL.Url.ToString()), @"c:\temp\somefile.zip");

            //Console.WriteLine(assetURL.ToString());

            //Console.WriteLine("um, download uRL?");
            //Console.WriteLine(allAsset.BrowserDownloadUrl);




            //Console.WriteLine("will try looping through the Assets");
            //foreach (var item in latestAsset.Assets)
            //{
            //    Console.WriteLine(item.ToString());
            //}
            //Console.ReadLine();



            //List<> assetProps = latestAsset.Assets;


            //var happy4 = await client.Repository.Release.GetAsset("olaafrossi", "or.github-release-proto", 1)

            //ReleaseAsset happy5 = new ReleaseAsset();


            //Console.WriteLine("abuot to download");

            //try
            //{
            //    Console.WriteLine("abuot to download in try");
            //    Console.WriteLine(happy1.AssetsUrl);

            //    Asset thisAsset = new Asset();
            //    thisAsset.url = happy1.AssetsUrl;



            //    Console.WriteLine("ABout to write the URL");
            //    Console.WriteLine(thisAsset.url);

            //    WebClient wc = new WebClient();
            //    Console.WriteLine("new webclinet, will try happy5");
            //    wc.DownloadFile(new Uri(happy5.BrowserDownloadUrl), @"c:\temp\somefile.zip");
            //    //wc.DownloadFile(new Uri(happy1.AssetsUrl), @"c:\temp\somefile.zip");


            //    Console.WriteLine("tried to download");
            //    //https://api.github.com/repos/olaafrossi/or.github-release-proto/releases/22502372
            //}
            //catch (Exception)
            //{

            //    throw;
            //}



            //Release happy3 = await client.Repository.Release.GetLatest("olaafrossi", "or.github-release-proto");
            //Console.WriteLine("about to run happy3");
            //GetFile(happy3);
        }

        public static async Task GetFile(Release input)
        {
            Console.WriteLine("running the web client");
            Console.WriteLine(input.Body);
            Console.WriteLine(input.Name);
            Console.WriteLine(input.Url);

        // almost there
        //https://developer.github.com/v3/repos/releases/#get-the-latest-release

            WebClient wc = new WebClient();
            //wc.DownloadFile(input.Url, @"c:\temp\somefile.zip");
            wc.DownloadFile(input.Name, @"c:\temp\somefile");
            //wc.DownloadData(input.HtmlUrl, @"c:\temp\somefile.zip");
            Console.WriteLine("this task ran, maybe");


            WebClient client = new WebClient();

            client.DownloadDataCompleted += new DownloadDataCompletedEventHandler(client_DownloadDataCompleted);

            client.DownloadFile(input.Url, @"C\temp\newfile.zip");

            void client_DownloadDataCompleted(object sender, DownloadDataCompletedEventArgs e)
            {
                // MessageBox.Show("File downloaded");
            }

            //HttpClient httpClient = new HttpClient();
            //await httpClient.GetAsync(input.Url);

            //using (HttpClient client = new HttpClient())
            //{
            //    using (HttpResponseMessage response = await client.GetAsync(input.Url))
            //    using (Stream streamToReadFrom = await response.Content.ReadAsStreamAsync())
            //    {
            //    }
            //}


        }
        public void DownloadAsset(string assetname)
        {
            // asset.Url is some api wizardry that we'll maybe use later
            //var assets = await _releaseClient.GetAssets(RepositoryOwner, RepostoryName, LatestRelease.Id);
            //var asset = assets.First(n => n.Name == assetname);

            // for now, do some ugly shit
            //const string template = "https://github.com/{0}/{1}/releases/download/{2}/{3}";
            //var url = string.Format(template, RepositoryOwner, RepostoryName, LatestRelease.TagName, assetname);

            //System.Diagnostics.Process.Start(url);
        }
    }
}

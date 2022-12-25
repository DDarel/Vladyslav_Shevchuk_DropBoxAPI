using Dropbox.Api;
using Dropbox.Api.FileRequests;
using Dropbox.Api.Files;
using System.Data;

namespace Homework.StepDefinitions
{
    [Binding]
    public sealed class HomeWorkStepDefinitions
    {
        static string token = "sl.BVkQq_hrLoyy_BZBEb34yXmgRVXJkNWx_zYGHhkJqWZh0toWu6YmDXhyfoK50TKSXioBMDL-jYo9i6aKFD4f9HXfeJ7pyBpwXB5-qnL4dwlqSY3Vaq-RWgekfWZh80hjncXkf7CoAwam";
        DropboxClient dbx = new DropboxClient(token);
        string url = "";
        string filePath = @"D:\VisualStudio\Visual Projects\WebAPI\HomeWork\FileToUpload.txt";
        string filename = "UploadedFile.txt";
        string folder = "";

        [Given(@"Output User info")]
        public void GivenOutputUserInfo()
        {
            var user = dbx.Users.GetCurrentAccountAsync().Result;
            Console.WriteLine("UserName: " + user.Name);
            Console.WriteLine("Email: " + user.Email);
            Console.WriteLine("Country: " + user.Country);
        }

        [When(@"Upload file")]
        public void WhenUploadFile()
        {
            var mem = new MemoryStream(File.ReadAllBytes(filePath));
            var updated = dbx.Files.UploadAsync(folder + "/" + filename, WriteMode.Overwrite.Instance, body: mem);
            updated.Wait();
            var tx = dbx.Sharing.CreateSharedLinkWithSettingsAsync(folder + "/" + filename);
            tx.Wait();
            url = tx.Result.Url;
        }

        [Then(@"Show succes message")]
        public void ThenShowSuccesMessage()
        {
            Console.Write("You succesfully upload a file!\nLink: " + url + "\n");
        }

        Metadata file = new Metadata();
        [Given(@"CheckFileAvailable")]
        public void GivenCheckFileAvailable()
        {
            WhenUploadFile();
            bool check = false;
            var list = dbx.Files.ListFolderAsync(string.Empty);
            foreach (var item in list.Result.Entries.Where(i => i.IsFile))
            {
                if (item.Name == filename) {
                    check = true;
                    break;
                }
            }
            if (!check) {
                throw new FileNotFoundException("File is not available");
            }
        }

        [When(@"GetMetaData")]
        public void WhenGetMetaData()
        {
            var list = dbx.Files.ListFolderAsync(string.Empty);
            foreach (var item in list.Result.Entries.Where(i => i.IsFile))
            {
                if (item.Name == filename)
                {
                    file = item;
                    break;
                }
            }
        }

        [Then(@"ShowMetaData")]
        public void ThenShowMetaData()
        {
            Console.WriteLine("Name: " + file.Name);
            Console.WriteLine("Path: " + file.PathDisplay);
            Console.WriteLine("ID: " + file.AsFile.Id);
            Console.WriteLine("Modified: " + file.AsFile.ClientModified);
        }

        [When(@"DeleteFile")]
        public void WhenDeleteFile()
        {
            var list = dbx.Files.ListFolderAsync(string.Empty);
            foreach (var item in list.Result.Entries.Where(i => i.IsFile))
            {
                if (item.Name == filename)
                {
                    file = item;
                    break;
                }
            }
            var folders = dbx.Files.DeleteV2Async(file.PathLower);
            var result = folders.Result;
        }

        [Then(@"Show delete succes message")]
        public void ThenShowDeleteSuccesMessage()
        {
            Console.Write("You succesfully delete a file!\n");
        }
    }
}
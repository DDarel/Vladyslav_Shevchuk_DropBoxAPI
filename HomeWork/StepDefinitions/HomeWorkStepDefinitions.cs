using Dropbox.Api;
using Dropbox.Api.FileRequests;
using Dropbox.Api.Files;
using HomeWork.StepDefinitions;
using System.Data;

namespace Homework.StepDefinitions
{
    [Binding]
    public sealed class HomeWorkStepDefinitions
    {

        static string token = "sl.BVkQq_hrLoyy_BZBEb34yXmgRVXJkNWx_zYGHhkJqWZh0toWu6YmDXhyfoK50TKSXioBMDL-jYo9i6aKFD4f9HXfeJ7pyBpwXB5-qnL4dwlqSY3Vaq-RWgekfWZh80hjncXkf7CoAwam";
        string filename = "UploadedFile.txt";
        DropBoxUtility client = new DropBoxUtility(new DropboxClient(token));


        [Given(@"Output User info")]
        public void GivenOutputUserInfo()
        {
            var user = client.GetUserInfo();
            Console.WriteLine("UserName: " + user.Name);
            Console.WriteLine("Email: " + user.Email);
            Console.WriteLine("Country: " + user.Country);
        }

        [When(@"Upload file")]
        public void WhenUploadFile()
        {
            client.UploadFile(filename);
        }

        Metadata file = new Metadata();

        [When(@"GetMetaData")]
        public void WhenGetMetaData()
        {
            file = client.GetMetaData(filename);
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
            client.DeleteFile(filename);
        }

        [Then(@"Check file is uploaded")]
        public void ThenCheckFileIsUploaded()
        {
            if (!client.ChechFileIsExist(filename)) {
                throw new Exception("File does not uploaded");
            }
        }

        [Given(@"Upload file")]
        public void GivenUploadFile()
        {
            client.UploadFile(filename);
        }

        [Then(@"Check file is deleted")]
        public void ThenCheckFileIsDeleted()
        {
            if (client.ChechFileIsExist(filename))
            {
                throw new Exception("File does not deleted");
            }
        }
    }
}
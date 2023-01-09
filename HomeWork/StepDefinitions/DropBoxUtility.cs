using Dropbox.Api;
using Dropbox.Api.Files;
using Dropbox.Api.Users;

namespace HomeWork.StepDefinitions
{
    internal class DropBoxUtility
    {
        DropboxClient dbx;

        string filePath = @"D:\VisualStudio\Visual Projects\WebAPI\HomeWork\FileToUpload.txt";
        
        public static string url = "";

        public DropBoxUtility(DropboxClient dbx) { 
            this.dbx = dbx;
        }
        public void UploadFile(string filename) {
            var mem = new MemoryStream(File.ReadAllBytes(filePath));
            var updated = dbx.Files.UploadAsync("/" + filename, WriteMode.Overwrite.Instance, body: mem);
            updated.Wait();
            var tx = dbx.Sharing.CreateSharedLinkWithSettingsAsync("/" + filename);
            tx.Wait();
            url = tx.Result.Url;
        }

        public void DeleteFile(string filename) {
            var list = dbx.Files.ListFolderAsync(string.Empty);
            Metadata file = new Metadata();
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

        public Metadata GetMetaData(string filename) {
            var list = dbx.Files.ListFolderAsync(string.Empty);
            Metadata file = new Metadata();
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
            return file;
        }

        public FullAccount GetUserInfo() {
            var user = dbx.Users.GetCurrentAccountAsync().Result;
            return user;
        }

        public bool ChechFileIsExist(string filename) {
            bool check = false;
            var list = dbx.Files.ListFolderAsync(string.Empty);
            foreach (var item in list.Result.Entries.Where(i => i.IsFile))
            {
                if (item.Name == filename)
                {
                    check = true;
                    break;
                }
            }
            return check;
        }
    }
}

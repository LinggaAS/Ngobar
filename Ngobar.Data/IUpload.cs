using Microsoft.WindowsAzure.Storage.Blob;

namespace Ngobar.Data
{
    public interface IUpload
    {
        CloudBlobContainer GetBlobContainer(string connectionString);
    }
}

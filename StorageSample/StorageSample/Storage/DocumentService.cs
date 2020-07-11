using Microsoft.Azure.Storage;
using Microsoft.Azure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace StorageSample.Storage
{
    public class DocumentService
    {
        public async Task<string> UploadImageAsync(HttpPostedFileBase docToUpload)
        {
            string docFullPath = null;
            if (docToUpload == null || docToUpload.ContentLength == 0)
            {
                return null;
            }
            try
            {
                CloudStorageAccount csa = ConnectionString.GetConnectionString();
                CloudBlobClient cbClient = csa.CreateCloudBlobClient();
                CloudBlobContainer cbContainer = cbClient.GetContainerReference("ContainerName");

                if (await cbContainer.CreateIfNotExistsAsync())
                {
                    await cbContainer.SetPermissionsAsync(
                        new BlobContainerPermissions
                        {
                            PublicAccess = BlobContainerPublicAccessType.Blob
                        });
                }
                string imageName = Guid.NewGuid().ToString() + "_" + docToUpload.FileName;

                CloudBlockBlob cloudBlockBlob = cbContainer.GetBlockBlobReference(imageName);
                cloudBlockBlob.Properties.ContentType = docToUpload.ContentType;
                await cloudBlockBlob.UploadFromStreamAsync(docToUpload.InputStream);

                docFullPath = cloudBlockBlob.Uri.ToString();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

            return docFullPath;
        }
    }
}
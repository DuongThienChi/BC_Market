using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Supabase;
using Supabase.Interfaces;
using Supabase.Storage;

namespace BC_Market.Helper
{
    /// <summary>
    /// Provides functionality to upload images to Supabase storage.
    /// </summary>
    public static class UploadImage
    {
        /// <summary>
        /// Uploads an image to Supabase storage and returns the public URL of the uploaded image.
        /// </summary>
        /// <param name="path">The local file path of the image to upload.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the public URL of the uploaded image.</returns>
        public static async Task<string> UploadImagePath(string path)
        {
            // Initialize the Supabase client
            var client = SupabaseHelper.GetClient();
            if (client != null)
            {
                string bucketName = "images";
                string fileName = Path.GetFileName(path);
                byte[] fileContent = File.ReadAllBytes(path);
                string uniqueFileName = $"{DateTimeOffset.UtcNow.ToUnixTimeMilliseconds()}_{fileName}";

                var response = client.Storage
                    .From(bucketName)
                    .Upload(fileContent, uniqueFileName);

                var url = client.Storage
                    .From(bucketName)
                    .GetPublicUrl(uniqueFileName);

                return url;
            }
            return path;
        }
    }
}

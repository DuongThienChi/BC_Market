using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Supabase;
using Supabase.Interfaces;
using Supabase.Storage;

namespace BC_Market.Helper
{
    public static class UploadImage
    {
        
        public static async Task<string> UploadImagePath(string path)
        {
            // Initialize the Supabase client
            var client = SupabaseHelper.GetClient();
            if(client!=null)
            {
                string bucketName = "images";
                string fileName = Path.GetFileName(path);
                byte[] fileContent = await File.ReadAllBytesAsync(path);
                string uniqueFileName = $"{DateTimeOffset.UtcNow.ToUnixTimeMilliseconds()}_{fileName}";

                var response = await client.Storage
                    .From(bucketName)
                    .Upload(fileContent, uniqueFileName);


                return response;
            }
            return path;
        }
    }
}

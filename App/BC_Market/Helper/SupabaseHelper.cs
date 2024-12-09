using Supabase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Supabase.Storage;

namespace BC_Market.Helper
{
    public static class SupabaseHelper
    {
        private static Supabase.Client client;

        public static async Task InitializeAsync()
        {
            var options = new SupabaseOptions { AutoConnectRealtime = true };

            Environment.SetEnvironmentVariable("SUPABASE_URL", "https://xlxwqmipwlkgvqfbihhi.supabase.co");
            Environment.SetEnvironmentVariable("SUPABASE_KEY", "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJzdXBhYmFzZSIsInJlZiI6InhseHdxbWlwd2xrZ3ZxZmJpaGhpIiwicm9sZSI6InNlcnZpY2Vfcm9sZSIsImlhdCI6MTczMjA2Nzc0MSwiZXhwIjoyMDQ3NjQzNzQxfQ.Pu_I2fb5ZFDcDwxDP-9nFc6bwlISDp77DwxPNM-MvQw");

            var url = Environment.GetEnvironmentVariable("SUPABASE_URL");
            var key = Environment.GetEnvironmentVariable("SUPABASE_KEY");

            // Initialize the Supabase client with your API URL and Key
            client = new Supabase.Client(
                url,
                key, 
                options);
            await client.InitializeAsync();
        }

        public static Supabase.Client GetClient()
        {
            return client;
        }
    }
}

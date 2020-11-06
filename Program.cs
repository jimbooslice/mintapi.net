using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using IronPython.Hosting;
using Microsoft.Scripting.Hosting;
using Newtonsoft.Json;

namespace MintApiTest.NET
{
    class Program
    {
        static async Task Main(string[] args)
        {
            ProcessStartInfo start = new ProcessStartInfo();
            start.FileName = @"C:\Program Files\Python39\python.exe";
            var username = "YOUR MINT USERNAME/EMAIL HERE";
            var password = "YOUR MINT PASSWORD HERE";
            start.Arguments = @$"mintapi\api.py -t --headless {username} {password}";
            start.UseShellExecute = false;
            start.RedirectStandardOutput = true;
            var result = "test";
            using(var p = Process.Start(start)){
                using(var reader = p.StandardOutput){
                     result = await reader.ReadToEndAsync();
                   
                }
            }
            var jsonStarter = result.IndexOf("[{");
            var json = result.Substring(result.IndexOf("[{"), result.Length - jsonStarter);
            Console.WriteLine("JSON here");
            //Console.WriteLine(json);
            var obj = JsonConvert.DeserializeObject<List<MintApiResult>>(json);
        }
    }

    public class MintApiResult
    {
        [JsonProperty("date")]
        public long Date { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("original_description")]
        public string OriginalDescription { get; set; }

        [JsonProperty("amount")]
        public double Amount { get; set; }

        [JsonProperty("transaction_type")]
        public string TransactionType { get; set; }

        [JsonProperty("category")]
        public string Category { get; set; }

        [JsonProperty("account_name")]
        public string AccountName { get; set; }

        [JsonProperty("labels")]
        public object Labels { get; set; }

        [JsonProperty("notes")]
        public object Notes { get; set; }
    }
    
}

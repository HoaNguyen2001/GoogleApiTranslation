using Google.Api.Gax.ResourceNames;
using Google.Apis.Auth.OAuth2;
using Google.Cloud.Storage.V1;
using Google.Cloud.Translate.V3;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var value = Environment.GetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS");
                if (value == null)
                {
                    Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", $"{Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json")}");
                    // Now retrieve it.
                    value = Environment.GetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS");
                }
                var builder = new ConfigurationBuilder()
                   .SetBasePath(Directory.GetCurrentDirectory())
                   .AddJsonFile("appsettings.json");
                var configuration = builder.Build();
                var project_id = configuration["project_id"];
                //var credential = GoogleCredential.GetApplicationDefault();
                //var storage = StorageClient.Create(credential);
                //// Make an authenticated API request.
                //var buckets = storage.ListBuckets("ggtranslatorapi");
                //foreach (var bucket in buckets)
                //{
                //    Console.WriteLine(bucket.Name);
                //}

                Console.WriteLine("Nhập từ muốn dịch: ");
                String txt = Console.ReadLine();
                TranslationServiceClient client = TranslationServiceClient.Create();
                TranslateTextRequest request = new TranslateTextRequest
                {
                    Contents = { $"{txt}" },
                    TargetLanguageCode = "vi-VI",
                    Parent = new ProjectName($"{project_id}").ToString()
                };
                TranslateTextResponse response = client.TranslateText(request);
                // response.Translations will have one entry, because request.Contents has one entry.
                Translation translation = response.Translations[0];
                Console.WriteLine($"Detected language: {translation.DetectedLanguageCode}");
                Console.WriteLine($"Translated text: {translation.TranslatedText}");


                Console.WriteLine("Hello World!");
                Console.WriteLine($"{DateTime.Now.ToString("dd/MM/yyyy")}");
                Console.ReadKey();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadKey();
            }
        }
    }
}

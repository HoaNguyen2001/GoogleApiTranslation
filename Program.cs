using Google.Api.Gax.ResourceNames;
using Google.Apis.Auth.OAuth2;
using Google.Cloud.Storage.V1;
using Google.Cloud.Translate.V3;
using System;

namespace ConsoleApp1
{
    class Program
    {

        public object AuthImplicit(string projectId)
        {
            // If you don't specify credentials when constructing the client, the
            // client library will look for credentials in the environment.
            var credential = GoogleCredential.GetApplicationDefault();
            var storage = StorageClient.Create(credential);
            // Make an authenticated API request.
            var buckets = storage.ListBuckets(projectId);
            foreach (var bucket in buckets)
            {
                Console.WriteLine(bucket.Name);
            }
            return null;
        }

        static void Main(string[] args)
        {
            try
            {

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
                    Parent = new ProjectName("ggtranslatorapi").ToString()
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

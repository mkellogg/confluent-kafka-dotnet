using System;
using System.Linq;
using System.Text.RegularExpressions;
using Confluent.SchemaRegistry;

namespace SchemaRegistry
{
    class Program
    {
        static void Main(string[] args)
        {
            var joinedArgs = string.Join(' ', args);

            string sslCertificatePath = "";
            bool useSsl = false;
            var sslMatch = Regex.Match(joinedArgs, @"--ssl ([^\s]+)", RegexOptions.IgnoreCase);
            if (sslMatch.Success)
            {
                useSsl = true;
                sslCertificatePath = sslMatch.Groups[1].Value;
            }

            var uri = new Uri(args[args.Length - 1]);

            ISchemaRegistryClient schemaRegistryClient;

            if (useSsl)
            {
                schemaRegistryClient = new CachedSchemaRegistryClient(new SchemaRegistryConfig
                {
                    SchemaRegistryUrl = uri.ToString(),
                    SchemaRegistryClientCertificatePath = sslCertificatePath
                });
            }
            else
            {
                schemaRegistryClient = new CachedSchemaRegistryClient(new SchemaRegistryConfig
                {
                    SchemaRegistryUrl = uri.ToString(),
                });
            }

            Console.WriteLine($"Fetching schema subjects from {uri}....");
            var schemas = schemaRegistryClient.GetAllSubjectsAsync().Result;
            foreach (var schema in schemas)
            {
                Console.WriteLine(schema);
            }

            Console.ReadKey();
        }
    }
}

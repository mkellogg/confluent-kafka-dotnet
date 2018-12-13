using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace SchemaRegistry
{
    class Program
    {
        static void Main(string[] args)
        {
            var joinedArgs = string.Join(' ', args);

            string sslCertificatePath = "";
            bool useSsl = false;
            var sslMatch = Regex.Match(joinedArgs, @"--ssl ([^\s].*)", RegexOptions.IgnoreCase);
            if (sslMatch.Success)
            {
                useSsl = true;
                sslCertificatePath = sslMatch.Groups[1].Value;
            }

            ISchemaRegistryClient schemaRegistryClient;

            if (useSsl)
            {
                schemaRegistryClient = new CachedSchemaRegistryClient();
            }
            
            Console.WriteLine("Hello World!");
        }
    }
}

using Boss_RЯ.Entity_Classes;
using Newtonsoft.Json;
using System.IO;

namespace Boss_RЯ.Helper_Static_Classes
{
    public static class JsonFileHelper
    {
        public const string fileName = "database.json";        
        public static void JSONSerialization(Database database)
        {
            var serializer = new JsonSerializer();
            using (StreamWriter streamWriter = new StreamWriter(fileName))
            {
                using (JsonTextWriter jsonTextWriter = new JsonTextWriter(streamWriter))
                {
                    jsonTextWriter.Formatting = Formatting.Indented;
                    serializer.Serialize(jsonTextWriter, database);
                }
            }
        }
        public static void JSONDeSerialization(ref Database database)
        {
            var serializer = new JsonSerializer();

            using (StreamReader streamReader = new StreamReader(fileName))
            {
                using (JsonTextReader jsonTextReader = new JsonTextReader(streamReader))
                {
                    database = serializer.Deserialize<Database>(jsonTextReader);
                }
            }
        }

    }

}

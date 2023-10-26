using Newtonsoft.Json;

namespace JsonTask
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string path = Path.GetFullPath(@"..\..\..");
            List<string> list = new();
            string result = JsonConvert.SerializeObject(list);

            if (!File.Exists($@"{path}\names.json"))
            {
                File.Create($@"{path}names.json");
            }
            path = $@"{path}\names.json";

            using (StreamWriter sw = new StreamWriter(path))
            {
                sw.WriteLine(result);
            }
            Add("Adil");
            //Delete("Adil");
            //Add("Adil");
            //Add("adiL");
            //Delete("adiL");
            //Add("Leyla");
            //Console.WriteLine(Search(name => name == "x"));
            Console.WriteLine(Search(name => name == "Ad" || name.Contains("Ad")));

        }


        public static void WriteToDb(List<string> names)
        {
            string path = Path.Combine(Path.GetFullPath(@"..\..\..\"), "names.json");
            string result = JsonConvert.SerializeObject(names);
            using (StreamWriter sw = new(path))
            {
                sw.Write(result);
            }
        }


        public static List<string> ReadFromDb()
        {
            string path = Path.Combine(Path.GetFullPath(@"..\..\..\"), "names.json");
            List<string> names;
            using (StreamReader sr = new(path))
            {
                string result = sr.ReadToEnd();
                if (result == "") return new List<string>();
                try
                {
                    names = JsonConvert.DeserializeObject<List<string>>(result);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Ad yoxdur");
                    names = new List<string>();
                }
            }

            return names;
        }

        public static void Add(string name)
        {
            List<string> names = ReadFromDb();
            names.Add(name);
            WriteToDb(names);
        }

        public static bool Search(Predicate<string> predicate)
        {
            List<string> names = ReadFromDb();
            if (names.Any(predicate.Invoke))
                return true;
            else return false;
        }
        public static void Delete(string name)
        {
            List<string> names = ReadFromDb();
            if (!names.Any(b => b == name))
                throw new Exception("Bele ad yoxdur");

            names.RemoveAll(b => b == name);
            
            WriteToDb(names);
        }
    }
}
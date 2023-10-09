namespace BuinessLogic
{
    public class Database : IDatabase
    {

        StreamReader _Reader;
        StreamWriter _Writer;

        string _Path = "housemates.txt";
        public Database(string path)
        {
            _Path = path;
            
        }

        public IEnumerable<Housemate> GetHouseMates()
        {
            List<Housemate> housemates = new();
            using (_Reader = new StreamReader(_Path))
            {
                var line = _Reader.ReadLine();
                AddHousemateFromLine(housemates, line);
                while (true)
                {
                    line = _Reader.ReadLine();
                    if (line is null) break;
                    AddHousemateFromLine(housemates, line);
                }

                _Reader.Close();
            }
            return housemates;
        }

        private static void AddHousemateFromLine(List<Housemate> housemates, string line)
        {
            var items = line.Split(',');
            DateTime.TryParse(items[2], out var dob);
            housemates.Add(new Housemate(items[0], items[1], dob, items[3]));
        }

        public void Save(IEnumerable<Housemate> housemates)
        {
            using (_Writer = new StreamWriter(_Path))
            {

                foreach (var housemate in housemates)
                {
                    var dob = housemate.DOB.ToString();
                    var line = housemate.ID + "," + housemate.Name + ","+dob   + "," + housemate.Emoji;
                    _Writer.WriteLine(line);
                }
                _Writer.Close();
            }

        }
    }
}
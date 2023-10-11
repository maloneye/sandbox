namespace BuinessLogic
{
    public class Housemate
    {
        public string ID { get; set; }

        public string Name { get; set; }

        public DateTime DOB { get; set; }

        public string Emoji { get; set; }

        public Housemate(string iD, string name, DateTime dOB, string emoji)
        {
            ID = iD;
            Name = name;
            DOB = dOB;
            Emoji = emoji;
        }

        public override string ToString()
        {
            return "Housemate: " + Name;
        }
    }
}

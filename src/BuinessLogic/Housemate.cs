namespace BuinessLogic
{
    public class Housemate
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public DateTime DOB { get; set; }
        public string Emoji { get; set; }
        public int Order { get; set; }

        public const string Identifier = "1";

        public Housemate()
        {

        }

        public Housemate(int iD, string name, DateTime dOB, string emoji, int order)
        {
            ID = iD;
            Name = name;
            DOB = dOB;
            Emoji = emoji;
            Order = order;
        }

        public override string ToString()
        {
            return "Housemate: " + Name + " | " + Emoji.ToImage();
        }

    }
}

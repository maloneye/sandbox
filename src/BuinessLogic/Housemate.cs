namespace BuinessLogic
{
    public class Housemate
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public DateTime DOB { get; set; }

        public string Emoji { get; set; }

        public Housemate()
        {

        }

        public Housemate(int iD, string name, DateTime dOB, string emoji)
        {
            ID = iD;
            Name = name;
            DOB = dOB;
            Emoji = emoji;

        }

        public override string ToString()
        {
            return "Housemate: " + Name + " | " + Emoji.ToImage();
        }

    }
}

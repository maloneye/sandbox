namespace BuinessLogic
{
    internal interface IDatabase
    {
        public IEnumerable<Housemate> GetHouseMates();

        public void Save(IEnumerable<Housemate> housemates);        
    }
}

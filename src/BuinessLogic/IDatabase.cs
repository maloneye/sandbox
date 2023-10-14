namespace BuinessLogic
{
    public interface IDatabase
    {
        public IEnumerable<Housemate> GetHouseMates();
        public void Save(IEnumerable<Housemate> housemates);
    }
}

namespace BuinessLogic
{
    public interface IDatabase
    {
        public Task<IEnumerable<Housemate>> GetHousemates();
        public void Save(IEnumerable<Housemate> housemates);
        public event EventHandler<IEnumerable<Housemate>> ListUpdated; 
    }
}

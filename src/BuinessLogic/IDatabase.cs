namespace BuinessLogic
{
    public interface IDatabase
    {
        public void Initalise(IWebSettings connectionSettings);
        public Task<IEnumerable<Housemate>> GetHousemates();
        public void Save(IEnumerable<Housemate> housemates);
        public event EventHandler<IEnumerable<Housemate>> ListUpdated;
    }
}

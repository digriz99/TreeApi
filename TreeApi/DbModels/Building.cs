namespace TreeApi.DbModels
{
    public class Building : IElement
    {
        public int Id { get; set; }

        public int Pid { get; set; }

        public string Name { get; set; }
    }
}
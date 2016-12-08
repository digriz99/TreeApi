namespace TreeApi.DbModels
{
    public class Appartment : IElement
    {
        public int Id { get; set; }

        public int Pid { get; set; }

        public string Name { get; set; }
    }
}
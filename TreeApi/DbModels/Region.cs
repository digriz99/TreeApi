namespace TreeApi.DbModels
{
    public class Region : IElement
    {
        public int Id { get; set; }

        public int Pid { get; set; }

        public string Name { get; set; }
    }
}
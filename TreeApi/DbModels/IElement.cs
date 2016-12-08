namespace TreeApi.DbModels
{
    interface IElement
    {
        int Id { get; set; }

        int Pid { get; set; }

        string Name { get; set; }
    }
}
namespace WebApiRest.NetCore.Repositories.Entities.SqLite
{
    public class WebSite
    {
        public int Key { get; set; }
        public string Title { get; set; }
        public string Id { get; set; }
        public string Tags { get; set; }
        public string Url { get; set; }
        public string Author { get; set; }
        public string Description { get; set; }
        public string Group { get; set; }
        public string Place { get; set; }
        public string Location { get; set; }
        public string State { get; set; }
        public string License { get; set; }
        public string Author_Url { get; set; }
        public string Created { get; set; }
        public string LastModified { get; set; }
        public string MetaDataModified { get; set; }
    }
}
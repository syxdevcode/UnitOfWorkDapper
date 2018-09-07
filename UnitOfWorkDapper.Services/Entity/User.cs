namespace UnitOfWorkDapper.Services.Entity
{
    public class User
    {
        public string Id { get; set; }

        public string UserName { get; set; }

        public string NickName { get; set; }

        public string PassWord { get; set; }

        public string RegisterTime { get; set; }

        public int Grade { get; set; }

        public int IsDelete { get; set; }

        public string AddTime { get; set; }
    }
}
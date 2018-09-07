using System;
using System.Collections.Generic;
using System.Text;

namespace UnitOfWorkDapper.Services.Entity
{
    public class UserAddress
    {
        public string Id { get; set; }

        public string UserId { get; set; }

        public string RecName { get; set; }

        public string PhoneNumber { get; set; }

        public int IsDefault { get; set; }

        public string Province { get; set; }

        public string City { get; set; }

        public string Regin { get; set; }

        public string Street { get; set; }

        public string AddTime { get; set; }

        public int IsDelete { get; set; }
    }
}

﻿namespace ProductCatalog.Client.Models
{
    public class UserModel
    {
        public string Id { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }

        public bool LockoutEnabled { get; set; }
    }
}

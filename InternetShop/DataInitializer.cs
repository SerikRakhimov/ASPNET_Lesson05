using InternetShop.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace InternetShop
{
    public class DataInitializer : CreateDatabaseIfNotExists<Context>
    {
        protected override void Seed(Context context)
        {
            // Создание уникального индекса по наименованию в таблице Products
            context.Database.ExecuteSqlCommand("create unique index ix_name on Products(Name)");

            // Создание уникального индекса по наименованию в таблице Users
            context.Database.ExecuteSqlCommand("create unique index ix_login on Users(Login)");

            // добавление записей по умолчанию в новой базе данных
            context.Products.AddRange(new List<Product>{
                new Product
                {
                    Name = "Молоко",
                    Price = 235
                },
                new Product
                {
                    Name = "Сметана",
                    Price = 330
                },
                new Product
                {
                    Name = "Творог",
                    Price = 415
                }
            });
            context.Users.AddRange(new List<User>{
                new User
                {
                    Login = "Admin",
                    Password = "123456"
                },
                new User
                {
                    Login = "User1",
                    Password = "100001"
                },
                new User
                {
                    Login = "User2",
                    Password = "100002"
                }
            });
            context.SaveChanges();
        }
    }
}
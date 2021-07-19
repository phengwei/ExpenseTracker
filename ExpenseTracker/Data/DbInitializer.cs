using ExpenseTracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExpenseTracker.Data
{
    public static class DbInitializer
    {
        public static void Initialize(ExpenseTrackerContext context)
        {
            context.Database.EnsureCreated();

            // Look for any students.
            if (context.Category.Any())
            {
                return;   // DB has been seeded
            }

            var categories = new Category[]
            {
            new Category{Housing="2000",Transportation="300",Food="1000",Utilities="200",Insurance="300",UserID=1},
            new Category{Housing="3000",Transportation="500",Food="1200",Utilities="300",Insurance="500",UserID=2}
            };
            foreach (Category s in categories)
            {
                context.Category.Add(s);
            }
            context.SaveChanges();

            var subCategories = new SubCategory[]
            {
            new SubCategory {
                CategoryID = categories.Single(s => s.UserID == 1).CategoryID,
                PersonalSpending = "Shopping",
                Entertainment = "Movie"
            },
            new SubCategory {
                CategoryID = categories.Single(s => s.UserID == 1).CategoryID,
                PersonalSpending = "Gift",
                Entertainment = "Game"
            },
            new SubCategory {
                CategoryID = categories.Single(s => s.UserID == 1).CategoryID,
                PersonalSpending = "Car",
                Entertainment = "Movie"
            },
             new SubCategory {
                CategoryID = categories.Single(s => s.UserID == 2).CategoryID,
                PersonalSpending = "Gift",
                Entertainment = "Movie"
            },
            new SubCategory {
                CategoryID = categories.Single(s => s.UserID == 2).CategoryID,
                PersonalSpending = "Shopping",
                Entertainment = "Movie"
            },
            };
            foreach (SubCategory c in subCategories)
            {
                context.SubCategory.Add(c);
            }
            context.SaveChanges();
        }
    }
}

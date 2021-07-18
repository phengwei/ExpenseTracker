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
            new Category{Housing="Carson",Transportation="Alexander",Food="Bihun",Utilities="Tnb",Insurance="Insuran",UserID=1},
            new Category{Housing="Bunga",Transportation="Car",Food="Bihun",Utilities="Tnb",Insurance="Insuran",UserID=2}
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
                PersonalSpending = "Car",
                Entertainment = "Movie"
            },
            new SubCategory {
                CategoryID = categories.Single(s => s.UserID == 1).CategoryID,
                PersonalSpending = "Car",
                Entertainment = "Game"
            },
            new SubCategory {
                CategoryID = categories.Single(s => s.UserID == 1).CategoryID,
                PersonalSpending = "Car",
                Entertainment = "Movie"
            },
             new SubCategory {
                CategoryID = categories.Single(s => s.UserID == 2).CategoryID,
                PersonalSpending = "Car",
                Entertainment = "Movie"
            },
            new SubCategory {
                CategoryID = categories.Single(s => s.UserID == 2).CategoryID,
                PersonalSpending = "Car",
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

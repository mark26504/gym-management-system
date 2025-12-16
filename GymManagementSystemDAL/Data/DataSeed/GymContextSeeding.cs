using GymManagementSystemDAL.Data.Context;
using GymManagementSystemDAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace GymManagementSystemDAL.Data.DataSeed
{
    public class GymContextSeeding
    {
        public static bool SeedDate(GymManagementSystemDbContext dbContext)
        {
			try
			{
                var HasPlans = dbContext.Plans.Any();
                var HasCategories = dbContext.Categories.Any();

                //if (HasPlans && HasCategories) return false;

                if (!HasPlans)
                {
                    var Plans = LoadDataFromJsonFile<Plan>("plans.json");
                    
                    if (Plans.Any())
                    {
                        dbContext.Plans.AddRange(Plans);
                    }
                }
                if (!HasCategories)
                {
                    var Categories = LoadDataFromJsonFile<Category>("categories.json");
                    if (Categories.Any())
                    {
                        dbContext.Categories.AddRange(Categories);
                    }
                }
                return dbContext.SaveChanges() > 0;
            }
			catch (Exception ex)
			{
                Console.WriteLine(ex);  // أو استخدم logger لو عندك
                throw;                  // خليه يفجّر الـ Exception عشان تشوفه
            }
        }

        private static List<T> LoadDataFromJsonFile<T>(string FileName)
        {
            var FilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Files", FileName);

            if (!File.Exists(FilePath))
                throw new FileNotFoundException("File Not Found", FilePath);

            string JsonData = File.ReadAllText(FilePath);
            var options = new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = true,
            };

            return JsonSerializer.Deserialize<List<T>>(JsonData, options) ?? new List<T>();
        }
    }
}

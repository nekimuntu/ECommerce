using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Core.Entities;
using Microsoft.Extensions.Logging;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.SeedData
{
    public class StoreContextSeed
    {

        public static async Task SeedAsync(StoreContext context, ILoggerFactory loggerFactory)
        {
            // string pathBrandLog = "C:\\Users\\ynaga\\OneDrive\\Documents\\BACK to PRORGAMMING" +
            //         "\\C#\\ECommerce\\skinet\\Infrastructure\\Data\\SeedData\\brandsLog.txt";
            // string pathTypeLog = "C:\\Users\\ynaga\\OneDrive\\Documents\\BACK to PRORGAMMING" +
            //         "\\C#\\ECommerce\\skinet\\Infrastructure\\Data\\SeedData\\typesLog.txt";

            string pathBrandFile = "C:\\Users\\ynaga\\OneDrive\\Documents" +
                    "\\BACK to PRORGAMMING\\C#\\ECommerce\\skinet\\Infrastructure" +
                    "\\Data\\SeedData\\brands.json";
            string pathTypeFile = "C:\\Users\\ynaga\\OneDrive\\Documents" +
                    "\\BACK to PRORGAMMING\\C#\\ECommerce\\skinet\\Infrastructure" +
                    "\\Data\\SeedData\\types.json";
            string pathProductFile = "C:\\Users\\ynaga\\OneDrive\\Documents" +
                    "\\BACK to PRORGAMMING\\C#\\ECommerce\\skinet\\Infrastructure" +
                    "\\Data\\SeedData\\products.json";
            // File.WriteAllText(pathBrandFile,"test");
            try
            {
                //Seed the brands in Db
                if (!context.Types.Any())
                {
                    var seedBrand = File.ReadAllText(pathBrandFile);
                    var jsonBrands = JsonSerializer.Deserialize<List<ProductBrand>>(seedBrand);

                    foreach (var item in jsonBrands)
                    {

                        context.Brands.Add(item);
                        // File.AppendAllText(pathBrandLog,item.Id+" iteration "+ item);                        
                    }
                    await context.SaveChangesAsync();
                }
                //Seed the types in Db
                if (!context.Types.Any())
                {
                    var seedType = File.ReadAllText(pathTypeFile);
                    var jsonTypes = JsonSerializer.Deserialize<List<ProductType>>(seedType);
                    
                    foreach (var item in jsonTypes)
                    {
                        context.Types.Add(item);
                        // File.AppendAllText(pathTypeLog, jsonTypes.ToString() + " iteration n " + i);
                    }
                    await context.SaveChangesAsync();
                }
                //Seed the products in Db if there is no datas 
                if (!context.Products.Any())
                {
                    var seedProduct = File.ReadAllText(pathProductFile);
                    var jsonProducts = JsonSerializer.Deserialize<List<Product>>(seedProduct);

                    foreach (var item in jsonProducts)
                    {
                        context.Products.Add(item);
                    }
                    await context.SaveChangesAsync();
                }
            }
            catch (System.Exception ex)
            {
                var logger = loggerFactory.CreateLogger<StoreContextSeed>();
                logger.LogError(ex.Message);
            }
        }

    }
}
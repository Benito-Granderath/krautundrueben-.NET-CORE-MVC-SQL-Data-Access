using krautundrueben.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Dapper;
using System.Data;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System;
using Microsoft.AspNetCore.Authorization;
using System.Reflection;
using static Org.BouncyCastle.Math.EC.ECCurve;

namespace krautundrueben.Controllers
{
    public class HomeController : Controller
    {
        private readonly IDbConnection _dbConnection;
        private readonly SQLQueries_Model _sqlQueries = new SQLQueries_Model();
        public HomeController(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;

        }

        public IActionResult index()
        {
            string _deliveryCount = "SELECT COUNT(*) FROM LIEFERANT";
            string _orderCount = "SELECT COUNT(*) FROM BESTELLUNG";
            string _customerCount = "SELECT COUNT(*) FROM KUNDE";


            using (var dbConnection = _dbConnection)
            {
                ViewModel model = new ViewModel
                {
                    GroupByIngredientQuery = _dbConnection.Query<DBData_Model>(_sqlQueries.GroupByIngredientQuery),
                    GroupByDeliveryQuery = _dbConnection.Query<DBData_Model>(_sqlQueries.GroupByDeliveryQuery),
                    GroupByCustomerQuery = _dbConnection.Query<DBData_Model>(_sqlQueries.GroupByCustomerQuery),
                    GroupByOrderQuery = _dbConnection.Query<DBData_Model>(_sqlQueries.GroupByOrderQuery),
                    DisplayAllQuery = _dbConnection.Query<DBData_Model>(_sqlQueries.DisplayAllQuery),
                    AnalyzeOrderTrend = _dbConnection.Query<DBData_Model>(_sqlQueries.AnalyzeOrderTrend).ToList(),
                    IngredientAnalysis = _dbConnection.Query<DBData_Model>(_sqlQueries.IngredientAnalysis).ToList(),
                    MostActiveSuppliers = _dbConnection.Query<DBData_Model>(_sqlQueries.MostActiveSuppliers).ToList(),
                    SalesPerDate = _dbConnection.Query<DBData_Model>(_sqlQueries.SalesPerDate).ToList(),
                };


                var DeliveryCount = _dbConnection.ExecuteScalar<int>(_deliveryCount);
                var OrderCount = _dbConnection.ExecuteScalar<int>(_orderCount);
                var CustomerCount = _dbConnection.ExecuteScalar<int>(_customerCount);

                ViewBag.DeliveryCount = DeliveryCount;
                ViewBag.OrderCount = OrderCount;
                ViewBag.CustomerCount = CustomerCount;

                return View(model);

            }
        }

        public IActionResult Recipe()
        {
            using (var dbConnection = _dbConnection)
            {

                
                var selectQuery = _dbConnection.Query<Recipe_Model>("SELECT * FROM REZEPT");

                var ingredients = _dbConnection.Query<DBData_Model>("SELECT ZUTATENNR, BEZEICHNUNG FROM ZUTAT");

                
                ViewBag.Ingredients = ingredients;


                return View(selectQuery);
            }
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Recipe(Recipe_Model model)
        {
            if (ModelState.IsValid)
            {
                using (var dbConnection = _dbConnection)
                {


                    var recipeInsertQuery = "INSERT INTO REZEPT (Rezeptname, Anleitung, Vegan, `Low-Carb`, Vegetarisch, Frutarisch, `High-Protein`) VALUES (@Rezeptname, @Anleitung, @Vegan, @LowCarb, @Vegetarisch, @Frutarisch, @HighProtein); SELECT LAST_INSERT_ID();";
                    var recipeId = _dbConnection.ExecuteScalar<int>(recipeInsertQuery, model);

                    foreach (var ingredientId in model.SelectedIngredients)
                    {
                        var recipeIngredientInsertQuery = "INSERT INTO REZEPTZUTAT (REZEPTNR, ZUTATENNR) VALUES (@REZEPTNR, @ZUTATENNR);";
                        _dbConnection.Execute(recipeIngredientInsertQuery, new { REZEPTNR = recipeId, ZUTATENNR = ingredientId });
                    }
                }

                return RedirectToAction("Index");
            }

            using (var dbConnection = _dbConnection)
            {


                var selectQuery = _dbConnection.Query<Recipe_Model>("SELECT * FROM REZEPT").ToList();
                var ingredients = _dbConnection.Query("SELECT ZUTATENNR, BEZEICHNUNG FROM ZUTAT").ToList();
                ViewBag.selectQuery = selectQuery;
                ViewBag.Ingredients = ingredients;
            }

            return View(model);
        }
    

    public IActionResult DeleteRecipe(int REZEPTNR)
        {
            using (var dbConnection = _dbConnection)
            {
                dbConnection.Open();
                dbConnection.Execute(_sqlQueries.DeleteQuery2, new { RecipeId = REZEPTNR });
                dbConnection.Execute(_sqlQueries.DeleteQuery, new { RecipeId = REZEPTNR });
            }

            return RedirectToAction("Recipe");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
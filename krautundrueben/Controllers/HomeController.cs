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
            string selectQuery = "SELECT * FROM REZEPTE";
            string ingredientQuery = "SELECT ZUTATENNR, BEZEICHNUNG FROM ZUTAT ORDER BY BEZEICHNUNG";

            using (var dbConnection = _dbConnection)
            {
                var recipes = _dbConnection.Query<Recipe_Model>(selectQuery);
                var ingredients = _dbConnection.Query<DBData_Model>(ingredientQuery);

                ViewBag.Ingredients = ingredients;

                return View(recipes);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Recipe(Recipe_Model recipe)
        {
            if (ModelState.IsValid)
            {
                var selectedIngredientIds = recipe.SelectedIngredientIds.Split(',');


                var ingredients = _dbConnection.Query<string>(_sqlQueries.IngredientQuery, new { SelectedIngredientIds = selectedIngredientIds}).ToList();

                var zutaten = string.Join("; ", ingredients);

                recipe.ZUTATEN = zutaten;

                _dbConnection.Execute(_sqlQueries.InsertQuery, recipe);

                return RedirectToAction("Recipe");
            }

            return View(recipe);
        }

        public IActionResult DeleteRecipe(int REZEPTNR)
        {
            using (var dbConnection = _dbConnection)
            {
                dbConnection.Open();
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
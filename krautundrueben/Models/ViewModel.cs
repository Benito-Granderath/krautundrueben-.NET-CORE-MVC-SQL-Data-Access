using Newtonsoft.Json;
using System.Data;
using System.Data.Common;
using System.Linq;

namespace krautundrueben.Models
{
    public class ViewModel
    {
        public IEnumerable<DBData_Model>? DisplayAllQuery { get; set; }
        public IEnumerable<DBData_Model>? GroupByOrderQuery { get; set; }
        public IEnumerable<DBData_Model>? GroupByCustomerQuery { get; set; }
        public IEnumerable<DBData_Model>? GroupByDeliveryQuery { get; set; }
        public IEnumerable<DBData_Model>? GroupByIngredientQuery { get; set; }
        public IEnumerable<DBData_Model>? AnalyzeOrderTrend { get; set; }
        public IEnumerable<DBData_Model>? IngredientAnalysis { get; set; }
        public IEnumerable<DBData_Model>? MostActiveSuppliers { get; set; }
        public IEnumerable<DBData_Model>? SalesPerDate { get; set; }

    }
}

using ApplicationCore.Commons.Repository;

namespace ApplicationCore.Models;

public class GameSales
{
    public decimal TotalSales { get; set; } 
    public decimal NorthAmericaSales { get; set; }
    public decimal JapanSales { get; set; } 
    public decimal EuropeSales { get; set; } 
    public decimal OtherSales { get; set; } 
}

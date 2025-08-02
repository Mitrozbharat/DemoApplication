using DemoApplication.Models;
using Microsoft.AspNetCore.Mvc;

namespace DemoApplication.Controllers
{
    public class CurrencyController : Controller
    {
        private static List<UserWallet> wallets = new List<UserWallet>
        {
            new UserWallet { UserId = "user1", USD = 100, INR = 5000, EUR = 50 }
        };

        private static decimal USD_INR = 83m;
        private static decimal USD_EUR = 0.91m;


        public ActionResult Index()
        
        {
            return View(wallets[0]); // always show first user for simplicity
        }


      

        [HttpPost]
        public ActionResult BuyCurrency(string from, string to, decimal amount)
        {
            var wallet = wallets[0]; 

          
            decimal rate = GetRate(from, to);
            if (rate == 0)
            {
                TempData["Message"] = "Rate not available.";
                return RedirectToAction("Index");
            }

            decimal required = amount / rate;

            // Check balance and update
            if (GetBalance(wallet, from) >= required)
            {
                SubtractBalance(wallet, from, required);
                AddBalance(wallet, to, amount);
                TempData["Message"] = $"Bought {amount} {to} by using {required:F2} {from}";
            }
            else
            {
                TempData["Message"] = "Insufficient balance.";
            }

            return RedirectToAction("Index");
        }


        [HttpPost]
        public ActionResult SellCurrency(string currency, decimal amount)
        {
            var wallet = wallets[0]; // fixed to user1

            if (GetBalance(wallet, currency) >= amount)
            {
                SubtractBalance(wallet, currency, amount);
                wallet.USD += amount; // convert to USD
                TempData["Message"] = $"Sold {amount} {currency}. Added to USD.";
            }
            else
            {
                TempData["Message"] = "Insufficient balance.";
            }

            return RedirectToAction("Index");
        }

        private decimal GetRate(string from, string to)
        {
            if (from == "USD" && to == "INR") return USD_INR;
            if (from == "INR" && to == "USD") return 1 / USD_INR;
            if (from == "USD" && to == "EUR") return USD_EUR;
            if (from == "EUR" && to == "USD") return 1 / USD_EUR;
            if (from == "INR" && to == "EUR") return 0.011m;
            if (from == "EUR" && to == "INR") return 1 / 0.011m;

            return 0;
        }

        private decimal GetBalance(UserWallet w, string currency)
        {
            return currency switch
            {
                "USD" => w.USD,
                "INR" => w.INR,
                "EUR" => w.EUR,
                _ => 0
            };
        }

        private void AddBalance(UserWallet w, string currency, decimal amt)
        {
            if (currency == "USD") w.USD += amt;
            if (currency == "INR") w.INR += amt;
            if (currency == "EUR") w.EUR += amt;
        }

        private void SubtractBalance(UserWallet w, string currency, decimal amt)
        {
            if (currency == "USD") w.USD -= amt;
            if (currency == "INR") w.INR -= amt;
            if (currency == "EUR") w.EUR -= amt;
        }
    }
}


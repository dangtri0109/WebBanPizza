using System.Collections.Generic;

namespace WebBanPizza.Models
{
    public class HomeIndexVM
    {
        public List<Pizza> FeaturedPizzas { get; set; } = new();
        public List<ComingSoonPizzaVM> ComingSoon { get; set; } = new();
    }
}

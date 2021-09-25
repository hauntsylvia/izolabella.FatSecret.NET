using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fatsecret.NET.Classes
{
    public class FoodsResult
    {
        public Foods foods;
    }
    public class Foods
    {
        public Food[] food;
        public int max_results;
        public int total_results;
        public int page_number;
    }
}

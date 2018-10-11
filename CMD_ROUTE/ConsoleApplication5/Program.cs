using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication5
{
    class Program
    {
        public static int a;
        public static void Main(string[] args)
        {
            List<Path> input = new List<Path>();
            input.Add(new Path() { From = "Москва", To = "Тюмень" });
            input.Add(new Path() { From = "Тюмень", To = "Сочи" });
            input.Add(new Path() { From = "Ростов", To = "Москва" });
            input.Add(new Path() { From = "Сочи", To = "Тамбов" });

            var city = getCity(input);
            var routes = getMatrix(city, input);
            var route = getRoute(city, routes);
            Console.WriteLine(route);
            Console.ReadKey();
        }

        public static string getRoute(List<City> city, int[,] matrix)
        {
            string route = "";
            int i = 0, index = 0;
            List<int> indx = new List<int>();
            do
            {
                index = getNextCity(matrix, index, i);
                indx.Add(index);
                i++;
                if (indx.Where(x => x == index).Count() > 1)
                    break;
            } while (indx.Count != a);
            if (i == a)
            {
                for (int ii = indx.Count - 1; ii >= 0; ii--)
                {
                    route += "->" + city[indx[ii]].Name;
                }
            }
            else
                route = "don't can build route";
            return route;
        }

        public static int getNextCity(int[,] matrix, int index, int iter)
        {
            int next = 0;
            if (iter == 0)
            {
                for (int i = 0; i < a; i++)
                {
                    int sum = 0;
                    for (int j = 0; j < a; j++)
                    {
                        sum += matrix[i, j];
                    }
                    if (sum == 0)
                    {
                        next = i;
                        break;
                    }
                }
            }
            else
            {
                for (int i = 0; i < a; i++)
                {
                    if (matrix[i, index] == 1)
                    {
                        next = i;
                        break;
                    }
                }
            }
            return next;
        }

        /// <summary>
        /// Get all Citis
        /// </summary>
        /// <param name="list">array of roats</param>
        /// <returns>array of city</returns>
        public static List<City> getCity(List<Path> list)
        {
            List<City> citis = new List<City>();
            int index = 0;
            foreach (var item in list)
            {
                int from = citis.Where(x => x.Name == item.From).Count();
                int to = citis.Where(x => x.Name == item.To).Count();
                if (from == 0)
                {
                    citis.Add(new City() { Name = item.From, index = index });
                    index++;
                }
                if (to == 0)
                {
                    citis.Add(new City() { Name = item.To, index = index });
                    index++;
                }
            }
            return citis;
        }

        /// <summary>
        /// get matrix of routs
        /// </summary>
        /// <param name="city">names City</param>
        /// <returns>matrix of routs</returns>
        public static int[,] getMatrix(List<City> city, List<Path> list)
        {
            a = city.Count();
            int[,] route = new int[a, a];
            for (int i = 0; i < a; i++)
            {
                for (int j = 0; j < a; j++)
                {
                    var way = list.Where(x => x.From == city[i].Name & x.To == city[j].Name).FirstOrDefault();
                    route[i, j] = way != null ? 1 : 0;
                }
            }
            return route;
        }
        /* var ways = list.Where(x => x.From == city[i].Name);
                foreach (var item in ways)
                {
                    var way = city.Where(x => x.Name == item.From).FirstOrDefault();
                    
                }*/

    }

    public class City
    {
        public string Name { get; set; }
        public int index { get; set; }
    }

    public class Path
    {
        public string From { get; set; }
        public string To { get; set; }
    }
}

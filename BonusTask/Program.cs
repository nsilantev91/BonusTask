using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace BonusTask
{
    class Program
    {

        static string getSymbols(SortedSet<char> set)
        {
            var aa = set.ToArray();
            if (aa.Length == 1)
                return aa[0].ToString();
            var res = aa[0] + ",";
            for (int i = 1; i <= aa.Length - 2; i++)
            {
                res += aa[i] + ",";
            }
             res += aa[aa.Length - 1];
            return res;
        }
        static void Main(string[] args)
        {
            string line;
            var file = new System.IO.StreamReader("task.txt");
            line = file.ReadLine();
            System.Console.WriteLine(line);
            var arr = line.Split(' ');
            var set = new SortedSet<char>();
            var pattern = @"^[a-z0-9]+$";
            var reg = new Regex(pattern);
            //шаг 1
            foreach (var a in arr)
            {
                var s = a.Split('|');
                foreach (var x in s)
                {
                    if (reg.IsMatch(x))
                        set.Add(a[0]);
                }
            }
            Console.WriteLine(getSymbols(set));

            while (true)
            {
                var ss2 = set;
                
                foreach (var aa in arr)
                {
                    var sss = aa.Split('|');
                    var patt2 = @"^[a-z," + getSymbols(ss2) + ",0-9]+$";
                    var reg2 = new Regex(patt2);
                    //Console.WriteLine(patt2);
                    foreach (var x in sss)
                    {
                        var str = x;
                        if (x.Contains("->"))
                            str = x.Substring(x.IndexOf("->") + 2);
                        if (reg2.IsMatch(str))
                        {
                            Console.WriteLine(aa);
                            ss2.Add(aa[0]);
                        }
                    }
                }
                if (ss2.Count == set.Count)
                {
                    Console.WriteLine(getSymbols(ss2));
                    break;
                }
                set = ss2;
            }
        }
    }
}

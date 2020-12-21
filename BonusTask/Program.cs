using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace BonusTask
{
    class Program
    {
        // функция для преобразования сета в строку Например, {1,2,3,9} => 1,2,3,9
        static string getSymbols(SortedSet<char> set)
        {
            var arr = set.ToArray();
            if (arr.Length == 1)
                return arr[0].ToString();
            var res = arr[0] + ",";
            for (int i = 1; i <= arr.Length - 2; i++)
            {
                res += arr[i] + ",";
            }
             res += arr[arr.Length - 1];
            return res;
        }
        static void Main(string[] args)
        {
            string line;
            var file = new System.IO.StreamReader("task.txt"); // можно также протестировать на файле task2.txt. Там нет бесполезных символов
            line = file.ReadLine(); // предпологается, что продукции будут размещены на одной строке
            var arr = line.Split(' ');
            var set = new SortedSet<char>();
            var pattern = @"^[a-z0-9]+$"; // для поиска только терминалов
            var reg = new Regex(pattern);
            //шаг 1, когда Yo = {}
            foreach (var a in arr)
            {
                var s = a.Split('|');
                foreach (var x in s)
                {
                    if (reg.IsMatch(x)) 
                        set.Add(a[0]); // добавляем нетерминал во множество
                }
            }
            //последующие шаги алгоритма Y1, Y2,...
            while (true)
            {
                var set2 = set;
                foreach (var aa in arr)
                {
                    var str2 = aa.Split('|');
                    var patt2 = @"^[a-z," + getSymbols(set2) + ",0-9]+$"; // для поиска только терминалов и нетерминалов из множества 
                    var reg2 = new Regex(patt2);
                    foreach (var x in str2)
                    {
                        var str = x;
                        if (x.Contains("->"))
                            str = x.Substring(x.IndexOf("->") + 2);
                        if (reg2.IsMatch(str))
                           set2.Add(aa[0]);
                    }
                }
                if (set2.Count == set.Count) // если на двух шагах алгоритма множества не изменились - завершаем работу алгоритма
                {
                    set = set2;
                    break;
                }
                set = set2;
            }
            Console.WriteLine("Исходная грамматика:");
            foreach (var s in arr)
                Console.WriteLine(s);
            var final = set.ToArray();
            Console.WriteLine("Полученная новая грамматика:");
            foreach(var c in final)
            { 
               Console.WriteLine(line.Substring(line.IndexOf(c + "->"), line.IndexOf(' ', line.IndexOf(c + "->")) - line.IndexOf(c + "->")));
            }
        }
    }
}

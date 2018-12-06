using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Lazy
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //LinqIsLazy.PrintStuff();

            IEnumerable<Employee> list = new List<Employee>() {
                new Employee("Jean-Marc", 170),
                new Employee("Jenny", 180),
                new Employee("Bogdan", 173),
                new Employee("Travis", 178),
                new Employee("Mark", 178),
                new Employee("Henrik", 180) };

            // other cool operations that we can do on IEnumerable - this is LINQ
            // Understand Action and Func!
            list.OrderBy(e => e.Height);

            var filteredList = list.Where(e =>
            {
                if (e.Height < 180)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            })
            .OrderBy(e => e.Height)
            .Select(e => e.Name);

            foreach (string filteredItem in filteredList)
            {
                Console.WriteLine(filteredItem);
            }

        }
    }

    // Typical example - load object from a database, or from a service, which is expensive
    // based on http://csharpindepth.com/articles/general/singleton.aspx

    public class ClassWithExpensiveResource
    {

        #region Eager loading
        private readonly string _resource0 = FetchValueFromSlowContainer();

        public string Resource0
        {
            get
            {
                return _resource0;
            }
        }

        #endregion

        #region Simple example #1

        private string _resource1;
        public string Resource1
        {
            get
            {
                if (_resource1 == null) // multi-threading problem?
                {
                    _resource1 = FetchValueFromSlowContainer();
                }
                return _resource1;
            }
        }

        #endregion


        #region With Thread support 

        private string _resource2;
        private static object lockObj = new object();

        public string Resource2
        {
            get
            {
                lock (lockObj) // a tad expensive
                {
                    if (_resource2 == null)
                    {
                        _resource2 = FetchValueFromSlowContainer();
                    }
                    return _resource2;
                }
            }
        }


        #endregion


        #region Excellent thread support, but not lazy 

        // Static ctors are guaranteed to be thread safe because they executed once and only once (when?)
        static ClassWithExpensiveResource()
        {
            _resource3 = FetchValueFromSlowContainer();
        }

        private readonly static string _resource3;

        public static string Resource3 // statics only 
        {
            get
            {
                return _resource3;
            }
        }


        #endregion


        #region Using Lazy



        public string Resource5
        {
            get
            {
                return "";
            }
        }

        #endregion

        private static string FetchValueFromSlowContainer()
        {
            // imagine an http request 
            return "42";
        }
    }

    public class Employee
    {
        public Employee(string name, int height)
        {
            Name = name;
            Height = height;
        }

        public string Name { get; }

        public int Height { get;  }
    }

    public static class LinqIsLazy
    {
        public static void PrintStuff()
        {
            // what does it mean to iterate over a collection - IEnumerable<T>
            IEnumerable<string> list = new List<string>() { "a", "b", "c" };

            foreach (var item in list)
            {
                Console.WriteLine(item);
            }

            // this sort of syntactic sugar for 
            IEnumerator<string> enumerator = list.GetEnumerator();
            enumerator.Reset();
            while (enumerator.MoveNext())
            {
                Console.WriteLine(enumerator.Current);
            }
        }

        // Non-Lazy impl
        public static IEnumerable<TSource> WhereB<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
        {
            var result = new List<TSource>();
            foreach (var item in source)
            {
                if (predicate(item))
                {
                    result.Add(item);
                }
            }

            return result;
        }

        // Lazy-Linq
        public static IEnumerable<TSource> WhereC<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
        {

            foreach (var item in source)
            {
                if (predicate(item))
                {
                    yield return item;
                }
            }

        }

    }


}

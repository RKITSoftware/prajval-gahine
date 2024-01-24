using LINQDemo.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LINQDemo
{
    internal partial class Program
    {
        static void PrintArray(int[] lstNums, string message)
        {
            Console.Write(message + ": ");
            foreach (int n in lstNums)
            {
                Console.Write(n + " ");
            }
            Console.WriteLine();
        }

        static void PrintPersonArray(Person[] lstPerson, string message)
        {
            Console.WriteLine(message + ": ");
            foreach (Person person in lstPerson)
            {
                Console.WriteLine($"{person.id}\t{person.name}\t{person.age}");
            }
            Console.WriteLine();
        }

        static void LinqMethods()
        {
            int[] lstNums = new int[] { 1, 2, 3, 2 };

            // LINQ Select - format the given enumerable object elements (like js map)
            int[] lstNumsSelectMethod = lstNums.Select<int, int>((n) => n * 2).ToArray<int>();
            int[] lstNumsSelectQuery = (from n in lstNums select n * 2).ToArray<int>();

            PrintArray(lstNumsSelectMethod, "select method");
            PrintArray(lstNumsSelectQuery, "select query");
            Console.WriteLine();

            // LINQ Where - filter out elements of enumrable object based on condition (like js filter)
            int[] lstNumsWhereMethod = lstNums.Where<int>(n => n > 1).ToArray<int>();
            int[] lstNumsWhereQuery = (from n in lstNums where n > 1 select n).ToArray<int>();

            PrintArray(lstNumsWhereMethod, "where method");
            PrintArray(lstNumsWhereQuery, "where query");
            Console.WriteLine();

            // LINQ Distinct - is used to remove duplicate element from IEnumerable object and return IEnumrable object with distinct elements
            int[] lstNumsDistinctMehtod = lstNums.Distinct<int>().ToArray<int>();
            int[] lstNumsDistinctQuery = (from n in lstNums select n).Distinct<int>().ToArray<int>();

            PrintArray(lstNumsDistinctMehtod, "distincy method");
            PrintArray(lstNumsDistinctQuery, "distinct query");
            Console.WriteLine();

            // LINQ ElementAt - used to return element at specified index
            int nElementAtMethod = lstNums.ElementAt<int>(1);
            int nElementAtQuery = (from n in lstNums select n).ElementAt<int>(1);

            Console.WriteLine("element at method: " + nElementAtMethod);
            Console.WriteLine();

            // LINQ - ElementAtOrDefault - return element at specified index, if index is out of range than return default value
            int nElementAtOrDefaultMethod = lstNums.ElementAtOrDefault<int>(10);
            int nElementAtOrDefaultQuery = (from n in lstNums select n).ElementAtOrDefault<int>(10);

            Console.WriteLine("element at or default method: " + nElementAtOrDefaultMethod);
            Console.WriteLine("element at or default qeury: " + nElementAtOrDefaultQuery);
            Console.WriteLine();

            // LINQ Empty - used to returna an empty collection of specified type.
            int[] lstNumsEmptyMethod = Enumerable.Empty<int>().ToArray<int>();
            PrintArray(lstNumsEmptyMethod, "empty method");
            Console.WriteLine();

            // LINQ DefaultIfEmpty - used to handle empty collection => if collection is empty then return a default collection object with one element as default value.
            int[] lstNumsDefaultIfEmpty = new int[] { }.DefaultIfEmpty<int>().ToArray<int>();
            PrintArray(lstNumsDefaultIfEmpty, "default if empty method");
            Console.WriteLine();

            // LINQ Contains - used to check if a seq. or collection conatins a specified el or not
            bool IsPresent = lstNums.Contains<int>(2);
            Console.WriteLine("Does 2 contained in lstNums: " + IsPresent);
            Console.WriteLine();

            // LINQ Single - is used to return single element from the collection that  satisfies the condition
            // Note - it will throw an exception if collection contains more than element satisfying the condition
            // also throw error if no element satisfies the condition
            //int nSingleMethod = lstNums.Single<int>(x => x < 1);
            //Console.WriteLine("single method: " + nSingleMethod);
            //Console.WriteLine();

            // LINQ SingleOrDefault - used to return single element from the collection staisfying the condition. If no element is satisfied then returns default value of TSource
            // Note - it will throw an exception if collection contains more than element satisfying the conditio
            int nSingleOrDefault = lstNums.SingleOrDefault<int>(x => x < 1);
            Console.WriteLine("single or default method: " + nSingleOrDefault);
            Console.WriteLine();

            // LINQ First - used to return first element from the collection
            // Note: it throws exception if the collection is empty
            //int nFirstMethod = new int[] { }.First<int>();
            //Console.WriteLine("first method: " + nFirstMethod);
            //Console.WriteLine();

            // LINQ FirstOrDefault - used to return first element from the collection => but if not element is present then return default value of TSource
            int nFirstOrDefaultMethod = new int[] { }.FirstOrDefault<int>();
            Console.WriteLine("first or default method: " + nFirstOrDefaultMethod);
            Console.WriteLine();

            // LINQ Last - used to return last element from the data source
            // Note: throws exception if the collection is empty
            //int nLastMethod = new int[] { }.Last<int>();
            //Console.WriteLine("last method: " + nLastMethod);
            //Console.WriteLine();

            // LINQ Last - used to return last element from the data source, if collection is empty the returns default value of TSource
            int nLastOrDefaultMethod = new int[] { }.LastOrDefault<int>();
            Console.WriteLine("last method: " + nLastOrDefaultMethod);
            Console.WriteLine();

            // LINQ All - is used to check whether all elements of datasource satisfies the given condition
            bool DoesAllSatisfy = lstNums.Any<int>(n => n > 0);
            Console.WriteLine("All method (are all elements > 1: " + DoesAllSatisfy);
            Console.WriteLine();

            // LINQ ANY - is used to check whether any element of data source satisfies the given condition
            bool DoesAnySatisfy = lstNums.Any<int>(n => n == 1);
            Console.WriteLine("Any method (is any elements == 1: " + DoesAnySatisfy);
            Console.WriteLine();

            // linq OrderBy - is used to sort the elements of collection in ascending order
            int[] lstNumsOrderByMethod = lstNums.OrderBy<int, int>(n => n).ToArray<int>();
            PrintArray(lstNumsOrderByMethod, "order by method");
            Console.WriteLine();

            // LINQ OrderByDescending - is used to sort the element of collection in descending order
            int[] lstNumsOrderByDescendingMethod = lstNums.OrderByDescending<int, int>(n => n).ToArray<int>();
            PrintArray(lstNumsOrderByDescendingMethod, "order by descending method");

            // LINQ ThenBy - is used to sort the element of collection in ascending order from second level onwards
            Person[] lstPerson = new Person[]
            {
                new Person() { id = 5, name = "prajval", age = 22 },
                new Person() { id = 4, name = "prajval", age = 19 },
                new Person() { id = 2, name = "deep", age = 21 },
                new Person() { id = 1, name = "yash", age = 20 },
                new Person() { id = 3, name = "krinsi", age = 21 },
                new Person() { id = 4, name = "krinsi", age = 20 }
            };
            Person[] lstThenByMethod = lstPerson.OrderBy<Person, int>(p => p.id)
                .ThenBy<Person, string>(p => p.name)
                .ThenBy<Person, int>(p => p.age).ToArray<Person>();
            PrintPersonArray(lstThenByMethod, "then by method");
            Console.WriteLine();

            // LINQ ThenByDescending - is used to sort the element of collection in ascending order from second level onwards


            // LINQ Reverse - is used to reverse the data stored in the data source
            int[] lstNumsReverseMethod = lstNums.Reverse<int>().ToArray<int>();
            PrintArray(lstNumsReverseMethod, "reverse method");
            Console.WriteLine();

            // LINQ Average - is used to calculate average of numeric value from the collection on which it is applied
            decimal avgAverageMethod = lstNums.Average<int>(n => (decimal)n);
            Console.WriteLine("Average method: " + avgAverageMethod);
            Console.WriteLine();

            // LINQ Max - return largest number in the list (based on property in selector delegate)
            int nMaxMethod = lstNums.Max<int>();
            Console.WriteLine("max method: " + nMaxMethod);
            Console.WriteLine();

            // LINQ Min - returns smallest number in the list (based on property selected in selector delegate)

            // LINQ Sum - is used to calculate sum of numeric value in the collection

            // LINQ Skip - is used to skip first n no. of elements from the data source and then return remaining no. of elements as output
            int[] lstNumsSkipMethod = lstNums.Skip(-110).ToArray<int>();
            PrintArray(lstNumsSkipMethod, "skip method");
            Console.WriteLine();

            // LINQ Take - is used to take first n no. of elements from the data source and return those
            int[] lstNumsTakeMethod = lstNums.Skip(2).ToArray<int>();
            PrintArray(lstNumsTakeMethod, "take method");
            Console.WriteLine();

            // LINQ TakeWhile - is used to fetch elements from start until specified condition is hit (true).
            int[] lstNumsTakeWhileMethod = lstNums.TakeWhile<int>(n => n < 3).ToArray<int>(); PrintArray(lstNumsTakeMethod, "take method");
            PrintArray(lstNumsTakeWhileMethod, "take while method");
            Console.WriteLine();

            // LINQ Concat - is used to concatenate two sequences into one sequence
            int[] lstNumsConcatMethod = lstNums.Concat<int>(new int[] { 100, 200 }).ToArray<int>();
            PrintArray(lstNumsConcatMethod, "concat method");
            Console.WriteLine();

            // LINQ Union -is used to combine multiple data sources into one by removing duplicate elements
            // The default equality comparer, Default, is used to compare values of the types that implement the IEqualityComparer generic interface.
            int[] lstNumsUnionMethod = lstNums.Union<int>(new int[] { 100, 200 }).ToArray<int>();

            // LINQ Count - is used to return no. of elements in datasource or no of elements that satisfies the given condition
            int count = lstNums.Count<int>(n => n > 1);
            Console.WriteLine("no. of elements that satisfy n > 1: " + count);
            Console.WriteLine();

            // LINQ Range - is used to genrate sequence of integral (integer) numbers within a specific range 
            // Note Range is not an extension method
            int[] lstRange = Enumerable.Range(10, 20).ToArray<int>();
            PrintArray(lstRange, "(Range) print 20 nos from 10");
            Console.WriteLine();

            // LINQ ToLookup - It is used for grouping data based on key/value pair
            ILookup<string, Person> lu = lstPerson.ToLookup<Person, string>(p => p.name);

            // LINQ Intersect - is used to return common elements from both collections
            int[] lstNumsIntesect = lstNums.Intersect<int>(new int[] { 1, 100, 2 }).ToArray<int>();
            PrintArray(lstNumsIntesect, "intersect method");
            Console.WriteLine();

            // LINQ Except - is used to return element that are present in first data source but not in second data source
            int[] lstNumsExcept = lstNums.Except<int>(new int[] { 1, 100, 2 }).ToArray<int>();
            PrintArray(lstNumsExcept, "except method");
            Console.WriteLine();

            // LINQ Repeat - is used to generate a sequence (or collection) with specified no. of elements and each element conatins same value
            int[] lstNumsRepeat = Enumerable.Repeat<int>(100, 10).ToArray<int>();
            PrintArray(lstNumsRepeat, "repeat method");
            Console.WriteLine();

            // LINQ toType - is used to filter out specific type of data from the data source
            object[] lstGeneral = new object[]
            {
                "Prajval",
                10,
                10.20,
                "Gahine"
            };
            string[] lstName = lstGeneral.OfType<string>().ToArray<string>();
            Console.WriteLine("First Name: " + lstName[0] + "\nLast Name: " + lstName[1]);
            Console.WriteLine();

            // LINQ GroupJoin - is used to create hierarchical data structures.
            Age[] lstAgeGroup = new Age[]
            {
                new Age{age = 20, ageGroup = "young adult"},
                new Age{age = 30, ageGroup = "middle age adult"},
                new Age{age = 40, ageGroup = "old age"},
            };

            var lstPersonAge = lstPerson.GroupJoin(lstAgeGroup, p => p.age, a => a.age, (person, ageGroups) => new { person, ageGroups });

            var lstPersonAgeQuery = (
                    from person in lstPerson
                    join age in lstAgeGroup
                    on person.age equals age.age
                    into ageGroups
                    select new { person, ageGroups }
                );
        }
    }
}

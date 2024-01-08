using System;
using System.IO;
using System.Net.Sockets;

namespace CSharpBasicApp
{
    /// <summary>
    /// MyClassExceptionHandling demonstrates Exception handling in C#
    /// </summary>
    internal class MyClassExceptionHandling
    {
        #region Public Methods
        public static void Main()
        {
            StreamReader sr = null;
            try
            {
                // In try block we write piece of code which is likely to genrated unforeseen errors
                sr = new StreamReader(@"C:\Users\prajv\source\repos\CSharpBasicApp\Data.txt");
                Console.WriteLine(sr.ReadToEnd());
                Console.Read(); 
            }
            catch (FileNotFoundException ex)
            {
                // In catch block we perform exception handling
                //Console.WriteLine(ex.Message);
                //Console.WriteLine(ex.StackTrace);
                Console.WriteLine("File(0) not found", ex.FileName);
            }
            catch(Exception ex)
            {
                Console.WriteLine("Path or File donot exits");
            }
            finally
            {
                // resource released code here
                sr.Close();
            }
        }
        #endregion
    }
}

// Project types in C#
// Class Library
// Console Application
// ASP.NET (API projecttype / the web api) => used by microservices, mobile project, blazer assembly, js front-end(angular/react/veu), wpf, winform, mvc, blazer server
// WinForms
// Blazer Servers
// Unit Testing => MS by default provides mstest, xunit, nunit.

// For web developer u should know => ASP.NET core, MVC, Razor pages, API, Blazer Server and Blazer Web Assembly.
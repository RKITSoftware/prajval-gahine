using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ConsoleToWebAPIProject.Controllers
{
    [Route("/api")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        /// <summary>
        /// GetAll - method returns all the books available
        /// </summary>
        /// <returns> testing string which represents sets of books</returns>
        [Route("get-all")]
        [Route("getall")]
        [Route("[controller]/[action]")]
        public string GetAll()
        {
            return "Hello from get all";
        }


        /// <summary>
        /// GetAllAuthors - method returns all the authors available in the store
        /// </summary>
        /// <returns> testing string that represents array of all authors</returns>
        [Route("get-all-authors")]
        public string GetAllAuthors()
        {
            return "Hello from get all authors";
        }

        /// <summary>
        /// GetById - method return the details of specified book by the variable in url
        /// </summary>
        /// <param name="id"> id represent the unique attribute of a book record </param>
        /// <returns> testing string that represent data of particular book </returns>
        [Route("getbyid/{id}")]
        public string GetById(int id)
        {
            return "Book " + id;
        }

        // lets see a action method with more than one variable (more than one varibale in url)
        /// <summary>
        /// GetAuthorDetail - method returns the author detail based his contribution in a book
        /// </summary>
        /// <param name="BookId"> a book record attribute that uniquely identify the book </param>
        /// <param name="AuthorId"> a author record attribute that uniquely identify the author </param>
        /// <returns> testing string that represent author data</returns>
        [Route("book/{BookId}/author/{AuthorId}")]
        public string GetAuthorDetail(int BookId, int AuthorId)
        {
            return $"Book {BookId} and Author {AuthorId}";
        }

        // Passing data to action method using Query String
        [Route("search")]
        public string SearchBook(string name, int price)
        {
            return "Book Name: " + name + ", Book Price: " + price;
        }
    }
}

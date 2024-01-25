using System.Collections.Generic;

namespace FinalDemo.Service
{

    /// <summary>
    /// Represents an abstract generic service for basic CRUD operations.
    /// </summary>
    /// <typeparam name="T">The type of entity managed by the service.</typeparam>
    public abstract class AbstractGenericService<T>
    {
        /// <summary>
        /// Gets all entities of type T.
        /// </summary>
        /// <returns>A list of entities.</returns>
        public abstract IEnumerable<T> GetEmployees();

        /// <summary>
        /// Gets an entity of type T by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the entity.</param>
        /// <returns>The entity with the specified identifier.</returns>
        public abstract T GetByID(int id);

        /// <summary>
        /// Inserts a new entity of type T.
        /// </summary>
        /// <param name="entity">The entity to be inserted.</param>
        /// <returns>A list of entities after the insertion operation.</returns>
        public abstract string AddEmployee(T entity);

        /// <summary>
        /// Deletes an entity of type T by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the entity to be deleted.</param>
        /// <returns>A list of entities after the deletion operation.</returns>
        public abstract string UpdateEmployee(int id , T Entity);
    }

}
namespace BrotAPI_Final.Repository
{
    interface IRepositoryDB<T>
    {
        /// <summary>
        /// Es una interfaz generica para todas las clases que usare, en este caso la que mapea con la base de datos
        /// </summary>
        /// <returns></returns>
        T GetById(int id);
        bool Post(T item);
        bool Delete(int id);
        bool Put(int id, T item);
    }
}

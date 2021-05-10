namespace ChatBot_DB
{

    interface ICRUD<T>
    {
        void CreateItem(T t);

        T ReadItem(T t);

        void UpdateItem(T t);

        void DeleteItem(T t);
    }
}

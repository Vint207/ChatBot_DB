namespace ChatBot_DB
{
    interface ICRUD<T>
    {
        bool AddItem(T t);

        bool DeleteItem(T t);

        T GetItem(T t);
    }

    interface ICRUD<T, T1>
    {
        bool AddItem(T t, T1 t1);

        bool DeleteItem(T t, T1 t1);

        T GetItem(T t, T1 t1);
    }
}

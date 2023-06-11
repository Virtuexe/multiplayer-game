using System;

public class Note<T>
{
    private T[] array;

    public int CurrentLength { get; private set; }

    public Note(int size)
    {
        array = new T[size];
        CurrentLength = 0;
    }

    public int MaxLength => array.Length;

    public ref T this[int index]
    {
        get { return ref array[index]; }
    }
    //Add
    public void Add(T item)
    {
        array[CurrentLength] = item;
        CurrentLength++;
        return;
    }
    //Remove
    public void Remove(int index)
    {
        if (index >= CurrentLength)
            Console.Error.WriteLine("index is bigger than current length");
        for (int i = index; i + 1 < CurrentLength; i++)
        {
            array[i] = array[i + 1];
        }
        CurrentLength--;
        return;
    }
    //Insert
    public void Insert(T item, int index)
    {
        if (index >= CurrentLength)
            Console.Error.WriteLine("index is bigger than current length");
        for (int i = index; i + 1 < CurrentLength; i++)
        {
            array[i + 1] = array[i];
        }
        array[index] = item;
        CurrentLength++;
        return;
    }
}
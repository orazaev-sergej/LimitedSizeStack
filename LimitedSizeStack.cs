using System.Collections.Generic;
using System;

public class LimitedSizeStack<T>
{
    private readonly int _limit;
    private readonly LinkedList<T> _storage;

    public LimitedSizeStack(int limit)
    {
        _limit = limit;
        _storage = new LinkedList<T>();
    }

    public void Push(T item)
    {
        if(_limit == 0)
            return;

        if (_storage.Count == _limit)
        {
            _storage.RemoveFirst();
        }
        _storage.AddLast(item);
    }

    public T Pop()
    {
        if (_storage.Count == 0)
        {
            throw new InvalidOperationException("Stack is empty");
        }

        T item = _storage.Last.Value;
        _storage.RemoveLast();
        return item;
    }

    public int Count => _storage.Count;
}
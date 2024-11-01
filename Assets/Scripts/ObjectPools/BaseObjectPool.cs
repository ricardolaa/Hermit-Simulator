using System;
using System.Collections.Generic;

public class BaseObjectPool<T>
{
    private readonly Func<T> _preloadFunc;
    private readonly Action<T> _getAction;
    private readonly Action<T> _returnAction;

    private Queue<T> _pool = new Queue<T>();
    private List<T> _active = new List<T>();

    public BaseObjectPool(Func<T> preloadFunc, Action<T> getAction, Action<T> returnAction, int preloadCount) 
    {
        if (preloadFunc == null) throw new ArgumentNullException(nameof(preloadFunc));

        _preloadFunc = preloadFunc;
        _getAction = getAction;
        _returnAction = returnAction;

        for (int i = 0; i < preloadCount; i++)
        {
            Return(_preloadFunc());
        }
    }

    public T Get()
    {
        T item = _pool.Count > 0 ? _pool.Dequeue() : _preloadFunc();
        _getAction(item);
        _active.Add(item);

        return item;
    }

    public void Return(T item)
    {
        _returnAction(item);
        _active.Remove(item);
        _pool.Enqueue(item);
    }

    public void ReturnAll()
    {
        foreach (var item in _active.ToArray())
        {
            Return(item);
        }
    }
}

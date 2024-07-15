using System;
using System.Collections.Generic;

namespace LimitedSizeStack;

public class ListModel<TItem>
{
	public List<TItem> Items { get; }
	public int UndoLimit;

    private LimitedSizeStack<Action> _undoStack;

    public ListModel(int undoLimit) : this(new List<TItem>(), undoLimit)
	{
        Items = new List<TItem>();
        UndoLimit = undoLimit;
        _undoStack = new LimitedSizeStack<Action>(UndoLimit);
    }

	public ListModel(List<TItem> items, int undoLimit)
	{
		Items = items;
		UndoLimit = undoLimit;
        _undoStack = new LimitedSizeStack<Action>(UndoLimit);
    }

    public void AddItem(TItem item)
	{
        Items.Add(item);
        _undoStack.Push(() => Items.Remove(item));
    }

	public void RemoveItem(int index)
	{
        TItem removedItem = Items[index];
        Items.RemoveAt(index);
        _undoStack.Push(() => Items.Insert(index, removedItem));
    }

	public bool CanUndo()
	{
        return _undoStack.Count > 0;
    }

    public void Undo()
	{
        if (CanUndo())
        {
            Action undoAction = _undoStack.Pop();
            undoAction.Invoke();
        }
    }
}
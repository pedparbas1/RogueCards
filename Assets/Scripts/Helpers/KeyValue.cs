using System;

[Serializable]
public class KeyValue<TK, TV>
{
    public TK Key;
    public TV Value;

    public KeyValue<TK, TV> Kvp { get; }

    public override string ToString()
    {
        return $"Key: {Key.ToString()}, Value: {Value.ToString()}";
    }
    public KeyValue(TK Key, TV Value)
    {
        this.Key = Key;
        this.Value = Value;
    }

    public KeyValue() { }
}
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class SerializedDict<TK, TV> where TK : class 
{
    [SerializeField]
    private KeyValue<TK, TV>[] dict;

    public KeyValue<TK, TV>[] Dict
    {
        get => dict;
        set => dict = value;
    }

    public SerializedDict()
    {
        this.dict = new KeyValue<TK, TV>[0];
    }
    
    public int Count { get => dict.Length;}

    public bool GetValue(TK Key, out KeyValue<TK, TV> keyValue)
    {
        keyValue = new();
        for(int i = 0; i < dict.Length; i++)
        {
            keyValue = dict[i];
            if (keyValue.Key == Key) return true;
        }
        return false;
    }

    public void Add(TK key, TV value)
    {
        if(!GetIndex(key, out int index))
        {
            // Debug.Log(index);
            Dict = Dict.Append(new KeyValue<TK, TV>(key, value)).ToArray();
        }
    }

    public virtual TV this[TK Key]
    {
        get
        {
            GetValue(Key, out KeyValue<TK, TV> kv);
            return kv.Value;
        }
        set
        {
            if(GetIndex(Key, out int i))
            {
                Dict[i].Value = value;
            }
            else
            {
                Dict = Dict.Append(new KeyValue<TK, TV>(Key, value)).ToArray();
            }
        }
    }
    
    public bool Remove(int index)
    {
        for (int i = index + 1; i < dict.Length; i++)
        {
           Dict[i - 1] = Dict[i];
        }
        int lastIndex = dict.Length - 1;
        KeyValue<TK, TV>[] res = new KeyValue<TK, TV>[lastIndex];
        for(int i = 0; i < lastIndex; i++) res[i] = dict[i];
        Dict = res;
        return true;
        
    }

    public bool Remove(TK key)
    {
        if (GetIndex(key, out int index))
            return Remove(index);
        return false;
    }

    public bool ContainsKey(TK key)
    {
        for (int i = 0; i < Dict.Length; i++)
        {
            if (Dict[i].Key == key) return true;
        }
        return false;
    }

    public bool GetIndex(TK key, out int index)
    {
        for(int i = 0; i < Dict.Length; i++)
        {
           if(key.Equals(Dict[i].Key)) 
           {
                index = i;
                return true;
           }
        }
        index = -1;
        return false;
    }

    public List<TV> GetValues()
    {
        List<TV> res = new();
        foreach (KeyValue<TK, TV> value in Dict)
        {
            res.Add(value.Value);
        }
        return res;
    }

    public override string ToString()
    {
        return string.Join(',', (IEnumerable) Dict);
    }
}
// Decompiled with JetBrains decompiler
// Type: cq_itemtypeToItemtypeDat.ItemData
// Assembly: cq_itemtypeToItemtypeDat, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8B76A649-B616-41DE-B4BF-9A85F97130BF
// Assembly location: D:\zero tools\ItemTypeConverter\cq_itemtypeToItemtypeDat.exe

using System.Collections.Generic;
using System.Linq;

namespace cq_itemtypeToItemtypeDat
{
  public class ItemData
  {
    protected Dictionary<string, Field> _fields;

    public Dictionary<string, Field> Fields
    {
      get
      {
        return this._fields;
      }
    }

    public ItemData()
    {
      this._fields = new Dictionary<string, Field>();
    }

    public ItemData(Structure structure, string[] fieldValues)
      : this()
    {
      for (int index = 0; fieldValues != null && index < fieldValues.Length; ++index)
      {
        string key = structure.Fields.Keys.ToArray<string>()[index];
        this._fields.Add(key, new Field(structure.Fields[key].Type.ToString() + "|" + fieldValues[index]));
      }
    }
  }
}

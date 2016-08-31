// Decompiled with JetBrains decompiler
// Type: cq_itemtypeToItemtypeDat.FieldStructure
// Assembly: cq_itemtypeToItemtypeDat, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8B76A649-B616-41DE-B4BF-9A85F97130BF
// Assembly location: D:\zero tools\ItemTypeConverter\cq_itemtypeToItemtypeDat.exe

using System;

namespace cq_itemtypeToItemtypeDat
{
  public class FieldStructure
  {
    private string _name;
    private FieldType _type;
    private long _byteOffset;
    private int _arraySize;
    private int _fieldIndex;

    public string Name
    {
      get
      {
        return this._name;
      }
      set
      {
        this._name = value;
      }
    }

    public FieldType Type
    {
      get
      {
        return this._type;
      }
      set
      {
        this._type = value;
      }
    }

    public long ByteOffset
    {
      get
      {
        return this._byteOffset;
      }
      set
      {
        this._byteOffset = value;
      }
    }

    public int ArraySize
    {
      get
      {
        return this.Type == FieldType.CHARARRAY ? this._arraySize : (this.Type == FieldType.BYTE || this.Type == FieldType.SBYTE ? 1 : (this.Type == FieldType.USHORT || this.Type == FieldType.SHORT ? 2 : (this.Type == FieldType.UINT || this.Type == FieldType.INT ? 4 : (this.Type == FieldType.ULONG || this.Type == FieldType.LONG ? 8 : -1))));
      }
      set
      {
        this._arraySize = value;
      }
    }

    public int FieldIndex
    {
      get
      {
        return this._fieldIndex;
      }
      set
      {
        this._fieldIndex = value;
      }
    }

    public FieldStructure(string _name, string _type, string _byteOffset, string _fieldIndex, string _arraySize)
    {
      this._name = _name;
      try
      {
        this._type = (FieldType) Enum.Parse(typeof (FieldType), _type.ToUpper());
      }
      catch
      {
        return;
      }
      try
      {
        this._byteOffset = Convert.ToInt64(_byteOffset);
      }
      catch
      {
        return;
      }
      try
      {
        this._fieldIndex = Convert.ToInt32(_fieldIndex);
      }
      catch
      {
        return;
      }
      try
      {
        if (_arraySize != null)
          this._arraySize = Convert.ToInt32(_arraySize);
      }
      catch
      {
      }
    }
  }
}

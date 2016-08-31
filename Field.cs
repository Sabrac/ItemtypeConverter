// Decompiled with JetBrains decompiler
// Type: cq_itemtypeToItemtypeDat.Field
// Assembly: cq_itemtypeToItemtypeDat, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8B76A649-B616-41DE-B4BF-9A85F97130BF
// Assembly location: D:\zero tools\ItemTypeConverter\cq_itemtypeToItemtypeDat.exe

using System;

namespace cq_itemtypeToItemtypeDat
{
  public class Field
  {
    protected FieldType _type;
    protected object _value;

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

    public object Value
    {
      get
      {
        return this._value;
      }
      set
      {
        this._value = value;
      }
    }

    public Field(string fieldString)
    {
      if (fieldString.IndexOf('|') == -1)
        return;
      string str1 = fieldString.Substring(0, fieldString.IndexOf('|'));
      string str2 = fieldString.Substring(fieldString.IndexOf('|') + 1);
      string upper = str1.ToUpper();
      try
      {
        this.Type = (FieldType) Enum.Parse(typeof (FieldType), upper);
      }
      catch
      {
        return;
      }
      switch (this.Type)
      {
        case FieldType.BYTE:
          try
          {
            this._value = (object) Convert.ToByte(str2);
            break;
          }
          catch
          {
            break;
          }
        case FieldType.SBYTE:
          try
          {
            this._value = (object) Convert.ToSByte(str2);
            break;
          }
          catch
          {
            break;
          }
        case FieldType.USHORT:
          try
          {
            this._value = (object) Convert.ToUInt16(str2);
            break;
          }
          catch
          {
            break;
          }
        case FieldType.SHORT:
          try
          {
            this._value = (object) Convert.ToInt16(str2);
            break;
          }
          catch
          {
            break;
          }
        case FieldType.UINT:
          try
          {
            this._value = (object) Convert.ToUInt32(str2);
            break;
          }
          catch
          {
            break;
          }
        case FieldType.INT:
          try
          {
            this._value = (object) Convert.ToInt32(str2);
            break;
          }
          catch
          {
            break;
          }
        case FieldType.ULONG:
          try
          {
            this._value = (object) Convert.ToUInt64(str2);
            break;
          }
          catch
          {
            break;
          }
        case FieldType.LONG:
          try
          {
            this._value = (object) Convert.ToInt64(str2);
            break;
          }
          catch
          {
            break;
          }
        case FieldType.STRING:
          this._value = (object) str2;
          break;
        case FieldType.CHARARRAY:
          this._value = (object) str2;
          break;
      }
    }

    public Field(FieldType _type)
    {
      this._type = _type;
    }

    public Field(FieldType _type, object _value)
      : this(_type)
    {
      this._value = _value;
    }

    public override string ToString()
    {
      return this._value.ToString() + "|" + this._type.ToString();
    }
  }
}

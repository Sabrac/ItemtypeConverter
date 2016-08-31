// Decompiled with JetBrains decompiler
// Type: cq_itemtypeToItemtypeDat.Itemtype
// Assembly: cq_itemtypeToItemtypeDat, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8B76A649-B616-41DE-B4BF-9A85F97130BF
// Assembly location: D:\zero tools\ItemTypeConverter\cq_itemtypeToItemtypeDat.exe

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace cq_itemtypeToItemtypeDat
{
  public class Itemtype
  {
    protected List<cq_itemtypeToItemtypeDat.ItemData> _itemData;
    protected Structure _structure;

    public List<cq_itemtypeToItemtypeDat.ItemData> ItemData
    {
      get
      {
        return this._itemData;
      }
    }

    public Structure Structure
    {
      get
      {
        return this._structure;
      }
      set
      {
        this._structure = value;
      }
    }

    public Itemtype(string structureFileName, string[] itemData, string fieldSeperator)
    {
      if (!File.Exists(structureFileName))
        return;
      this._structure = new Structure(structureFileName);
      this._itemData = new List<cq_itemtypeToItemtypeDat.ItemData>();
      for (int index = 0; itemData != null && index < itemData.Length; ++index)
        this._itemData.Add(new cq_itemtypeToItemtypeDat.ItemData(this._structure, itemData[index].Split(fieldSeperator.ToCharArray())));
    }

    public Itemtype(Structure structure, string[] itemData, string fieldSeperator)
    {
      this._structure = structure;
      this._itemData = new List<cq_itemtypeToItemtypeDat.ItemData>();
      for (int index = 0; itemData != null && index < itemData.Length; ++index)
        this._itemData.Add(new cq_itemtypeToItemtypeDat.ItemData(this._structure, itemData[index].Split(fieldSeperator.ToCharArray())));
    }

    public void SaveDatFile(string fileName)
    {
      BinaryWriter binaryWriter = new BinaryWriter((Stream) new FileStream(fileName, FileMode.Create, FileAccess.Write));
      if (this._structure.HasEntryCount)
        binaryWriter.Write(this._itemData.Count);
      if (this._structure.HasIndexTable && (this._structure.IndexFieldName != null || this._structure.IndexFieldName != string.Empty))
      {
        for (int index = 0; index < this._itemData.Count; ++index)
          binaryWriter.Write(Itemtype.GetBytes(this._itemData[index].Fields[this._structure.IndexFieldName]));
      }
      long[] array = this._structure.OrderedDatFieldNames.Keys.ToArray<long>();
      Array.Sort<long>(array);
      for (int index1 = 0; index1 < this._itemData.Count; ++index1)
      {
        long num = 0;
        for (int index2 = 0; index2 < array.Length; ++index2)
        {
          if (array[index2] > num)
          {
            binaryWriter.Write(this._structure.GetDefaultByteArray(array[index2] - num));
            num = array[index2];
          }
          if (this._structure.OrderedDatFieldNames[array[index2]].Count > 1)
          {
            List<Field> fields = new List<Field>();
            List<string> orderedDatFieldName = this._structure.OrderedDatFieldNames[array[index2]];
            for (int index3 = 0; index3 < orderedDatFieldName.Count; ++index3)
              fields.Add(this._itemData[index1].Fields[orderedDatFieldName[index3]]);
            binaryWriter.Write(Itemtype.GetBytes(fields, this._structure.PrefferedDataType));
            num += (long) Itemtype.GetBytes(fields, this._structure.PrefferedDataType).Length;
          }
          else
          {
            binaryWriter.Write(Itemtype.GetBytes(this._itemData[index1].Fields[this._structure.OrderedDatFieldNames[array[index2]][0]]));
            num += (long) Itemtype.GetBytes(this._itemData[index1].Fields[this._structure.OrderedDatFieldNames[array[index2]][0]]).Length;
          }
        }
        if (num != (long) this._structure.EntrySize)
        {
          if ((ulong) num < this._structure.EntrySize)
            binaryWriter.Write(this._structure.GetDefaultByteArray((long) this._structure.EntrySize - num));
          else
            binaryWriter.BaseStream.Position += (long) this._structure.EntrySize - num;
        }
        binaryWriter.Flush();
      }
      binaryWriter.Close();
    }

    public static byte[] GetBytes(List<Field> fields, PrefferedDataType prefferedDataType)
    {
      List<Int128> int128List = new List<Int128>();
      for (int index = 0; index < fields.Count; ++index)
      {
        switch (fields[index].Type)
        {
          case FieldType.BYTE:
            int128List.Add((Int128) ((byte) fields[index].Value));
            break;
          case FieldType.SBYTE:
            int128List.Add((Int128) ((sbyte) fields[index].Value));
            break;
          case FieldType.USHORT:
            int128List.Add((Int128) ((ushort) fields[index].Value));
            break;
          case FieldType.SHORT:
            int128List.Add((Int128) ((short) fields[index].Value));
            break;
          case FieldType.UINT:
            int128List.Add((Int128) ((uint) fields[index].Value));
            break;
          case FieldType.INT:
            int128List.Add((Int128) ((int) fields[index].Value));
            break;
          case FieldType.ULONG:
            int128List.Add((Int128) ((ulong) fields[index].Value));
            break;
          case FieldType.LONG:
            int128List.Add((Int128) ((long) fields[index].Value));
            break;
          case FieldType.CHARARRAY:
            int128List.Add((Int128) ((char[]) fields[index].Value).Length);
            break;
        }
      }
      Int128[] array1 = int128List.ToArray();
      Field[] array2 = fields.ToArray();
      Array.Sort<Int128, Field>(array1, array2);
      switch (prefferedDataType)
      {
        case PrefferedDataType.First:
          return Itemtype.GetBytes(fields[0]);
        case PrefferedDataType.Last:
          return Itemtype.GetBytes(fields[fields.Count - 1]);
        case PrefferedDataType.Smallest:
          return Itemtype.GetBytes(array2[0]);
        case PrefferedDataType.Biggest:
          return Itemtype.GetBytes(array2[array2.Length - 1]);
        default:
          return (byte[]) null;
      }
    }

    public static byte[] GetBytes(Field field)
    {
      byte[] numArray;
      switch (field.Type)
      {
        case FieldType.BYTE:
        case FieldType.SBYTE:
          numArray = new byte[1]
          {
            (byte) field.Value
          };
          break;
        case FieldType.USHORT:
          numArray = BitConverter.GetBytes((ushort) field.Value);
          break;
        case FieldType.SHORT:
          numArray = BitConverter.GetBytes((short) field.Value);
          break;
        case FieldType.UINT:
          numArray = BitConverter.GetBytes((uint) field.Value);
          break;
        case FieldType.INT:
          numArray = BitConverter.GetBytes((int) field.Value);
          break;
        case FieldType.ULONG:
          numArray = BitConverter.GetBytes((ulong) field.Value);
          break;
        case FieldType.LONG:
          numArray = BitConverter.GetBytes((long) field.Value);
          break;
        case FieldType.CHARARRAY:
          char[] charArray = ((string) field.Value).ToCharArray();
          numArray = new byte[charArray.Length];
          for (int index = 0; index < charArray.Length; ++index)
            numArray[index] = BitConverter.GetBytes(charArray[index])[0];
          break;
        default:
          numArray = (byte[]) null;
          break;
      }
      return numArray;
    }
  }
}

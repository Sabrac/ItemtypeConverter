// Decompiled with JetBrains decompiler
// Type: cq_itemtypeToItemtypeDat.Structure
// Assembly: cq_itemtypeToItemtypeDat, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8B76A649-B616-41DE-B4BF-9A85F97130BF
// Assembly location: D:\zero tools\ItemTypeConverter\cq_itemtypeToItemtypeDat.exe

using Data.InputOutput.XFile;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace cq_itemtypeToItemtypeDat
{
  public class Structure
  {
    private const string MAIN_MENU_NAME = "%Main%";
    private INIFile configFile;
    private Dictionary<string, FieldStructure> fields;
    private Dictionary<long, List<string>> orderedDatFieldNames;
    private bool hasIndexTable;
    private bool hasEntryCount;
    private string indexFieldName;
    private byte defaultByte;
    private ulong entrySize;
    private PrefferedDataType prefferedDataType;

    public Dictionary<string, FieldStructure> Fields
    {
      get
      {
        return this.fields;
      }
    }

    public Dictionary<long, List<string>> OrderedDatFieldNames
    {
      get
      {
        return this.orderedDatFieldNames;
      }
    }

    public bool HasIndexTable
    {
      get
      {
        return this.hasIndexTable;
      }
      set
      {
        this.hasIndexTable = value;
      }
    }

    public bool HasEntryCount
    {
      get
      {
        return this.hasEntryCount;
      }
      set
      {
        this.hasEntryCount = value;
      }
    }

    public string IndexFieldName
    {
      get
      {
        return this.indexFieldName;
      }
      set
      {
        this.indexFieldName = value;
      }
    }

    public byte DefaultByte
    {
      get
      {
        return this.defaultByte;
      }
      set
      {
        this.defaultByte = value;
      }
    }

    public ulong EntrySize
    {
      get
      {
        return this.entrySize;
      }
      set
      {
        this.entrySize = value;
      }
    }

    public PrefferedDataType PrefferedDataType
    {
      get
      {
        return this.prefferedDataType;
      }
      set
      {
        this.prefferedDataType = value;
      }
    }

    public Structure()
    {
      this.fields = new Dictionary<string, FieldStructure>();
      this.orderedDatFieldNames = new Dictionary<long, List<string>>();
      this.hasIndexTable = false;
      this.hasEntryCount = false;
      this.indexFieldName = string.Empty;
      this.defaultByte = (byte) 0;
      this.entrySize = 0UL;
      this.prefferedDataType = PrefferedDataType.Biggest;
    }

    public Structure(string configFileName)
    {
      this.entrySize = 0UL;
      this.fields = new Dictionary<string, FieldStructure>();
      this.orderedDatFieldNames = new Dictionary<long, List<string>>();
      this.configFile = new INIFile(configFileName);
      if (this.configFile.GetKeys("%Main%") == null)
        return;
      this.hasEntryCount = Convert.ToBoolean(this.configFile.GetValue("%Main%", "HasEntryCount"));
      this.hasIndexTable = Convert.ToBoolean(this.configFile.GetValue("%Main%", "HasIndexTable"));
      this.indexFieldName = this.configFile.GetValue("%Main%", "IndexFieldName");
      this.defaultByte = byte.Parse(this.configFile.GetValue("%Main%", "DefaultByte"));
      this.prefferedDataType = (PrefferedDataType) Enum.Parse(typeof (PrefferedDataType), this.configFile.GetValue("%Main%", "PrefferedDataType"));
      this.entrySize = (ulong) Convert.ToUInt32(this.configFile.GetValue("%Main%", "EntrySize"));
      string[] menus = this.configFile.GetMenus();
      for (int index = 0; menus != null && index < menus.Length; ++index)
      {
        if (!this.fields.ContainsKey(menus[index]) && !(menus[index] == "%Main%"))
        {
          FieldStructure fieldStructure = new FieldStructure(menus[index], this.configFile.GetValue(menus[index], "Type"), this.configFile.GetValue(menus[index], "DatOffset"), this.configFile.GetValue(menus[index], "TableFieldIndex"), this.configFile.GetValue(menus[index], "Size"));
          this.fields.Add(menus[index], fieldStructure);
          if (fieldStructure.ByteOffset >= 0L && fieldStructure.ArraySize > 0)
          {
            if (!this.orderedDatFieldNames.ContainsKey(fieldStructure.ByteOffset))
              this.orderedDatFieldNames.Add(fieldStructure.ByteOffset, new List<string>());
            this.orderedDatFieldNames[fieldStructure.ByteOffset].Add(menus[index]);
          }
        }
      }
    }

    public byte[] GetDefaultByteArray(long size)
    {
      byte[] numArray = new byte[size];
      for (int index = 0; index < numArray.Length; ++index)
        numArray[index] = this.defaultByte;
      return numArray;
    }

    public void Save(string fileName)
    {
      List<string> list = this.fields.Keys.ToList<string>();
      List<string> stringList1 = new List<string>();
      stringList1.Add("[%Main%]");
      stringList1.Add("HasEntryCount=" + this.hasEntryCount.ToString());
      stringList1.Add("HasIndexTable=" + this.hasIndexTable.ToString());
      stringList1.Add("IndexFieldName=" + this.indexFieldName);
      stringList1.Add("DefaultByte=" + this.defaultByte.ToString());
      stringList1.Add("PrefferedDataType=" + this.prefferedDataType.ToString());
      stringList1.Add("EntrySize=" + this.entrySize.ToString());
      stringList1.Add("");
      for (int index = 0; index < this.fields.Count; ++index)
      {
        stringList1.Add("[" + list[index] + "]");
        stringList1.Add("Type=" + this.fields[list[index]].Type.ToString());
        int num;
        if (this.fields[list[index]].Type == FieldType.CHARARRAY)
        {
          List<string> stringList2 = stringList1;
          string str1 = "Size=";
          num = this.fields[list[index]].ArraySize;
          string str2 = num.ToString();
          string str3 = str1 + str2;
          stringList2.Add(str3);
        }
        stringList1.Add("DatOffset=" + this.fields[list[index]].ByteOffset.ToString());
        List<string> stringList3 = stringList1;
        string str4 = "TableFieldIndex=";
        num = this.fields[list[index]].FieldIndex;
        string str5 = num.ToString();
        string str6 = str4 + str5;
        stringList3.Add(str6);
        stringList1.Add("");
      }
      File.WriteAllLines(fileName, (IEnumerable<string>) stringList1);
    }
  }
}

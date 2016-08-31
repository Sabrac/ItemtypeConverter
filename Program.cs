// Decompiled with JetBrains decompiler
// Type: cq_itemtypeToItemtypeDat.Program
// Assembly: cq_itemtypeToItemtypeDat, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8B76A649-B616-41DE-B4BF-9A85F97130BF
// Assembly location: D:\zero tools\ItemTypeConverter\cq_itemtypeToItemtypeDat.exe

using System;
using System.Windows.Forms;

namespace cq_itemtypeToItemtypeDat
{
  internal static class Program
  {
    [STAThread]
    private static void Main()
    {
      Application.EnableVisualStyles();
      Application.SetCompatibleTextRenderingDefault(false);
      Application.Run((Form) new MainForm());
    }
  }
}

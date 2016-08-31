// Decompiled with JetBrains decompiler
// Type: cq_itemtypeToItemtypeDat.Int128
// Assembly: cq_itemtypeToItemtypeDat, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8B76A649-B616-41DE-B4BF-9A85F97130BF
// Assembly location: D:\zero tools\ItemTypeConverter\cq_itemtypeToItemtypeDat.exe

using System;

namespace cq_itemtypeToItemtypeDat
{
  public struct Int128 : IComparable, IFormattable, IConvertible, IComparable<Int128>, IEquatable<Int128>
  {
    public static readonly Int128 MaxValue = new Int128(ulong.MaxValue, long.MaxValue);
    public static readonly Int128 MinValue = new Int128(0UL, long.MinValue);
    private ulong _low;
    private long _high;

    public ulong Low
    {
      get
      {
        return this._low;
      }
      set
      {
        this._low = value;
      }
    }

    public long High
    {
      get
      {
        return this._high;
      }
      set
      {
        this._high = value;
      }
    }

    public Int128(ulong low, long high)
    {
      this._low = low;
      this._high = high;
    }

    public static implicit operator Int128(long signedValue)
    {
      return new Int128((ulong) signedValue, signedValue < 0L ? -1L : 0L);
    }

    public static implicit operator Int128(ulong unsignedValue)
    {
      return new Int128(unsignedValue, 0L);
    }

    public static implicit operator Int128(int signedValue)
    {
      return (Int128) ((long) signedValue);
    }

    public static implicit operator Int128(uint unsignedValue)
    {
      return (Int128) ((ulong) unsignedValue);
    }

    public static implicit operator Int128(short signedValue)
    {
      return (Int128) ((long) signedValue);
    }

    public static implicit operator Int128(ushort unsignedValue)
    {
      return (Int128) ((ulong) unsignedValue);
    }

    public static implicit operator Int128(char character)
    {
      return (Int128) ((long) character);
    }

    public static implicit operator Int128(sbyte signedValue)
    {
      return (Int128) ((long) signedValue);
    }

    public static implicit operator Int128(byte unsignedValue)
    {
      return (Int128) ((ulong) unsignedValue);
    }

    public static explicit operator long(Int128 value)
    {
      return (long) value._low;
    }

    public static explicit operator ulong(Int128 value)
    {
      return value._low;
    }

    public static explicit operator int(Int128 value)
    {
      return (int) value._low;
    }

    public static explicit operator uint(Int128 value)
    {
      return (uint) value._low;
    }

    public static explicit operator short(Int128 value)
    {
      return (short) value._low;
    }

    public static explicit operator ushort(Int128 value)
    {
      return (ushort) value._low;
    }

    public static explicit operator char(Int128 value)
    {
      return (char) value._low;
    }

    public static explicit operator sbyte(Int128 value)
    {
      return (sbyte) value._low;
    }

    public static explicit operator byte(Int128 value)
    {
      return (byte) value._low;
    }

    public static Int128 operator +(Int128 value)
    {
      return value;
    }

    public static Int128 operator ~(Int128 value)
    {
      return new Int128(~value._low, ~value._high);
    }

    public static Int128 operator ++(Int128 value)
    {
      ++value._low;
      if ((long) value._low == 0L)
        ++value._high;
      return value;
    }

    public static Int128 operator --(Int128 value)
    {
      if ((long) value._low == 0L)
        --value._high;
      --value._low;
      return value;
    }

    public static Int128 operator -(Int128 value)
    {
      return Int128.op_Increment(~value);
    }

    public static Int128 operator +(Int128 valueOne, Int128 valueTwo)
    {
      Int128 int128 = valueOne;
      ulong low = valueOne._low;
      int128._low += valueTwo._low;
      int128._high += valueTwo._high;
      if (int128._low < low)
        ++int128._high;
      return int128;
    }

    public static Int128 operator -(Int128 valueOne, Int128 valueTwo)
    {
      return valueOne + -valueTwo;
    }

    public static Int128 operator &(Int128 valueOne, Int128 valueTwo)
    {
      return new Int128(valueOne._low & valueTwo._low, valueOne._high & valueTwo._high);
    }

    public static Int128 operator |(Int128 valueOne, Int128 valueTwo)
    {
      return new Int128(valueOne._low | valueTwo._low, valueOne._high | valueTwo._high);
    }

    public static Int128 operator ^(Int128 valueOne, Int128 valueTwo)
    {
      return new Int128(valueOne._low ^ valueTwo._low, valueOne._high ^ valueTwo._high);
    }

    public static Int128 operator <<(Int128 value, int moveCounter)
    {
      if (moveCounter <= 0)
        return value;
      if (moveCounter > (int) sbyte.MaxValue)
        return (Int128) 0;
      Int128 int128 = value;
      if (moveCounter > 63)
      {
        moveCounter -= 64;
        int128._high = (long) int128._low;
        int128._low = 0UL;
      }
      if (moveCounter > 0)
      {
        ulong num = int128._low >> 64 - moveCounter;
        int128._low <<= moveCounter;
        int128._high = int128._high << moveCounter | (long) num;
      }
      return int128;
    }

    public static Int128 operator >>(Int128 value, int moveCounter)
    {
      if (moveCounter <= 0)
        return value;
      if (moveCounter > (int) sbyte.MaxValue)
        return (Int128) 0;
      Int128 int128 = value;
      if (moveCounter > 63)
      {
        moveCounter -= 64;
        int128._low = (ulong) int128._high;
        int128._high = 0L;
      }
      if (moveCounter > 0)
      {
        ulong num = (ulong) (int128._high << 64 - moveCounter);
        int128._high >>= moveCounter;
        int128._low = int128._low >> moveCounter | num;
      }
      return int128;
    }

    public static bool operator ==(Int128 leftValue, Int128 rightValue)
    {
      return leftValue._high == rightValue._high && (long) leftValue._low == (long) rightValue._low;
    }

    public static bool operator !=(Int128 leftValue, Int128 rightValue)
    {
      return !(leftValue == rightValue);
    }

    public static bool operator >(Int128 leftValue, Int128 rightValue)
    {
      int num = leftValue._high.CompareTo(rightValue._high);
      if (num == 0)
        return leftValue._low > rightValue._low;
      return num > 0;
    }

    public static bool operator <(Int128 leftValue, Int128 rightValue)
    {
      int num = leftValue._high.CompareTo(rightValue._high);
      if (num == 0)
        return leftValue._low < rightValue._low;
      return num < 0;
    }

    public static bool operator >=(Int128 leftValue, Int128 rightValue)
    {
      return !(leftValue < rightValue);
    }

    public static bool operator <=(Int128 leftValue, Int128 rightValue)
    {
      return !(leftValue > rightValue);
    }

    TypeCode IConvertible.GetTypeCode()
    {
      throw new NotImplementedException();
    }

    bool IConvertible.ToBoolean(IFormatProvider provider)
    {
      throw new NotImplementedException();
    }

    byte IConvertible.ToByte(IFormatProvider provider)
    {
      throw new NotImplementedException();
    }

    char IConvertible.ToChar(IFormatProvider provider)
    {
      throw new NotImplementedException();
    }

    DateTime IConvertible.ToDateTime(IFormatProvider provider)
    {
      throw new NotImplementedException();
    }

    Decimal IConvertible.ToDecimal(IFormatProvider provider)
    {
      throw new NotImplementedException();
    }

    double IConvertible.ToDouble(IFormatProvider provider)
    {
      throw new NotImplementedException();
    }

    short IConvertible.ToInt16(IFormatProvider provider)
    {
      throw new NotImplementedException();
    }

    int IConvertible.ToInt32(IFormatProvider provider)
    {
      throw new NotImplementedException();
    }

    long IConvertible.ToInt64(IFormatProvider provider)
    {
      throw new NotImplementedException();
    }

    sbyte IConvertible.ToSByte(IFormatProvider provider)
    {
      throw new NotImplementedException();
    }

    float IConvertible.ToSingle(IFormatProvider provider)
    {
      throw new NotImplementedException();
    }

    string IConvertible.ToString(IFormatProvider provider)
    {
      throw new NotImplementedException();
    }

    object IConvertible.ToType(Type conversionType, IFormatProvider provider)
    {
      throw new NotImplementedException();
    }

    ushort IConvertible.ToUInt16(IFormatProvider provider)
    {
      throw new NotImplementedException();
    }

    uint IConvertible.ToUInt32(IFormatProvider provider)
    {
      throw new NotImplementedException();
    }

    ulong IConvertible.ToUInt64(IFormatProvider provider)
    {
      throw new NotImplementedException();
    }

    public int CompareTo(object obj)
    {
      if (!(obj is Int128))
        throw new ArgumentException("Object must be of type Int128.");
      return this.CompareTo((Int128) obj);
    }

    public string ToString(string format, IFormatProvider formatProvider)
    {
      throw new NotImplementedException();
    }

    public int CompareTo(Int128 other)
    {
      int num = this._high.CompareTo(other._high);
      if (num == 0)
        num = this._low.CompareTo(other._low);
      return num;
    }

    public bool Equals(Int128 other)
    {
      return this == other;
    }

    public override bool Equals(object obj)
    {
      if (!(obj is Int128))
        return false;
      return this == (Int128) obj;
    }

    public override int GetHashCode()
    {
      long num = this._high ^ (long) this._low;
      return (int) (num >> 32 ^ num & (long) uint.MaxValue);
    }
  }
}

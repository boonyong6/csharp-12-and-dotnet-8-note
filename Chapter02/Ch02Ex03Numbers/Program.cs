string horizontalLine = new('-', 109);

WriteLine(horizontalLine);
WriteLine("{0,-8} {1,-18} {2, 40} {3, 40}", "Type", "Byte(s) of memory", "Min", "Max");
WriteLine(horizontalLine);
WriteLine("{0,-8} {1,-18} {2, 40} {3, 40}", "sbyte", sizeof(sbyte), sbyte.MinValue, sbyte.MaxValue);
WriteLine("{0,-8} {1,-18} {2, 40} {3, 40}", "byte", sizeof(byte), byte.MinValue, byte.MaxValue);
WriteLine("{0,-8} {1,-18} {2, 40} {3, 40}", "short", sizeof(short), short.MinValue, short.MaxValue);
WriteLine("{0,-8} {1,-18} {2, 40} {3, 40}", "ushort", sizeof(ushort), ushort.MinValue, ushort.MaxValue);
WriteLine("{0,-8} {1,-18} {2, 40} {3, 40}", "int", sizeof(int), int.MinValue, int.MaxValue);
WriteLine("{0,-8} {1,-18} {2, 40} {3, 40}", "uint", sizeof(uint), uint.MinValue, uint.MaxValue);
WriteLine("{0,-8} {1,-18} {2, 40} {3, 40}", "long", sizeof(long), long.MinValue, long.MaxValue);
WriteLine("{0,-8} {1,-18} {2, 40} {3, 40}", "ulong", sizeof(ulong), ulong.MinValue, ulong.MaxValue);
unsafe
{
    WriteLine("{0,-8} {1,-18} {2, 40} {3, 40}", "Int128", sizeof(Int128), Int128.MinValue, Int128.MaxValue);
    WriteLine("{0,-8} {1,-18} {2, 40} {3, 40}", "UInt128", sizeof(UInt128), UInt128.MinValue, UInt128.MaxValue);
    WriteLine("{0,-8} {1,-18} {2, 40} {3, 40}", "Half", sizeof(Half), Half.MinValue, Half.MaxValue);
}
WriteLine("{0,-8} {1,-18} {2, 40} {3, 40}", "float", sizeof(float), float.MinValue, float.MaxValue);
WriteLine("{0,-8} {1,-18} {2, 40} {3, 40}", "double", sizeof(double), double.MinValue, double.MaxValue);
WriteLine("{0,-8} {1,-18} {2, 40} {3, 40}", "decimal", sizeof(decimal), decimal.MinValue, decimal.MaxValue);

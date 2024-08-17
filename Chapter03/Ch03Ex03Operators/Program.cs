int x;
int y;

x = 3;
y = 2 + ++x;
WriteLine($"x is {x}");
WriteLine($"y is {y}");

WriteLine();

x = 3 << 2;
y = 10 >> 1;
WriteLine($"x is {x}");
WriteLine($"y is {y}");

WriteLine();

x = 10 & 8;
y = 10 | 7;
WriteLine($"x is {x}");
WriteLine($"y is {y}");

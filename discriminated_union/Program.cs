using System;
using static System.Console;

// discriminated union, common base to implement sum type
record R;
record R0() : R;
record R1(int X) : R;
record R2(int X, int Y) : R;
record R3(int X, int Y, int Z) : R;

// discriminated union, no common base
record F0();
record F1(int X);
record F2(int X, int Y);
record F3(int X, int Y, int Z);

// discriminated union, common base to implement sum type
//#pragma warning disable CS8524
enum DU_TYPECONSTR {DU1, DU2, DU3, DU4};
class DU_TYPE {
    // base class provides enumeration
    public DU_TYPECONSTR DU_typeconstr;
    public DU_TYPE(DU_TYPECONSTR DU_typeconstr) {
        this.DU_typeconstr = DU_typeconstr;
    }
}

class DU1_TYPE : DU_TYPE {

    public int X;
    public DU1_TYPE(int x) : base(DU_TYPECONSTR.DU1) {
        X = x;
    }
}
class DU2_TYPE : DU_TYPE {

    public int X;
    public int Y;
    public DU2_TYPE(int x, int y) : base(DU_TYPECONSTR.DU2) {
        X = x;
        Y = y;
    }
}

class DU3_TYPE : DU_TYPE {

    public int X;
    public int Y;
    public int Z;
    public DU3_TYPE(int x, int y, int z) : base(DU_TYPECONSTR.DU3) {
        X = x;
        Y = y;
        Z = z;
    }
}

class DU4_TYPE : DU_TYPE {

    public int X1;
    public int X2;
    public int X3;
    public int X4;
    public DU4_TYPE(int x1, int x2, int x3, int x4) : base(DU_TYPECONSTR.DU4) {
        X1 = x1;
        X2 = x2;
        X3 = x3;
        X4 = x4;
    }
}

class App {
    public static void Main() {
        WriteLine("main");

        // discrimated union
        test0();    // simple example with dynamic with unrelated types
        test1();    // types without relationship
        test2();    // types with is-a relationship
        test3();
        test4();
        test5();
    }

    public static void test0() {
        dynamic x = new int();
        switch (x) {
            case int i when i > 0:
                break;
            case int i when i < 0:
                break;
            case int i when i == 0:
                break;
            // other type
            case float:
                break;
            case double _:
                break;
            default:
                break;
        }

    }

    public static void test1() {
        dynamic? f = ReadLine()?.TrimEnd() switch {
            "0" => new F0(),
            "1" => new F1(1),
            "2" => new F2(1,2),
            "3" => new F3(1,2,3),
            _ or null => null,
        };
        var fmsg = f switch {
            F0 => "F0",
            F1 {X:0} => "F1(0)",
            F1 {X:var x_} f1 when x_ > 0 => $"F1 {f1.ToString()}",
            F1 {X:var x_} f1 when x_ < 0 => $"F1 {f1.ToString()}",
            F2 => $"F2",
            F3 => $"F3",
            _ => "other",
        };
        WriteLine($"f => {fmsg}");
    }

    public static void test2() {
        dynamic? f = ReadLine()?.TrimEnd() switch {
            "0" => new R0(),
            "1" => new R1(1),
            "2" => new R2(1,2),
            "3" => new R3(1,2,3),
            _ or null => null,
        };
        var fmsg = f switch {
            R0 => "R0",                     // 0 dimension
            R1 {X:0} => "R1=0",                                         // 1 dimension
            R1 {X:var x_} f1 when x_ > 0 => $"R1>0 {f1.ToString()}",    // 1 dimension
            R1 {X:var x_} f1 when x_ < 0 => $"R1<0 {f1.ToString()}",    // 1 dimension
            R2 {X:0, Y:0} => $"R2=0,0",
            R2 {X:var x_, Y:var y_} f1 when x_ > 0 && y_ > 0 => $"R2>0,>0 {f1.ToString()}",
            R2 {X:var x_, Y:var y_} f1 when x_ > 0 && y_ < 0 => $"R2>0,<0 {f1.ToString()}",
            R2 {X:var x_, Y:var y_} f1 when x_ < 0 && y_ > 0 => $"R2<0,>0 {f1.ToString()}",
            R2 {X:var x_, Y:var y_} f1 when x_ < 0 && y_ < 0 => $"R2>0,>0 {f1.ToString()}",
            R2 => $"R2",
            R3 => $"R3",
            _ => "other",
        };
        WriteLine($"f => {fmsg}");
    }

    public static void test3() {
        object? f = ReadLine()?.TrimEnd() switch {
            "0" => new R0(),
            "1" => new R1(1),
            "2" => new R2(1,2),
            "3" => new R3(1,2,3),
            _ or null => null,
        };
        var fmsg = f switch {
            R0 => "R0",                     // 0 dimension
            R1 {X:0} => "R1=0",                                         // 1 dimension
            R1 {X:var x_} f1 when x_ > 0 => $"R1>0 {f1.ToString()}",    // 1 dimension
            R1 {X:var x_} f1 when x_ < 0 => $"R1<0 {f1.ToString()}",    // 1 dimension
            R2 {X:0, Y:0} => $"R2=0,0",
            R2 {X:var x_, Y:var y_} f1 when x_ > 0 && y_ > 0 => $"R2>0,>0 {f1.ToString()}",
            R2 {X:var x_, Y:var y_} f1 when x_ > 0 && y_ < 0 => $"R2>0,<0 {f1.ToString()}",
            R2 {X:var x_, Y:var y_} f1 when x_ < 0 && y_ > 0 => $"R2<0,>0 {f1.ToString()}",
            R2 {X:var x_, Y:var y_} f1 when x_ < 0 && y_ < 0 => $"R2>0,>0 {f1.ToString()}",
            R2 => $"R2",
            R3 => $"R3",
            _ => "other",
        };
        WriteLine($"f => {fmsg}");
    }

    public static void test4() {

        object? o = null;
        dynamic? d = null;
        int? i = null;
        var v = i;  // var? makes no sense, var is always concrete and includes nullable types as e.g. int|int? 
        WriteLine($"{o},{d}");  // unused warning otherwise

        // object? and dynamic? would be OK
        // instead here each derived types provides the hint to its base => fix for CS8506
        var f = ReadLine()?.TrimEnd() switch {
            "0" => new R0() as R,   // actually first hint is enough
            "1" => new R1(1) as R,
            "2" => new R2(1,2) as R,
            "3" => new R3(1,2,3) as R,
            _ or null => null as R,
        };
        var fmsg = f switch {
            R0 => "R0",                     // 0 dimension
            R1 {X:0} => "R1=0",                                         // 1 dimension
            R1 {X:var x_} f1 when x_ > 0 => $"R1>0 {f1.ToString()}",    // 1 dimension
            R1 {X:var x_} f1 when x_ < 0 => $"R1<0 {f1.ToString()}",    // 1 dimension
            R2 {X:0, Y:0} => $"R2=0,0",
            R2 {X:var x_, Y:var y_} f1 when x_ > 0 && y_ > 0 => $"R2>0,>0 {f1.ToString()}",
            R2 {X:var x_, Y:var y_} f1 when x_ > 0 && y_ < 0 => $"R2>0,<0 {f1.ToString()}",
            R2 {X:var x_, Y:var y_} f1 when x_ < 0 && y_ > 0 => $"R2<0,>0 {f1.ToString()}",
            R2 {X:var x_, Y:var y_} f1 when x_ < 0 && y_ < 0 => $"R2>0,>0 {f1.ToString()}",
            R2 => $"R2",
            R3 => $"R3",
            // inheritance is open for extension, no warning for complete switch possible
            // extension of discriminated union will show warning when switch not complete
            _ => "other",   
        };
        WriteLine($"f => {fmsg}");
    }
    public static void test5() {

        var f = ReadLine()?.TrimEnd() switch {
            "1" => new DU1_TYPE(1) as DU_TYPE,
            "2" => new DU2_TYPE(1,2) as DU_TYPE,
            "3" => new DU3_TYPE(1,2,4) as DU_TYPE,
            "4" => new DU4_TYPE(1,2,3,4) as DU_TYPE,
            _ or null => null as DU_TYPE,
        };
        var fmsg = f?.DU_typeconstr switch {
            DU_TYPECONSTR.DU1 => "1",
            DU_TYPECONSTR.DU2 => "2",
            DU_TYPECONSTR.DU3 => "3",
            DU_TYPECONSTR.DU4 => "4",
            null => "null",
        };
        fmsg = f switch {
            {DU_typeconstr: DU_TYPECONSTR.DU1} du => $"{du.ToString()}",
            {DU_typeconstr: DU_TYPECONSTR.DU2} du => $"{du.ToString()}",
            {DU_typeconstr: DU_TYPECONSTR.DU3} du => $"{du.ToString()}",
            {DU_typeconstr: DU_TYPECONSTR.DU4} du => $"{du.ToString()}",
            null => "null",
            _ => "other",
        };
        WriteLine($"f => {fmsg}");
    }
}

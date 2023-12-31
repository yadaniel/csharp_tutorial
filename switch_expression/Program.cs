using System;
using System.Collections;
using System.Dynamic;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Xml.Linq;
using System.Xml.Schema;
using static System.Console;

enum State { S0, S1, S2, S3, S4 }
//record Data(Int32 X, Int32 Y) {}    // use {} without ;
//record Data(Int32 X, Int32 Y);    // or use ;
record Data(Int32 X, Int32 Y, Int32 Z);     // additional unused property in pattern matching OK

struct DataS(Int32 x_, Int32 y_) {
    public Int32 X = x_;
    public Int32 Y = y_;
    public Int32 Z = 0;     // additional unused property in pattern matching OK
}

/*
struct DataS {
    public DataS(int x_, int y_) {
        X = x_;
        Y = y_;
    }
    public Int32 X;
    public Int32 Y;
    public Int32 Z;     // additional unused property in pattern matching OK
}
*/

class DataC(int a, int b) {
    public int A = a;
    public int B = b;
    public int C = a + b;

    public void Deconstruct (out int X, out int Y, out int Z) {
        X = A;
        Y = B;
        Z = A + B;
    }
}

class DataC2 {
    public DataC2(int X, int Y) {
        this.X = X;
        this.Y = Y;
    }

    static DataC2() {
        XS = 1;
        YS = 2;
    }

    public void Deconstruct(out int xsum, out int ysum) {
        xsum = X + XS;
        ysum = Y + YS;
    }

    public static int XS;
    public static int YS;
    public int X;
    public int Y;
}

class App {
    public static void Main() {
        WriteLine("main");
        //test1_run();
        //test2_run();
        //test3_run();
        //test4_run();
        test5_run();
    }

    static State state = State.S0;

    public static void test1(State state) {
        WriteLine($"test1, given {state.ToString()}");

        // explicit
        var x = state switch {
            var s when s == State.S0 || s == State.S1 => 0,     // one of both first 2
            var s when s >= State.S2 && s <= State.S3 => 1,     // one of both last 2
            _ => -1
        };
        WriteLine($"x = {x}");

        // implicit
        x = state switch {
            State.S0 or State.S1 => 0,          // one of both first 2
            >= State.S2 and <= State.S3 => 1,   // one of both last 2
            _ => -1
        };
        WriteLine($"x = {x}");

        // mixed
        x = state switch {
            State.S0 or State.S1 => 0,                          // one of both first 2
            var s when s >= State.S2 && s <= State.S3 => 1,     // one of both last 2
            _ => -1
        };
        WriteLine($"x = {x}");

        // old style
        x = int.MinValue;   // switch statement can omit assignment to x => provide default
        switch (state) {
            case State.S0:
            case State.S1:
                x = 0;      // switch expression always evaluate to value and is type checked
                break;
            case State.S2:
            case State.S3:
                x = 1;
                break;
            default:
                x = -1;
                break;
        }
        WriteLine($"x = {x}");

    }

    public static void test1_run() {
        test1(State.S0);
        test1(State.S1);
        test1(State.S2);
        test1(State.S3);
        test1(State.S4);
    }

    public static void test2(Data data) {
        WriteLine($"test2, given {data}");

        // implicit
        var x = data switch {
            {X: 0, Y: 0} =>  0,
            {X: 1, Y: 1} =>  1,
            {X: 1, Y: _} =>  2,
            {X: _, Y: 1} =>  3,
            {X: >10 and <100, Y: 10 or 100} =>  4,
            {X: var x_, Y: var y_} =>  x_ + y_,     // mixed form required when binding needed on the right
            _ => -1     // executed when null
        };
        WriteLine($"x = {x}");

        // explicit
        x = data switch {
            {X: var x_, Y: var y_} when x_ == 0 && y_ == 0 =>  0,
            {X: var x_, Y: var y_} when x_ == 1 && y_ == 1 =>  1,
            {X: var x_, Y: var y_} when x_ == 1 =>  2,
            {X: var x_, Y: var y_} when y_ == 1 =>  3,
            {X: var x_, Y: var y_} when (x_ > 10 && x_ < 100) && (y_ == 10 || y_ == 100) =>  4,
            {X: var x_, Y: var y_} =>  x_ + y_,     // catch all or default
            _ => -1     // executed when null
        };
        WriteLine($"x = {x}");
    }

    public static void test2_run() {
        //test2(new Data(0,0));
        //test2(new Data(1,1));
        //test2(new Data(1,2));
        //test2(new Data(2,1));
        //test2(new Data(11,10));
        //test2(new Data(11,11));
        //test2(new Data(99,100));
        //test2(new Data(99,99));
        //test2(null);
        test2(new Data(0,0,-1));
        test2(new Data(1,1,-1));
        test2(new Data(1,2,-1));
        test2(new Data(2,1,-1));
        test2(new Data(11,10,-1));
        test2(new Data(11,11,-1));
        test2(new Data(99,100,-1));
        test2(new Data(99,99,-1));
        test2(null);
    }

    public static void test3(DataS dataS) {
        WriteLine($"test3, given {dataS}");

        // implicit
        var x = dataS switch {
            {X: 0, Y: 0} =>  0,
            {X: 1, Y: 1} =>  1,
            {X: 1, Y: _} =>  2,
            {X: _, Y: 1} =>  3,
            {X: >10 and <100, Y: 10 or 100} =>  4,
            {X: var x_, Y: var y_} =>  x_ + y_,     // mixed form required when binding needed on the right
            //_ => -1     // never executed
        };
        WriteLine($"x = {x}");        

        // explicit
        x = dataS switch {
            {X: var x_, Y: var y_} when x_ == 0 && y_ == 0 =>  0,
            {X: var x_, Y: var y_} when x_ == 1 && y_ == 1 =>  1,
            {X: var x_, Y: var y_} when x_ == 1 =>  2,
            {X: var x_, Y: var y_} when y_ == 1 =>  3,
            {X: var x_, Y: var y_} when (x_ > 10 && x_ < 100) && (y_ == 10 || y_ == 100) =>  4,
            {X: var x_, Y: var y_} =>  x_ + y_,     // catch all or default
            //_ => -1     // never executed
        };
        WriteLine($"x = {x}");

    }

    public static void test3_run() {
        test3(new DataS(0,0));
        test3(new DataS(1,1));
        test3(new DataS(1,2));
        test3(new DataS(2,1));
        test3(new DataS(11,10));
        test3(new DataS(11,11));
        test3(new DataS(99,100));
        test3(new DataS(99,99));
        //test3(null);
    }

    public static void test4(DataC dataC) {
        WriteLine($"test4, given {dataC}");

        // implicit using properties
        var x = dataC switch {
            {A: 0, B: 0} =>  0,
            {A: 1, B: 1} =>  1,
            {A: 1, B: _} =>  2,
            {A: _, B: 1} =>  3,
            {A: >10 and <100, B: 10 or 100} =>  4,
            {A: var x_, B: var y_} =>  x_ + y_,     // mixed form required when binding needed on the right
            _ => -1     // when null
        };
        WriteLine($"x = {x}");   

        // explicit using deconstruct
        x = dataC switch {
            var (X_, Y, _) when X_ == 0 && Y == 0 => 0,     // when deconstruct is used, the variable name not required to be the same
            var (X, Y, _) when X == 1 && Y == 1 => 1,       // var may be extern to () or be within the ()
            (var X, var Y, var _) when X == 1 =>  2,
            (int X, int Y, int _) when Y == 1 =>  3,
            var (X, Y, _) when (X > 10 && X < 100) && (Y == 10 || Y == 100) =>  4,
            var (X, Y, _) =>  X + Y,     // catch all when non null
            _ => -1     // when null
        };
        WriteLine($"x = {x}");  
    }

    public static void test4_run() {
        test4(new DataC(0,0));
        test4(new DataC(1,1));
        test4(new DataC(1,2));
        test4(new DataC(2,1));
        test4(new DataC(11,10));
        test4(new DataC(11,11));
        test4(new DataC(99,100));
        test4(new DataC(99,99));
        test4(null);
    }

    public static void test5(DataC2 dataC2) {
        WriteLine($"test5, given {dataC2}");

        /*
        var x = DataC2 switch {     // DataC2 is a type and not possible in this context
            {XS:0, YS:0} => 0,
            {XS:1, YS:1} => 1,
            _ => -1
        };
        WriteLine($"x = {x}");  
        */

        // deconstruction context within (), flexible access, var outside and inside
        // property context within {}, no access to static properties, no var outside
        var x = dataC2 switch {
            {X:0, Y:0} => 0,
            {X:1, Y:1} => 1,
            {X: var x_, Y: var y_} when x_ > 0 && y_ > 0 => 2,
            {X: >100, Y: var y_} when y_ > 100 => 3,
            var (xsum, ysum) when xsum > 100 && ysum > 100 => 4,
            var (xsum, _) when xsum > 1000 => 5,
            var (_, ysum) when ysum > 1000 => 6,
            null => -1,
            _ => -1,
        };
        WriteLine($"x = {x}");
    }

    public static void test5_run() {
        test5(new DataC2(0,0));
        test5(new DataC2(1,1));
        test5(new DataC2(1,2));
        test5(new DataC2(2,1));
        test5(new DataC2(11,10));
        test5(new DataC2(11,11));
        test5(new DataC2(99,100));
        test5(new DataC2(99,99));
        test5(null);
    }

}
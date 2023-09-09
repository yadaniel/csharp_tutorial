using System;
using System.IO;

using static System.Math;
using static System.Console;

// dotnet clean
// dotnet build /p:StartupObject=App1
// dotnet run
//
// dotnet clean
// dotnet build /p:StartupObject=App2
// dotnet run

public class App1 {
    public static void Main() {
        Console.WriteLine("in main1");

        int? count;
        count = null;

        int cnt = count switch {
            null => 0,
            _ => (int)count
        };

        int cnt2;
        if(count is null) {
            cnt2 = 0;
        } else {
            cnt2 = (int)count;
            // cnt2 = count;
        }

        var o = 1;
        if (o is int i) {
            WriteLine($"{i}");
        } else {
            WriteLine("not int");
        }

        switch(o) {
            case int x:
                break;
            default:
                break;
        }

        int cnt3;
        switch(count) {
            case null:
                cnt3 = 0;
                break;
            case int x:
                cnt3 = x;
                break;
        }

        var v = 1 switch {
            1 => 2,
            2 => 3,
            _ => 0,
        };

        var v1 = 1 switch {
            1 => 2,
            2 => 3,
            var it => 0,
        };

        var v2 = 1 switch {
            1 => 2,
            2 => 3,
            var it => it,
            // _ => 0,      // das Muster kann nicht erreicht werden
        };

        var p1 = 100.0 switch {
            >100.0 or <0.0 => Double.NaN,
            var p => p,
        };
        WriteLine($"{p1}");


    }
}

public class App2 {
    public static void Main() {
        Console.WriteLine("in main2");
    }
}



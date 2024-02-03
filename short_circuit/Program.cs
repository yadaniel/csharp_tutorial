using System;
using static System.Console;

// c# short circuit evaluation &&,|| when using bool
// c# full evaluation &,| when using bool

// c# numerical evaluation &,| when int

class App {
    public static void Main() {
        Console.WriteLine("main");
        
        if(fpos(1) && fneg(-1)) {
            WriteLine("ok");
        } else {
            WriteLine("nok");
        }

        // short circuit evaluation
        if(fpos(-1) && fneg(-1)) {
            WriteLine("nok");
        } else {
            WriteLine("ok");
        }

        // full evaluation
        if(fpos(-1) & fneg(-1)) {
            WriteLine("nok");
        } else {
            WriteLine("ok");
        }

        // short circuit evaluation
        if(fpos(1) || fneg(-1)) {
            WriteLine("ok");
        } else {
            WriteLine("nok");
        }

        // full evaluation
        if(fpos(1) | fneg(-1)) {
            WriteLine("ok");
        } else {
            WriteLine("nok");
        }

        // full evaluation only => no short circuit feasible since the result depends an all conditions
        if(fpos(1) ^ fneg(-1) ^ fpos(1)) {
            WriteLine("ok");
        } else {
            WriteLine("nok");
        }

    }

    static bool fpos(int x) {
        WriteLine($"fpos({x})");
        return x >= 0;
    }

    static bool fneg(int x) {
        WriteLine($"fneg({x})");
        return x <= 0;
    }

}


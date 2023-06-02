using System;
using System.IO;
using System.Net;
using System.Data;
using System.Numerics;
using System.Drawing;
using System.Windows;
using System.Text;
using System.Text.RegularExpressions;
using System.Linq;
using System.Threading;
using System.Collections;
using System.Collections.Generic;

// dotnet add package BenchmarkDotNet
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

// 1 Main
// run in release
// dotnet run -c release
//
// dotnet clean
// dotnet build /p:StartupObject=App -c release
// dotnet run -c release

// >1 Main
// dotnet clean
// dotnet build /p:StartupObject=App
// dotnet run

// dotnet clean
// dotnet build /p:StartupObject=App2
// dotnet run

using static System.Console;
using static System.Math;

enum E0 {}
//
enum E1 : sbyte {}
enum E2 : short {}
enum E3 : int {}
enum E4 : long {}
//
enum E5 : byte { A = 255, B = 254, C}   // C equals A
enum E6 : ushort {}
enum E7 : uint {}
enum E8 : ulong {}
//
// enum E9 : System.Int8 {}
enum E10 : System.Int16 {}
enum E11 : System.Int32 {}
enum E12 : System.Int64 {}
// enum E13 : System.Int128 {}

[Flags]
enum FlagsFoo : System.UInt16 { B0 = 1, B1 = 2, B2 = 4, B3 = 8, B4 = 16, B5 = 32, B6 = 64, B7 = 128,
                                B8 = 256, B9 = 512, B10 = 1024, B11 = 2048, B12 = 4096, B13 = 8192, B14 = 16384, B16 = 32768,
                                ALL = 0xFFFF, LOWBYTE = 255}

public static class Utils {
    public static double d1 = 1.0;
    public static double d2 = 1.0d;
    public static float f = 1.0f;
}

[MemoryDiagnoser]
public class Test {
    [Benchmark]
    public void test1() {
        Thread.Sleep(100);
    }

    [Benchmark]
    public void test2() {
        int sum = 0;
        for(int i=0; i<100; i+=1) {
            sum += i;
        }
    }
}

public class App {
    public static void Main() {
        Console.WriteLine("App1");

        // WriteLine($"{E5.A.ToString()}");
        // WriteLine($"{E5.B.ToString()}");
        // WriteLine($"{E5.C.ToString()}");

        // var b = FlagsFoo.B0 | FlagsFoo.B1;
        // WriteLine($"{b.ToString()}");
        // b = FlagsFoo.LOWBYTE;
        // WriteLine($"{b.ToString()}");
        // b = FlagsFoo.LOWBYTE | FlagsFoo.B0;
        // WriteLine($"{b.ToString()}");
        // WriteLine($"{FlagsFoo.B0.ToString()}");

        var runner = BenchmarkRunner.Run<Test>();

    }

}

public class App2 {
    public static void Main(string[] _) {
        Console.WriteLine("App2");
    }
}


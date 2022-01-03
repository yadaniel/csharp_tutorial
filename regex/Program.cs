using System;
using static System.Console;
using System.Text;
using System.Text.RegularExpressions;
using RE = System.Text.RegularExpressions;
using REO = System.Text.RegularExpressions.RegexOptions;
// using REO = RE.RegexOptions;
// the order in which using-alias are written has no significance
// therefore REO cannot resolve, assumed RE not yet known

using System.Data;
using System.Collections;
using System.Collections.Generic;   // List<T>


// using as type alias
using Alphabet = Letters;
using C0 = N0.C0;

// defined later in nested namespace
namespace N0 {
    class C0 { static public int data = 0; }
}

// defined later
enum Letters {A, B, C, D, E, F, G, H, I, J, K, L, M, N, O, P, Q, R, S, T, U, V, W, X, Y, Z}

struct App {
    public static void Main(string[] args) {
        foreach(var arg in args) {
            WriteLine($"{arg}");
        }

        var app = new App(args);
        app.test_match0();
        app.test_match1();
        WriteLine(app);

    }

    void test_match0() {
        WriteLine("\n=== test_match0 === ");

        // string regex = @"(\w+)=(\d+)";           // \w contains \d
        string regex = @"([a-zA-Z_]+)=(\d+)";       // just letters and underscore
        Regex r0 = new Regex(regex);
        Regex r1 = new Regex(regex, RegexOptions.Compiled);
        Regex r2 = new Regex(regex, RegexOptions.Compiled | RegexOptions.IgnoreCase);
        Regex r3 = new Regex(regex, REO.Compiled | REO.IgnoreCase | REO.Multiline);
        Regex r4 = new Regex(regex, REO.Compiled | REO.IgnoreCase | REO.Multiline | REO.ExplicitCapture);
        // RegexOptions.None        = explcit no pararmeters
        // RegexOptions.RightToLeft = apply regex from right to left
        // RegexOptions.Singleline  = . does not match '\n'
        // RegexOptions.Multiline   = . matches '\n', ^ matches start of line, $ matches end of line
        // RegexOptions.ECMAScript  = javascript regex
        // RegexOptions.ExplicitCapture = groups only named or numbered group
        // RegexOptions.CultureInvariant = ignore culture setting
        // RegexOptions.IgnorePatternWhitespace   = delete all whitespaces from pattern

        string text = "foo=1234";
        Match m = r0.Match(text);
        if(m.Success) {
            WriteLine($"{regex} matched {text}");
            GroupCollection g = m.Groups;
            string matchstr = m.Groups[0].Value;
            string varstr = m.Groups[1].Value;
            string valstr = m.Groups[2].Value;
            WriteLine($"group[0]={matchstr}, group[1]={varstr}, group[2]={valstr}, groups={g.Count}");
            WriteLine();
        }

        // Match used for pythonic match and search
        text = "blabla foo=1234 blabla";
        m = r0.Match(text);
        if(m.Success) {
            WriteLine($"{regex} matched {text}");
            GroupCollection g = m.Groups;
            string matchstr = m.Groups[0].Value;
            string varstr = m.Groups[1].Value;
            string valstr = m.Groups[2].Value;
            WriteLine($"group[0]={matchstr}, group[1]={varstr}, group[2]={valstr}, groups={g.Count}");
            WriteLine();
        }

        // replace none
        text = "blabla 1234=1234 blabla";
        string text_replaced = r0.Replace(text, "");
        WriteLine($"{text} => {text_replaced}");

        // replace once
        text = "blabla foo=1234 blabla";
        text_replaced = r0.Replace(text, "");
        WriteLine($"{text} => {text_replaced}");

        // replace more than once
        text = "blabla foo=1234 blabla bar=5678 blabla";
        text_replaced = r0.Replace(text, "");
        WriteLine($"{text} => {text_replaced}");

    }

    void test_match1() {
        WriteLine("\n=== test_match1 === ");

        string regex = @"(?<varname>[a-zA-Z_]+)=(?<varvalue>\d+)";
        Regex r0 = new Regex(regex);

        string text = "blabla foo=1234 blabla";
        Match m = r0.Match(text);
        if(m.Success) {
            WriteLine($"{regex} matched {text}");
            GroupCollection g = m.Groups;
            string matchstr = m.Groups[0].Value;

            // indexed groups
            string varstr = m.Groups[1].Value;
            string valstr = m.Groups[2].Value;

            // named groups
            string varstr1 = m.Groups["varname"].Value;
            string valstr1 = m.Groups["varvalue"].Value;

            WriteLine($"group[0]={matchstr}, group[1]={varstr}, group[2]={valstr}, groups={g.Count}");
            WriteLine($"group[0]={matchstr}, group[\"varname\"]={varstr1}, group[\"varvalue\"]={valstr1}, groups={g.Count}");
            WriteLine();
        }

        // only first match
        text = "blabla foo=1234 bar=5678 blabla";
        m = r0.Match(text);
        if(m.Success) {
            WriteLine($"{regex} matched {text}");
            GroupCollection g = m.Groups;
            string matchstr = m.Groups[0].Value;

            // indexed groups
            string varstr = m.Groups[1].Value;
            string valstr = m.Groups[2].Value;

            // named groups
            string varstr1 = m.Groups["varname"].Value;
            string valstr1 = m.Groups["varvalue"].Value;

            WriteLine($"group[0]={matchstr}, group[1]={varstr}, group[2]={valstr}, groups={g.Count}");
            WriteLine($"group[0]={matchstr}, group[\"varname\"]={varstr1}, group[\"varvalue\"]={valstr1}, groups={g.Count}");
            WriteLine();
        }

        // first and second matches
        text = "blabla foo=1234 bar=5678 blabla";
        MatchCollection mx = r0.Matches(text);
        if(mx.Count > 0) {
            WriteLine($"{text} matches count => {mx.Count}");

            int cnt = 0;
            foreach(Match mi in mx) {
                cnt += 1;
                WriteLine($"[{cnt}]: {regex} matched {text}");
                GroupCollection g = mi.Groups;
                string matchstr = mi.Groups[0].Value;
                // indexed groups
                string varstr = mi.Groups[1].Value;
                string valstr = mi.Groups[2].Value;
                // named groups
                string varstr1 = mi.Groups["varname"].Value;
                string valstr1 = mi.Groups["varvalue"].Value;
                WriteLine($"group[0]={matchstr}, group[1]={varstr}, group[2]={valstr}, groups={g.Count}");
                WriteLine($"group[0]={matchstr}, group[\"varname\"]={varstr1}, group[\"varvalue\"]={valstr1}, groups={g.Count}");
                WriteLine();
            }

        }

    }

    List<string> argsList;
    Dictionary<int, string> argsDict;
    int? argNums = null;   // int option, option<int>, Maybe int

    // private constructor => instantiate from public static method
    App(string[] args) {
        argNums = args.Length;
        int cnt = 0;
        argsList = new List<string>();
        argsDict = new Dictionary<int, string>();
        foreach(var arg in args) {
            argsList.Add(arg);
            argsDict[cnt] = arg;
            cnt += 1;
        }
    }

    // virtual methods must be public => try protected
    // access modifier cannot be changed when overriding => back to public
    public override string ToString() {
        return $"argNums = {argNums}";
    }
}


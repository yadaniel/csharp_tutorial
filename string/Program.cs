using System;
using System.Text;
using System.Text.RegularExpressions;
using static Utils;

static class Utils {

    public static void test_join() {
        string input = Console.ReadLine();
        Console.WriteLine(input);
        Console.Write(input);
    }

    public static void test_regex_replace() {
        string txt = Regex.Replace("abcd1234efgh", @"\d", "0");
        Console.WriteLine(txt);
        txt = Regex.Replace("abcd1234efgh", @"\d", "");
        Console.WriteLine(txt);
    }

    public static void test_regex_match() {
        Match m = Regex.Match("((1234)a(4321)b(8931))", @"\d{4}");
        if(m.Success) {
            for(int i=0; i<m.Groups.Count; i++ ) {
                Group grp = m.Groups[i];
                Console.WriteLine(grp);
            }
            foreach(Group grp in m.Groups) {
                Console.WriteLine($"{grp}=={grp.Value}");
                foreach(Capture c in grp.Captures) {
                    Console.WriteLine($"\t{c}");
                }
            }
        }
        string txt = string.Empty;
        Console.WriteLine(txt);
    }

    public static void test_regex_match2() {
        // Match m = Regex.Match("foo=1234", @"(((\w+)=(\d+)))");
        // Match m = Regex.Match("foo=1234", @"((\w+)=(\d+))");
        Match m = Regex.Match("foo=1234", @"(\w+)=(\d+)");
        if(m.Success) {
            foreach(Group grp in m.Groups) {
                Console.WriteLine($"{grp}=={grp.Value}");
                foreach(Capture c in grp.Captures) {
                    Console.WriteLine($"\t{c}");
                }
            }
        }
    }

    public static void test_regex_match3() {
        Match m = Regex.Match("foo=1234", @"(?<var>\w+)=(?<val>\d+)");
        if(m.Success) {
            // access by name
            Console.Write($"{m.Groups["var"].Value} variable set ");
            Console.Write($"to value {m.Groups["val"].Value}" + Environment.NewLine);
            Console.WriteLine($"group 0 => {m.Groups["0"].Value}");     // "0" implicit name
            Console.WriteLine($"group 1 => {m.Groups["1"].Value}");     // "1" empty, not "var"
            Console.WriteLine($"group 2 => {m.Groups["2"].Value}");     // "2" empty, not "val"
            // access by index
            Console.Write($"{m.Groups[1].Value} variable set ");
            Console.Write($"to value {m.Groups[2].Value}" + Environment.NewLine);
            Console.WriteLine($"group 0 => {m.Groups[0].Value}");
        }
        if(m.Success) {
            Group vars = m.Groups["var"];
            Group val = m.Groups["val"];
            Group v = m.Groups["v"];    // no exception
            Group w = m.Groups["w"];
            if(v == null) {
                Console.WriteLine("no such group");
            } else {
                Console.WriteLine($"group v = {v} with index {v.Index}");
                Console.WriteLine($"group w = {w} with index {w.Index}");
            }
        }
    }

    public static void test_regex_match4() {
        Match m = Regex.Match(" foo = 1234; // some comment", @"(?<varval>(?<var>\w+)\s*=\s*(?<val>\d+))\s*;\s*//(?<comment>.*?)$", RegexOptions.IgnoreCase);
        if(m.Success) {
            Console.WriteLine("match");
            Console.WriteLine($"{m.Groups["0"]}");
            Console.WriteLine($"{m.Groups["varval"]}");
            Console.WriteLine($"{m.Groups["var"]}");
            Console.WriteLine($"{m.Groups["val"]}");
            Console.WriteLine($"{m.Groups["comment"]}");
            Console.WriteLine($"{m.Groups["comment"].ToString().Trim(' ')}");
            // Index Property
            Console.WriteLine($"|{m.Groups["0"]}| with index {m.Groups["0"].Index}");
        } else {
            Console.WriteLine("no match");
        }
     }

    public static void test_regex_match5() {
        Match m = Regex.Match("Data test text", @"^data\b\s*\b(\w+)\b\s*\b(\w+)$",
                RegexOptions.IgnoreCase |
                RegexOptions.Compiled |
                // RegexOptions.ECMAScript |
                // RegexOptions.Singleline
                RegexOptions.Multiline
                // RegexOptions.None
                );
        // RegexOptions.None
        // RegexOptions.Compiled
        // RegexOptions.ECMAScript
        // RegexOptions.Ignorecase
        // RegexOptions.Multiline
        // RegexOptions.Singleline
        // RegexOptions.RightToLeft
        // RegexOptions.ExplicitCapture
        // RegexOptions.CultureInvariant
        // RegexOptions.IgnorePatternWhitespace
        if(m.Success) {
            Console.WriteLine($"match with {m.Groups.Count} groups");
            Group all = m.Groups[0];
            Group word1 = m.Groups[1];
            Group word2 = m.Groups[2];
            Console.WriteLine($"{all.Value} at {all.Index}");
            Console.WriteLine($"{word1.Value} at {word1.Index}");
            Console.WriteLine($"{word2.Value} at {word2.Index}");
        }  else {
            Console.WriteLine("no match");
        }
    }
}

class Program {
    static void Main(string[] args) {
        // test_regex_replace();
        // Utils.test_regex_match();
        // Utils.test_regex_match3();
        // Utils.test_regex_match4();
        Utils.test_regex_match5();
    }
}


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;
using System.ComponentModel;

namespace WireCodingChallenge.Lib
{
    public static class ProcessFile
    {

        public static void Out( string inputPath, string outPath )
        {
            var articlePath = Path.Combine(inputPath, Settings.Get("Article"));
            var wordsPath = Path.Combine(inputPath, Settings.Get("Words"));

            if (!File.Exists(articlePath) || !File.Exists(wordsPath))
                return;
             

            var strOut = GenerateOutPut(File.ReadAllText(articlePath), File.ReadAllText(wordsPath).Replace("\r", ""));

            // create output directory if not existing
            if ( !Directory.Exists(outPath))
            {
                Directory.CreateDirectory(outPath);
            }
           
            var outFile = Path.Combine(outPath, Settings.Get("Output"));
           

            File.WriteAllText(outFile, strOut);

        }

        public static string GenerateOutPut(string  articleStr, string wordsStr)
        {
            string[] articles = Regex.Split(articleStr.Replace("Mr.", "|").Replace("Mrs.", "~"), @"(?<=[\.!\?])\s+");

            for (int i = 0; i < articles.Length; i++)
            {
                articles[i] = articles[i].Replace("|", "Mr").Replace("~", "Mrs");
            }

            string[] words = wordsStr.Split('\n');

            var numI = 97;
            var padNumCnt = 1;

            StringBuilder sb = new StringBuilder();

            foreach (string word in words)
            {
                var str = ((char)numI).ToString();

                if (padNumCnt > 1)
                    str = str.PadRight(padNumCnt, (char)numI);
                sb.AppendLine(String.Format(" {0}. {1} {2}", str, word, CountWords(articles, word) ));
                numI++;
                if (numI > 122)
                {
                    numI = 97;
                    padNumCnt++;
                }
            }
 
            return sb.ToString();
        }

        

        private static string CountWords(string[] lines, string word)
        { 
            var cnt = 0;
            var lnNo = new List<int>();
            for (int i = 0; i < lines.Length; i++)
            {
                var wc = WordCnt(lines[i], word);
                cnt += wc;
                if (wc > 0)
                {
                    for (int j = 1; j <= wc; j++)
                    {
                        lnNo.Add(i + 1);
                    }
                }
            }
            StringBuilder sb = new StringBuilder();
            foreach (var l in lnNo)
            {
                sb.Append(l + ",");
            }
            var lnStr = sb.ToString();
            if (!string.IsNullOrWhiteSpace(lnStr))
                lnStr = lnStr.Substring(0, lnStr.Length - 1);
            return "{" + cnt +  ":" + lnStr + "}";
        }

        private static int WordCnt(string line, string searchWord)
        {
            var ln = line.EndsWith(".") ? line.Substring(0, line.Length - 1) : line;
            var srchCriteria = searchWord.EndsWith(".") ? searchWord.Substring(0, searchWord.Length - 1) : searchWord;
            string[] source = ln.ToLower().Split(new char[] { '?', '!', ' ', ';', ':', ',' }, StringSplitOptions.RemoveEmptyEntries);
 
            var matchQuery = from word in source
                             where word.ToLowerInvariant() == srchCriteria.ToLowerInvariant()
                             select word;
            return matchQuery.Count();
        } 
    }
}

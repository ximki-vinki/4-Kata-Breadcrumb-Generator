using System;
using System.Linq;
using System.Text;
using static System.Net.WebRequestMethods;

namespace _4_Kata_Breadcrumb_Generator
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string url = "http://www.agcpartners.co.uk/pictures/uber-insider-skin-immunity/login.htm#team?rank=recent_first&hide=sold";
            string b = " : ";
            string[] wordsIgnore = new string[] { "the", "of", "in", "from", "by", "with", "and", "or", "for", "to", "at", "a" };
            Console.WriteLine(Kata.GenerateBC(url, b));
        }

        public class Kata
        {

            public static string GenerateBC(string url, string separator)
            {
                string[] wordsIgnore = new string[] { "the", "of", "in", "from", "by", "with", "and", "or", "for", "to", "at", "a" };
                UriBuilder UB = new UriBuilder(url);
                Uri uri = new Uri(UB.ToString());
                string[] urlClear = uri.Segments.Where(x => !x.Contains("index.")).Skip(1).ToArray();
                StringBuilder SB = new StringBuilder();

                if (urlClear.Length == 0)
                {
                    SB.Append("<span class=\"active\">HOME</span>");
                }
                else
                {
                    SB.Append("<a href=\"/\">HOME</a>");
                    string addBefore = "";
                    for (int i = 0; i < urlClear.Length; i++)
                    {
                        string addOut = urlClear[i].Split('.')[0].TrimEnd('/');
                        string addIn = addOut.Replace('-', ' ');
                        if (addOut.Length > 30) addIn = string.Concat(addOut.Split('-').Where(x => !wordsIgnore.Contains(x)).Select(x => x[0]).ToArray());
                        if (i != urlClear.Length - 1) SB.Append(separator + $"<a href=\"/{addBefore + addOut}/\">{addIn.ToUpper()}</a>");
                        else SB.Append(separator + $"<span class=\"active\">{addIn.ToUpper()}</span>");
                        addBefore = urlClear[i];
                    }
                }
                return SB.ToString();

            }

        }

    }
}

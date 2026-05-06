using System.Data;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;


namespace SAWWiki
{
    public class WikiParser
    {

        public static string GenerateWikiText(string input)
        {
            return GenerateWikiText(input, null);
        }

        public static string GenerateWikiText(string input, DataTable Links)
        {
            Regex r; //mostly just use the static replace method except in spots

            //to get a number of things to work right, must insert a line break in the beginning
            input = input.Insert(0, "\n\n");

            //handle HTML characters
            input = input.Replace("<", "&lt;").Replace(">", "&gt;");

            //reformat /r/n into /n
            input = input.Replace("\r\n", "\n");

            //The fancy attempt at a url regex - worked only partly
            //input = Regex.Replace(input, @"((http|ftp|news|mailto)[\://]*\w+\.\w+\.\w+[/\w+]*[.\w+]*(\?\w+[=]?)?[^\.\]\""])", @"<a href=$&>$&</a>");

            //named hyperlinks
            input = Regex.Replace(input, @"\{([^\|]+)\|([^}]+)\}", @"<a href=$2>$1</a>", RegexOptions.Compiled);
            //input = Regex.Replace(input, @"\{((?:\w+\s?)+)\|(.+)\}", @"<a href=$2>$1</a>", RegexOptions.Compiled); - this sent the process into an infinite loop with some text!

            //These are the most expensive operations and we see a small performance gain from setting the compiled flag 
            input = Regex.Replace(input, @"([^=])((?:http|ftp|news|mailto)[\://][^\s^\]]+)", @"$1<a href=$2>$2</a>", RegexOptions.Compiled);
            input = Regex.Replace(input, @"<a href=(.*)(?=[.!?]>).*(?=[.!?]</a>)", @"<a href=$1>$1</a>", RegexOptions.Compiled);  //if it was the end of the sentence remove the trailing punctuation

            //manual wiki links
            input = Regex.Replace(input, @"[\[](\w+)\]", @"<a href=default.aspx?page=$1>$1</a>", RegexOptions.Compiled);

            //automatic wiki links
            if (Links != null)
            {
                foreach (DataRow row in Links.Rows)
                {
                    //was a lot faster this way than constantly reassigning a string value
                    r = new Regex(@"([\s\n])(" + row["PageName"].ToString() + @"\b)", RegexOptions.IgnoreCase);
                    if (r.IsMatch(input))
                    {
                        input = Regex.Replace(input, @"([\s\n])(" + row["PageName"].ToString() + @"\b)", "$1<a href=default.aspx?page=" +
                            HttpUtility.UrlEncode(row["PageName"].ToString()) + ">$2</a>", RegexOptions.IgnoreCase);
                    }
                }
            }

            //file attachment
            //input = Regex.Replace(input, @"\^\^(.+):([a-fA-F0-9\-]{36})\^\^", @"<a href=""Javascript:ShowFilePopup('$2','$1')"">$1</a>"); 
            input = Regex.Replace(input, @"\^\^(.+)\|([a-fA-F0-9\-]{36})\^\^", @"<a href=""ShowFile.aspx?id=$2"">$1</a>", RegexOptions.Compiled);

            //image attachment
            input = Regex.Replace(input, @"\^([a-fA-F0-9\-]{36})\^", @"<img src=""ShowFile.aspx?id=$1""></img>", RegexOptions.Compiled);

            //center
            input = Regex.Replace(input, @"^\s?=(.*)=", "<center>$1</center>", RegexOptions.Multiline | RegexOptions.Compiled);

            //h1 
            input = Regex.Replace(input, @"^\s?!{1}([^!].+)$", @"<H1 id=""$1"">$1</H1>", RegexOptions.Multiline | RegexOptions.Compiled);

            //h2
            input = Regex.Replace(input, @"^\s?!{2}([^!].+)$", @"<H2 id=""$1"">$1</H2>", RegexOptions.Multiline | RegexOptions.Compiled);

            //h3
            input = Regex.Replace(input, @"^\s?!{3}([^!].+)$", @"<H3 id=""$1"">$1</H3>", RegexOptions.Multiline | RegexOptions.Compiled);

            //Translate 2 + spaces into formatted spaces
            input = input.Replace("  ", "&nbsp;&nbsp;");

            //Horizontal rule
            input = Regex.Replace(input, @"\-{4,}", "<hr>", RegexOptions.Compiled);

            //Ordered Lists - up to 4 deep
            input = Regex.Replace(input, @"((?:\n\s?#+.*)+)", "\n<ol>$1\n</ol>", RegexOptions.Multiline);
            input = Regex.Replace(input, @"((?:\n\s?#{2,}.*)+)", "\n<ol>$1\n</ol>", RegexOptions.Multiline);
            input = Regex.Replace(input, @"((?:\n\s?#{3,}.*)+)", "\n<ol>$1\n</ol>", RegexOptions.Multiline);
            input = Regex.Replace(input, @"((?:\n\s?#{4,}.*)+)", "\n<ol>$1\n</ol>", RegexOptions.Multiline);

            //Unordered Lists - up to 4 deep       
            input = Regex.Replace(input, @"((?:\n\s?\++.*)+)", "\n<ul>$1\n</ul>");
            input = Regex.Replace(input, @"((?:\n\s?\+{2,}.*)+)", "\n<ul>$1\n</ul>");
            input = Regex.Replace(input, @"((?:\n\s?\+{3,}.*)+)", "\n<ul>$1\n</ul>");
            input = Regex.Replace(input, @"((?:\n\s?\+{4,}.*)+)", "\n<ul>$1\n</ul>");

            input = Regex.Replace(input, @"^\s?[+|#]{1,4}(.*)$", "<li>$1</li>", RegexOptions.Multiline);  //convert to <li> tags

            //purge newline characters
            input = Regex.Replace(input, @"\n<li>(.*)\n?</li>\n", "<li>$1</li>", RegexOptions.Compiled);
            //input = Regex.Replace(input, @"\n(<ul>|<ol>)", "$1", RegexOptions.Compiled);
            input = Regex.Replace(input, @"\n(</?(?:ul|ol)>)\n?", "$1", RegexOptions.Compiled);

            //custom formatter processing
            input = Regex.Replace(input, @"&lt;&lt;(\w+)&gt;&gt;(.*?)&lt;&lt;/\1&gt;&gt;", @"<span class=""$1"">$2</span>", RegexOptions.Compiled);

            //handle basic formatting and line breaks
            input = CustomWikiParse(input);
            input = CustomLineBreakParse(input);

            return input;
        }

        /// <summary>
        /// Handles line break characters
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string CustomLineBreakParse(string input)
        {
            StringBuilder sb = new StringBuilder();
            bool ParaOpen = false;
            bool changed = false;
            for (int i = 0; i < input.Length - 1; i++)
            {
                changed = false;

                //try to wrap most of the text in paragraphs
                if (!changed && (input.Substring(i, 2) == "\n\n"))
                {
                    if (!ParaOpen)
                    {
                        sb.Append("<p>");
                        ParaOpen = true;
                    }
                    else
                    {
                        sb.Append("</p><p>");
                    }
                    i++;
                    changed = true;
                }

                if (!changed && (input.Substring(i, 1) == "\n"))
                {
                    sb.Append("<br />");
                    changed = true;
                }

                //fallthrough
                if (!changed) sb.Append(input.Substring(i, 1));

            }

            //add the last character
            sb.Append(input.Substring(input.Length - 1, 1));

            if (ParaOpen) sb.Append("</p>");

            return sb.ToString();

        }

        /// <summary>
        /// Replaces regular expressions for custom wiki formatting
        /// </summary>
        /// <param name="input"></param>
        /// <returns>formatted wiki text</returns>
        public static string CustomWikiParse(string input)
        {
            StringBuilder sb = new StringBuilder();

            bool StrongOpen = false;
            bool BoldOpen = false;
            bool EmOpen = false;
            bool UnderOpen = false;
            int i = 0;
            for (; i < input.Length - 1; i++)
            {
                if (i < input.Length - 2)
                {
                    switch (input.Substring(i, 3))
                    {
                        case "'''":
                            if (!StrongOpen)
                            {
                                sb.Append("<strong>");
                                StrongOpen = true;
                            }
                            else
                            {
                                sb.Append("</strong>");
                                StrongOpen = false;
                            }
                            i += 3;
                            break;
                    }
                }

                switch (input.Substring(i, 2))
                {
                    case "**":
                        if (!BoldOpen)
                        {
                            sb.Append("<b>");
                            BoldOpen = true;
                        }
                        else
                        {
                            sb.Append("</b>");
                            BoldOpen = false;
                        }
                        i++;
                        break;
                    case "__":
                        if (!UnderOpen)
                        {
                            sb.Append("<u>");
                            UnderOpen = true;
                        }
                        else
                        {
                            sb.Append("</u>");
                            UnderOpen = false;
                        }
                        i++;
                        break;
                    case "''":
                        if (!EmOpen)
                        {
                            sb.Append("<em>");
                            EmOpen = true;
                        }
                        else
                        {
                            sb.Append("</em>");
                            EmOpen = false;
                        }
                        i++;
                        break;
                    default:
                        sb.Append(input.Substring(i, 1));
                        break;
                }
            }

            //add the last character
            if (i < input.Length) sb.Append(input.Substring(input.Length - 1, 1));

            return sb.ToString();

        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace RssReader
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var reader = new RssReader("http://rss.cnn.com/rss/edition.rss");
            reader.LoadStories();
            var stories = reader.GetTopStories();

            Searcher searcher = new Searcher(args);

            foreach (var story in stories.Where(r =>
                {
                    try
                    {
                        searcher.SearchItHard(r.Title);
                        return true;
                    }
                    catch(Exception e)
                    { return false; }
                }))
            {
                story.ToString();
            }

            Console.ReadLine();
        }
    }

    public class RssStory
    {

        ///
        /// JORDAN GOULET
        /// 
        /// Make all variables public because there is no needed validations for the properties of RssStory objects
        /// this means we can dispose of the private variables
        ///

        public int Index { get; set; }

        //private readonly string _title;
        public string Title { get; set; }

       // private readonly string _description;
        public string Description { get; set; }

        //public string _link;
        public Uri Link { get; set; }

        public DateTime Published { get; set; }

        Regex regex = new Regex("(?<scheme>[a-z]{3,5})://(?<host>[a-z0-9_-]+(.[a-z0-9_-]+)*)/(?<path>.*)");

        ///
        /// JORDAN GOULET
        /// 
        /// Every story we add will always have a published date as well as an index, so I am going 
        /// to add this as parameters as necessary to initiate an RssStory objects
        /// 
        public RssStory(string title, string description, string link, DateTime publishedDate, int index)
        {
            Title = title;
            Description = description;
            //_link = link;

            ///
            /// JORDAN GOULET
            /// 
            /// Not sure what regex is being matched here.
            /// Why was it commented out and why need a regex in the first place?
            /// I've uncommented to create a URI using the UriBuilder but must discuss the regex 
            ///
            Match match = regex.Match(link);
            Link = new UriBuilder(match.Groups["scheme"].Value, match.Groups["host"].Value, -1, match.Groups["path"].Value).Uri;

            ///
            /// JORDAN GOULET
            /// 
            /// Even though randomly picking a integer from 0 - 9999, it's probably going to avoid collisions, but it's not 100% 
            /// So, I am going to assign an integer while looping that iterates while adding stories to the story list
            /// which you could never get the same index twice
            ///
            // 10,000 should be sufficient to avoid collisions.
            //_index = new Random().Next(10000);

            ///
            /// JORDAN GOULET
            /// 
            /// Added parementers initiation
            ///

            Published = publishedDate;
            Index = index;
        }

        public override string ToString()
        {
            Console.WriteLine("Title: " + Title);
            Console.WriteLine("Description: " + Description);
            Console.WriteLine("Published On: " + Published);
            Console.WriteLine("Link: " + Link);
            Console.WriteLine();

            return string.Empty;
        }
    }
}

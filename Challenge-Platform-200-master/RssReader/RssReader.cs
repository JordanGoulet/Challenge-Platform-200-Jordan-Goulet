using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace RssReader
{
    public class RssReader
    {
        private const string FEED_ADDRESSS = "http://feeds.bbci.co.uk/news/rss.xml";

        private Dictionary<int, RssStory> _storyLookup;
        private ThreadSafeQueue _storyIndex;
        private List<RssStory> _stories;
        private XElement _feed;

        // Constructors
        #region Constructors

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="feedAddress"></param>
        public RssReader(string feedAddress)
        {
            _feed = XElement.Load(FEED_ADDRESSS).Element("channel");
            _storyLookup = new Dictionary<int, RssStory>();
            _storyIndex = new ThreadSafeQueue();
            _stories = new List<RssStory>();
        }

        #endregion

        /**********************
        *  ALL STORIES MUST BE
        *  LOADED PRIOR TO USE.
        ***********************/
        public void LoadStories()
        {
            ///
            /// JORDAN GOULET
            /// 
            /// This is the new way I'm setting the unique index for the stories to never have two or more stories with the same index
            ///
            int iterator_index = 0;
            foreach (var item in _feed.Elements("item"))
            {
                //_stories.Add(new RssStory(item.Element("title").Value, item.Element("description").Value, item.Element("link").Value));
                //_stories.Last().Published = DateTime.Parse(item.Element("pubDate").Value);

                ///
                /// JORDAN GOULET
                /// 
                /// Adding the newly RssStory object with the new iterator
                ///
                _stories.Add(new RssStory(item.Element("title").Value, item.Element("description").Value, item.Element("link").Value, DateTime.Parse(item.Element("pubDate").Value),iterator_index));


                ///
                /// JORDAN GOULET
                /// 
                /// This doesn't make any sense, you've assigned a random integer to a story
                /// although likely, all stories should have unique indices, however, there 
                /// is a small probability that two unique stories may share the same index,
                /// and what you are doing here is remove a story that shares the index
                /// This does not ensure that two stories are the same
                ///
                /*if (_storyLookup.ContainsKey(_stories.Last().Index))
                {
                    // Story is already in the list.
                    // Not sure why this happens, but we should remove it.
                    _stories.Remove(_stories.Last());
                }
                else
                {
                    //_storyLookup.Add(_stories.Last().Index, _stories.Last());
                }*/
                _storyLookup.Add(_stories.Last().Index, _stories.Last());
                iterator_index += 1;
            }

        }

        public IEnumerable<RssStory> GetTopStories()
        {
            var latestStories = _stories.OrderBy(s => s.Published).Reverse();
            var recentStories = new List<RssStory>();

            // Get the five most recent stories from the feed.
            ///
            /// JORDAN GOULET
            /// 
            /// This is not taking the top five stories, it is only taking the published stories within thirty minutes
            /// it also rescrambles to order of the stories.
            /// So here, I will create a for loop that will simply grab the top 5 stories of the latestStories List
            /// 
            /// Also the while loop is really interesting, and have never seen something like this done before
            /// I can't explain why this is wrong so I'll leave it but it would be interesting to discuss
            /// One point I can make is what if the try-catch catches an exception other than BACKING_LIST is empty, it is now going to break the loop
            /// and assume that the loop is over
            ///
            /*var fiveLatestStoryIndices = latestStories.TakeWhile(s => s.Published < DateTime.Now.AddMinutes(30)).Select(s => s.Index);

            foreach (int index in fiveLatestStoryIndices)
            {
                _storyIndex.Enqueue(index);
            }

            while (true)
            {
                try
                {
                    recentStories.Add(_storyLookup[_storyIndex.Pop()]);
                }
                catch (Exception)
                {
                    // Queue is empty.
                    break;
                }
            }*/

            int iterator_five_max = 0;
            foreach (RssStory story in latestStories)
            {
                _storyIndex.Enqueue(story.Index);
                iterator_five_max += 1;

                if (iterator_five_max >= 5)
                {
                    break;
                }
            }

            while (true)
            {
                try
                {
                    recentStories.Add(_storyLookup[_storyIndex.Pop()]);
                }
                catch (Exception)
                {
                    // Queue is empty.
                    break;
                }
            }
            ///
            /// JORDAN GOULET
            /// 
            /// Return the recent stories in the correct order
            ///
            return recentStories.OrderBy(s => s.Published).Reverse();
        }
    }
}

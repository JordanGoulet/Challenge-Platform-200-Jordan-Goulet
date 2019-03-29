# Challenge-Platform-200-Jordan-Goulet
IGLOO Challenge Platform

FIRST TIME GOING THROUGH THE CODE:

Program starts with creating a variable reader which is an RssReader object that will go to the specified website and will prepare to populate its properties (e.i. feed, storylookup, storyindex and stories), where

- feed: and XML file containing information about the site with the items of interest (the "channel", which is an element containing three required child elements, "title", "link", "description", and other non required child elements such as image, generator and so forth. Channel elements can have 1 or more item tags, which also require the following child elements: title, link, description, as well as other non required elements, such as pubDate, media and so forth.

- storyLookup: a declaration of a list with multiparameter, one being int, and the other RssStory object

- storyIndex: initiates a BACKING_LIST for the ThreadSafeQueue

- stories: initiates a list of RssStory objects

Next, it will load the stories from the feed going through a foreach loop, iterating through each item of the feed and 

Loop starts with creating an RssStory object and adding it to the stories list, while adding "Published" to the newly added item to the stories list. It will obtain the title, the description, the link and create this random index between 0 and 9999. 

** Problems I see so far:
	- trying to use this regex but decided against it, but probably to build
	- this random number isn't good

We've loading stories (it seems random at this point, only three stories this time, check if more are loaded)

Now we are initiating and declaring a variables "stories" with the "top stories":
	- ordering list by published date
	- creating a recentStories object which is a new list containing RssStory objects
	- it grabs the 5 latest stories, which not sure how it only knows its 5
	- loop through each of the last 5 stories and add the index to the BACKING_LIST and creating a new _lock 	object
	- now its grabbing the last index from the BACKING_LIST and returning it, while removing it from the 	BACKING_LIST
	- returns top 5 recentStories from oldest to newest (which I'm assuming needs to be newest to oldest)

** NOT A FAN OF THE WHILE TRUE AND BREAK TACTIC IN "GETTOPSTORIES" (but maybe its fine)

Now a searcher object is created, creating an empty string initially because the "args" is an empty. If args isnt empty, it doesn't filter anything because it will always return True, need to fix. 

Now, doing another foreach loop that will go through the stories that have a title, and if it does, it prints to screen the title, description, date published and link

Then it will print out all the values and have a readline so the command prompt stays open.

And then done





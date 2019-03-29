using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RssReader
{
    /// <summary>
    /// Ensures that only stuff being searched for is returned.
    /// </summary>
    public class Searcher
    {
        public bool not = false;
        public String s = "";

        public Searcher(string[] args)
        {
            try
            {
                ///
                /// JORDAN GOULET
                /// 
                /// should start with args[0] and not args[1] because no where is arg[0] being used
                /// 
                ///
                s = args[0] ?? " ";
                not = args[1] == "true" ? true : args[1] == "True" ? true : args[1] == "TRUE" ? true : false;
            }
            ///
            /// JORDAN GOULET
            /// 
            /// Changed this to IndexOutOfRangeException because ArgumentOutOfRangeException doesn't catch the index of args when it doesn't exist
            /// 
            ///
            catch (IndexOutOfRangeException exception)
            {
                s = " ";
                not = false;
            }
            catch (Exception exception)
            {
                // more detailed message when something goes wrong in debug mode
#if DEBUG
                Console.WriteLine("Could not parse search parameters. There are " + args.Length + " parameters");
#else
                Console.WriteLine(args[0]);
#endif
                s = " ";
                not = false;
            }
            finally
            {
                s = s != " " ? s : "";
            }
        }

        /// <summary>
        /// Check and see if input matches conditions.
        /// </summary>
        /// <param name="toSearch"></param>
        /// <returns>Returns true or throws an exception.</returns>
        public bool SearchItHard(string toSearch)
        {
            ///
            /// JORDAN GOULET
            /// 
            /// This is always returning matches to be true, I've modified it to return true 
            /// when needs to be true and false when searching is not required or input not found
            ///
            //bool matches = (toSearch.Contains(s) && !not) || (!toSearch.Contains(s) && not);
            bool matches = (toSearch.Contains(s) && not) || (!not);


            if (matches == false)
            {
                throw new Exception("Doesn't match!");
            }

            return true;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackernewsScraper.Model
{
    /// <summary>
    /// Model of an hacker news post
    /// </summary>
    public class Post
    {
        // Title
        public string Title { get; set; }
        // Uri
        public string Url { get; set; }
        // Author
        public string By { get; set; }
        // Points
        public int Score { get; set; }
        // Ids of comments
        public IList<int> Kids { get; set; }
    }
}

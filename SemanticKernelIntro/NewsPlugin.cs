using Microsoft.SemanticKernel;
using SimpleFeedReader;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SemanticKernelIntro
{
    public class NewsPlugin
    {
        [KernelFunction("get_news")]
        [Description("Get the latest news")]
        [return: Description("A list of current news articles")] 
        public List<FeedItem> GetNews(Kernel kernel, string category)
        {
            var reader= new FeedReader();
            return reader.RetrieveFeed($"https://rss.nytimes.com/services/xml/rss/nyt/{category}.xml").Take(5).ToList();
        }
    }
}

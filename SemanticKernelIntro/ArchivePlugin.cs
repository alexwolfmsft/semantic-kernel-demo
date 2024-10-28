using Microsoft.SemanticKernel;
using SimpleFeedReader;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SemanticKernelIntro
{
    internal class ArchivePlugin
    {
        [KernelFunction("archive_data")]
        [Description("Saves data to a file on your computer.")]
        public async Task WriteData(Kernel kernel, string fileName, string data)
        {
            var reader = new FeedReader();
            await File.WriteAllTextAsync($@"C:\Users\alexwolf\source\repos\SemanticKernelIntro\{fileName}.txt", data);
        }
    }
}

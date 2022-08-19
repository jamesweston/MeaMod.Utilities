using System;
using System.Collections.Generic;
using System.Text;

namespace MeaMod.Utilities.Zip
{
    public class ZipProgress
    {
        public ZipProgress(int total, int processed, string currentItem)
        {
            Total = total;
            Processed = processed;
            CurrentItem = currentItem;
        }
        public int Total { get; }
        public int Processed { get; }
        public string CurrentItem { get; }
    }
}

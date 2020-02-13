﻿using HtmlAgilityPack;
using System.Collections.Generic;

namespace SimpleTelegramBot
{
    public abstract class ScrappedObjectBuilder
    {
        private List<KeyValuePair<string, string>> elements;
        public abstract void FilterPart(HtmlNode node);
        public abstract ScrappedObject buildScrappedObject();
    }
}
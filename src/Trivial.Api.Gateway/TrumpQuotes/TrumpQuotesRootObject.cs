using System;

namespace Trivial.Api.Gateway.TrumpQuotes
{
    public class TrumpQuotesRootObject
    {
        public DateTime appeared_at { get; set; }
        public DateTime created_at { get; set; }
        public string quote_id { get; set; }
        public string[] tags { get; set; }
        public DateTime updated_at { get; set; }
        public string value { get; set; }
        public _Links _links { get; set; }
        public _Embedded _embedded { get; set; }
    }

    public class _Links
    {
        public Self self { get; set; }
    }

    public class Self
    {
        public string href { get; set; }
    }

    public class _Embedded
    {
        public Author[] author { get; set; }
        public Source[] source { get; set; }
    }

    public class Author
    {
        public DateTime created_at { get; set; }
        public object bio { get; set; }
        public string author_id { get; set; }
        public string name { get; set; }
        public string slug { get; set; }
        public DateTime updated_at { get; set; }
    }

    public class Source
    {
        public DateTime created_at { get; set; }
        public object filename { get; set; }
        public string quote_source_id { get; set; }
        public object remarks { get; set; }
        public DateTime updated_at { get; set; }
        public string url { get; set; }
    }
}

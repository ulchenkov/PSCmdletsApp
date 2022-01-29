// example of boolean expression parser: https://www.meziantou.net/creating-a-parser-for-boolean-expressions.htm

// https://stackoverflow.com/questions/1669/learning-to-write-a-compiler

using HtmlTags;

//string str = "(2 > 3 || ( 6 = 6 && 10 > 9))";

HtmlTag tag = new HtmlTag("div");

var tag1 = new HtmlTag("span")
    .Text("Hello & Goodbye")
    .AddClass("important")
    .AddClass("import")
    .Attr("title", "Greeting");

tag.Append(tag1);

Console.WriteLine(tag.ToString());
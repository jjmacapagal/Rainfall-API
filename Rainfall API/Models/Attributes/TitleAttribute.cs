namespace Rainfall_API.Models.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class TitleAttribute : Attribute
    {
        public string Title { get; }

        public TitleAttribute(string title)
        {
            Title = title;
        }
    }
}
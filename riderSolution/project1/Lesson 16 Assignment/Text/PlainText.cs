namespace project1.Lesson_16_Assignment.Text;

public class PlainText : IText
{
    private string _body;

    public PlainText(string body) {
        _body = body;
    }

    public string GetText() {
        return $"'''\n{_body}\n'''\n";
    }
}

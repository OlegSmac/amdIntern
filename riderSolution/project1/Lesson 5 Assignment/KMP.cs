namespace project1.Lesson_5_Assignment;

public class KMP
{
    private static int[] CaclPrefix(string text)
    {
        int n = text.Length;
        int[] p = new int[n];
        p[0] = 0;

        int sufIdx = 1, prefIdx = 0;
        while (sufIdx < n)
        {
            if (text[sufIdx] == text[prefIdx])
            {
                p[sufIdx] = prefIdx + 1;
                prefIdx++;
                sufIdx++;
            }
            else
            {
                if (prefIdx == 0)
                {
                    p[sufIdx] = 0;
                    sufIdx++;
                }
                else
                {
                    prefIdx = p[prefIdx - 1];
                }
            }
        }
        
        return p;
    }

    private static int FindSubstring(string text, string substring, int startIdx, int[] p)
    {
        int n = text.Length, textIdx = startIdx, subIdx = 0;
        while (textIdx < n)
        {
            if (text[textIdx] == substring[subIdx])
            {
                textIdx++;
                subIdx++;
            }

            if (subIdx == substring.Length) return textIdx - subIdx;
            if (textIdx < n && text[textIdx] != substring[subIdx])
            {
                if (subIdx != 0) subIdx = p[subIdx - 1];
                else textIdx++;
            }
        }

        return -1;
    }

    public static List<int> FindAllSubstrings(string text, string substring)
    {
        List<int> res = new List<int>();
        int n = text.Length, idx = 0;
        int[] p = CaclPrefix(substring);
        
        while (idx < n)
        {
            int nextSubstring = FindSubstring(text, substring, idx, p);
            if (nextSubstring == -1) break;

            res.Add(nextSubstring);
            idx = nextSubstring + 1;
        }

        return res;
    }

    public static int FindAllSubstringsCount(string text, string substring)
    {
        return FindAllSubstrings(text, substring).Count;
    }
}
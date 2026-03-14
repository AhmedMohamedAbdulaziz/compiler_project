using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
public class Scanner
{
    private static readonly (string Pattern, TokenType Type)[] Patterns =
   {
        (@"\b(if|then|else|end|repeat|until|read|write)\b", TokenType.KEYWORD),
        (@"[a-zA-Z][a-zA-Z0-9]*",                          TokenType.IDENTIFIER),
        (@"\d+",                                            TokenType.NUMBER),
        (@":=|[+\-*/=<>]",                                 TokenType.OPERATOR),
        (@"[();,{}]",                                       TokenType.SPECIAL_SYMBOL),
    };
    public List<Token> Scan(string code)
    {
        var tokens = new List<Token>();

        code = Regex.Replace(code, @"\{[^}]*\}", " ");

        int i = 0;
        while (i < code.Length)
        {
            if (char.IsWhiteSpace(code[i])) { i++; continue; }

            bool matched = false;
            foreach (var (pattern, type) in Patterns)
            {
                var match = Regex.Match(code.Substring(i), "^(?:" + pattern + ")");
                if (match.Success)
                {
                    tokens.Add(new Token(match.Value, type));
                    i += match.Length;
                    matched = true;
                    break;
                }
            }

            if (!matched)
            {
                tokens.Add(new Token(code[i].ToString(), TokenType.UNKNOWN));
                i++;
            }
        }

        return tokens;
    }

}

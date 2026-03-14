using System;

public enum TokenType
{
	KEYWORD,
	IDENTIFIER,
	NUMBER,
	OPERATOR,
    SPECIAL_SYMBOL,
    UNKNOWN
}
public class Token
{
	public string Value { get; set; }
	public TokenType Type { get; set; }
	public Token(string value,TokenType tokenType)
	{
		Value = value;
		Type = tokenType;
    }
}

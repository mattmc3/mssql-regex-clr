# mssql-regex-clr
CLR RegEx User Defined Functions for SQL Server

## MSSQL CLR Documentation
[https://docs.microsoft.com/en-us/sql/relational-databases/clr-integration-database-objects-user-defined-functions/clr-scalar-valued-functions](https://docs.microsoft.com/en-us/sql/relational-databases/clr-integration-database-objects-user-defined-functions/clr-scalar-valued-functions)

## Installation
*Instructions:*
1. Compile CLR script into .dll.

```
C:\Windows\Microsoft.NET\Framework\v2.0.50727\csc.exe /t:library RegexFunctions.cs
```

2. Put .dll into path: `C:\Program Files\Microsoft SQL Server\CLR\RegexFunctions.dll`
3. Run Install.sql script

## RegexFunctions

### Regex

#### IsRegexMatch
```
-- Determines if the string is matched by the regex pattern provided
select dbo.IsRegexMatch('abc123xyz', '\d')   -- 1 (True)
select dbo.IsRegexMatch('abc123xyz', 'ABC')  -- 0 (False)
select dbo.IsRegexMatch(NULL, NULL)          -- NULL
```

#### RegexReplace
```
-- Replaces all instances of the pattern with the replacement string
select dbo.RegexReplace('abc123xyz', '\d', '?')   -- 'abc???xyz'
select dbo.RegexReplace('abc123xyz', 'ABC', 'X')  -- 'abc123xyz'
select dbo.RegexReplace(NULL, NULL, NULL)         -- NULL
```

#### RegexMatchGroup
```
-- Returns the numbered match within the regex parens. 0 means the whole match.
select dbo.RegexMatchGroup('abc123xyz', '([^\d]+)(\d+)([^\d]+)', 0)  -- 'abc123xyz'
select dbo.RegexMatchGroup('abc123xyz', '([^\d]+)(\d+)([^\d]+)', 2)  -- '123'
select dbo.RegexMatchGroup(NULL, NULL, NULL)                         -- NULL
```

#### RegexIndex
```
-- Returns the 1-based index of the first occurrence of the pattern. Similar to CHARINDEX.
select dbo.RegexIndex('abc123xyz', '\d')  -- 4
select dbo.RegexIndex('abc123xyz', 'Q')   -- 0
select dbo.RegexIndex(NULL, NULL)         -- NULL
```

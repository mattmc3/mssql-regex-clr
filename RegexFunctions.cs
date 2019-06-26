using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.SqlServer.Server;

/// <summary>
/// compile using:
///    C:\Windows\Microsoft.NET\Framework\v2.0.50727\csc.exe /t:library RegexFunctions.cs
/// https://docs.microsoft.com/en-us/sql/relational-databases/clr-integration-database-objects-user-defined-functions/clr-scalar-valued-functions
/// CREATE ASSEMBLY RegexFunctions from 'C:\Program Files\Microsoft SQL Server\CLR\RegexFunctions.dll' WITH PERMISSION_SET = SAFE;
/// </summary>
public partial class RegexFunctions {

	private const RegexOptions Xms = RegexOptions.IgnorePatternWhitespace | RegexOptions.Multiline | RegexOptions.Singleline;
	private const RegexOptions Xmsi = Xms | RegexOptions.IgnoreCase;

	// CREATE FUNCTION IsRegexMatch(@input NVARCHAR(4000), @pattern NVARCHAR(4000)) RETURNS BIT AS
	// EXTERNAL NAME RegexFunctions.RegexFunctions.IsRegexMatch
	// GO
	[Microsoft.SqlServer.Server.SqlFunction()]
	public static SqlBoolean IsRegexMatch(SqlString input, SqlString pattern) {
		if (input.IsNull || pattern.IsNull) return SqlBoolean.Null;
		return Regex.IsMatch(input.Value, pattern.Value, Xms);
	}

	// CREATE FUNCTION RegexReplace(@input NVARCHAR(4000), @pattern NVARCHAR(4000), @replacement NVARCHAR(4000)) RETURNS NVARCHAR(4000) AS
	// EXTERNAL NAME RegexFunctions.RegexFunctions.RegexReplace
	// GO
	[Microsoft.SqlServer.Server.SqlFunction()]
	public static SqlString RegexReplace(SqlString input, SqlString pattern, SqlString replacement) {
		if (input.IsNull || pattern.IsNull || replacement.IsNull)
			return SqlString.Null;
		return Regex.Replace(input.Value, pattern.Value, replacement.Value, Xms);
	}

	// CREATE FUNCTION RegexMatchGroup(@input NVARCHAR(4000), @pattern NVARCHAR(4000), @groupNum int) RETURNS NVARCHAR(4000) AS
	// EXTERNAL NAME RegexFunctions.RegexFunctions.RegexMatchGroup
	// GO
	[Microsoft.SqlServer.Server.SqlFunction()]
	public static SqlString RegexMatchGroup(SqlString input, SqlString pattern, SqlInt32 groupNum) {
		if (input.IsNull || pattern.IsNull || groupNum.IsNull) return SqlString.Null;
		if (groupNum.Value < 0) return SqlString.Null;
		Regex re = new Regex(pattern.Value, Xms);
		Match m = re.Match(input.Value);
		if (!m.Success || m.Groups.Count < groupNum.Value) {
			return SqlString.Null;
		}
		else {
			return m.Groups[groupNum.Value].Value;
		}
	}

	// CREATE FUNCTION RegexIndex(@input NVARCHAR(4000), @pattern NVARCHAR(4000)) RETURNS INT AS
	// EXTERNAL NAME RegexFunctions.RegexFunctions.RegexIndex
	// GO
	[Microsoft.SqlServer.Server.SqlFunction()]
	public static SqlInt32 RegexIndex(SqlString input, SqlString pattern) {
		if (input.IsNull || pattern.IsNull) return SqlInt32.Null;
		Regex re = new Regex(pattern.Value, Xms);
		Match m = re.Match(input.Value);
		if (!m.Success) {
			return 0;
		}
		return m.Index + 1;  // SQL indexes strings by 1
	}
}

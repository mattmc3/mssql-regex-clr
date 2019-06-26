-- https://docs.microsoft.com/en-us/sql/relational-databases/clr-integration-database-objects-user-defined-functions/clr-scalar-valued-functions
-- Instructions:
-- 1.) Run script
-- 2.) Remove .dll from path: C:\Program Files\Microsoft SQL Server\CLR\RegexFunctions.dll
USE [master]
GO

DROP FUNCTION IsRegexMatch;
GO

DROP FUNCTION RegexReplace;
GO

DROP FUNCTION RegexMatchGroup;
GO

DROP FUNCTION RegexIndex;
GO

DROP ASSEMBLY RegexFunctions;
GO

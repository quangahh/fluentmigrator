using FluentMigrator.Runner.Generators.Generic;

namespace FluentMigrator.Runner.Generators.Sybase
{
    public class SybaseQuoter : GenericQuoter
    {
        public override string OpenQuote => "[";

        public override string CloseQuote => "]";

        public override string CloseQuoteEscapeString => "]]";

        public override string OpenQuoteEscapeString => string.Empty;

        public override string QuoteSchemaName(string schemaName)
        {
            return (string.IsNullOrEmpty(schemaName)) ? "[dbo]" : Quote(schemaName);
        }

        public override string FormatNationalString(string value)
        {
            return $"N{FormatAnsiString(value)}";
        }

        public override string FormatSystemMethods(SystemMethods value)
        {
            switch (value)
            {
                case SystemMethods.NewSequentialId:
                case SystemMethods.NewGuid:
                    return "NEWID()";
                case SystemMethods.CurrentDateTimeOffset:
                case SystemMethods.CurrentDateTime:
                    return "CURRENT TIMESTAMP";
                case SystemMethods.CurrentUTCDateTime:
                    return "CURRENT UTC TIMESTAMP";
                case SystemMethods.CurrentUser:
                    return "CURRENT USER";
            }

            return base.FormatSystemMethods(value);
        }
    }
}

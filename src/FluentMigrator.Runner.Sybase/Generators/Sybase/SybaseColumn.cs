#region License
// Copyright (c) 2007-2018, FluentMigrator Project
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
// http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
#endregion

using System;
using System.Collections.Generic;

using FluentMigrator.Model;
using FluentMigrator.Runner.Generators.Base;

namespace FluentMigrator.Runner.Generators.Sybase
{
    internal class SybaseColumn : ColumnBase
    {
        public SybaseColumn(ITypeMap typeMap)
            : base(typeMap, new SybaseQuoter())
        {
            ClauseOrder = new List<Func<ColumnDefinition, string>>
            {
                FormatString,
                FormatType,
                FormatCollation,
                FormatDefaultValue,
                FormatPrimaryKey,
                FormatUniqueConstraint,
                FormatIdentity,
                FormatNullable
            };
        }

        /// <inheritdoc />
        protected override string FormatNullable(ColumnDefinition column)
        {
            if (column.IsNullable.HasValue && column.IsNullable.Value)
            {
                return "NULL";
            }

            return "NOT NULL";
        }

        /// <inheritdoc />
        protected override string FormatDefaultValue(ColumnDefinition column)
        {
            if (DefaultValueIsSqlFunction(column.DefaultValue))
                return "DEFAULT " + column.DefaultValue;

            var defaultValue = base.FormatDefaultValue(column);

            if (!string.IsNullOrEmpty(defaultValue))
                return defaultValue;

            return string.Empty;
        }

        private static bool DefaultValueIsSqlFunction(object defaultValue)
        {
            return defaultValue is string && defaultValue.ToString().EndsWith("()");
        }

        protected virtual string FormatUniqueConstraint(ColumnDefinition column)
        {
            // Define unique constraints on columns in addition to creating a unique index
            return column.IsUnique ? "UNIQUE" : string.Empty;
        }

        /// <inheritdoc />
        protected override string FormatIdentity(ColumnDefinition column)
        {
            return column.IsIdentity ? GetIdentityString() : string.Empty;
        }

        private static string GetIdentityString()
        {
            return "IDENTITY";
        }

        public static string FormatDefaultValue(object defaultValue, IQuoter quoter)
        {
            if (DefaultValueIsSqlFunction(defaultValue))
                return defaultValue.ToString();

            return quoter.QuoteValue(defaultValue);
        }

        public static string GetDefaultConstraintName(string tableName, string columnName)
        {
            return $"DF_{tableName}_{columnName}";
        }
    }
}

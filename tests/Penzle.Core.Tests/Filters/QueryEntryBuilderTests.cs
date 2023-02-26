﻿// Copyright (c) 2022 Penzle LLC. All Rights Reserved. Licensed under the MIT license. See License.txt in the project root for license information.

using System.Linq.Expressions;

namespace Penzle.Core.Tests.Filters
{
    public class QueryEntryBuilderTests
    {
        [Fact]
        public void Build_Should_Return_Empty_String_When_No_Query_Parameters_Are_Added()
        {
            // Arrange
            var builder = QueryEntryBuilder<Person>.New;

            // Act
            var result = builder.Build();

            // Assert
            Assert.Equal("", result);
        }

        [Fact]
        public void Build_Should_Return_Rql_Code_With_Where_Parameter()
        {
            // Arrange
            var builder = QueryEntryBuilder<Person>.New;
            Expression<Func<Person, bool>> predicate = x => x.Id == 1;
            var expectedRqlCode = "filter[where][and][Id]=1";

            // Act
            builder.Where(predicate);
            var result = builder.Build();

            // Assert
            Assert.Equal(expectedRqlCode, result);
        }

        [Fact]
        public void Build_Should_Return_Rql_Code_With_OrderBy_Parameter()
        {
            // Arrange
            var builder = QueryEntryBuilder<Person>.New;
            Expression<Func<Person, int>> keySelector = x => x.Id;
            var expectedRqlCode = "filter[order]=Id ASC";

            // Act
            builder.OrderBy(keySelector);
            var result = builder.Build();

            // Assert
            Assert.Equal(expectedRqlCode, result);
        }

        [Fact]
        public void Build_Should_Return_Rql_Code_With_OrderByDescending_Parameter()
        {
            // Arrange
            var builder = QueryEntryBuilder<Person>.New;
            Expression<Func<Person, int>> keySelector = x => x.Id;
            var expectedRqlCode = "filter[order]=Id DESC";

            // Act
            builder.OrderByDescending(keySelector);
            var result = builder.Build();

            // Assert
            Assert.Equal(expectedRqlCode, result);
        }

        [Fact]
        public void Where_Should_Add_WhereParameter_With_Simple_Equality()
        {
            // Arrange
            var builder = QueryEntryBuilder<Person>.New;

            // Act
            builder.Where(x => x.Id == 1);
            var result = builder.Build();

            // Assert
            Assert.Equal("filter[where][and][Id]=1", result);
        }

        [Fact]
        public void Where_Should_Add_WhereParameter_With_Simple_Inequality()
        {
            // Arrange
            var builder = QueryEntryBuilder<Person>.New;

            // Act
            builder.Where(x => x.Id != 1);
            var result = builder.Build();

            // Assert
            Assert.Equal("filter[where][and][Id][ne]=1", result);
        }

        [Fact]
        public void Where_Should_Add_WhereParameter_With_Simple_GreaterThan()
        {
            // Arrange
            var builder = QueryEntryBuilder<Person>.New;

            // Act
            builder.Where(x => x.Id > 1);
            var result = builder.Build();

            // Assert
            Assert.Equal("filter[where][and][Id][gt]=1", result);
        }

        [Fact]
        public void Where_Should_Add_WhereParameter_With_Simple_GreaterThanOrEqual()
        {
            // Arrange
            var builder = QueryEntryBuilder<Person>.New;

            // Act
            builder.Where(x => x.Id >= 1);
            var result = builder.Build();

            // Assert
            Assert.Equal("filter[where][and][Id][ge]=1", result);
        }

        [Fact]
        public void Where_Should_Add_WhereParameter_With_Simple_LessThan()
        {
            // Arrange
            var builder = QueryEntryBuilder<Person>.New;

            // Act
            builder.Where(x => x.Id < 1);
            var result = builder.Build();

            // Assert
            Assert.Equal("filter[where][and][Id][lt]=1", result);
        }

        [Fact]
        public void Where_Should_Add_WhereParameter_With_Simple_LessThanOrEqual()
        {
            // Arrange
            var builder = QueryEntryBuilder<Person>.New;

            // Act
            builder.Where(x => x.Id <= 1);
            var result = builder.Build();

            // Assert
            Assert.Equal("filter[where][and][Id][le]=1", result);
        }

        [Fact]
        public void Where_Should_Add_WhereParameter_With_AndAlso()
        {
            // Arrange
            var builder = QueryEntryBuilder<Person>.New;

            // Act
            builder.Where(x => x.Id == 1 && x.FirstName == "John");
            var result = builder.Build();

            // Assert
            Assert.Equal("filter[where][and][Id]=1&filter[where][and][FirstName]=John", result);
        }

        [Fact]
        public void Where_Should_Add_WhereParameter_With_OrElse()
        {
            // Arrange
            var builder = QueryEntryBuilder<Person>.New;

            // Act
            builder.Where(x => x.Id == 1 || x.FirstName == "John");
            var result = builder.Build();

            // Assert
            Assert.Equal("filter[where][and][Id]=1&filter[where][or][FirstName]=John", result);
        }

        [Fact]
        public void Where_Should_Add_WhereParameter_With_Contains()
        {
            // Arrange
            var builder = QueryEntryBuilder<Person>.New;

            // Act
            builder.Where(x => x.FirstName.Contains("ohn"));
            var result = builder.Build();

            // Assert
            Assert.Equal("filter[where][and][FirstName]=*ohn*", result);
        }

        [Fact]
        public void Where_Should_Add_WhereParameter_With_Contains_List()
        {
            // Arrange
            var builder = QueryEntryBuilder<Person>.New;

            // Act
            builder.Where(x => x.FirstName.Contains(new List<string> { "John", "Jane" }));
            var result = builder.Build();

            // Assert
            Assert.Equal("filter[where][and][FirstName][inq]=John,Jane", result);
        }

        [Fact]
        public void Where_Should_Add_WhereParameter_With_StartsWith()
        {
            // Arrange
            var builder = QueryEntryBuilder<Person>.New;

            // Act
            builder.Where(x => x.FirstName.StartsWith("J"));
            var result = builder.Build();

            // Assert
            Assert.Equal("filter[where][and][FirstName]=^J", result);
        }

        [Fact]
        public void Where_Should_Add_WhereParameter_With_EndsWith()
        {
            // Arrange
            var builder = QueryEntryBuilder<Person>.New;

            // Act
            builder.Where(x => x.FirstName.EndsWith("doe"));
            var result = builder.Build();

            // Assert
            Assert.Equal("filter[where][and][FirstName]=*doe$", result);
        }

        [Fact]
        public void Where_Should_Add_WhereParameter_With_And_Expression()
        {
            // Arrange
            var builder = QueryEntryBuilder<Person>.New;

            // Act
            builder.Where(x => x.FirstName.StartsWith("J") && x.Age >= 30);
            var result = builder.Build();

            // Assert
            Assert.Equal("filter[where][and][FirstName]=^J&filter[where][and][Age][ge]=30", result);
        }

        [Fact]
        public void Where_Should_Add_WhereParameter_With_Or_Expression()
        {
            // Arrange
            var builder = QueryEntryBuilder<Person>.New;

            // Act
            builder.Where(x => x.FirstName.Contains("ohn") || x.Age < 30);
            var result = builder.Build();

            // Assert
            Assert.Equal("filter[where][and][FirstName]=*ohn*&filter[where][or][Age][lt]=30", result);
        }

        [Fact]
        public void Build_Should_Create_Correct_Rql_Query_With_Where_Select_OrderBy()
        {
            // Arrange
            var builder = QueryEntryBuilder<Person>.New;
            
            // Act
            builder.Where(x => x.FirstName == "John").Select(x => new { x.Id, x.FirstName }).OrderBy(x => x.Id);
            var result = builder.Build();

            // Assert
            Assert.Equal("filter[where][and][FirstName]=John&filter[fields][Id]=true&filter[fields][FirstName]=true&filter[order]=Id ASC", result);
        }

        [Fact]
        public void Build_Should_Create_Correct_Rql_Query_With_Where_Select_OrderByDescending()
        {
            // Arrange
            var builder = QueryEntryBuilder<Person>.New;

            // Act
            builder.Where(x => x.FirstName == "John").Select(x => new { x.Id, x.FirstName }).OrderByDescending(x => x.Id);
            var result = builder.Build();

            // Assert
            Assert.Equal("filter[where][and][FirstName]=John&filter[fields][Id]=true&filter[fields][FirstName]=true&filter[order]=Id DESC", result);
        }

        [Fact]
        public void Build_Should_Create_Correct_Rql_Query_With_Where_MultipleSelects_OrderBy()
        {
            // Arrange
            var builder = QueryEntryBuilder<Person>.New;

            // Act
            builder.Where(x => x.FirstName == "John").Select(x => new { x.Id, x.FirstName }).Select(x => new { x.Id, x.Age }).OrderBy(x => x.Id);
            var result = builder.Build();

            // Assert
            Assert.Equal("filter[where][and][FirstName]=John&filter[fields][Id]=true&filter[fields][FirstName]=true&filter[fields][Id]=true&filter[fields][Age]=true&filter[order]=Id ASC", result);
        }

        [Fact]
        public void Build_Should_Create_Correct_Rql_Query_With_Page_Filter()
        {
            // Arrange
            var builder = QueryEntryBuilder<Person>.New;

            // Act
            builder.Where(x => x.FirstName == "John").Select(x => new { x.Id, x.FirstName }).OrderBy(x => x.Id).Page(2);
            var result = builder.Build();

            // Assert
            Assert.Equal("filter[where][and][FirstName]=John&filter[fields][Id]=true&filter[fields][FirstName]=true&filter[order]=Id ASC&filter[page]=1", result);
        }

        [Fact]
        public void Build_Should_Create_Correct_Rql_Query_With_Page_Size_Filter()
        {
            // Arrange
            var builder = QueryEntryBuilder<Person>.New;

            // Act
            builder.Where(x => x.FirstName == "John").Select(x => new { x.Id, x.FirstName }).OrderBy(x => x.Id).PageSize(25);
            var result = builder.Build();

            // Assert
            Assert.Equal("filter[where][and][FirstName]=John&filter[fields][Id]=true&filter[fields][FirstName]=true&filter[order]=Id ASC&filter[PageSize]=25", result);
        }

        [Fact]
        public void Build_Should_Create_Correct_Rql_Query_With_Page_And_Page_Size_Filters()
        {
            // Arrange
            var builder = QueryEntryBuilder<Person>.New;

            // Act
            builder.Where(x => x.FirstName == "John").Select(x => new { x.Id, x.FirstName }).OrderBy(x => x.Id).Page(2).PageSize(25);
            var result = builder.Build();

            // Assert
            Assert.Equal("filter[where][and][FirstName]=John&filter[fields][Id]=true&filter[fields][FirstName]=true&filter[order]=Id ASC&filter[page]=1&filter[PageSize]=25", result);
        }
    }
}

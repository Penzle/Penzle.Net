// Copyright (c) 2022 Penzle LLC. All Rights Reserved. Licensed under the MIT license. See License.txt in the project root for license information.

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
            Assert.Equal("filter[where][and][Id][neq]=1", result);
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
            builder.Where(x => x.Id > 1);
            var result = builder.Build();

            // Assert
            Assert.Equal("filter[where][and][Id][gt]=1", result);
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
            Assert.Equal("filter[where][and][Id][lte]=1", result);
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
            Assert.Equal("filter[where][and][FirstName][like]=*ohn*", result);
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
            Assert.Equal("filter[where][and][FirstName][in]=John,Jane", result);
        }

        [Fact]
        public void Where_Should_Add_WhereParameter_With_Not_Contains_List()
        {
            // Arrange
            var builder = QueryEntryBuilder<Person>.New;

            // Act
            builder.Where(x => !x.FirstName.Contains(new List<string> { "John", "Jane" }));
            var result = builder.Build();

            // Assert
            Assert.Equal("filter[where][and][FirstName][nin]=John,Jane", result);
        }

        [Fact]
        public void Where_Should_Add_WhereParameter_With_Empty()
        {
            // Arrange
            var builder = QueryEntryBuilder<Person>.New;

            // Act
            builder.Where(x => x.FirstName == null);

            var result = builder.Build();

            // Assert
            Assert.Equal("filter[where][and][FirstName][empty]", result);
        }

        [Fact]
        public void Where_Should_Add_WhereParameter_With_Not_Empty()
        {
            // Arrange
            var builder = QueryEntryBuilder<Person>.New;

            // Act
            builder.Where(x => x.FirstName != null);

            var result = builder.Build();

            // Assert
            Assert.Equal("filter[where][and][FirstName][nempty]", result);
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
            Assert.Equal("filter[where][and][FirstName][like]=^J", result);
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
            Assert.Equal("filter[where][and][FirstName][like]=*doe$", result);
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
            Assert.Equal("filter[where][and][FirstName][like]=^J&filter[where][and][Age][gte]=30", result);
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
            Assert.Equal("filter[where][and][FirstName][like]=*ohn*&filter[where][or][Age][lt]=30", result);
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

        [Fact]
        public void Where_Should_Add_Work_WithSystemProperty()
        {
            // Arrange
            var builder = QueryEntryBuilder<Entry<Person>>.New;

            // Act
            builder.Where(x => x.System.Slug == "cms/docs/reference");

            var result = builder.Build();

            // Assert
            Assert.Equal("filter[where][and][system.slug]=cms/docs/reference", result.ToLower());
        }

        [Fact]
        public void Where_Should_Add_Work_With_Contains_And_Id()
        {
            // Arrange
            var builder = QueryEntryBuilder<Entry<Person>>.New;
            var listOfIds = new List<Guid>() { new Guid("585e4435-b2c8-4e66-bb66-4e61f028a5bd"), new Guid("b77259e1-d4ac-4aa8-a8dd-bfff0ab214eb") };

            // Act
            //builder.Where(x => x.Fields.Id.Contains(listOfIds));
            builder.Where(x => x.System.Id.Contains(listOfIds));

            var result = builder.Build();

            // Assert
            Assert.Equal("filter[where][and][system.id][in]=585e4435-b2c8-4e66-bb66-4e61f028a5bd,b77259e1-d4ac-4aa8-a8dd-bfff0ab214eb", result.ToLower());
        }
    }
}

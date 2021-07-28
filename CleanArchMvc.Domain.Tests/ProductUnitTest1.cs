using System;
using CleanArchMvc.Domain.Entities;
using FluentAssertions;
using Xunit;

namespace CleanArchMvc.Domain.Tests
{
    public class ProductUnitTest1
    {
        [Fact(DisplayName = "Create Product With Valid State")]
        public void CreateCategory_WithValidParameters_ResultObjectValidState()
        {
            Action action = () => new Product(1, "Product Name", "Product Description", 9.99m, 99, "Product image");
            action.Should()
                .NotThrow<CleanArchMvc.Domain.Validation.DomainExceptionValidation>();
        }

        [Fact(DisplayName = "Create Product With Negative Id")]
        public void CreateCategory_WithNegativeId_DomainExceptionInvalidId()
        {
            Action action = () => new Product(-1, "Product Name", "Product Description", 9.99m, 99, "Product image");
            action.Should()
                .Throw<CleanArchMvc.Domain.Validation.DomainExceptionValidation>()
                    .WithMessage("Invalid Id Value");
        }

        [Fact(DisplayName = "Create Product With Short Name")]
        public void CreateCategory_WithShortName_DomainExceptionShortName()
        {
            Action action = () => new Product(1, "Pr", "Product Description", 9.99m, 99, "Product image");
            action.Should()
                .Throw<CleanArchMvc.Domain.Validation.DomainExceptionValidation>()
                    .WithMessage("Invalid name. Name needs to be at least 3 characters long");
        }

        [Fact(DisplayName = "Create Product With Long Image Name")]
        public void CreateCategory_WithLongImageName_DomainExceptionLongImageName()
        {
            Action action = () => new Product(1, "Product Name", "Product Description", 9.99m, 99, "Product imageeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeee");
            action.Should()
                .Throw<CleanArchMvc.Domain.Validation.DomainExceptionValidation>()
                    .WithMessage("Image name too long, needs to be 250 characters at most");
        }

        [Fact(DisplayName = "Create Product With Null Image Name")]
        public void CreateCategory_WithNullImageName_NoDomainException()
        {
            Action action = () => new Product(1, "Product Name", "Product Description", 9.99m, 99, null);
            action.Should()
                .NotThrow<CleanArchMvc.Domain.Validation.DomainExceptionValidation>();
        }

        [Fact(DisplayName = "Create Product With Null Image Name")]
        public void CreateCategory_WithNullImageName_NoNullReferenceException()
        {
            Action action = () => new Product(1, "Product Name", "Product Description", 9.99m, 99, null);
            action.Should()
                .NotThrow<NullReferenceException>();
        }

        [Fact(DisplayName = "Create Product With Empty Image Name")]
        public void CreateCategory_WithEmptyImageName_NoDomainException()
        {
            Action action = () => new Product(1, "Product Name", "Product Description", 9.99m, 99, "");
            action.Should()
                .NotThrow<CleanArchMvc.Domain.Validation.DomainExceptionValidation>();

        }

        [Fact(DisplayName = "Create Product With Short Description")]
        public void CreateCategory_WithShortDescription_DomainExceptionShortDescription()
        {
            Action action = () => new Product(1, "Product Name", "Pr", 9.99m, 99, "image");
            action.Should()
                .Throw<CleanArchMvc.Domain.Validation.DomainExceptionValidation>()
                    .WithMessage("Invalid description. It needs to be at least 5 characters long");
        }

        [Fact(DisplayName = "Create Product With Null Description")]
        public void CreateCategory_WithNullDescription_DomainExceptionNoDescription()
        {
            Action action = () => new Product(1, "Product Name", null, 9.99m, 99, "image");
            action.Should()
                .Throw<CleanArchMvc.Domain.Validation.DomainExceptionValidation>()
                    .WithMessage("Description is a required field");
        }

        [Fact(DisplayName = "Create Product With Empty Description")]
        public void CreateCategory_WithEmptyDescription_DomainExceptionNoDescription()
        {
            Action action = () => new Product(1, "Product Name", "", 9.99m, 99, "image");
            action.Should()
                .Throw<CleanArchMvc.Domain.Validation.DomainExceptionValidation>()
                    .WithMessage("Description is a required field");
        }

        [Fact(DisplayName = "Create Product With Invalid Price")]
        public void CreateCategory_WithInvalidPriceValue_DomainExceptionInvalidPrice()
        {
            Action action = () => new Product(1, "Product Name", "Product Description", -2, 99, "Product Image");
            action.Should()
                .Throw<CleanArchMvc.Domain.Validation.DomainExceptionValidation>()
                    .WithMessage("Invalid price value");
        }

        [Theory]
        [InlineData(-5)]
        public void CreateCategory_WithInvalidStockValue_DomainExceptionInvalidStock(int value)
        {
            Action action = () => new Product(1, "Product Name", "Product Description", 2.99m, value, "Product Image");
            action.Should()
                .Throw<CleanArchMvc.Domain.Validation.DomainExceptionValidation>()
                    .WithMessage("Invalid stock value");
        }
    }
}

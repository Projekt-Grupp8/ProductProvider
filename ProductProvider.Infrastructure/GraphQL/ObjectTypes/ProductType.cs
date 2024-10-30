﻿using ProductProvider.Infrastructure.Data.Entities;
namespace ProductProvider.Infrastructure.GraphQL.ObjectTypes;

public class ProductType : ObjectType<ProductEntity>
{
    protected override void Configure(IObjectTypeDescriptor<ProductEntity> descriptor)
    {
        //descriptor.Name("Product");
        
        descriptor.Field(p => p.Id).Type<NonNullType<IdType>>();
        descriptor.Field(p => p.Brand).Type<NonNullType<StringType>>();
        descriptor.Field(p => p.Name).Type<NonNullType<StringType>>();
        descriptor.Field(p => p.Rating).Type<DecimalType>();
        descriptor.Field(p => p.Sizes).Type<ListType<SizeType>>();
        descriptor.Field(p => p.Description).Type<StringType>();
        descriptor.Field(p => p.Price).Type<DecimalType>();
        descriptor.Field(p => p.Category).Type<NonNullType<CategoryType>>();
        descriptor.Field(p => p.CategoryName).Type<StringType>();
        descriptor.Field(p => p.StockQuantity).Type<IntType>();
        descriptor.Field(p => p.ImageURL).Type<StringType>();
        descriptor.Field(p => p.IsNewArrival).Type<NonNullType<BooleanType>>();
    }
}

public class SizeType : ObjectType<SizeEntity>
{
    protected override void Configure(IObjectTypeDescriptor<SizeEntity> descriptor)
    {
        //descriptor.Name("Size");

        descriptor.Field(s => s.Size).Type<NonNullType<IdType>>();        
    }
}

public class CategoryType : ObjectType<CategoryEntity>
{
    protected override void Configure(IObjectTypeDescriptor<CategoryEntity> descriptor)
    {
        //descriptor.Name("Category");

        //descriptor.Field(c => c.CategoryId).Type<NonNullType<IdType>>();
        descriptor.Field(c => c.CategoryName).Type<NonNullType<StringType>>();
        descriptor.Field(c => c.CategoryDescription).Type<NonNullType<StringType>>();
        
    }
}
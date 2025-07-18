﻿using InventoryAndOrderManagementAPI.Data;
using InventoryAndOrderManagementAPI.Interfaces;
using InventoryAndOrderManagementAPI.Models;
using Microsoft.EntityFrameworkCore;
using InventoryAndOrderManagementAPI.Models;
using InventoryAndOrderManagementAPI.Dtos.Product;
using Microsoft.AspNetCore.Http.HttpResults;
using InventoryAndOrderManagementAPI.Helpers;
using InventoryAndOrderManagementAPI.Mapper;

namespace InventoryAndOrderManagementAPI.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDBContext _context;
        public ProductRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<Product> CreateProductAsync(Product productModel)
        {
            var categoryExists = await _context.Categories.AnyAsync(category => category.CategoryId == productModel.CategoryId);

            if (!categoryExists)
            {
                throw new ArgumentException($"Category with ID: {productModel.CategoryId} doesn't exists");
            }

            await _context.Products.AddAsync(productModel);
            await _context.SaveChangesAsync();

            return productModel;
        }

        public async Task<Product?> DeleteProductAsync(int id)
        {
            var productModel = await _context.Products
                               .Include(product => product.OrderItems)
                               .FirstOrDefaultAsync(product => product.ProductId == id);

            if (productModel == null) return null;

            var isUsedInOrderItems = await _context.OrderItems.AnyAsync(orderItem => orderItem.ProductId == id);

            if (isUsedInOrderItems)
            {
                throw new InvalidOperationException("Cannot delete product, it is used in one or more order items.");
            }

            _context.Products.Remove(productModel);
            await _context.SaveChangesAsync();

            return productModel;
        }

        public async Task<List<Product>> GetAllProductsAsync(ProductQueryObject query)
        {

            var products = _context.Products.AsQueryable();

            if (!string.IsNullOrWhiteSpace(query.Name))
            {
                products = products.Where(product => product.Name.Contains(query.Name));
            }

            if (!string.IsNullOrWhiteSpace(query.SortBy))
            {
                var allowedSortProperties = new[] { "Name" };

                if (allowedSortProperties.Contains(query.SortBy))
                {
                    products = query.IsDescending ? products.OrderByDescending(product => product.Name) : products.OrderBy(product => product.Name);
                }
                else
                {
                    products = products.OrderBy(product => product.Name);
                }
            }

            var skipNumber = (query.PageNumber - 1) * query.PageSize;

            return await products.Skip(skipNumber).Take(query.PageSize).AsNoTracking().ToListAsync();
        }

        public async Task<Product?> GetProductByIdAsync(int id)
        {
            return await _context.Products.FindAsync(id);
        }

        public async Task<List<Product>> GetProductsWithLowStockAsync(int threshold)
        {
            return await _context.Products.Where(product => product.QuantityInStock < threshold).ToListAsync();
        }

        public async Task<Product?> UpdateProductAsync(int id, UpdateProductDto productDto)
        {
            var productModel = await _context.Products.FirstOrDefaultAsync(product => product.ProductId == id);

            if (productModel == null) return null;

            productModel.UpdateProductModel(productDto);

            await _context.SaveChangesAsync();

            return productModel;
        }
    }
}




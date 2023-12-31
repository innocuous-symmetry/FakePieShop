﻿using FakePieShop.Repositories;
using FakePieShop.Models;
using Microsoft.AspNetCore.Mvc;

namespace FakePieShop.Components
{
    public class CategoryMenu : ViewComponent
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryMenu(ICategoryRepository categoryRepository) => _categoryRepository = categoryRepository;

        public IViewComponentResult Invoke()
        {
            IEnumerable<Category> categories = _categoryRepository.AllCategories.OrderBy(c => c.CategoryName);
            return View(categories);
        }
    }
}

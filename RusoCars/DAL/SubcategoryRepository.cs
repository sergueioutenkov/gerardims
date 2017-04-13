using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using RusoCars.Models;
using System.Data.Entity;

namespace RusoCars.DAL
{
    public class SubcategoryRepository
    {
        internal CarsRusoEntities context;

        public SubcategoryRepository() { }

        public SubcategoryRepository(CarsRusoEntities context)
        {
            this.context = context;
        }
        public List<Subcategory> GetAllInCategory(int categoryId)
        {
            return context.Subcategory_B(null, categoryId).ToList<Subcategory>();
        }

        public IQueryable<Subcategory> GetAllWithCategories()
        {
            return context.Subcategories.Include(s => s.Category);
        }
        public Subcategory GetByID(int? id)
        {
           return GetByID(id, null);
        }

        public Subcategory GetByID(int? id, int? categoryId)
        {
            return context.Subcategory_B(id, categoryId).ToList<Subcategory>().First();
        }
        public void Insert(int? categoryId, int? subcategoryId, int? imageId, string name, string imageTitle, string imagePath)
        {
            context.Subcategory_Image_I(categoryId, subcategoryId, imageId, name, imageTitle, imagePath);
        }

        public List<Image> GetImages(int subcategoryId)
        {
            return context.Subcategory_Image_B(subcategoryId).ToList<Image>();
        }

        public void Delete(Subcategory subcategory)
        {
            context.Subcategories.Remove(subcategory);
        }
        public void DeleteWithImages(int? subcategoryId, int? imageId)
        {
            context.Subcategory_Image_D(subcategoryId, imageId);
        }

        public void Update(Subcategory entityToUpdate)
        {

            context.Entry(entityToUpdate).State = EntityState.Modified;
        }

        public int getMaxId()
        {
            return context.Subcategories.Max(s => s.SubcategoryId);
        }
    }









}

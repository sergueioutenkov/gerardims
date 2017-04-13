using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RusoCars.Models;
using System.Data.Entity;
using System.Data;
using System.Diagnostics.Contracts;


namespace RusoCars.DAL
{
    public class CategoryRepository : GenericRepository<Category>
    {
        public CategoryRepository(CarsRusoEntities context)
            : base(context)
        {
        }
        public override Category GetByID(int? id)
        {

            return context.Category_B(id).ToList<Category>().First();
        }
        public override List<Category> GetAll()
        {
            return context.Category_B(null).ToList<Category>();
        }

    }

    public class CertificationRepository : GenericRepository<Certification>
    {
        public CertificationRepository(CarsRusoEntities context)
            : base(context)
        {
        }
        public override Certification GetByID(int? id)
        {
            base.GetByID(id);
            return context.Certification_B(id).ToList<Certification>().First();
        }
        public override List<Certification> GetAll()
        {
            return context.Certification_B(null).ToList<Certification>();
        }

    }

    public class ClientRepository : GenericRepository<Client>
    {
        public ClientRepository(CarsRusoEntities context)
            : base(context)
        {
        }
        public override Client GetByID(int? id)
        {
            base.GetByID(id);
            return context.Client_B(id).ToList<Client>().First();
        }
        public override List<Client> GetAll()
        {
            return context.Client_B(null).ToList<Client>();
        }
    }

    public class ImageRepository : GenericRepository<Image>
    {
        public ImageRepository(CarsRusoEntities context)
            : base(context)
        {
        }
        public override Image GetByID(int? id)
        {
            base.GetByID(id);
            return context.Image_B(id).ToList<Image>().First();
        }
        public override List<Image> GetAll()
        {
            return context.Image_B(null).ToList<Image>();
        }
    }

    public class LinkRepository : GenericRepository<Link>
    {
        public LinkRepository(CarsRusoEntities context)
            : base(context)
        {
        }

        public override Link GetByID(int? id)
        {
            base.GetByID(id);
            return context.Link_B(id, null).ToList<Link>().First();
        }
        public override Link GetByID(int? id, int? linkCategoryId)
        {
            base.GetByID(id);
            return context.Link_B(id, linkCategoryId).ToList<Link>().First();
        }
        public override List<Link> GetAll()
        {
            return context.Link_B(null, null).ToList<Link>();
        }
        public override List<Link> GetAllFromCategory(int categoryId)
        {
            return context.Link_B(null, categoryId).ToList<Link>();
        }
    }

    public class LinksCategoryRepository : GenericRepository<LinksCategory>
    {
        public LinksCategoryRepository(CarsRusoEntities context)
            : base(context)
        {
        }
        public override LinksCategory GetByID(int? id)
        {
            base.GetByID(id);
            return context.LinksCategory_B(id).ToList<LinksCategory>().First();
        }
        public override List<LinksCategory> GetAll()
        {
            return context.LinksCategory_B(null).ToList<LinksCategory>();
        }
    }

    public class NewsRepository : GenericRepository<News>
    {
        public NewsRepository(CarsRusoEntities context)
            : base(context)
        {
        }
        public override News GetByID(int? id)
        {
            base.GetByID(id);
            return context.News_B(id).ToList<News>().First();
        }
        public override List<News> GetAll()
        {
            return context.News_B(null).ToList<News>();
        }
    }

    public class PilotRepository : GenericRepository<Pilot>
    {
        public PilotRepository(CarsRusoEntities context)
            : base(context)
        {
        }
        public override Pilot GetByID(int? id)
        {
            base.GetByID(id);
            return context.Pilot_B(id).ToList<Pilot>().First();
        }
        public override List<Pilot> GetAll()
        {
            return context.Pilot_B(null).ToList<Pilot>();
        }
    }

    public class TeamRepository : GenericRepository<Team>
    {
        public TeamRepository(CarsRusoEntities context)
            : base(context)
        {
        }
        public override Team GetByID(int? id)
        {
            base.GetByID(id);
            return context.Team_B(id).ToList<Team>().First();
        }
        public override List<Team> GetAll()
        {
            return context.Team_B(null).ToList<Team>();
        }
    }


    public class ServiceRepository : GenericRepository<Service>
    {
        public ServiceRepository(CarsRusoEntities context)
            : base(context)
        {
        }
        public override Service GetByID(int? id)
        {
            return context.Service_B(id,null).ToList<Service>().First();
        }
           public override List<Service> GetAllFromCategory(int isCompetition)
        {
            bool competition = false;
                if(isCompetition == 1)
                        competition = true;
                return context.Service_B(null, competition).ToList<Service>();
        }


    }



    public class TextRepository : GenericRepository<Text>
    {
        public TextRepository(CarsRusoEntities context)
            : base(context)
        {
        }
        public override Text GetByID(int? id, int? languageId)
        {
            return context.Texts_B(id, languageId).ToList<Text>().First();
        }
        
        public override List<Text> GetAll()
        {
            return context.Texts_B(null, null).ToList<Text>();
        }

    }
    /*  public class SubcategoryRepository : GenericRepository<Subcategory>
      {
          public SubcategoryRepository(CarsRusoEntities context)
              : base(context)
          {
          }
          public override Subcategory GetByID(int? id)
          {
              base.GetByID(id);
              return context.Subcategory_B(id, null).ToList<Subcategory>().First();
          }
          public override Subcategory GetByID(int? id, int categoryId)
          {
              base.GetByID(id);
              return context.Subcategory_B(id, categoryId).ToList<Subcategory>().First();
          }
          public override List<Subcategory> GetAll()
          {
              return context.Subcategory_B(null, null).ToList<Subcategory>();
          }

      }*/


}
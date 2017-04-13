using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Mvc;

namespace RusoCars.Models
{
    [MetadataType(typeof(CategoryMetadata))]
    partial class Category
    {
    }

    class CategoryMetadata
    {
        [DisplayName("Nombre")]
        [Required(ErrorMessage = "Debe insertar un nombre")]
        public string Name { get; set; }
    }

    [MetadataType(typeof(CertificationMetadata))]
    partial class Certification
    {

    }
    class CertificationMetadata
    {
        [DisplayName("Titulo")]
        [Required(ErrorMessage = "Debe insertar un titulo")]
        public string CertificationTitle { get; set; }

        [DisplayName("Descripcion")]
        [Required(ErrorMessage = "Debe insertar una descripcion")]
        public string Description { get; set; }
    }
    [MetadataType(typeof(NewsMetadata))]
    partial class News
    {
    }

    class NewsMetadata
    {
        [DisplayName("Titulo")]
        [Required(ErrorMessage = "Debe insertar un titulo")]
        [StringLength(50, MinimumLength = 5,
            ErrorMessage = "Debe tener como minimo 5 caracteres")]
        public string Title { get; set; }
        [DisplayName("Descripcion corta")]
        [Required(ErrorMessage = "Debe insertar una descripcion corta")]
        [StringLength(150, ErrorMessage = "La descripcion no debe tener mas de 150 caracteres")]
        public string ShortDescription { get; set; }
        [DisplayName("Descripcion larga")]
        [Required(ErrorMessage = "Debe insertar una descripcion larga")]
        public string LongDescription { get; set; }
        [DisplayName("Fecha")]
        [DataType(DataType.Date)]
        public Nullable<System.DateTime> Date { get; set; }
    }


    [MetadataType(typeof(TeamMetadata))]
    public partial class Team
    {
    }

    class TeamMetadata
    {
        [DisplayName("Nombre")]
        [Required(ErrorMessage = "Debe insertar nombre")]
        public string FirstName { get; set; }
        [DisplayName("Apellido")]
        [Required(ErrorMessage = "Debe insertar apellido")]
        public string LastName { get; set; }
        [DisplayName("Email")]
        [Required(ErrorMessage = "Debe insertar un email")]
        [DataType(DataType.EmailAddress)]
        public string mail { get; set; }

    }
    [MetadataType(typeof(ImageMetadata))]
    public partial class Image
    {
    }
    class ImageMetadata
    {
        [DisplayName("Titulo imagen")]
        public string ImageTitle { get; set; }
    }

    [MetadataType(typeof(subcategoryMetadata))]
    public partial class subcategory
    {
    }
    class subcategoryMetadata
    {
        [DisplayName("Nombre")]
        [Required(ErrorMessage = "Debe insertar un nombre")]
        public string Name { get; set; }
    }
    [MetadataType(typeof(ClientMetadata))]
    public partial class Client
    {
    }
    class ClientMetadata
    {
        [DisplayName("Nombre")]
        [Required(ErrorMessage = "Debe insertar un nombre")]
        public string Name { get; set; }
        [DisplayName("Descripción")]
        [Required(ErrorMessage = "Debe insertar una descripcion")]
        public string Description { get; set; }
    }
    [MetadataType(typeof(PilotMetadata))]
    public partial class Pilot
    { }

    class PilotMetadata
    {
        [DisplayName("Nombre")]
        [Required(ErrorMessage = "Debe insertar un nombre")]
        public string FirstName { get; set; }
        [DisplayName("Apellido")]
        [Required(ErrorMessage = "Debe insertar un apellido")]
        public string LastName { get; set; }
        [DisplayName("Descripción")]
        public string Description { get; set; }
    }
    [MetadataType(typeof(LinkMetadata))]
    public partial class Link
    {
    }

    class LinkMetadata
    {
        [DisplayName("Nombre")]
        [Required(ErrorMessage = "Debe insertar un nombre")]
        public string Name { get; set; }
        [DisplayName("Link")]
        [Required(ErrorMessage = "Debe insertar un link")]
        public string LinkUrl { get; set; }

    }
    [MetadataType(typeof(LinksCategoryMetadata))]
    public partial class LinksCategory
    {
    }

    class LinksCategoryMetadata
    {
        [DisplayName("Nombre")]
        [Required(ErrorMessage = "Debe insertar un nombre")]
        public string Name { get; set; }
    }
    [MetadataType(typeof(SubcategoryMetada))]
    public partial class Subcategory { }

    class SubcategoryMetada
    {
        [DisplayName("Nombre")]
        [Required(ErrorMessage = "Debe insertar un nombre")]
        public string Name { get; set; }
        [DisplayName("Categoria")]
        public int CategoryId { get; set; }
    }
    [MetadataType(typeof(ServiceMetadata))]
    public partial class Service { }

    class ServiceMetadata
    {
        [DisplayName("Nombre")]
        [Required(ErrorMessage = "Debe insertar un nombre")]
        public string Name { get; set; }
        [DisplayName("Descripción")]
        [AllowHtml]
        [Required(ErrorMessage = "Debe insertar una descripción")]
        public string Description { get; set; }
        [DisplayName("Tipo de Servicio")]
        public bool isCompetition { get; set; }
    }

    [MetadataType(typeof(Contact))]
    public partial class Contact
    {
        [DisplayName("Nombre y Apellido")]
        [Required(ErrorMessage = "Debe insertar un nombre y apellido")]
        public string Name { get; set; }
        [DisplayName("Teléfono")]
        [Required(ErrorMessage = "Debe insertar un teléfono")]
        public string Phone { get; set; }
        [DisplayName("E-Mail")]
        [Required(ErrorMessage = "Debe insertar un E-mail")]
        public string Email { get; set; }
        [DisplayName("Consulta")]
        [Required(ErrorMessage = "Debe insertar una consulta")]
        public string Consult { get; set; }
    }

    [MetadataType(typeof(TextMetadata))]
    public partial class Text { }

    class TextMetadata
    {
        [DisplayName("Descriptor")]
        [Required(ErrorMessage = "Debe insertar un descriptor")]
        public string Descriptor { get; set; }
        [DisplayName("Contenido")]
        [Required(ErrorMessage = "Debe tener contendio")]
        public string Content { get; set; }
    }

}

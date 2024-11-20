using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Oqtane.Models;

namespace RLTechnologies.Module.Banner.Models
{
    [Table("RLTechnologiesBanner")]
    public class Banner : IAuditable
    {
        [Key]
        public int BannerId { get; set; }
        public int ModuleId { get; set; }
        public string Name { get; set; }
        public string BannerTitle { get; set; }
        public string BannerSubTitle { get; set; }
        public string BannerUrl { get; set; }
        public string BannerText { get; set; }

        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using System.Xml.Serialization;
using Microsoft.EntityFrameworkCore;

namespace Ch10Ex02DataSerialization.AutoGenModels;

[Index("CategoryName", Name = "CategoryName")]
public partial class Category
{
    [Key]
    public int CategoryId { get; set; }

    [Column(TypeName = "nvarchar (15)")]
    public string CategoryName { get; set; } = null!;

    [Column(TypeName = "ntext")]
    public string? Description { get; set; }

    [Column(TypeName = "image")]
    public byte[]? Picture { get; set; }

    [XmlIgnore]
    [JsonIgnore]
    [InverseProperty("Category")]
    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}

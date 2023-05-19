﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace WebSiteTest.Areas.ProductsManager.Models
{
    public partial class Products
    {
        public Products()
        {
            OrderDetail = new HashSet<OrderDetail>();
        }

        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public int ProductUnitPrice { get; set; }
        public int ProductQty { get; set; }
        public string ProductCategory { get; set; }
        public string ProductSupplierId { get; set; }
        public string ProductStatus { get; set; }
        public DateTime ProductOnDate { get; set; }
        public DateTime ProductOffDate { get; set; }
        public string ProductPic { get; set; }
        public string ProductNotes { get; set; }

        public virtual ICollection<OrderDetail> OrderDetail { get; set; }
    }
}
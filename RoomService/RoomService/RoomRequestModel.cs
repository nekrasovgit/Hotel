﻿using Microsoft.AspNetCore.Http;
using RoomService.RoomModel;
using RoomService.ValidationAttributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RoomService.RoomService
{
    public class RoomRequestModel
    {
        [Required(ErrorMessage = "Name is required", AllowEmptyStrings = false)]
        [StringLength(10)]
        public string Name { get; set; }
        [Required(ErrorMessage = "Number is required")]
        [Range(1, 100)]
        public int Number { get; set; }
        [Required(ErrorMessage = "Number Of People is required")]
        [Range(1, 10)]
        public int NumberOfPeople { get; set; }
        [Required(ErrorMessage = "Price For Night required")]
        [Range(1, 1000)]
        public decimal PriceForNight { get; set; }
        [Required(ErrorMessage = "Description is required")]
        [StringLength(200)]
        public string Description { get; set; }
        [Required(ErrorMessage = "Room Type is required")]
        [Range(1, 4)]
        public RoomType RoomType { get; set; }
        [Required(ErrorMessage = "Images is required")]
        [MaxFileSize(5 * 1024 * 1024)]
        [AllowedExtensions(new string[] { ".jpg", ".png", ".jpeg" })]
        public IFormFileCollection Images { get; set; }
    }
}

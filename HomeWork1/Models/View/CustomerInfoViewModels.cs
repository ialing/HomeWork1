﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HomeWork1.Models.View
{
    public class CustomerInfoViewModels
    {
        public int Id { get; set; }
        public string 客戶名稱 { get; set; }
        public Nullable<int> 聯絡人數量 { get; set; }

        public Nullable<int> 銀行帳戶數量 { get; set; }

    }
}
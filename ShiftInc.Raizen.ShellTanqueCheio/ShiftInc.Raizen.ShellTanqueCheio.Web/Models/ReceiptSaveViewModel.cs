﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShiftInc.Raizen.ShellTanqueCheio.Web.Models
{
    public class ReceiptSaveViewModel
    {
        public string Status { get; set; }
        public Entity.Receipt Receipt { get; set; }
        public List<Entity.Receipt> Receipts { get; set; }
    }
}
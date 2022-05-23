﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orden.Model
{
    [Table("OrderStateGet")]
    public class OrderStatesGet
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdOrder { get; set; }
        [Key]
        [Column(Order = 1)]
        public int IdState { get; set; }
        public bool Active { get; set; }
    }
}

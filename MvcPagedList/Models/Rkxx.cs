using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MvcPagedList.Models
{
    [Table("rkxx")]
    public class Rkxx
    {
        [Key]
        public int idzz { get; set; }
        public string 单号 { get; set; }
        public DateTime 日期 { get; set; }
        public string 商品名称 { get; set; }
        public int 数量 { get; set; }
        public string 供货商 { get; set; }
        public string 经办人 { get; set; }
        public string 仓库 { get; set; }
    }
}
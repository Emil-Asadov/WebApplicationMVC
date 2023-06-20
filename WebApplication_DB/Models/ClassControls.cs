using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace WebApplication_DB.Models
{
    public class ClassControls
    {
        [Display(Name = "Qeydiyyat")]
        public int custIdn { get; set; }

        [Display(Name = "Ad"), /*Required(ErrorMessage = "Adı daxil edin"),*/ StringLength(20, ErrorMessage = "Simvol sayı {2} ve {1} aralığında olmalıdır", MinimumLength = 3), DataType(DataType.Text, ErrorMessage = "Mətn tipdir")]
        public string name { get; set; }

        [Display(Name = "Soyad"), Required(ErrorMessage = "Soyadı daxil edin"), StringLength(20, ErrorMessage = "Simvol sayı {2} ve {1} aralığında olmalıdır", MinimumLength = 3), DataType(DataType.Text, ErrorMessage = "Mətn tipdir")]
        public string surname { get; set; }

        [Display(Name = "Doğum yeri"), Required(ErrorMessage = "Doğum yerini daxil edin"), StringLength(20, ErrorMessage = "Simvol sayı {2} ve {1} aralığında olmalıdır", MinimumLength = 3), DataType(DataType.Text, ErrorMessage = "Mətn tipdir")]
        public string birthPlace { get; set; }

        [Display(Name = "Doğum tarixi"), Required(ErrorMessage = "Doğum tarixini daxil edin"), StringLength(20, ErrorMessage = "Simvol sayı {2} ve {1} aralığında olmalıdır", MinimumLength = 3), DataType(DataType.Text, ErrorMessage = "Mətn tipdir")]
        public string birthDate { get; set; }

        [Display(Name = "Cins"), Required(ErrorMessage = "Cinsi daxil edin")]
        public int gender { get; set; }

        [Display(Name = "Cins")]
        public string genderName { get; set; }

        [Display(Name = "Sənəd nömrəsi"), Required(ErrorMessage = "Sənəd nömrəsini daxil edin"), StringLength(20, ErrorMessage = "Simvol sayı {2} ve {1} aralığında olmalıdır", MinimumLength = 3), DataType(DataType.Text, ErrorMessage = "Mətn tipdir")]
        public string docNo { get; set; }

        [Display(Name = "Finkod"), Required(ErrorMessage = "Finkodu daxil edin"), StringLength(20, ErrorMessage = "Simvol sayı {2} ve {1} aralığında olmalıdır", MinimumLength = 3), DataType(DataType.Text, ErrorMessage = "Mətn tipdir")]
        public string finCode { get; set; }

        [Display(Name = "Telefon nömrəsi"), Required(ErrorMessage = "Telefon nömrəsini daxil edin"), StringLength(20, ErrorMessage = "Simvol sayı {2} ve {1} aralığında olmalıdır", MinimumLength = 3), DataType(DataType.Text, ErrorMessage = "Mətn tipdir")]
        public string phoneNumber { get; set; }

        [Display(Name = "Email"), Required(ErrorMessage = "Emaili daxil edin"), DataType(DataType.EmailAddress), RegularExpression(@"^[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}$", ErrorMessage = "Emaili düzgün daxil edin")]
        public string email { get; set; }

        [NotMapped]
        public List<ClassGender> lstGender { get; set; }
    }
}

using Model.In.Base;
using System.ComponentModel.DataAnnotations;

namespace Model.In
{
    public class ServerIn: BaseIn
    {
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ValidationText))]
        public string Name { get; set; }
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ValidationText))]
        [RegularExpression(@"\b((25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)(\.|$)){4}\b", ErrorMessageResourceName = "MaskIp", ErrorMessageResourceType = typeof(ValidationText))]
        public string Ip { get; set; }
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ValidationText))]
        public int Port { get; set; }
    }
}

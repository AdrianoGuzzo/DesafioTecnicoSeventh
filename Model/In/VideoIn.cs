using Model.In.Base;
using Model.Out;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text;

namespace Model.In
{
    public class VideoIn : BaseIn
    {
        public VideoIn(string serverId, string description)
        {
            ServerId = serverId;
            Description = description;
        }
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ValidationText))]
        public Guid Id { get; private set; }
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ValidationText))]
        public string ServerId { get; }
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ValidationText))]
        public string Description { get; }
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ValidationText))]
        public long SizeInBytes { get; private set; }

        public void SetFileInfo(FileOut fileOut)
        {
            Id = fileOut.Id;
            SizeInBytes = fileOut.SizeInBytes;
        }
    }
}

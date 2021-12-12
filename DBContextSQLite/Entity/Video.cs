using DBContextSQLite.Entity.Base;
using Model.In;
using Model.Out;
using System;
using System.Collections.Generic;
using System.Text;

namespace DBContextSQLite.Entity
{
    public class Video : EntityBase<VideoIn, VideoOut>
    {
        public Video()
        {

        }
        public Guid ServerId { get; set; }
        public string Description { get; set; }
        public long SizeInBytes { get; set; }
        public Server Server { get; set; }
        public override void CreateFrom(VideoIn modelIn)
        {
            this.Id = modelIn.Id;
            this.ServerId = Guid.Parse(modelIn.ServerId);
            this.Description = modelIn.Description;
            this.SizeInBytes = modelIn.SizeInBytes;
        }

        public override VideoOut MapperToOut()
        => EntiyToOut(this);
        public static Func<Video, VideoOut> ProjectionToOut()
           => x => EntiyToOut(x);

        public override void UpdateFrom(VideoIn modelIn)
        {
            this.ServerId = Guid.Parse(modelIn.ServerId);
            this.Description = modelIn.Description;
            this.SizeInBytes = modelIn.SizeInBytes;
        }
        private static VideoOut EntiyToOut(Video video)
            => new VideoOut(
                video.Id, video.ServerId, video.Description, video.SizeInBytes);
    }
}

using System;

namespace Model.Out
{
    public class VideoOut
    {
        public VideoOut(Guid id, Guid serverId, string description, long sizeInBytes)
        {
            Id = id;
            ServerId = serverId;
            Description = description;
            SizeInBytes = sizeInBytes;
           
        }
        public Guid Id { get; }
        public Guid ServerId { get; }
        public string Description { get; }
        public long SizeInBytes { get; }       
    }
}

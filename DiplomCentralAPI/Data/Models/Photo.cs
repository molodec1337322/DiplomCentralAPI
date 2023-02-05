using System.Reflection.Metadata;

namespace DiplomCentralAPI.Data.Models
{
    public class Photo
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Path { get; set; }

        public Experiment Experiment { get; set; }
        public int ExperimentId { get; set; }
    }
}

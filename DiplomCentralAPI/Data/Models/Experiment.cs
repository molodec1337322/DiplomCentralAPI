namespace DiplomCentralAPI.Data.Models
{
    public class Experiment
    {
        public int Id { get; set; }
        public DateTime StartedAt { get; set; }
        public DateTime EndedAt { get; set; }
        public string VideoPath { get; set; }
        public string ResultPath { get; set; }

        public IEnumerable<Photo> Photos { get; set; }
        public int SchemaId { get; set; }
        public Schema Schema { get; set; }
        public int? HandlerId { get; set; }
        public Handler? Handler { get; set; }
    }
}

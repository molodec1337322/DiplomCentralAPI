namespace DiplomCentralAPI.Data.Models
{
    public class Schema
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public string VideoPath { get; set; }
        public string Text { get; set; }

        public IEnumerable<Experiment> Experiments { get; set; }
        public int HandlerId { get; set; }
        public Handler Handler { get; set; }
    }
}

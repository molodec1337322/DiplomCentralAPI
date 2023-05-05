namespace DiplomCentralAPI.Data.Models
{
    public class MyHandler
    {
        public int Id { get; set; }
        public string ScriptPath { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }

        public IEnumerable<Experiment> Experiments { get; set; }
    }
}

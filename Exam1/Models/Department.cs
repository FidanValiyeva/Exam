namespace Exam1.Models
{
    public class Department:BaseEntity
    {
        public string Name {  get; set; }   
        public IEnumerable<Teacher> Teachers { get; set; }
    }
}

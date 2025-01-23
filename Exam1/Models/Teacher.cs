namespace Exam1.Models
{
    public class Teacher:BaseEntity
    {
        public string FullName {  get; set; }  
        public string ImageUrl {  get; set; }  
        public int DepartmentId {  get; set; }
        public Department Department { get; set; }
    }
}

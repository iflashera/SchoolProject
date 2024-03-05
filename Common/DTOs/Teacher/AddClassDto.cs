namespace Common.DTOs.Teacher;

public class AddClassDto
{
    public string ClassName { get; set; }
    public string ClassSection { get; set; }
    public ICollection<int> SubjectIds { get; set; }

}

using System.ComponentModel.DataAnnotations;

namespace Todo.Repo
{
    public class TodoView
    {
        [Required]
        public string Titulo { get; set; }
        public string Descricao { get; set; }
    }
}

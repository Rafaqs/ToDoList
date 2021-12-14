using System;

namespace Todo.Dominio
{
    public class ToDo
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public DateTime Data { get; set; } = DateTime.Now;
        public bool Finalizado { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace AgendaProPH.Models
{
    public class Projeto
    {
        public int ID { get; set; }
        public string Titulo { get; set; } = ""; // inicializa vazio
        public string Descricao { get; set; } = "";
        public string Responsavel { get; set; } = "";
        public string Status { get; set; } = "";
        public List<Tarefa> ListaTarefas { get; set; } = new List<Tarefa>();
    }
}

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Tarefas.Web.Models;

public class TarefaViewModel
{
    public int Id { get; set; }
    [Required(ErrorMessage ="Por favor informe o titulo!")]
    [MinLength(5, ErrorMessage = "O titulo deverá conter pelo menos 5 caracteres!")]
    [DisplayName("Título")]    
    public string? Titulo { get; set; }        
    [Required(ErrorMessage ="Descrição obrigatória!")]
    [MinLength(5, ErrorMessage = "A Descrição deverá conter no mínimo 5 caracteres!!")]
    [DisplayName("Descrição")]    
    public string? Descricao { get; set; }  
    
    [DisplayName("Concluída")]
    public bool Concluida { get; set; }
}
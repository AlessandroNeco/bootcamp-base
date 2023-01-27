using Microsoft.AspNetCore.Mvc;
using Tarefas.Web.Models;
using Tarefas.DTO;
using Tarefas.DAO;

namespace Tarefas.Web.Controllers
{
    public class TarefaController : Controller
    {
        public List<Tarefa> listaDeTarefas { get; set; }

        public TarefaController()
        {
             
        }
        
        public IActionResult Details(int id)
        {
            var tarefaDAO = new TarefaDAO();
            var tarefaDTO = tarefaDAO.Consultar(id);
            var tarefa = new Tarefa(){
                Id = tarefaDTO.Id,
                Titulo = tarefaDTO.Titulo,
                Descricao = tarefaDTO.Descricao,
                Concluida = tarefaDTO.Concluida
            };
            return View(tarefa);
        }

        public IActionResult Index()
        {            
            var tarefaDAO = new TarefaDAO();
            var listaDeTarefasDTO = tarefaDAO.Consultar();

            var listaDeTarefa = new List<Tarefa>();

            foreach (var tarefaDTO in listaDeTarefasDTO){
                listaDeTarefa.Add(new Tarefa(){
                    Id = tarefaDTO.Id,
                    Titulo = tarefaDTO.Titulo,
                    Descricao = tarefaDTO.Descricao,
                    Concluida = tarefaDTO.Concluida

                });
            }
            return View(listaDeTarefa);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Tarefa tarefa)
        {
            var tarefaDTO = new TarefaDTO 
            {
                Titulo = tarefa.Titulo,
                Descricao = tarefa.Descricao,
                Concluida = tarefa.Concluida
            };

            var tarefaDAO = new TarefaDAO();
            tarefaDAO.Criar(tarefaDTO);

            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult Update(Tarefa tarefa){
            var tarefaDAO = new TarefaDAO();
            var tarefaDTO = new TarefaDTO{
                Id = tarefa.Id,
                Titulo = tarefa.Titulo,
                Descricao = tarefa.Descricao,
                Concluida = tarefa.Concluida
            };
            
            tarefaDAO.Atualizar(tarefaDTO);
            return RedirectToAction("Index");
        }

        public IActionResult Update(int id){
            var tarefaDAO = new TarefaDAO();
            var tarefaDTO = tarefaDAO.Consultar(id);

            var tarefa = new Tarefa(){
                Id = tarefaDTO.Id,
                Titulo = tarefaDTO.Titulo,
                Descricao = tarefaDTO.Descricao,
                Concluida = tarefaDTO.Concluida
            };
            return View(tarefa);
        }

        public IActionResult Delete(int id){
            var tarefaDAO = new TarefaDAO();
            tarefaDAO.Exlcuir(id);
            
            return RedirectToAction("Index");
        }   
    }
}
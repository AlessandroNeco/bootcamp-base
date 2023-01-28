using Microsoft.AspNetCore.Mvc;
using Tarefas.Web.Models;
using Tarefas.DTO;
using Tarefas.DAO;



namespace Tarefas.Web.Controllers
{
    public class TarefaController : Controller
    {
        public List<TarefaViewModel> listaDeTarefas { get; set; }
        //private readonly TarefaDAO _tarefaDAO;
        private readonly ITarefaDAO _tarefaDAO;

        public TarefaController(ITarefaDAO tarefaDAO)
        {
             _tarefaDAO = tarefaDAO;
        }
        
        public IActionResult Details(int id)
        {
            var tarefaDAO = new TarefaDAO();
            var tarefaDTO = tarefaDAO.Consultar(id);
            var tarefa = new TarefaViewModel(){
                
                Id = tarefaDTO.Id,
                Titulo = tarefaDTO.Titulo,
                Descricao = tarefaDTO.Descricao,
                Concluida = tarefaDTO.Concluida
            };
            return View(tarefa);
        }

        public IActionResult Index()
        {            
            
            var listaDeTarefasDTO = _tarefaDAO.Consultar();

            var listaDeTarefa = new List<TarefaViewModel>();

            foreach (var tarefaDTO in listaDeTarefasDTO){
                listaDeTarefa.Add(new TarefaViewModel(){
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
        public IActionResult Create(TarefaViewModel tarefa)
        {

            if(!ModelState.IsValid){
                return View();
            }

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
        public IActionResult Update(TarefaViewModel tarefa){
            if(!ModelState.IsValid){
                return View();
            }

            var tarefaDTO = new TarefaDTO{
                Id = tarefa.Id,
                Titulo = tarefa.Titulo,
                Descricao = tarefa.Descricao,
                Concluida = tarefa.Concluida
            };
            
            _tarefaDAO.Atualizar(tarefaDTO);
            return RedirectToAction("Index");
        }

        public IActionResult Update(int id){
          
            var tarefaDTO = _tarefaDAO.Consultar(id);

            var tarefa = new TarefaViewModel(){
                Id = tarefaDTO.Id,
                Titulo = tarefaDTO.Titulo,
                Descricao = tarefaDTO.Descricao,
                Concluida = tarefaDTO.Concluida
            };
            return View(tarefa);
        }

        public IActionResult Delete(int id){
           
            _tarefaDAO.Exlcuir(id);
            
            return RedirectToAction("Index");
        }   
    }
}
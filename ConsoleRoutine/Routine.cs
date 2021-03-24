using System;
using System.Threading;

namespace TaskScheduler
{
    public partial class Routine
    {

        TaskScheduler oAgendador;
        //Para tratar a definição da tarefa
        ITaskDefinition oDefinicaoTarefa;
        //Para tratar a informação do Trigger
        ITimeTrigger oTrigger;
        //Para tratar a informação da Ação
        IExecAction oAcao;

        public  void CriarTarefa()
        {
            try
            {
                oAgendador = new TaskScheduler();
                oAgendador.Connect();

                //Atribuindo Definição de tarefa
                AtribuiDefinicaoTarefa();
                //Definindo a informação do gatilho da tarefa
                DefineInformacaoGatilho();
                //Definindo a informção da ação da tarefa
                DefineInformacaoAcao();

                //Obtendo a pasta raiz
                ITaskFolder root = oAgendador.GetFolder("\\");
                //Registrando a tarefa , se a tarefa ja estiver registrada então ela será atualizada
                IRegisteredTask regTask = root.RegisterTaskDefinition("_Rotina_Tarefa", oDefinicaoTarefa, (int)_TASK_CREATION.TASK_CREATE_OR_UPDATE, null, null, _TASK_LOGON_TYPE.TASK_LOGON_INTERACTIVE_TOKEN, "");

                //Para executar a tarefa imediatamenteo chamamos o método Run()
                IRunningTask runtask = regTask.Run(null);
                //exibe mensagem
                Console.WriteLine("Tarefa foi criada com sucesso !!");

                Thread.Sleep(TimeSpan.FromSeconds(2));
            }
            catch (Exception ex)
            {
                //exibe erros
                Console.WriteLine("Erro ao criar a tarefa !");
                System.Diagnostics.Debug.WriteLine("Erro ao criar a tarefa !");
            }
        }
        private void AtribuiDefinicaoTarefa()
        {
            try
            {
                oDefinicaoTarefa = oAgendador.NewTask(0);
                //Registra informação para a tarefa
                //nome do autor da tarefa
                oDefinicaoTarefa.RegistrationInfo.Author = "Flavio Junior";
                //descrição da tarefa
                oDefinicaoTarefa.RegistrationInfo.Description = "Rotina WebAPI";
                //Registro da data da tarefa
                oDefinicaoTarefa.RegistrationInfo.Date = DateTime.Today.ToString("yyyy-MM-ddTHH:mm:ss"); //formatacao
                //Definição da tarefa
                //Prioridade da Thread
                oDefinicaoTarefa.Settings.Priority = 7;
                //Habilita a tarefa
                oDefinicaoTarefa.Settings.Enabled = true;
                //Para ocultar/exibir a tarefa
                oDefinicaoTarefa.Settings.Hidden = false;
                //Tempo de execução limite para a tarefa
                oDefinicaoTarefa.Settings.ExecutionTimeLimit = "PT2M"; //10 minutos
                //Define que não precisa de conexão de rede
                oDefinicaoTarefa.Settings.RunOnlyIfNetworkAvailable = false;

                Console.WriteLine("Definindo Tarefa ....");

                Thread.Sleep(TimeSpan.FromSeconds(2));
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao definir tarefa !");
                throw ex;
            }


        }
        private void DefineInformacaoGatilho()
        {
            try
            {
                //informação do gatilho baseada no tempo - TASK_TRIGGER_TIME
                oTrigger = (ITimeTrigger)oDefinicaoTarefa.Triggers.Create(_TASK_TRIGGER_TYPE2.TASK_TRIGGER_TIME);           
                //ID do Trigger
                oTrigger.Id = "Trigger_Da_Tarefa";
                //hora de inicio
                oTrigger.StartBoundary = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss"); //yyyy-MM-ddTHH:mm:ss
                //hora de encerramento
                oTrigger.EndBoundary = DateTime.Now.AddDays(30).ToString("yyyy-MM-ddTHH:mm:ss"); //yyyy-MM-ddTHH:mm:ss

                oTrigger.Repetition.Interval = "PT1M";

                Console.WriteLine("Definindo Gatilho ....");

                Thread.Sleep(TimeSpan.FromSeconds(2));
            }
            catch (Exception ex)
            {

                Console.WriteLine("Erro ao definir Gatilho !!");
                throw ex;
            }
        }
        private void DefineInformacaoAcao()
        {
            try
            {
                //Informação da Ação baseada no exe- TASK_ACTION_EXEC
                oAcao = (IExecAction)oDefinicaoTarefa.Actions.Create(_TASK_ACTION_TYPE.TASK_ACTION_EXEC);
                //ID da Ação
                oAcao.Id = "Rotina";
                //Define o caminho do arquivo EXE a executar (Vamos abrir o Paint)                                                                                 
                oAcao.Path = "C:/Users/Flavio.Junior/Downloads/FileZilla_3.52.2_win64-setup.exe";


                Console.WriteLine("Definindo Informação da ação ....");
                Thread.Sleep(TimeSpan.FromSeconds(2));
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao definir as Informação da ação !");
                throw ex;
            }
        }
        public void DeletaTarefa()
        {
            try
            {
                //cria instância do agendador
                TaskScheduler oAgendador = new TaskScheduler();
                oAgendador.Connect();
                //define a pasta raiz
                ITaskFolder containingFolder = oAgendador.GetFolder("\\");
                //Deleta a tarefa
                containingFolder.DeleteTask("_Rotina_Tarefa", 0);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao deletar a tarefas!");
                System.Diagnostics.Debug.WriteLine("Erro ao deletar a tarefas!");
            }
        }
    }
}

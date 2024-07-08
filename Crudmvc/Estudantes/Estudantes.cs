namespace Crudmvc.Estudantes;

public class Estudante
{
    //parametros do aluno
    public Guid Id { get; init; }    
    public string Nome { get; private set; }
    public  bool ativo { get; private set; }
    public int matricula { get; private set; }
    public string Serie { get; private set; }

    public Estudante ( string nome, int matricula, string serie)
    {
        //Construtor 
        Id = Guid.NewGuid ();
        Nome = nome;
        ativo = true;
        this.matricula = matricula;
        Serie = serie;
    }

    //Metodo para atualizar dados do aluno
    public void AtualizarAluno(string nome,string serie)
    {
        Nome = nome;
        Serie = serie;
    }

    public void DesativarEstudante()
    {
        ativo = false;
    }
    public void AtivarEstudante()
    {
        ativo = true;
    }
}

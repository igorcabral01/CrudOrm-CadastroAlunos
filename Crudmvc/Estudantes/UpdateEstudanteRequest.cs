namespace Crudmvc.Estudantes;

//Record para buscar parametros para fazer Update do Aluno
public record UpdateEstudanteRequest(string Nome, int matricula, string Serie);


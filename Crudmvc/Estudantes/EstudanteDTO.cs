namespace Crudmvc.Estudantes;

//DTO DATA TRANSFER OBJECT É A NECESSIDADE DE PASSAR PARAMETROS PARA O FRONTEND MOSTRAR

public record EstudanteDTO(Guid Id,string Nome,int matricula,string Serie);

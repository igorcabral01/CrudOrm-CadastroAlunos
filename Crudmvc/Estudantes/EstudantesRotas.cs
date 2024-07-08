using Crudmvc.Data;
using Microsoft.EntityFrameworkCore;

namespace Crudmvc.Estudantes;

//Rotas De Estudantes
public static class EstudantesRotas
{
    public static void AddRotasEsutantes(this WebApplication app)
    {
        //Rota MAPGROUP para nao ficar chamando o estudante o tempo todo "Universal"
        var rotasestudantes = app.MapGroup("Estudantes");
        ////////////////////////////////////////////////////////////////////////////

        //Rota Post para criação de um novo estudante
        rotasestudantes.MapPost("",async (AddEstudantesRequest request, AppDbContext context) =>
        {
            //Condição Para verificar se ja existe um estudante com o mesmo nome
           var EstudanteCadastrado = await context.Estudantes.AnyAsync(Estudante => Estudante.Nome == request.Nome);
            //condição de retorno de erro caso cadastro de estudante com o mesmo nome
            if (EstudanteCadastrado)
             return  Results.Conflict("Estudante ja existe em nossa base de dados!, por favor verificar... ");
  
            //Criando um novo estudante e Adicionando no Banco
           var novoEstudante = new Estudante(request.Nome,request.matricula,request.Serie);
           await context.Estudantes.AddAsync(novoEstudante);
           await context.SaveChangesAsync();

            var RetonarEstudantesDTO = new EstudanteDTO(novoEstudante.Id, novoEstudante.Nome,
                novoEstudante.matricula,novoEstudante.Serie);
            return Results.Ok(RetonarEstudantesDTO);
        });

        //Rota Get para buscar usuarios no banco
        rotasestudantes.MapGet("Usuarios", async (AppDbContext context) =>
        {
            //usando where para filtrar usuarios ativos 
            var estudantes = await context.Estudantes.Where
            (Estudante => Estudante.ativo).Select(estudante => new EstudanteDTO(estudante.Id,estudante.Nome,estudante.matricula,estudante.Serie)).ToListAsync();
            return estudantes;
        });

        //Rota Put para alteração de usuarios no banco
        rotasestudantes.MapPut("{Id}", async (Guid Id,UpdateEstudanteRequest request, AppDbContext context) =>
        {
            //Logica para fazer a alteração conforme o ID do aluno no banco
            var estudante = await context.Estudantes.SingleOrDefaultAsync(estudante => estudante.Id == Id);
            if (estudante == null)
                return Results.NotFound("Aluno nao encontrado, tente novamente");
            estudante.AtualizarAluno(request.Nome, request.Serie);
            
            //Contexto para salvar e retonar o resultado da alteração
            await context.SaveChangesAsync();
            return Results.Ok( new EstudanteDTO(estudante.Id,estudante.Nome,estudante.matricula,estudante.Serie));
           
        });

        //Rota Delet para desativar matriculas de aluno no banco
        rotasestudantes.MapDelete("{Id}", async (Guid Id, AppDbContext context) =>
        {
            var estudante = await context.Estudantes.SingleOrDefaultAsync(estudante => estudante.Id == Id);

            if(estudante == null)
                return Results.NotFound("Estudante nao encontrado");

            estudante.DesativarEstudante();
            await context.SaveChangesAsync();
            return Results.Ok($"Aluno {estudante.Nome} foi desativado com sucesso");
        });

    }
}



using ScreenSound.Core.Modelos;
using ScreenSound.EntityFrameworkCore.Repositories;
using System.Linq.Expressions;

namespace ScreenSound.Web.Host.services
{
    public class AppServiceBase<T>(Repository<T> repository) where T : class, IEntity
    {
        private readonly Repository<T> _repository = repository;

        public IResult Listar()
        {
            var lista = _repository.Listar();
            return Results.Ok(lista);
        }

        public IResult RecuperarPor(Expression<Func<T, bool>> predicate)
        {
            var compiledPredicate = predicate.Compile();
            var item = _repository.RecuperarPor(compiledPredicate);
            if (item == null)
            {
                return Results.NotFound();
            }
            return Results.Ok(item);
        }

        public IResult Adicionar(T item)
        {
            _repository.Adicionar(item);
            return Results.Ok();
        }

        public IResult Atualizar(T item, Action<T> updateAction)
        {
            var itemExistente = _repository.RecuperarPor(x => x.Id == item.Id);
            if (itemExistente == null)
            {
                return Results.NotFound();
            }

            updateAction(itemExistente);
            _repository.Atualizar(itemExistente);
            return Results.Ok();
        }

        public IResult Deletar(int id)
        {
            try
            {
                var item = _repository.RecuperarPor(x => (x as IEntity)?.Id == id);
                if (item == null)
                {
                    return Results.NotFound();
                }
                _repository.Deletar(item);
                return Results.NoContent();
            }
            catch (Exception)
            {
                return Results.BadRequest();
            }

        }
    }
}

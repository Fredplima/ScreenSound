using ScreenSound.Core.Modelos;
using ScreenSound.Core.Repositories;
using ScreenSound.EntityFrameworkCore.Banco;

namespace ScreenSound.EntityFrameworkCore.Repositories
{
    public class Repository<T> : IRepository<T> where T : class, IEntity
    {
        protected readonly ScreenSoundContext context;

        public Repository(ScreenSoundContext context)
        {
            this.context = context;
        }

        public T? Get(int id)
        {
            return context.Set<T>().Find(id);            
        }

        public IEnumerable<T> Listar()
        {
            return [.. context.Set<T>()];
        }

        public void Adicionar(T objeto)
        {
            context.Set<T>().Add(objeto);
            context.SaveChanges();
        }

        public void Atualizar(T objeto)
        {
            context.Set<T>().Update(objeto);
            context.SaveChanges();
        }

        public void Deletar(T objeto)
        {
            context.Set<T>().Remove(objeto);
            context.SaveChanges();
        }

        public T? RecuperarPor(Func<T, bool> condicao)
        {
            return context.Set<T>().Where(condicao).FirstOrDefault();
        }

        public ICollection<T>? RecuperarTodosPor(Func<T, bool> condicao)
        {
            return context.Set<T>().Where(condicao).ToList();
        }

    }
}

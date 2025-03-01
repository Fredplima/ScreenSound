
using ScreenSound.Core.Modelos;

namespace ScreenSound.Core.Repositories
{
    public interface IRepository<T> where T : IEntity
    {
        public T? Get(int id);
        public IEnumerable<T> Listar();

        public void Adicionar(T objeto);

        public void Atualizar(T objeto);

        public void Deletar(T objeto);

        public T? RecuperarPor(Func<T, bool> condicao);

        public ICollection<T>? RecuperarTodosPor(Func<T, bool> condicao);
    }
}

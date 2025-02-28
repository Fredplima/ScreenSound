namespace ScreenSound.Banco
{
    public class Dal<T>(ScreenSoundContext context) where T : class
    {
        protected readonly ScreenSoundContext context = context;

        public IEnumerable<T> Listar()
        {
            return [.. context.Set<T>()];
        }

        public  void Adicionar(T objeto)
        {
            context.Set<T>().Add(objeto);
            context.SaveChanges();
        }

        public  void Atualizar(T objeto)
        {
            context.Set<T>().Update(objeto);
            context.SaveChanges();
        }

        public  void Deletar(T objeto)
        {
            context.Set<T>().Remove(objeto);
            context.SaveChanges();
        }

        public T? RecuperarPor(Func<T, bool> condicao)
        {
            return context.Set<T>().FirstOrDefault(condicao);
        }

        public ICollection<T>? RecuperarTodosPor(Func<T, bool> condicao)
        {
            return context.Set<T>().Where(condicao).ToList();
        }
    }
}

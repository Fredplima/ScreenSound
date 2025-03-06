namespace ScreenSound.Shared.Dados.Banco;
public class Repository<T>(ScreenSoundContext context) where T : class
{
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
        return context.Set<T>().FirstOrDefault(condicao);
    }

    public IQueryable<T> GetAll()
    {
        return context.Set<T>();
    }
}

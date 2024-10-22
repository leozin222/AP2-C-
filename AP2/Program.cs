using System;
using System.Collections.Generic;

// Classe que gerencia uma coleção de ItemBiblioteca
public class Biblioteca
{
    // Coleção de itens e usuários
    private List<ItemBiblioteca> itens;
    private List<Usuario> usuarios;

    // Construtor
    public Biblioteca()
    {
        itens = new List<ItemBiblioteca>();
        usuarios = new List<Usuario>();
    }

    // Método para adicionar um item
    public void AdicionarItem(ItemBiblioteca item)
    {
        itens.Add(item);
        Console.WriteLine($"{item.Titulo} foi adicionado à biblioteca.");
    }

    // Método para remover um item
    public void RemoverItem(ItemBiblioteca item)
    {
        itens.Remove(item);
        Console.WriteLine($"{item.Titulo} foi removido da biblioteca.");
    }

    // Métodos com sobrecarga para buscar um item por título ou por ID
    public ItemBiblioteca BuscarItemPorTitulo(string titulo)
    {
        ItemBiblioteca itemEncontrado = itens.Find(item => item.Titulo.Equals(titulo, StringComparison.OrdinalIgnoreCase));

        if (itemEncontrado != null)
        {
            Console.WriteLine($"Livro encontrado por título: {itemEncontrado.Titulo}");
        }
        else
        {
            Console.WriteLine("Livro não encontrado por título.");
        }

        return itemEncontrado;
    }

    public ItemBiblioteca BuscarItemPorId(int id)
    {
        ItemBiblioteca itemEncontrado = itens.Find(item => item.Id == id);

        if (itemEncontrado != null)
        {
            Console.WriteLine($"Livro encontrado por ID: {itemEncontrado.Titulo}");
        }
        else
        {
            Console.WriteLine("Livro não encontrado por ID.");
        }

        return itemEncontrado;
    }

    // Métodos para gerenciar usuários
    public void AdicionarUsuario(Usuario usuario)
    {
        usuarios.Add(usuario);
        Console.WriteLine($"{usuario.Nome} foi cadastrado na biblioteca.");
    }

    public Usuario BuscarUsuarioPorId(int id)
    {
        return usuarios.Find(usuario => usuario.Id == id);
    }

    public void RegistrarEmprestimo(int usuarioId, int itemId)
    {
        var usuario = BuscarUsuarioPorId(usuarioId);
        var item = BuscarItemPorId(itemId);

        if (usuario != null && item != null)
        {
            usuario.HistoricoEmprestimos.Add(item);
            RemoverItem(item);
            Console.WriteLine($"{usuario.Nome} emprestou o livro: {item.Titulo}");
        }
        else
        {
            Console.WriteLine("Usuário ou item não encontrado.");
        }
    }

    public void DevolverLivro(int usuarioId, int itemId)
    {
        var usuario = BuscarUsuarioPorId(usuarioId);
        var item = usuario?.HistoricoEmprestimos.Find(i => i.Id == itemId);

        if (item != null)
        {
            usuario.HistoricoEmprestimos.Remove(item);
            AdicionarItem(item);
            Console.WriteLine($"{usuario.Nome} devolveu o livro: {item.Titulo}");
        }
        else
        {
            Console.WriteLine("Item não encontrado nos empréstimos do usuário.");
        }
    }
}

// Classe base abstrata
public abstract class ItemBiblioteca
{
    // Propriedades
    public int Id { get; set; }
    public string Titulo { get; set; }
    protected string DescricaoInterna { get; set; }

    // Construtor
    protected ItemBiblioteca(int id, string titulo)
    {
        Id = id;
        Titulo = titulo;
        DescricaoInterna = "O livro está em uso no momento";
    }
}

// Classe derivada de ItemBiblioteca
public class Livro : ItemBiblioteca
{
    // Propriedade adicional
    public string Autor { get; set; }

    // Construtor
    public Livro(int id, string titulo, string autor) : base(id, titulo)
    {
        Autor = autor;
    }

    // Método para exibir a DescricaoInterna
    public void ExibirDescricaoInterna()
    {
        Console.WriteLine($"Descrição Interna: {DescricaoInterna}");
    }
}

// Classe para usuários
public class Usuario
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public List<ItemBiblioteca> HistoricoEmprestimos { get; set; }

    public Usuario(int id, string nome)
    {
        Id = id;
        Nome = nome;
        HistoricoEmprestimos = new List<ItemBiblioteca>();
    }
}

// Ponto de entrada do programa
class Program
{
    static void Main()
    {
        // Criar instâncias de Biblioteca, Livro e Usuário
        Biblioteca biblioteca = new Biblioteca();
        Livro livro1 = new Livro(1, "Sapiens - Uma Breve História da Humanidade", "Yuval Harari");
        Livro livro2 = new Livro(2, "21 lições para o século 21", "Yuval Harari");
        Usuario usuario1 = new Usuario(1, "Leonardo Martins"); 

        // Adicionar livros à biblioteca
        biblioteca.AdicionarItem(livro1);
        biblioteca.AdicionarItem(livro2);

        // Cadastrar usuário
        biblioteca.AdicionarUsuario(usuario1);
 
        // Buscar livro por título
        ItemBiblioteca livroEncontradoPorTitulo = biblioteca.BuscarItemPorTitulo("Sapiens - Uma Breve História da Humanidade");

        // Buscar livro por ID
        ItemBiblioteca livroEncontradoPorId = biblioteca.BuscarItemPorId(1);

        if (livroEncontradoPorTitulo != null)
        {
            // Registrar empréstimo do livro
            biblioteca.RegistrarEmprestimo(usuario1.Id, livro1.Id);

            // Devolver o livro
            biblioteca.DevolverLivro(usuario1.Id, livro1.Id);

            // Exibir a DescricaoInterna do livro
            ((Livro)livroEncontradoPorTitulo).ExibirDescricaoInterna();
        }
    }
}







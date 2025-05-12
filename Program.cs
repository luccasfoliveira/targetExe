using System.Data;
using System.Globalization;
using System.Text.Json;
using Exercicios;

class Program
{
    static void Main()
    {
        ExercicioUm();
        ExercicioDois();
        ExercicioTres();
        ExercicioQuatro();
        ExercicioCinco();
    }

    static void ExercicioUm()
    {
        int SOMA = 0, K = 0, INDICE = 13;
        while (K < INDICE)
        {
            K += 1;
            SOMA += K;
        }
        Console.WriteLine($"Resultado do Exercício 1: {SOMA}");  // saída: 91
    }

    static void ExercicioDois()
    {
        Console.Write("Informe um número para verificar se pertence à sequência de Fibonacci: ");
        if (int.TryParse(Console.ReadLine(), out int numero))
        {
            bool pertence = Fibonacci()
                // pego cada elemento menor que o numero infomado pelo usuário
                .TakeWhile(n => n <= numero)
                // e aqui verifica se contem o numero informado
                .Contains(numero);

            Console.WriteLine(pertence
                ? $"{numero} pertence à sequência de Fibonacci."
                : $"{numero} NÃO pertence à sequência de Fibonacci.");
        }
        else
        {
            Console.WriteLine("Número inválido.");
        }
    }

    static IEnumerable<int> Fibonacci()
    {
        // estou utilizando o yield, pois ele é um gerador (infinito, neste caso)
        // e acaba melhorando a performace
        int a = 0, b = 1;
        while (true)
        {
            yield return a;
            // atualizo os valores correspondentes aos dois ultimos numeros da sequencia
            (a, b) = (b, a + b);
        }
    }
    static void ExercicioTres()
    {
        string filePath = "dados.json";
        // aqui eu carrego o faturamento, e serializo numa lista (tipada)
        var faturamentos = CarregarFaturamentos(filePath);

        if (faturamentos == null)
        {
            Console.WriteLine("Arquivo de faturamento não encontrado ou inválido.");
            return;
        }

        var faturamentosValidos = faturamentos.Where(f => f.valor > 0).ToList();

        var menorFaturamento = CalculoFaturamento.CalcularMenorFaturamento(faturamentosValidos);
        var maiorFaturamento = CalculoFaturamento.CalcularMaiorFaturamento(faturamentosValidos);
        var diasAcimaDaMedia = CalculoFaturamento.CalcularDiasAcimaDaMedia(faturamentosValidos);

        Console.WriteLine("Resultado do Exercício 3:");
        Console.WriteLine($"Menor valor de faturamento: {menorFaturamento:C}");
        Console.WriteLine($"Maior valor de faturamento: {maiorFaturamento:C}");
        Console.WriteLine($"Número de dias com faturamento superior à média: {diasAcimaDaMedia}");
    }


    static List<FaturamentoDia>? CarregarFaturamentos(string filePath)
    {
        if (File.Exists(filePath))
        {
            string jsonData = File.ReadAllText(filePath);
            var faturamentos = JsonSerializer.Deserialize<List<FaturamentoDia>>(jsonData);
            return faturamentos;
        }
        else
        {
            return null;
        }
    }

    public static void ExercicioQuatro()
    {
        var faturamentoPorEstado = new Dictionary<string, double>
            {
                { "SP", 67836.43 },
                { "RJ", 36678.66 },
                { "MG", 29229.88 },
                { "ES", 27165.48 },
                { "Outros", 19849.53 }
            };

        double total = faturamentoPorEstado.Values.Sum();

        Console.WriteLine("Percentual por estado:\n");

        foreach (var (estado, valor) in faturamentoPorEstado)
        {
            double percentual = CalcularPercentual(valor, total);
            Console.WriteLine($"{estado,-7}: {percentual.ToString("F2", CultureInfo.InvariantCulture)}%");
        }
    }

    private static double CalcularPercentual(double valor, double total) =>
        // essa condição evita a divisão por zero se a lista for vazia
        // neste caso, nunca vai ser (rs)
        total == 0 ? 0 : valor / total * 100;

    public static void ExercicioCinco()
    {
        Console.Write("Digite uma palavra ou frase para inverter: ");
        string? textoOriginal = Console.ReadLine();

        if (string.IsNullOrEmpty(textoOriginal))
        {
            Console.WriteLine("Texto inválido. Por favor, digite algo.");
            return;
        }

        string textoInvertido = InverterTexto(textoOriginal);
        Console.WriteLine($"\nTexto invertido: {textoInvertido}");
    }

    private static string InverterTexto(string texto)
    {
        // criamos um array de caracteres do tamanho do input
        char[] invertido = new char[texto.Length];
        int j = 0;

        // e iteramos de forma inversa ao indice para realocar os caracteres
        for (int i = texto.Length - 1; i >= 0; i--)
        {
            invertido[j] = texto[i];
            j++;
        }

        return new string(invertido);
    }
}

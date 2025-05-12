namespace Exercicios
{
    public static class CalculoFaturamento
    {
        public static double CalcularMenorFaturamento(List<FaturamentoDia> faturamentos) =>
            faturamentos.Min(f => f.valor);

        public static double CalcularMaiorFaturamento(List<FaturamentoDia> faturamentos) =>
            faturamentos.Max(f => f.valor);

        public static double CalcularMediaMensal(List<FaturamentoDia> faturamentos) =>
            faturamentos.Average(f => f.valor);

        public static int CalcularDiasAcimaDaMedia(List<FaturamentoDia> faturamentos) =>
            faturamentos.Count(f => f.valor > CalcularMediaMensal(faturamentos));
    }
}

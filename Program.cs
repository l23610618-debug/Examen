using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exmane_segunda_op
{
    internal class Program
    {
        static void Main()
        {
            var reglas = new List<(string Id, int Prior, string[] Ante, string[] Conse)>
            {
                ("R1", 60, new[]{"Frecuencia_reposo_ok", "sueno_optimo" }, new[] {"Recuperacion_completa"}),
                ("R2", 80, new[]{"Recuperacion_completa", "-Fatiga_muscular" }, new[] {"Entrenamiento_intenso"}),
                ("R3", 90, new[]{"Entrenamiento_intenso", "Objetivo_hipertrofia" }, new[] {"Rutina_fuerza"}),
                ("R4", 75, new[]{"Recupoeracion_completa", "Fatiga_muscular" }, new[] {"Sesion_movilidad"}),
            };

            var casos = new[]
            {
                new[] {"Frecuencia_reposo_ok","sueno_optimo","Objetivo_hipertrofia"},
                new[] {"Frecuencia_reposo_ok", "sueno_optimo", "Fatiga_muscular"},
                new[] {"sueno_optimo", "Objetivo_hipertrofia"}
            };

            for(int i = 0; i < casos.Length; i++)
            {
                Console.WriteLine($"\n===Casos{i + 1} {{{string.Join(", ", casos[i])}}}===");
                var Hechos = new HashSet<string>(casos[i]);
                var Disparadas = new HashSet<string>();
                var Pasos = 1;
                while(true)
                {
                    var candidata = reglas
                        .Where(r => !Disparadas.Contains(r.Id) && r.Ante.All(cond => cond.StartsWith(".") ? !Hechos.Contains(cond.Substring(1)) : Hechos.Contains(cond)))
                        .OrderByDescending(r => r.Prior)
                        .FirstOrDefault();
                    if (candidata.Id == null)
                    {
                        Console.WriteLine($"[Paso{Pasos}] " );
                        break;

                    }
                    Disparadas.Add(candidata.Id);
                    foreach (var c in candidata.Conse) Hechos.Add(c);
                    Console.WriteLine($"[Paso {Pasos++}] Dispara {candidata.Id} (P:{candidata.Prior}) -> Deduce: {string.Join(", ", candidata.Conse)}");
                }
                Console.WriteLine($"Hechos Finales: {{ {string.Join(", ", Hechos)} }}");
            }

        }

    }
}

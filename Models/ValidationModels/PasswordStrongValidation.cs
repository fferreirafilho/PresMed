
using System;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace PresMed.Models.ValidationModels {
    public class PasswordStrongValidation : ValidationAttribute {

        public override bool IsValid(object value) {
            if (value == null || string.IsNullOrEmpty(value.ToString()))
                return false;

            return geraPontosSenha(value.ToString());
        }

        public bool geraPontosSenha(string senha) {
            if (senha == null) return false;
            int pontosPorTamanho = GetPontoPorTamanho(senha);
            int pontosPorMinusculas = GetPontoPorMinusculas(senha);
            int pontosPorMaiusculas = GetPontoPorMaiusculas(senha);
            int pontosPorDigitos = GetPontoPorDigitos(senha);
            int pontosPorSimbolos = GetPontoPorSimbolos(senha);
            int pontosPorRepeticao = GetPontoPorRepeticao(senha);
            var placar = pontosPorTamanho + pontosPorMinusculas + pontosPorMaiusculas + pontosPorDigitos + pontosPorSimbolos - pontosPorRepeticao;

            if (placar > 80)
                return true;
            else
                return false;
        }

        private int GetPontoPorTamanho(string senha) {
            return Math.Min(10, senha.Length) * 6;
        }

        private int GetPontoPorMinusculas(string senha) {
            int rawplacar = senha.Length - Regex.Replace(senha, "[a-z]", "").Length;
            return Math.Min(2, rawplacar) * 5;
        }

        private int GetPontoPorMaiusculas(string senha) {
            int rawplacar = senha.Length - Regex.Replace(senha, "[A-Z]", "").Length;
            return Math.Min(2, rawplacar) * 5;
        }

        private int GetPontoPorDigitos(string senha) {
            int rawplacar = senha.Length - Regex.Replace(senha, "[0-9]", "").Length;
            return Math.Min(2, rawplacar) * 5;
        }

        private int GetPontoPorSimbolos(string senha) {
            int rawplacar = Regex.Replace(senha, "[a-zA-Z0-9]", "").Length;
            return Math.Min(2, rawplacar) * 5;
        }

        private int GetPontoPorRepeticao(string senha) {
            Regex regex = new Regex(@"(\w)*.*\1");
            bool repete = regex.IsMatch(senha);
            if (repete) {
                return 30;
            }
            else {
                return 0;
            }
        }

    }
}

﻿using SAED.Core.Interfaces;
using System;

namespace SAED.Core.Entities
{
    public class Cpf : IEntity
    {
        public string Codigo { get; set; }

        public Cpf()
        {
        }

        public Cpf(string codigo)
        {
            Codigo = codigo;
        }

        public int Id { get; set; }

        public static Cpf Parse(string codigo)
        {
            if (TryParse(codigo, out var result))
            {
                return result;
            }

            throw new ArgumentException("CPF Inválido");
        }

        public static bool IsValid(string cpf)
        {
            var multiplicador1 = new[] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            var multiplicador2 = new[] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

            var cpfNormalized = cpf.Trim().Replace(".", "").Replace("-", "");
            if (cpfNormalized.Length != 11) return false;

            for (var j = 0; j < 10; j++)
            {
                if (j.ToString().PadLeft(11, char.Parse(j.ToString())) == cpfNormalized)
                {
                    return false;
                }
            }

            var tempCpf = cpfNormalized.Substring(0, 9);
            var soma = 0;

            for (var i = 0; i < 9; i++)
            {
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];
            }

            var resto = soma % 11;
            if (resto < 2)
            {
                resto = 0;
            }
            else
            {
                resto = 11 - resto;
            }

            var digito = resto.ToString();
            tempCpf += digito;
            soma = 0;
            for (var i = 0; i < 10; i++)
            {
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];
            }

            resto = soma % 11;
            if (resto < 2)
            {
                resto = 0;
            }
            else
            {
                resto = 11 - resto;
            }

            digito += resto;

            if (cpfNormalized.EndsWith(digito))
            {
                return true;
            }

            return false;
        }

        public static bool TryParse(string codigo, out Cpf cpf)
        {
            var multiplicador1 = new[] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            var multiplicador2 = new[] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

            cpf = new Cpf(codigo);

            var codigoNormalized = codigo.Trim().Replace(".", "").Replace("-", "");
            if (codigoNormalized.Length != 11) return false;

            for (var j = 0; j < 10; j++)
            {
                if (j.ToString().PadLeft(11, char.Parse(j.ToString())) == codigoNormalized)
                {
                    return false;
                }
            }

            var tempCpf = codigoNormalized.Substring(0, 9);
            var soma = 0;

            for (var i = 0; i < 9; i++)
            {
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];
            }

            var resto = soma % 11;
            if (resto < 2)
            {
                resto = 0;
            }
            else
            {
                resto = 11 - resto;
            }

            var digito = resto.ToString();
            tempCpf += digito;
            soma = 0;
            for (var i = 0; i < 10; i++)
            {
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];
            }

            resto = soma % 11;
            if (resto < 2)
            {
                resto = 0;
            }
            else
            {
                resto = 11 - resto;
            }

            digito += resto;

            if (codigoNormalized.EndsWith(digito))
            {
                cpf = new Cpf(codigo);
                return true;
            }

            cpf = new Cpf(codigo);
            return false;
        }

        public string Normalize()
        {
            return Codigo.Replace(".", string.Empty).Replace("-", string.Empty);
        }

        public override string ToString()
        {
            return Codigo;
        }

        public static implicit operator Cpf(string codigo)
        {
            return Parse(codigo);
        }
    }
}
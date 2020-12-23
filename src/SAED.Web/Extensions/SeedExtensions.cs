using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SAED.Core.Constants;
using SAED.Core.Entities;
using SAED.Infrastructure.Data;
using SAED.Infrastructure.Data.Seed;

namespace SAED.Web.Extensions
{
    public static class SeedExtensions
    {
        public static IApplicationBuilder SeedDatabase(this IApplicationBuilder app, IServiceProvider serviceProvider)
        {
            var context = serviceProvider.GetRequiredService<ApplicationDbContext>();
            var configuration = serviceProvider.GetRequiredService<IConfiguration>();
            var databaseProvider = configuration[DatabaseConstants.Database];

            var avaliacoes = new List<Avaliacao>
            {
                new Avaliacao {Id = 1, Codigo = "2020", Status = StatusAvaliacao.EmAndamento},
                new Avaliacao {Id = 2, Codigo = "2021", Status = StatusAvaliacao.ARealizar}
            };

            new EntitySeed<Avaliacao>(context, databaseProvider).Load(avaliacoes, "Avaliacoes");

            var distritos = new List<Distrito>
            {
                new Distrito {Id = 1, Nome = "Sede", Zona = Zona.Urbana, Distancia = 0},
                new Distrito {Id = 2, Nome = "Abóbora", Zona = Zona.Rural, Distancia = 110},
                new Distrito {Id = 3, Nome = "Itamotinga", Zona = Zona.Rural, Distancia = 75},
                new Distrito {Id = 4, Nome = "Juremal", Zona = Zona.Rural, Distancia = 50},
                new Distrito {Id = 5, Nome = "Carnaíba", Zona = Zona.Rural, Distancia = 25},
                new Distrito {Id = 6, Nome = "Maniçoba", Zona = Zona.Rural, Distancia = 40},
                new Distrito {Id = 7, Nome = "Pinhões", Zona = Zona.Rural, Distancia = 75},
                new Distrito {Id = 8, Nome = "Junco", Zona = Zona.Rural, Distancia = 35},
                new Distrito {Id = 9, Nome = "Massaroca", Zona = Zona.Rural, Distancia = 70},
                new Distrito {Id = 10, Nome = "Mandacaru", Zona = Zona.Rural, Distancia = 10}
            };

            new EntitySeed<Distrito>(context, databaseProvider).Load(distritos, "Distritos");

            var escolas = new List<Escola>
            {
                new () { Id = 1, Inep = 29024935, Nome = "02 DE JULHO", Telefone = "74988440798", Email = "DOISDEJULHOJUAZEIRO@HOTMAIL.COM", DistritoId = 4 },
                new () { Id = 2, Inep = 29024994, Nome = "15 DE JULHO", Telefone = "7436113278", Email = "QUINZEDEJULHO2014@OUTLOOK.COM", DistritoId = 4 },
                new () { Id = 3, Inep = 29026130, Nome = "25 DE JULHO", Telefone = "7488068024", Email = "ESCOLA25.DEJULHO@OUTLOOK.COM", DistritoId = 4 },
                new () { Id = 4, Inep = 29026601, Nome = "AMÉRICO TANURI - JUNCO", Telefone = "7498811198", Email = "", DistritoId = 5 },
                new () { Id = 5, Inep = 29026148, Nome = "AMÉRICO TANURI - MANIÇOBA", Telefone = "74988067161", Email = "", DistritoId = 4 },
                new () { Id = 6, Inep = 29025699, Nome = "AMÉRICO TANURY - ABÓBORA", Telefone = "7436179072", Email = "EMMSBONFIM@HOTMAIL.COM", DistritoId = 2 },
                new () { Id = 7, Inep = 29341256, Nome = "ANÁLIA BARBOSA DE SOUZA", Telefone = "7491552384", Email = "ANALIABARBOSA.EDU@GMAIL.COM", DistritoId = 1 },
                new () { Id = 8, Inep = 29026482, Nome = "ANTONIO FRANCISCO DE OLIVEIRA", Telefone = "7488158634", Email = "VALDETEMSF@HOTMAIL.COM", DistritoId = 5 },
                new () { Id = 9, Inep = 29429536, Nome = "ARGEMIRO JOSE DA CRUZ", Telefone = "7436110018", Email = "CEAJC1@HOTMAIL.COM", DistritoId = 1 },
                new () { Id = 10, Inep = 29026156, Nome = "BOM JESUS - BARAÚNA", Telefone = "7436187055", Email = "ESCOLA_BOMJESUSBARAUNA@HOTMAIL.COM", DistritoId = 4 },
                new () { Id = 11, Inep = 29025907, Nome = "BOM JESUS - NH1", Telefone = "", Email = "BOMJESUS_NH01@HOTMAIL.COM", DistritoId = 4 },
                new () { Id = 12, Inep = 29469120, Nome = "C.R.A. PROFª MAZZARELLO CAVALCANTI REIS DA ROCHA", Telefone = "", Email = "MAZZARELLOROCHA@OUTLOOK.COM", DistritoId = 1 },
                new () { Id = 13, Inep = 29362415, Nome = "CAIC - MISAEL AGUILAR", Telefone = "7436118041", Email = "ESCOLACAICJUA@GMAIL.COM", DistritoId = 1 },
                new () { Id = 14, Inep = 29024650, Nome = "CAXANGÁ", Telefone = "7436122900", Email = "ESCOLA_CAXANGA@HOTMAIL.COM", DistritoId = 1 },
                new () { Id = 15, Inep = 29025346, Nome = "CELSO CAVALCANTE DE CARVALHO", Telefone = "7436177211", Email = "EMPMANDACARU@GMAIL.COM", DistritoId = 4 },
                new () { Id = 16, Inep = 29024668, Nome = "CENTRO SOCIAL URBANO - CSU", Telefone = "7436112744", Email = "ESCOLACSU.JUA@GMAIL.COM", DistritoId = 1 },
                new () { Id = 17, Inep = 29025974, Nome = "CORAÇÃO DE JESUS - JUREMA VERMELHA", Telefone = "", Email = "", DistritoId = 4 },
                new () { Id = 18, Inep = 29026164, Nome = "CORAÇÃO DE JESUS - SERRA DA MADEIRA", Telefone = "7496357165", Email = "", DistritoId = 4 },
                new () { Id = 19, Inep = 29026890, Nome = "DEPUTADO RAIMUNDO DA CUNHA LEITE", Telefone = "7436175001", Email = "ESCOLA_RAIMUNDOCUNHALEITE@HOTMAIL.COM", DistritoId = 6 },
                new () { Id = 20, Inep = 29378893, Nome = "DOM JOSÉ RODRIGUES", Telefone = "7488110737", Email = "ESCOLADJR@GMAIL.COM", DistritoId = 1 },
                new () { Id = 21, Inep = 29026024, Nome = "DOUTOR EDSON RIBEIRO", Telefone = "74988133633", Email = "AMILTONGOMES2016@HOTMAIL.COM", DistritoId = 4 },
                new () { Id = 22, Inep = 29025478, Nome = "DOUTOR JOSÉ DE ARAÚJO SOUZA", Telefone = "7436130580", Email = "ESCOLADRJOSEDEARAUJO@HOTMAIL.COM", DistritoId = 1 },
                new () { Id = 23, Inep = 29026911, Nome = "DURVAL BARBOSA DA CUNHA", Telefone = "7436175004", Email = "ESCOLADURVALBARBOSA@GMAL.COM", DistritoId = 6 },
                new () { Id = 24, Inep = 29025664, Nome = "E.M.E.I. ABÓBORA", Telefone = "7436179072", Email = "EMMSBONFIM@HOTMAIL.COM", DistritoId = 2 },
                new () { Id = 25, Inep = 29461375, Nome = "E.M.E.I. ADJALVA FERREIRA LIMA", Telefone = "", Email = "CLEIABARRETO02@OUTLOOK.COM", DistritoId = 1 },
                new () { Id = 26, Inep = 29025842, Nome = "E.M.E.I. AMÉLIA BORGES", Telefone = "7488223271", Email = "", DistritoId = 3 },
                new () { Id = 27, Inep = 29402140, Nome = "E.M.E.I. AMÉLIA DUARTE", Telefone = "", Email = "", DistritoId = 1 },
                new () { Id = 28, Inep = 29401220, Nome = "E.M.E.I. ANA MARIA MORGADO CHAVES", Telefone = "7436123354", Email = "ROSANGELA.ALMEIDA@HOTMAIL.COM", DistritoId = 1 },
                new () { Id = 29, Inep = 29402120, Nome = "E.M.E.I. ANNA HILDA LEITE FARIAS", Telefone = "7436124696", Email = "CENTROANNAHILDA@GMAIL.COM", DistritoId = 1 },
                new () { Id = 30, Inep = 29461340, Nome = "E.M.E.I. ANTÔNIO GUILHERMINO", Telefone = "74988054502", Email = "", DistritoId = 1 },
                new () { Id = 31, Inep = 29429790, Nome = "E.M.E.I. ARCENIO JOSÉ DA SILVA", Telefone = "", Email = "EMAILPRIMAVEAARCENIOJOSE@GMAIL.COM", DistritoId = 1 },
                new () { Id = 32, Inep = 29464064, Nome = "E.M.E.I. ARLINDA ALVES VARJÃO", Telefone = "7488333869", Email = "JANE.SILVABARBOSA@HOTMAIL.COM", DistritoId = 1 },
                new () { Id = 33, Inep = 29470820, Nome = "E.M.E.I. BEATRIZ ANGÉLICA MOTA", Telefone = "", Email = "EMEI.DIRETORIA@GMAIL.COM", DistritoId = 1 },
                new () { Id = 34, Inep = 29026423, Nome = "E.M.E.I. BOLIVAR SANTANA", Telefone = "7488054854", Email = "SANTANABOL14@GMAIL.COM", DistritoId = 1 },
                new () { Id = 35, Inep = 29402170, Nome = "E.M.E.I. BOM JESUS DOS NAVEGANTES", Telefone = "7436173029", Email = "SUENI-SANTOS@YAHOO.COM.BR", DistritoId = 4 },
                new () { Id = 36, Inep = 29932777, Nome = "E.M.E.I. CAIC MISAEL AGUILAR", Telefone = "", Email = "", DistritoId = 1 },
                new () { Id = 37, Inep = 29402190, Nome = "E.M.E.I. DILMA CALMON DE OLIVEIRA", Telefone = "", Email = "JAMMYS.GUERRA@GMAIL.COM", DistritoId = 1 },
                new () { Id = 38, Inep = 29461189, Nome = "E.M.E.I. EDIVÂNIA SANTOS CARDOSO", Telefone = "7488511345", Email = "ROSALILAS_BISPO@HOTMAIL.COM", DistritoId = 1 },
                new () { Id = 39, Inep = 29402100, Nome = "E.M.E.I. GENTIL DAMÁSIO DO NASCIMENTO", Telefone = "7436133763", Email = "", DistritoId = 1 },
                new () { Id = 40, Inep = 29026628, Nome = "E.M.E.I. HERBET MOUSE RODRIGUES", Telefone = "", Email = "SOLANGETIASOL@HOTMAIL.COM", DistritoId = 5 },
                new () { Id = 41, Inep = 29402210, Nome = "E.M.E.I. JANDIRA BORGES", Telefone = "", Email = "", DistritoId = 1 },
                new () { Id = 42, Inep = 29461367, Nome = "E.M.E.I. LENI LOPES DE ARAÚJO SANTOS", Telefone = "", Email = "", DistritoId = 1 },
                new () { Id = 43, Inep = 29467152, Nome = "E.M.E.I. LUANA DA SILVA NASCIMENTO", Telefone = "74981300440", Email = "EMEILUANADASILVA@GMAIL.COM", DistritoId = 1 },
                new () { Id = 44, Inep = 29461219, Nome = "E.M.E.I. LUZINETE DE OLIVEIRA", Telefone = "", Email = "", DistritoId = 1 },
                new () { Id = 45, Inep = 29461227, Nome = "E.M.E.I. MANOEL ALVES DA MOTA", Telefone = "", Email = "VALDATDB12@GMAIL.COM", DistritoId = 4 },
                new () { Id = 46, Inep = 29469635, Nome = "E.M.E.I. MARIA FERREIRA DE SOUZA", Telefone = "7436176145", Email = "TULIOROZARORIZ@GMAIL.COM", DistritoId = 7 },
                new () { Id = 47, Inep = 29461243, Nome = "E.M.E.I. MARIA HELENA DA SILVA PEREIRA", Telefone = "7488342914", Email = "EMEIMARIAHELENA@HOTMAIL.COM.BR", DistritoId = 1 },
                new () { Id = 48, Inep = 29461235, Nome = "E.M.E.I. MARIA HOZANA NUNES", Telefone = "7436124696", Email = "CLAUDIANA.PROF@GMAIL.COM", DistritoId = 1 },
                new () { Id = 49, Inep = 29461170, Nome = "E.M.E.I. MARIA JÚLIA RODRIGUES TANURI", Telefone = "", Email = "", DistritoId = 1 },
                new () { Id = 50, Inep = 29025338, Nome = "E.M.E.I. MARIA SUELY MEDRADO ARAÚJO", Telefone = "7488419813", Email = "TRPAQUINO@HOTMAIL.COM", DistritoId = 1 },
                new () { Id = 51, Inep = 29402160, Nome = "E.M.E.I. MARIÁ VIANA TANURI", Telefone = "", Email = "", DistritoId = 1 },
                new () { Id = 52, Inep = 29461197, Nome = "E.M.E.I. NÉLIA DE SOUZA COSTA", Telefone = "7436123354", Email = "", DistritoId = 1 },
                new () { Id = 53, Inep = 29402150, Nome = "E.M.E.I. NAILDE DE SOUZA COSTA", Telefone = "", Email = "EMEINAILDE@HOTMAIL.COM", DistritoId = 1 },
                new () { Id = 54, Inep = 29025788, Nome = "E.M.E.I. NOSSA SENHORA DAS GROTAS - CARNAÍBA", Telefone = "", Email = "ESCOLA_NSGROTAS@HOTMAIL.COM", DistritoId = 3 },
                new () { Id = 55, Inep = 29445256, Nome = "E.M.E.I. NOSSO SENHOR DOS AFLITOS", Telefone = "", Email = "", DistritoId = 1 },
                new () { Id = 56, Inep = 29026792, Nome = "E.M.E.I. PASSAGEM DO SARGENTO", Telefone = "", Email = "", DistritoId = 5 },
                new () { Id = 57, Inep = 29469252, Nome = "E.M.E.I. PASTOR MANOEL MARQUES DE SOUZA", Telefone = "74988136212", Email = "JOSEFACRISTINAT@GMAIL.COM", DistritoId = 1 },
                new () { Id = 58, Inep = 29470811, Nome = "E.M.E.I. PREFEITO APRÍGIO DUARTE", Telefone = "74988320498", Email = "MARI_LUCA@MSN.COM", DistritoId = 1 },
                new () { Id = 59, Inep = 29461251, Nome = "E.M.E.I. PRIMAVERA", Telefone = "", Email = "EMAILPRIMAVERAARCENIOJOSE@GMAIL.COM", DistritoId = 1 },
                new () { Id = 60, Inep = 29402110, Nome = "E.M.E.I. PROFª HELOISA HELENA BENEVIDES FARIAS", Telefone = "", Email = "EMEIHELOISAHELENA@HOTMAIL.COM", DistritoId = 1 },
                new () { Id = 61, Inep = 29026261, Nome = "E.M.E.I. PROFª JOANA RAMOS NETA", Telefone = "7499346352", Email = "", DistritoId = 4 },
                new () { Id = 62, Inep = 29027098, Nome = "E.M.E.I. SÃO FRANCISCO DE ASSIS", Telefone = "74999563806", Email = "ALINEDEFATIMA92@GMAIL.COM", DistritoId = 8 },
                new () { Id = 63, Inep = 29469112, Nome = "E.M.R.T.I. SÃO JOSÉ", Telefone = "74999522470", Email = "ANACCN2007@YAHOO.COM.BR", DistritoId = 4 },
                new () { Id = 64, Inep = 29450004, Nome = "E.M.T.I. PROFª IRACEMA PEREIRA DA PAIXÃO", Telefone = "74988090992", Email = "LINDY_CPC@HOTMAIL.COM", DistritoId = 1 },
                new () { Id = 65, Inep = 29401610, Nome = "EDUCANDÁRIO JOÃO XXIII", Telefone = "", Email = "EDUCANDARIOJOAO23@HOTMAIL.COM", DistritoId = 1 },
                new () { Id = 66, Inep = 29341744, Nome = "ELEOTÉRIO SOARES FONSÊCA", Telefone = "", Email = "ALINEDEFATIMA92@GMAIL.COM", DistritoId = 8 },
                new () { Id = 67, Inep = 29026113, Nome = "ELISEU SANTOS", Telefone = "7488078555", Email = "ESCOLAMUNICIPALELISEUSANTOS@HOTMAIL.COM", DistritoId = 4 },
                new () { Id = 68, Inep = 29026199, Nome = "EURÍDICE RIBEIRO VIANA", Telefone = "", Email = "EURIDICERIBEIROVIANA.QUIPA@HOTMAIL.COM", DistritoId = 4 },
                new () { Id = 69, Inep = 29026202, Nome = "FAMÍLIA UNIDA", Telefone = "7488367939", Email = "KLERISSON@YAHOO.COM.BR", DistritoId = 4 },
                new () { Id = 70, Inep = 29025524, Nome = "FUNDAÇÃO JUAZEIRENSE PROMENOR", Telefone = "7436113992", Email = "ESCOLAPROMENOR@HOTMAIL.COM", DistritoId = 1 },
                new () { Id = 71, Inep = 29025753, Nome = "GRACIOSA XAVIER RAMOS GOMES", Telefone = "7436181029", Email = "ESCOLAGRACIOSAXAVIER@GMAIL.COM", DistritoId = 3 },
                new () { Id = 72, Inep = 29025575, Nome = "HELENA ARAÚJO PINHEIRO", Telefone = "7436111626", Email = "PATRICIACARLA01@HOTMAIL.COM", DistritoId = 1 },
                new () { Id = 73, Inep = 29025583, Nome = "JATOBÁ", Telefone = "", Email = "AMILTONGOMES2016@HOTMAIL.COM", DistritoId = 4 },
                new () { Id = 74, Inep = 29026741, Nome = "JOÃO DIAS FERREIRA", Telefone = "", Email = "", DistritoId = 5 },
                new () { Id = 75, Inep = 29025222, Nome = "JOÃO NEVES DE ALMEIDA", Telefone = "74988111398", Email = "", DistritoId = 5 },
                new () { Id = 76, Inep = 29025168, Nome = "JOCA DE SOUZA OLIVEIRA", Telefone = "", Email = "JOCA.DIRETORIA@GMAIL.COM", DistritoId = 1 },
                new () { Id = 77, Inep = 29026636, Nome = "JOSÉ DE AMORIM", Telefone = "", Email = "", DistritoId = 5 },
                new () { Id = 78, Inep = 29025370, Nome = "JOSÉ PADILHA DE SOUZA", Telefone = "7436120372", Email = "ESCOLAJOSEPADILHA@HOTMAIL.COM", DistritoId = 1 },
                new () { Id = 79, Inep = 29025176, Nome = "JUDITE LEAL COSTA", Telefone = "7436114939", Email = "ESCOLAJUDITELCOSTA@HOTMAIL.COM", DistritoId = 1 },
                new () { Id = 80, Inep = 29026644, Nome = "LÚCIA CARMEM SOBREIRA", Telefone = "", Email = "", DistritoId = 5 },
                new () { Id = 81, Inep = 29025923, Nome = "LINDAURA MARIA DE JESUS", Telefone = "7436172000", Email = "", DistritoId = 4 },
                new () { Id = 82, Inep = 29025184, Nome = "LUDGERO DE SOUZA COSTA", Telefone = "7436121731", Email = "ESCOLALUDGERO@GMAIL.COM", DistritoId = 1 },
                new () { Id = 83, Inep = 29025206, Nome = "MANDACARU", Telefone = "", Email = "", DistritoId = 1 },
                new () { Id = 84, Inep = 29025672, Nome = "MANOEL DE SOUZA BONFIM", Telefone = "7436179072", Email = "EMMSBONFIM@HOTMAIL.COM", DistritoId = 2 },
                new () { Id = 85, Inep = 29026440, Nome = "MANOEL GOMES MARTINS", Telefone = "", Email = "", DistritoId = 1 },
                new () { Id = 86, Inep = 29026296, Nome = "MANOEL LUIZ DA SILVA", Telefone = "7436139057", Email = "JAQUELLINEASSESSORIA27@GMAIL.COM", DistritoId = 4 },
                new () { Id = 87, Inep = 29341710, Nome = "MANOEL NUNES AMORIM", Telefone = "7436115110", Email = "", DistritoId = 5 },
                new () { Id = 88, Inep = 29026776, Nome = "MARIA AMÉLIA DE SOUZA OLIVEIRA", Telefone = "7491044270", Email = "", DistritoId = 5 },
                new () { Id = 89, Inep = 29026652, Nome = "MARIA DO CARMO SÁ NOGUEIRA", Telefone = "", Email = "", DistritoId = 5 },
                new () { Id = 90, Inep = 29026660, Nome = "MARIA MONTEIRO BACELAR", Telefone = "7436184001", Email = "", DistritoId = 5 },
                new () { Id = 91, Inep = 29025230, Nome = "MARIANO RODRIGUES DE SOUZA", Telefone = "7436183004", Email = "ESCOLAMARIANORODRIGUES@YAHOO.COM.BR", DistritoId = 4 },
                new () { Id = 92, Inep = 29026679, Nome = "MIGUEL ÂNGELO DE SOUZA", Telefone = "7499867445", Email = "", DistritoId = 5 },
                new () { Id = 93, Inep = 29026105, Nome = "NOSSA SENHORA DAS GROTAS - BOQUEIRÃO", Telefone = "7436178287", Email = "NOSSASENHORASAOFRANCISCO@GMAIL.COM", DistritoId = 4 },
                new () { Id = 94, Inep = 29025311, Nome = "NOSSA SENHORA DAS GROTAS-SEDE", Telefone = "", Email = "ENSGSEDUC@GMAIL.COM", DistritoId = 1 },
                new () { Id = 95, Inep = 29025958, Nome = "NOSSA SENHORA RAINHA DOS ANJOS", Telefone = "7491957370", Email = "ESCOLARAINHA@GMAIL.COM", DistritoId = 4 },
                new () { Id = 96, Inep = 29025869, Nome = "OSORIO TELES DE MENEZES", Telefone = "7436181270", Email = "ESCOLA_OSORIOTELES@HOTMAIL.COM", DistritoId = 3 },
                new () { Id = 97, Inep = 29461359, Nome = "PAULO FREIRE", Telefone = "7499077828", Email = "JAELTON.OLIVEIRA@HOTMAIL.COM", DistritoId = 5 },
                new () { Id = 98, Inep = 29024587, Nome = "PAULO VI", Telefone = "7436115675", Email = "PAULOVI_2511@HOTMAIL.COM", DistritoId = 1 },
                new () { Id = 99, Inep = 29025834, Nome = "PEDRO DIAS", Telefone = "", Email = "ESCOLAGRACIOSAXAVIER@GMAIL.COM", DistritoId = 3 },
                new () { Id = 100, Inep = 29026431, Nome = "PONTAL", Telefone = "", Email = "", DistritoId = 4 },
                new () { Id = 101, Inep = 29025362, Nome = "PREFEITO APRÍGIO DUARTE", Telefone = "", Email = "", DistritoId = 1 },
                new () { Id = 102, Inep = 29401600, Nome = "PRESIDENTE TANCREDO NEVES", Telefone = "", Email = "", DistritoId = 1 },
                new () { Id = 103, Inep = 29026873, Nome = "PROFª ANTONILA DA FRANÇA CARDOSO", Telefone = "", Email = "ESCOLAAFC@GMAIL.COM", DistritoId = 6 },
                new () { Id = 104, Inep = 29026989, Nome = "PROFª ATANILHA LUZ ARAÚJO", Telefone = "", Email = "", DistritoId = 7 },
                new () { Id = 105, Inep = 29026695, Nome = "PROFª BERNADETE BRAGA", Telefone = "", Email = "", DistritoId = 5 },
                new () { Id = 106, Inep = 29024692, Nome = "PROFª CARMEM COSTA SANTOS", Telefone = "", Email = "", DistritoId = 1 },
                new () { Id = 107, Inep = 29024900, Nome = "PROFª CRENILDES LUIS BRANDÃO", Telefone = "", Email = "E.CRENILDESBRANDAO@GMAIL.COM", DistritoId = 1 },
                new () { Id = 108, Inep = 29025036, Nome = "PROFª DINORAH ALBERNAZ MELO DA SILVA", Telefone = "", Email = "ESCOLADINORAH@MSN.COM", DistritoId = 1 },
                new () { Id = 109, Inep = 29026709, Nome = "PROFª EDUALDINA DAMÁSIO", Telefone = "", Email = "", DistritoId = 5 },
                new () { Id = 110, Inep = 29024838, Nome = "PROFª GUIOMAR LUSTOSA RODRIGUES", Telefone = "", Email = "ESCOLAGUIOMARLUSTOSA@HOTMAIL.COM", DistritoId = 1 },
                new () { Id = 111, Inep = 29024960, Nome = "PROFª HAYDÉE FONSECA FALCÃO", Telefone = "74999858835", Email = "ESCOLAHAYDEE@GMAIL.COM", DistritoId = 1 },
                new () { Id = 112, Inep = 29341809, Nome = "PROFª IRACY NUNES DA SILVA", Telefone = "", Email = "COLEGIOIRACYNUNES@GMAIL.COM", DistritoId = 4 },
                new () { Id = 113, Inep = 29024978, Nome = "PROFª LEOPOLDINA LEAL", Telefone = "", Email = "", DistritoId = 1 },
                new () { Id = 114, Inep = 29024943, Nome = "PROFª MARIA DE LOURDES DUARTE", Telefone = "", Email = "ESCOLAMARIALOURDESDUARTE@HOTMAIL.COM", DistritoId = 1 },
                new () { Id = 115, Inep = 29024986, Nome = "PROFª MARIA FRANCA PIRES", Telefone = "7436137115", Email = "", DistritoId = 1 },
                new () { Id = 116, Inep = 29362504, Nome = "PROFª MARIA JOSÉ LIMA DA ROCHA", Telefone = "", Email = "WILZAMIRANDA@HOTMAIL.COM", DistritoId = 1 },
                new () { Id = 117, Inep = 29025770, Nome = "PROFª MATILDE COSTA MEDRADO", Telefone = "74988526535", Email = "ESCOLA_MATILDECOSTA@HOTMAIL.COM", DistritoId = 3 },
                new () { Id = 118, Inep = 29026725, Nome = "PROFª OSCARLINA TANURI", Telefone = "", Email = "", DistritoId = 5 },
                new () { Id = 119, Inep = 29025397, Nome = "PROFª TEREZINHA FERREIRA DE OLIVEIRA", Telefone = "", Email = "ESCOLATEREZINHA.ETFO@HOTMAIL.COM", DistritoId = 1 },
                new () { Id = 120, Inep = 29415160, Nome = "PROFº CARLOS DA COSTA SILVA", Telefone = "", Email = "ESCOLACARLOSCOSTA_2012@HOTMAIL.COM", DistritoId = 1 },
                new () { Id = 121, Inep = 29405106, Nome = "PROFº JOSÉ PEREIRA DA SILVA", Telefone = "", Email = "MUNICIPALJOSEPEREIRA@BOL.COM.BR", DistritoId = 1 },
                new () { Id = 122, Inep = 29024714, Nome = "PROFº LUIS CURSINO DA FRANÇA CARDOSO", Telefone = "", Email = "LUCYSOARES1@HOTMAIL.COM", DistritoId = 1 },
                new () { Id = 123, Inep = 29027055, Nome = "RAIMUNDO CLEMENTINO DE SOUZA", Telefone = "74999759179", Email = "ALINEDEFATIMA92@GMAIL.COM", DistritoId = 8 },
                new () { Id = 124, Inep = 29424445, Nome = "RAIMUNDO MEDRADO PRIMO", Telefone = "", Email = "RAIMUNDOMEDRADOPRIMO@HOTMAIL.COM", DistritoId = 1 },
                new () { Id = 125, Inep = 29341779, Nome = "RURAL DE MASSAROCA - ERUM", Telefone = "74994436003", Email = "RURALMASSAROCA@HOTMAIL.COM", DistritoId = 7 },
                new () { Id = 126, Inep = 29026350, Nome = "SÃO FRANCISCO DE ASSIS - MULUNGÚ", Telefone = "7436178287", Email = "NOSSASENHORASAOFRANCISCO@GMAIL.COM", DistritoId = 4 },
                new () { Id = 127, Inep = 29025982, Nome = "SÃO FRANCISCO DE ASSIS - NH2", Telefone = "7436189018", Email = "NH2SAOFRANCISCO@GMAIL.COM", DistritoId = 4 },
                new () { Id = 128, Inep = 29026377, Nome = "SÃO JOSÉ", Telefone = "", Email = "", DistritoId = 4 },
                new () { Id = 129, Inep = 29025931, Nome = "SÃO SEBASTIÃO", Telefone = "", Email = "", DistritoId = 4 },
                new () { Id = 130, Inep = 29026288, Nome = "SANTA INÊS", Telefone = "", Email = "", DistritoId = 4 },
                new () { Id = 131, Inep = 29026300, Nome = "SANTA TEREZINHA", Telefone = "7488110653", Email = "", DistritoId = 4 },
                new () { Id = 132, Inep = 29025010, Nome = "SANTO ANTÔNIO", Telefone = "7436187055", Email = "", DistritoId = 4 },
                new () { Id = 133, Inep = 29026520, Nome = "VEREADOR AMADEUS DAMÁSIO", Telefone = "7435381883", Email = "ESCOLAAMADEUSDAMASIO@GMAIL.COM", DistritoId = 5 }
            };

            new EntitySeed<Escola>(context, databaseProvider).Load(escolas, "Escolas");

            var cursos = new List<Curso>
            {
                new Curso {Id = 1, Nome = "Educação Infantil", Sigla = "EI"},
                new Curso {Id = 2, Nome = "Ensino Fundamental", Sigla = "EF"},
                new Curso {Id = 3, Nome = "Educação de Jovens e Adultos", Sigla = "EJA"}
            };

            new EntitySeed<Curso>(context, databaseProvider).Load(cursos, "Cursos");

            var segmentos = new List<Segmento>
            {
                new Segmento {Id = 1, CursoId = 1, Nome = "Creche", Sigla = "CR"},
                new Segmento {Id = 2, CursoId = 1, Nome = "Pré-Escola", Sigla = "PE"},
                new Segmento {Id = 3, CursoId = 2, Nome = "Ensino Fundamental I", Sigla = "EF I"},
                new Segmento {Id = 4, CursoId = 2, Nome = "Ensino Fundamental II", Sigla = "EF II"},
                new Segmento {Id = 5, CursoId = 3, Nome = "Educação de Jovens e Adultos I", Sigla = "EJA I"},
                new Segmento {Id = 6, CursoId = 3, Nome = "Educação de Jovens e Adultos II", Sigla = "EJA II"},
                new Segmento {Id = 7, CursoId = 2, Nome = "Correção de Fluxo", Sigla = "CF"}
            };

            new EntitySeed<Segmento>(context, databaseProvider).Load(segmentos, "Segmentos");

            var etapas = new List<Etapa>
            {
                new Etapa {Id = 1, SegmentoId = 1, Nome = "Berçário", Normativa = 1},
                new Etapa {Id = 2, SegmentoId = 1, Nome = "Infantil I", Normativa = 1},
                new Etapa {Id = 3, SegmentoId = 1, Nome = "Infantil II", Normativa = 1},
                new Etapa {Id = 4, SegmentoId = 1, Nome = "Infantil III", Normativa = 1},
                new Etapa {Id = 5, SegmentoId = 2, Nome = "Infantil IV", Normativa = 1},
                new Etapa {Id = 6, SegmentoId = 2, Nome = "Infantil V", Normativa = 1},
                new Etapa {Id = 7, SegmentoId = 3, Nome = "1º Ano", Normativa = 1},
                new Etapa {Id = 8, SegmentoId = 3, Nome = "2º Ano", Normativa = 1},
                new Etapa {Id = 9, SegmentoId = 3, Nome = "3º Ano", Normativa = 1},
                new Etapa {Id = 10, SegmentoId = 3, Nome = "4º Ano", Normativa = 1},
                new Etapa {Id = 11, SegmentoId = 3, Nome = "5º Ano", Normativa = 1},
                new Etapa {Id = 12, SegmentoId = 4, Nome = "6º Ano", Normativa = 1},
                new Etapa {Id = 13, SegmentoId = 4, Nome = "7º Ano", Normativa = 1},
                new Etapa {Id = 14, SegmentoId = 4, Nome = "8º Ano", Normativa = 1},
                new Etapa {Id = 15, SegmentoId = 4, Nome = "9º Ano", Normativa = 1},
                new Etapa {Id = 16, SegmentoId = 5, Nome = "Etapa I", Normativa = 1},
                new Etapa {Id = 17, SegmentoId = 5, Nome = "Etapa II", Normativa = 1},
                new Etapa {Id = 18, SegmentoId = 5, Nome = "Etapa III", Normativa = 1},
                new Etapa {Id = 19, SegmentoId = 6, Nome = "Etapa IV", Normativa = 1},
                new Etapa {Id = 20, SegmentoId = 6, Nome = "Etapa V", Normativa = 1},
                new Etapa {Id = 21, SegmentoId = 7, Nome = "Se Liga", Normativa = 1},
                new Etapa {Id = 22, SegmentoId = 7, Nome = "Acelera", Normativa = 1}
            };

            new EntitySeed<Etapa>(context, databaseProvider).Load(etapas, "Etapas");

            var turnos = new List<Turno>
            {
                new Turno {Id = 1, Nome = "Manhã"},
                new Turno {Id = 2, Nome = "Tarde"},
                new Turno {Id = 3, Nome = "Noite"},
                new Turno {Id = 4, Nome = "Integral"},
                new Turno {Id = 5, Nome = "Não Informado"}
            };

            new EntitySeed<Turno>(context, databaseProvider).Load(turnos, "Turnos");

            var formas = new List<Forma> {new Forma {Id = 1, Nome = "Seriada"}, new Forma {Id = 2, Nome = "Multi"}};

            new EntitySeed<Forma>(context, databaseProvider).Load(formas, "Formas");

            var salas = new List<Sala>
            {
                new Sala {Id = 1, EscolaId = 1, Nome = "SALA 01", Capacidade = 10},
                new Sala {Id = 2, EscolaId = 1, Nome = "SALA 02", Capacidade = 20},
                new Sala {Id = 3, EscolaId = 1, Nome = "SALA 01", Capacidade = 30}
            };

            new EntitySeed<Sala>(context, databaseProvider).Load(salas, "Salas");

            var turmas = new List<Turma>
            {
                new Turma { Id = 1, SalaId = 1, EtapaId = 11, TurnoId = 1, FormaId = 1, Nome = "A", QtdAlunos = 15, Extinta = false },
                new Turma { Id = 2, SalaId = 2, EtapaId = 11, TurnoId = 1, FormaId = 1, Nome = "B", QtdAlunos = 20, Extinta = false }
            };

            new EntitySeed<Turma>(context, databaseProvider).Load(turmas, "Turmas");

            var alunos = new List<Aluno>
            {
                new Aluno
                {
                    Id = 1,
                    Nome = "Maria Luz",
                    Nascimento = new DateTime(day: 10, month: 08, year: 2002),
                    TurmaId = 1
                },
                new Aluno
                {
                    Id = 2,
                    Nome = "João carlos",
                    Nascimento = new DateTime(day: 09, month: 09, year: 1999),
                    TurmaId = 1
                }
            };

            new EntitySeed<Aluno>(context, databaseProvider).Load(alunos, "Alunos");

            var disciplinas = new List<Disciplina>
            {
                new Disciplina {Id = 1, Nome = "Português", Sigla = "Port"},
                new Disciplina {Id = 2, Nome = "Matemática", Sigla = "Mat"},
                new Disciplina {Id = 3, Nome = "Ciências", Sigla = "Cie"}
            };

            new EntitySeed<Disciplina>(context, databaseProvider).Load(disciplinas, "Disciplinas");

            var temas = new List<Tema>
            {
                new Tema {Id = 1, DisciplinaId = 1, Nome = "Tema 01 - Disciplina Port"},
                new Tema {Id = 2, DisciplinaId = 1, Nome = "Tema 02 - Disciplina Port"},
                new Tema {Id = 3, DisciplinaId = 1, Nome = "Tema 03 - Disciplina Port"},
                new Tema {Id = 4, DisciplinaId = 1, Nome = "Tema 04 - Disciplina Port"},
                new Tema {Id = 5, DisciplinaId = 1, Nome = "Tema 05 - Disciplina Port"},
                new Tema {Id = 6, DisciplinaId = 2, Nome = "Tema 01 - Disciplina Mat"},
                new Tema {Id = 7, DisciplinaId = 2, Nome = "Tema 02 - Disciplina Mat"},
                new Tema {Id = 8, DisciplinaId = 2, Nome = "Tema 03 - Disciplina Mat"},
                new Tema {Id = 9, DisciplinaId = 2, Nome = "Tema 04 - Disciplina Mat"},
                new Tema {Id = 10, DisciplinaId = 2, Nome = "Tema 05 - Disciplina Mat"},
                new Tema {Id = 11, DisciplinaId = 3, Nome = "Tema 01 - Disciplina Cie"},
                new Tema {Id = 12, DisciplinaId = 3, Nome = "Tema 02 - Disciplina Cie"},
                new Tema {Id = 13, DisciplinaId = 3, Nome = "Tema 03 - Disciplina Cie"},
                new Tema {Id = 14, DisciplinaId = 3, Nome = "Tema 04 - Disciplina Cie"},
                new Tema {Id = 15, DisciplinaId = 3, Nome = "Tema 05 - Disciplina Cie"}
            };

            new EntitySeed<Tema>(context, databaseProvider).Load(temas, "Temas");

            var descritores = new List<Descritor>
            {
                new Descritor {Id = 1, TemaId = 1, Nome = "Descritor 01 - Tema 1"},
                new Descritor {Id = 2, TemaId = 1, Nome = "Descritor 02 - Tema 1"},
                new Descritor {Id = 3, TemaId = 1, Nome = "Descritor 03 - Tema 1"},
                new Descritor {Id = 4, TemaId = 1, Nome = "Descritor 04 - Tema 1"},
                new Descritor {Id = 5, TemaId = 1, Nome = "Descritor 05 - Tema 1"},
                new Descritor {Id = 6, TemaId = 2, Nome = "Descritor 01 - Tema 2"},
                new Descritor {Id = 7, TemaId = 2, Nome = "Descritor 02 - Tema 2"},
                new Descritor {Id = 8, TemaId = 2, Nome = "Descritor 03 - Tema 2"},
                new Descritor {Id = 9, TemaId = 2, Nome = "Descritor 04 - Tema 2"},
                new Descritor {Id = 10, TemaId = 2, Nome = "Descritor 05 - Tema 2"},
                new Descritor {Id = 11, TemaId = 3, Nome = "Descritor 01 - Tema 3"},
                new Descritor {Id = 12, TemaId = 3, Nome = "Descritor 02 - Tema 3"},
                new Descritor {Id = 13, TemaId = 3, Nome = "Descritor 03 - Tema 3"},
                new Descritor {Id = 14, TemaId = 3, Nome = "Descritor 04 - Tema 3"},
                new Descritor {Id = 15, TemaId = 3, Nome = "Descritor 05 - Tema 3"},
                new Descritor {Id = 16, TemaId = 6, Nome = "Descritor 05 - Tema 3"}
            };

            new EntitySeed<Descritor>(context, databaseProvider).Load(descritores, "Descritores");

            var avaliacaoDisciplinaEtapas = new List<AvaliacaoDisciplinaEtapa>
            {
                new AvaliacaoDisciplinaEtapa {AvaliacaoId = 1, DisciplinaId = 1, EtapaId = 10},
                new AvaliacaoDisciplinaEtapa {AvaliacaoId = 1, DisciplinaId = 1, EtapaId = 11},
                new AvaliacaoDisciplinaEtapa {AvaliacaoId = 1, DisciplinaId = 2, EtapaId = 11},
                new AvaliacaoDisciplinaEtapa {AvaliacaoId = 2, DisciplinaId = 3, EtapaId = 11}
            };

            new EntitySeed<AvaliacaoDisciplinaEtapa>(context, databaseProvider).Load(avaliacaoDisciplinaEtapas,
                "AvaliacaoDisciplinasEtapas");

            // var questoes = new List<Questao>
            // {
            //     new Questao
            //     {
            //         Id = 1,
            //         DescritorId = 1,
            //         EtapaId = 11,
            //         Item = "P01",
            //         Enunciado = "Questao 01 - Descritor 1"
            //     },
            //     new Questao
            //     {
            //         Id = 2,
            //         DescritorId = 1,
            //         EtapaId = 11,
            //         Item = "P02",
            //         Enunciado = "Questao 02 - Descritor 1"
            //     },
            //     new Questao
            //     {
            //         Id = 3,
            //         DescritorId = 1,
            //         EtapaId = 11,
            //         Item = "P03",
            //         Enunciado = "Questao 03 - Descritor 1"
            //     },
            //     new Questao
            //     {
            //         Id = 4,
            //         DescritorId = 1,
            //         EtapaId = 11,
            //         Item = "P04",
            //         Enunciado = "Questao 04 - Descritor 1"
            //     },
            //     new Questao
            //     {
            //         Id = 5,
            //         DescritorId = 2,
            //         EtapaId = 11,
            //         Item = "P01",
            //         Enunciado = "Questao 01 - Descritor 2"
            //     },
            //     new Questao
            //     {
            //         Id = 6,
            //         DescritorId = 2,
            //         EtapaId = 11,
            //         Item = "P02",
            //         Enunciado = "Questao 02 - Descritor 2"
            //     },
            //     new Questao
            //     {
            //         Id = 7,
            //         DescritorId = 3,
            //         EtapaId = 11,
            //         Item = "P01",
            //         Enunciado = "Questao 01 - Descritor 3"
            //     },
            //     new Questao
            //     {
            //         Id = 8,
            //         DescritorId = 4,
            //         EtapaId = 11,
            //         Item = "P01",
            //         Enunciado = "Questao 01 - Descritor 4"
            //     },
            //     new Questao
            //     {
            //         Id = 9,
            //         DescritorId = 4,
            //         EtapaId = 11,
            //         Item = "P02",
            //         Enunciado = "Questao 02 - Descritor 4"
            //     },
            //     new Questao
            //     {
            //         Id = 10,
            //         DescritorId = 2,
            //         EtapaId = 11,
            //         Item = "P01",
            //         Enunciado = "Teste"
            //     },
            //     new Questao
            //     {
            //         Id = 11,
            //         DescritorId = 3,
            //         EtapaId = 11,
            //         Item = "P02",
            //         Enunciado = "Teste2"
            //     }
            // };
            //
            // new EntitySeed<Questao>(context, databaseProvider).Load(questoes, "Questoes");
            //
            // var alternativas = new List<Alternativa>
            // {
            //     new Alternativa {Id = 1, QuestaoId = 1, Descricao = "20", Correta = true},
            //     new Alternativa {Id = 2, QuestaoId = 1, Descricao = "15", Correta = false},
            //     new Alternativa {Id = 3, QuestaoId = 1, Descricao = "12", Correta = false},
            //     new Alternativa {Id = 4, QuestaoId = 1, Descricao = "17", Correta = false},
            //     new Alternativa {Id = 5, QuestaoId = 2, Descricao = "Pedro", Correta = false},
            //     new Alternativa {Id = 6, QuestaoId = 2, Descricao = "Carlos", Correta = true},
            //     new Alternativa {Id = 7, QuestaoId = 2, Descricao = "José", Correta = false},
            //     new Alternativa {Id = 8, QuestaoId = 2, Descricao = "Maria", Correta = false},
            //     new Alternativa {Id = 9, QuestaoId = 3, Descricao = "1,5", Correta = false},
            //     new Alternativa {Id = 10, QuestaoId = 3, Descricao = "2,9", Correta = false},
            //     new Alternativa {Id = 11, QuestaoId = 3, Descricao = "5,4", Correta = true},
            //     new Alternativa {Id = 12, QuestaoId = 3, Descricao = "4,2", Correta = false},
            //     new Alternativa {Id = 13, QuestaoId = 4, Descricao = "Ronaldo", Correta = false},
            //     new Alternativa {Id = 14, QuestaoId = 4, Descricao = "Adriana", Correta = false},
            //     new Alternativa {Id = 15, QuestaoId = 4, Descricao = "Marcelo", Correta = false},
            //     new Alternativa {Id = 16, QuestaoId = 4, Descricao = "Simone", Correta = true},
            //     new Alternativa {Id = 17, QuestaoId = 5, Descricao = "20", Correta = false},
            //     new Alternativa {Id = 18, QuestaoId = 5, Descricao = "10", Correta = true},
            //     new Alternativa {Id = 19, QuestaoId = 5, Descricao = "30", Correta = false},
            //     new Alternativa {Id = 20, QuestaoId = 5, Descricao = "40", Correta = false}
            // };
            //
            // new EntitySeed<Alternativa>(context, databaseProvider).Load(alternativas, "Alternativas");
            //
            //
            // var respostaAlunos = new List<RespostaAluno>
            // {
            //     new RespostaAluno {AvaliacaoId = 1, AlunoId = 1, AlternativaId = 5},
            //     new RespostaAluno {AvaliacaoId = 1, AlunoId = 1, AlternativaId = 11},
            //     new RespostaAluno {AvaliacaoId = 1, AlunoId = 2, AlternativaId = 7},
            //     new RespostaAluno {AvaliacaoId = 1, AlunoId = 2, AlternativaId = 10}
            // };
            //
            // new EntitySeed<RespostaAluno>(context, databaseProvider).Load(respostaAlunos, "RespostaAlunos");
            //
            // var avaliacaoQuestoes = new List<AvaliacaoQuestao>
            // {
            //     new AvaliacaoQuestao {QuestaoId = 1, AvaliacaoId = 1},
            //     new AvaliacaoQuestao {QuestaoId = 2, AvaliacaoId = 1}
            // };
            //
            // new EntitySeed<AvaliacaoQuestao>(context, databaseProvider).Load(avaliacaoQuestoes, "AvaliacaoQuestoes");

            return app;
        }
    }
}
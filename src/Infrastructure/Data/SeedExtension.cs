using Microsoft.EntityFrameworkCore;
using SAED.ApplicationCore.Entities;
using System.Collections.Generic;

namespace SAED.Infrastructure.Data
{
    public static class SeedExtension
    {
        public static ModelBuilder SeedDatabase(this ModelBuilder builder)
        {
            var avaliacoes = new List<Avaliacao>
            {
                new Avaliacao { Id = 1, Codigo = "2020", Status = StatusAvaliacao.EmAndamento },
                new Avaliacao { Id = 2, Codigo = "2021", Status = StatusAvaliacao.ARealizar }
            };

            builder.Entity<Avaliacao>().HasData(avaliacoes);

            var distritos = new List<Distrito>
            {
                new Distrito { Id = 1, Nome = "Sede", Zona = "Urbana", Distancia = 0 },
                new Distrito { Id = 2, Nome = "Abóbora", Zona = "Rural", Distancia = 110 },
                new Distrito { Id = 3, Nome = "Itamotinga", Zona = "Rural", Distancia = 75 },
                new Distrito { Id = 4, Nome = "Juremal", Zona = "Rural", Distancia = 50 },
                new Distrito { Id = 5, Nome = "Carnaíba", Zona = "Rural", Distancia = 25 },
                new Distrito { Id = 6, Nome = "Maniçoba", Zona = "Rural", Distancia = 40 },
                new Distrito { Id = 7, Nome = "Pinhões", Zona = "Rural", Distancia = 75 },
                new Distrito { Id = 8, Nome = "Junco", Zona = "Rural", Distancia = 35 },
                new Distrito { Id = 9, Nome = "Massaroca", Zona = "Rural", Distancia = 70 },
                new Distrito { Id = 10, Nome = "Mandacaru", Zona = "Rural", Distancia = 10 }
            };

            builder.Entity<Distrito>().HasData(distritos);

            var escolas = new List<Escola>
            {
                new Escola { Id = 1, Inep = 29024935, Nome = "02 DE JULHO", Telefone = "(74) 98844-0798", Email = "DOISDEJULHOJUAZEIRO@HOTMAIL.COM", DistritoId = 4 },
                new Escola { Id = 2, Inep = 29024994, Nome = "15 DE JULHO", Telefone = "(74) 3611-3278", Email = "QUINZEDEJULHO2014@OUTLOOK.COM", DistritoId = 4 },
                new Escola { Id = 3, Inep = 29026130, Nome = "25 DE JULHO", Telefone = "(74) 8806-8024", Email = "ESCOLA25.DEJULHO@OUTLOOK.COM", DistritoId = 4 },
               new Escola { Id = 4, Inep = 29026601, Nome = "AMÉRICO TANURI - JUNCO", Telefone = "(74) 9881-1198", Email = "", DistritoId = 5 },
               new Escola { Id = 5, Inep = 29026148, Nome = "AMÉRICO TANURI - MANIÇOBA", Telefone = "(74) 98806-7161", Email = "", DistritoId = 4 },
               new Escola { Id = 6, Inep = 29025699, Nome = "AMÉRICO TANURY - ABÓBORA", Telefone = "(74) 3617-9072", Email = "EMMSBONFIM@HOTMAIL.COM", DistritoId = 2 },
               new Escola { Id = 7, Inep = 29341256, Nome = "ANÁLIA BARBOSA DE SOUZA", Telefone = "(74) 9155-2384", Email = "ANALIABARBOSA.EDU@GMAIL.COM", DistritoId = 1 },
               new Escola { Id = 8, Inep = 29026482, Nome = "ANTONIO FRANCISCO DE OLIVEIRA", Telefone = "(74) 8815-8634", Email = "VALDETEMSF@HOTMAIL.COM", DistritoId = 5 },
               new Escola { Id = 9, Inep = 29429536, Nome = "ARGEMIRO JOSE DA CRUZ", Telefone = "(74) 3611-0018", Email = "CEAJC1@HOTMAIL.COM", DistritoId = 1 },
               new Escola { Id = 10, Inep = 29026156, Nome = "BOM JESUS - BARAÚNA", Telefone = "(74) 3618-7055", Email = "ESCOLA_BOMJESUSBARAUNA@HOTMAIL.COM", DistritoId = 4 },
               new Escola { Id = 11, Inep = 29025907, Nome = "BOM JESUS - NH1", Telefone = "", Email = "BOMJESUS_NH01@HOTMAIL.COM", DistritoId = 4 },
               new Escola { Id = 12, Inep = 29469120, Nome = "C.R.A. PROFª MAZZARELLO CAVALCANTI REIS DA ROCHA", Telefone = "", Email = "MAZZARELLOROCHA@OUTLOOK.COM", DistritoId = 1 },
               new Escola { Id = 13, Inep = 29362415, Nome = "CAIC - MISAEL AGUILAR", Telefone = "(74) 3611-8041", Email = "ESCOLACAICJUA@GMAIL.COM", DistritoId = 1 },
               new Escola { Id = 14, Inep = 29024650, Nome = "CAXANGÁ", Telefone = "(74) 3612-2900", Email = "ESCOLA_CAXANGA@HOTMAIL.COM", DistritoId = 1 },
               new Escola { Id = 15, Inep = 29025346, Nome = "CELSO CAVALCANTE DE CARVALHO", Telefone = "(74) 3617-7211", Email = "EMPMANDACARU@GMAIL.COM", DistritoId = 4 },
               new Escola { Id = 16, Inep = 29024668, Nome = "CENTRO SOCIAL URBANO - CSU", Telefone = "(74) 3611-2744", Email = "ESCOLACSU.JUA@GMAIL.COM", DistritoId = 1 },
               new Escola { Id = 17, Inep = 29025974, Nome = "CORAÇÃO DE JESUS - JUREMA VERMELHA", Telefone = "", Email = "", DistritoId = 4 },
               new Escola { Id = 18, Inep = 29026164, Nome = "CORAÇÃO DE JESUS - SERRA DA MADEIRA", Telefone = "(74) 9635-7165", Email = "", DistritoId = 4 },
               new Escola { Id = 19, Inep = 29026890, Nome = "DEPUTADO RAIMUNDO DA CUNHA LEITE", Telefone = "(74) 3617-5001", Email = "ESCOLA_RAIMUNDOCUNHALEITE@HOTMAIL.COM", DistritoId = 6 },
               new Escola { Id = 20, Inep = 29378893, Nome = "DOM JOSÉ RODRIGUES", Telefone = "(74) 8811-0737", Email = "ESCOLADJR@GMAIL.COM", DistritoId = 1 },
               new Escola { Id = 21, Inep = 29026024, Nome = "DOUTOR EDSON RIBEIRO", Telefone = "(74) 98813-3633", Email = "AMILTONGOMES2016@HOTMAIL.COM", DistritoId = 4 },
               new Escola { Id = 22, Inep = 29025478, Nome = "DOUTOR JOSÉ DE ARAÚJO SOUZA", Telefone = "(74) 3613-0580", Email = "ESCOLADRJOSEDEARAUJO@HOTMAIL.COM", DistritoId = 1 },
               new Escola { Id = 23, Inep = 29026911, Nome = "DURVAL BARBOSA DA CUNHA", Telefone = "(74) 3617-5004", Email = "ESCOLADURVALBARBOSA@GMAL.COM", DistritoId = 6 },
               new Escola { Id = 24, Inep = 29025664, Nome = "E.M.E.I. ABÓBORA", Telefone = "(74) 3617-9072", Email = "EMMSBONFIM@HOTMAIL.COM", DistritoId = 2 },
               new Escola { Id = 25, Inep = 29461375, Nome = "E.M.E.I. ADJALVA FERREIRA LIMA", Telefone = "", Email = "CLEIABARRETO02@OUTLOOK.COM", DistritoId = 1 },
               new Escola { Id = 26, Inep = 29025842, Nome = "E.M.E.I. AMÉLIA BORGES", Telefone = "(74) 8822-3271", Email = "", DistritoId = 3 },
               new Escola { Id = 27, Inep = 29402140, Nome = "E.M.E.I. AMÉLIA DUARTE", Telefone = "", Email = "", DistritoId = 1 },
               new Escola { Id = 28, Inep = 29401220, Nome = "E.M.E.I. ANA MARIA MORGADO CHAVES", Telefone = "(74) 3612-3354", Email = "ROSANGELA.ALMEIDA@HOTMAIL.COM", DistritoId = 1 },
               new Escola { Id = 29, Inep = 29402120, Nome = "E.M.E.I. ANNA HILDA LEITE FARIAS", Telefone = "(74) 3612-4696", Email = "CENTROANNAHILDA@GMAIL.COM", DistritoId = 1 },
               new Escola { Id = 30, Inep = 29461340, Nome = "E.M.E.I. ANTÔNIO GUILHERMINO", Telefone = "(74) 98805-4502", Email = "", DistritoId = 1 },
               new Escola { Id = 31, Inep = 29429790, Nome = "E.M.E.I. ARCENIO JOSÉ DA SILVA", Telefone = "", Email = "EMAILPRIMAVEAARCENIOJOSE@GMAIL.COM", DistritoId = 1 },
               new Escola { Id = 32, Inep = 29464064, Nome = "E.M.E.I. ARLINDA ALVES VARJÃO", Telefone = "(74) 8833-3869", Email = "JANE.SILVABARBOSA@HOTMAIL.COM", DistritoId = 1 },
               new Escola { Id = 33, Inep = 29470820, Nome = "E.M.E.I. BEATRIZ ANGÉLICA MOTA", Telefone = "", Email = "EMEI.DIRETORIA@GMAIL.COM", DistritoId = 1 },
               new Escola { Id = 34, Inep = 29026423, Nome = "E.M.E.I. BOLIVAR SANTANA", Telefone = "(74) 8805-4854", Email = "SANTANABOL14@GMAIL.COM", DistritoId = 1 },
               new Escola { Id = 35, Inep = 29402170, Nome = "E.M.E.I. BOM JESUS DOS NAVEGANTES", Telefone = "(74) 3617-3029", Email = "SUENI-SANTOS@YAHOO.COM.BR", DistritoId = 4 },
               new Escola { Id = 36, Inep = 29932777, Nome = "E.M.E.I. CAIC MISAEL AGUILAR", Telefone = "", Email = "", DistritoId = 1 },
               new Escola { Id = 37, Inep = 29402190, Nome = "E.M.E.I. DILMA CALMON DE OLIVEIRA", Telefone = "", Email = "JAMMYS.GUERRA@GMAIL.COM", DistritoId = 1 },
               new Escola { Id = 38, Inep = 29461189, Nome = "E.M.E.I. EDIVÂNIA SANTOS CARDOSO", Telefone = "(74) 8851-1345", Email = "ROSALILAS_BISPO@HOTMAIL.COM", DistritoId = 1 },
               new Escola { Id = 39, Inep = 29402100, Nome = "E.M.E.I. GENTIL DAMÁSIO DO NASCIMENTO", Telefone = "(74) 3613-3763", Email = "", DistritoId = 1 },
               new Escola { Id = 40, Inep = 29026628, Nome = "E.M.E.I. HERBET MOUSE RODRIGUES", Telefone = "", Email = "SOLANGETIASOL@HOTMAIL.COM", DistritoId = 5 },
               new Escola { Id = 41, Inep = 29402210, Nome = "E.M.E.I. JANDIRA BORGES", Telefone = "", Email = "", DistritoId = 1 },
               new Escola { Id = 42, Inep = 29461367, Nome = "E.M.E.I. LENI LOPES DE ARAÚJO SANTOS", Telefone = "", Email = "", DistritoId = 1 },
               new Escola { Id = 43, Inep = 29467152, Nome = "E.M.E.I. LUANA DA SILVA NASCIMENTO", Telefone = "(74) 98130-0440", Email = "EMEILUANADASILVA@GMAIL.COM", DistritoId = 1 },
               new Escola { Id = 44, Inep = 29461219, Nome = "E.M.E.I. LUZINETE DE OLIVEIRA", Telefone = "", Email = "", DistritoId = 1 },
               new Escola { Id = 45, Inep = 29461227, Nome = "E.M.E.I. MANOEL ALVES DA MOTA", Telefone = "", Email = "VALDATDB12@GMAIL.COM", DistritoId = 4 },
               new Escola { Id = 46, Inep = 29469635, Nome = "E.M.E.I. MARIA FERREIRA DE SOUZA", Telefone = "(74) 3617-6145", Email = "TULIOROZARORIZ@GMAIL.COM", DistritoId = 7 },
               new Escola { Id = 47, Inep = 29461243, Nome = "E.M.E.I. MARIA HELENA DA SILVA PEREIRA", Telefone = "(74) 8834-2914", Email = "EMEIMARIAHELENA@HOTMAIL.COM.BR", DistritoId = 1 },
               new Escola { Id = 48, Inep = 29461235, Nome = "E.M.E.I. MARIA HOZANA NUNES", Telefone = "(74) 3612-4696", Email = "CLAUDIANA.PROF@GMAIL.COM", DistritoId = 1 },
               new Escola { Id = 49, Inep = 29461170, Nome = "E.M.E.I. MARIA JÚLIA RODRIGUES TANURI", Telefone = "", Email = "", DistritoId = 1 },
               new Escola { Id = 50, Inep = 29025338, Nome = "E.M.E.I. MARIA SUELY MEDRADO ARAÚJO", Telefone = "(74) 8841-9813", Email = "TRPAQUINO@HOTMAIL.COM", DistritoId = 1 },
               new Escola { Id = 51, Inep = 29402160, Nome = "E.M.E.I. MARIÁ VIANA TANURI", Telefone = "", Email = "", DistritoId = 1 },
               new Escola { Id = 52, Inep = 29461197, Nome = "E.M.E.I. NÉLIA DE SOUZA COSTA", Telefone = "(74) 3612-3354", Email = "", DistritoId = 1 },
               new Escola { Id = 53, Inep = 29402150, Nome = "E.M.E.I. NAILDE DE SOUZA COSTA", Telefone = "", Email = "EMEINAILDE@HOTMAIL.COM", DistritoId = 1 },
               new Escola { Id = 54, Inep = 29025788, Nome = "E.M.E.I. NOSSA SENHORA DAS GROTAS - CARNAÍBA", Telefone = "", Email = "ESCOLA_NSGROTAS@HOTMAIL.COM", DistritoId = 3 },
               new Escola { Id = 55, Inep = 29445256, Nome = "E.M.E.I. NOSSO SENHOR DOS AFLITOS", Telefone = "", Email = "", DistritoId = 1 },
               new Escola { Id = 56, Inep = 29026792, Nome = "E.M.E.I. PASSAGEM DO SARGENTO", Telefone = "", Email = "", DistritoId = 5 },
               new Escola { Id = 57, Inep = 29469252, Nome = "E.M.E.I. PASTOR MANOEL MARQUES DE SOUZA", Telefone = "(74) 98813-6212", Email = "JOSEFACRISTINAT@GMAIL.COM", DistritoId = 1 },
               new Escola { Id = 58, Inep = 29470811, Nome = "E.M.E.I. PREFEITO APRÍGIO DUARTE", Telefone = "(74) 98832-0498", Email = "MARI_LUCA@MSN.COM", DistritoId = 1 },
               new Escola { Id = 59, Inep = 29461251, Nome = "E.M.E.I. PRIMAVERA", Telefone = "", Email = "EMAILPRIMAVERAARCENIOJOSE@GMAIL.COM", DistritoId = 1 },
               new Escola { Id = 60, Inep = 29402110, Nome = "E.M.E.I. PROFª HELOISA HELENA BENEVIDES FARIAS", Telefone = "", Email = "EMEIHELOISAHELENA@HOTMAIL.COM", DistritoId = 1 },
               new Escola { Id = 61, Inep = 29026261, Nome = "E.M.E.I. PROFª JOANA RAMOS NETA", Telefone = "(74) 9934-6352", Email = "", DistritoId = 4 },
               new Escola { Id = 62, Inep = 29027098, Nome = "E.M.E.I. SÃO FRANCISCO DE ASSIS", Telefone = "(74) 99956-3806", Email = "ALINEDEFATIMA92@GMAIL.COM", DistritoId = 8 },
               new Escola { Id = 63, Inep = 29469112, Nome = "E.M.R.T.I. SÃO JOSÉ", Telefone = "(74) 99952-2470", Email = "ANACCN2007@YAHOO.COM.BR", DistritoId = 4 },
               new Escola { Id = 64, Inep = 29450004, Nome = "E.M.T.I. PROFª IRACEMA PEREIRA DA PAIXÃO", Telefone = "(74) 98809-0992", Email = "LINDY_CPC@HOTMAIL.COM", DistritoId = 1 },
               new Escola { Id = 65, Inep = 29401610, Nome = "EDUCANDÁRIO JOÃO XXIII", Telefone = "", Email = "EDUCANDARIOJOAO23@HOTMAIL.COM", DistritoId = 1 },
               new Escola { Id = 66, Inep = 29341744, Nome = "ELEOTÉRIO SOARES FONSÊCA", Telefone = "", Email = "ALINEDEFATIMA92@GMAIL.COM", DistritoId = 8 },
               new Escola { Id = 67, Inep = 29026113, Nome = "ELISEU SANTOS", Telefone = "(74) 8807-8555", Email = "ESCOLAMUNICIPALELISEUSANTOS@HOTMAIL.COM", DistritoId = 4 },
               new Escola { Id = 68, Inep = 29026199, Nome = "EURÍDICE RIBEIRO VIANA", Telefone = "", Email = "EURIDICERIBEIROVIANA.QUIPA@HOTMAIL.COM", DistritoId = 4 },
               new Escola { Id = 69, Inep = 29026202, Nome = "FAMÍLIA UNIDA", Telefone = "(74) 8836-7939", Email = "KLERISSON@YAHOO.COM.BR", DistritoId = 4 },
               new Escola { Id = 70, Inep = 29025524, Nome = "FUNDAÇÃO JUAZEIRENSE PROMENOR", Telefone = "(74) 3611-3992", Email = "ESCOLAPROMENOR@HOTMAIL.COM", DistritoId = 1 },
               new Escola { Id = 71, Inep = 29025753, Nome = "GRACIOSA XAVIER RAMOS GOMES", Telefone = "(74) 3618-1029", Email = "ESCOLAGRACIOSAXAVIER@GMAIL.COM", DistritoId = 3 },
               new Escola { Id = 72, Inep = 29025575, Nome = "HELENA ARAÚJO PINHEIRO", Telefone = "(74) 3611-1626", Email = "PATRICIACARLA01@HOTMAIL.COM", DistritoId = 1 },
               new Escola { Id = 73, Inep = 29025583, Nome = "JATOBÁ", Telefone = "", Email = "AMILTONGOMES2016@HOTMAIL.COM", DistritoId = 4 },
               new Escola { Id = 74, Inep = 29026741, Nome = "JOÃO DIAS FERREIRA", Telefone = "", Email = "", DistritoId = 5 },
               new Escola { Id = 75, Inep = 29025222, Nome = "JOÃO NEVES DE ALMEIDA", Telefone = "(74) 98811-1398", Email = "", DistritoId = 5 },
               new Escola { Id = 76, Inep = 29025168, Nome = "JOCA DE SOUZA OLIVEIRA", Telefone = "", Email = "JOCA.DIRETORIA@GMAIL.COM", DistritoId = 1 },
               new Escola { Id = 77, Inep = 29026636, Nome = "JOSÉ DE AMORIM", Telefone = "", Email = "", DistritoId = 5 },
               new Escola { Id = 78, Inep = 29025370, Nome = "JOSÉ PADILHA DE SOUZA", Telefone = "(74) 3612-0372", Email = "ESCOLAJOSEPADILHA@HOTMAIL.COM", DistritoId = 1 },
               new Escola { Id = 79, Inep = 29025176, Nome = "JUDITE LEAL COSTA", Telefone = "(74) 3611-4939", Email = "ESCOLAJUDITELCOSTA@HOTMAIL.COM", DistritoId = 1 },
               new Escola { Id = 80, Inep = 29026644, Nome = "LÚCIA CARMEM SOBREIRA", Telefone = "", Email = "", DistritoId = 5 },
               new Escola { Id = 81, Inep = 29025923, Nome = "LINDAURA MARIA DE JESUS", Telefone = "(74) 3617-2000", Email = "", DistritoId = 4 },
               new Escola { Id = 82, Inep = 29025184, Nome = "LUDGERO DE SOUZA COSTA", Telefone = "(74) 3612-1731", Email = "ESCOLALUDGERO@GMAIL.COM", DistritoId = 1 },
               new Escola { Id = 83, Inep = 29025206, Nome = "MANDACARU", Telefone = "", Email = "", DistritoId = 1 },
               new Escola { Id = 84, Inep = 29025672, Nome = "MANOEL DE SOUZA BONFIM", Telefone = "(74) 3617-9072", Email = "EMMSBONFIM@HOTMAIL.COM", DistritoId = 2 },
               new Escola { Id = 85, Inep = 29026440, Nome = "MANOEL GOMES MARTINS", Telefone = "", Email = "", DistritoId = 1 },
               new Escola { Id = 86, Inep = 29026296, Nome = "MANOEL LUIZ DA SILVA", Telefone = "(74) 3613-9057", Email = "JAQUELLINEASSESSORIA27@GMAIL.COM", DistritoId = 4 },
               new Escola { Id = 87, Inep = 29341710, Nome = "MANOEL NUNES AMORIM", Telefone = "(74) 3611-5110", Email = "", DistritoId = 5 },
               new Escola { Id = 88, Inep = 29026776, Nome = "MARIA AMÉLIA DE SOUZA OLIVEIRA", Telefone = "(74) 9104-4270", Email = "", DistritoId = 5 },
               new Escola { Id = 89, Inep = 29026652, Nome = "MARIA DO CARMO SÁ NOGUEIRA", Telefone = "", Email = "", DistritoId = 5 },
               new Escola { Id = 90, Inep = 29026660, Nome = "MARIA MONTEIRO BACELAR", Telefone = "(74) 3618-4001", Email = "", DistritoId = 5 },
               new Escola { Id = 91, Inep = 29025230, Nome = "MARIANO RODRIGUES DE SOUZA", Telefone = "(74) 3618-3004", Email = "ESCOLAMARIANORODRIGUES@YAHOO.COM.BR", DistritoId = 4 },
               new Escola { Id = 92, Inep = 29026679, Nome = "MIGUEL ÂNGELO DE SOUZA", Telefone = "(74) 9986-7445", Email = "", DistritoId = 5 },
               new Escola { Id = 93, Inep = 29026105, Nome = "NOSSA SENHORA DAS GROTAS - BOQUEIRÃO", Telefone = "(74) 3617-8287", Email = "NOSSASENHORASAOFRANCISCO@GMAIL.COM", DistritoId = 4 },
               new Escola { Id = 94, Inep = 29025311, Nome = "NOSSA SENHORA DAS GROTAS-SEDE", Telefone = "", Email = "ENSGSEDUC@GMAIL.COM", DistritoId = 1 },
               new Escola { Id = 95, Inep = 29025958, Nome = "NOSSA SENHORA RAINHA DOS ANJOS", Telefone = "(74) 9195-7370", Email = "ESCOLARAINHA@GMAIL.COM", DistritoId = 4 },
               new Escola { Id = 96, Inep = 29025869, Nome = "OSORIO TELES DE MENEZES", Telefone = "(74) 3618-1270", Email = "ESCOLA_OSORIOTELES@HOTMAIL.COM", DistritoId = 3 },
               new Escola { Id = 97, Inep = 29461359, Nome = "PAULO FREIRE", Telefone = "(74) 9907-7828", Email = "JAELTON.OLIVEIRA@HOTMAIL.COM", DistritoId = 5 },
               new Escola { Id = 98, Inep = 29024587, Nome = "PAULO VI", Telefone = "(74) 3611-5675", Email = "PAULOVI_2511@HOTMAIL.COM", DistritoId = 1 },
               new Escola { Id = 99, Inep = 29025834, Nome = "PEDRO DIAS", Telefone = "", Email = "ESCOLAGRACIOSAXAVIER@GMAIL.COM", DistritoId = 3 },
               new Escola { Id = 100, Inep = 29026431, Nome = "PONTAL", Telefone = "", Email = "", DistritoId = 4 },
               new Escola { Id = 101, Inep = 29025362, Nome = "PREFEITO APRÍGIO DUARTE", Telefone = "", Email = "", DistritoId = 1 },
               new Escola { Id = 102, Inep = 29401600, Nome = "PRESIDENTE TANCREDO NEVES", Telefone = "", Email = "", DistritoId = 1 },
               new Escola { Id = 103, Inep = 29026873, Nome = "PROFª ANTONILA DA FRANÇA CARDOSO", Telefone = "", Email = "ESCOLAAFC@GMAIL.COM", DistritoId = 6 },
               new Escola { Id = 104, Inep = 29026989, Nome = "PROFª ATANILHA LUZ ARAÚJO", Telefone = "", Email = "", DistritoId = 7 },
               new Escola { Id = 105, Inep = 29026695, Nome = "PROFª BERNADETE BRAGA", Telefone = "", Email = "", DistritoId = 5 },
               new Escola { Id = 106, Inep = 29024692, Nome = "PROFª CARMEM COSTA SANTOS", Telefone = "", Email = "", DistritoId = 1 },
               new Escola { Id = 107, Inep = 29024900, Nome = "PROFª CRENILDES LUIS BRANDÃO", Telefone = "", Email = "E.CRENILDESBRANDAO@GMAIL.COM", DistritoId = 1 },
               new Escola { Id = 108, Inep = 29025036, Nome = "PROFª DINORAH ALBERNAZ MELO DA SILVA", Telefone = "", Email = "ESCOLADINORAH@MSN.COM", DistritoId = 1 },
               new Escola { Id = 109, Inep = 29026709, Nome = "PROFª EDUALDINA DAMÁSIO", Telefone = "", Email = "", DistritoId = 5 },
               new Escola { Id = 110, Inep = 29024838, Nome = "PROFª GUIOMAR LUSTOSA RODRIGUES", Telefone = "", Email = "ESCOLAGUIOMARLUSTOSA@HOTMAIL.COM", DistritoId = 1 },
               new Escola { Id = 111, Inep = 29024960, Nome = "PROFª HAYDÉE FONSECA FALCÃO", Telefone = "(74) 99985-8835", Email = "ESCOLAHAYDEE@GMAIL.COM", DistritoId = 1 },
               new Escola { Id = 112, Inep = 29341809, Nome = "PROFª IRACY NUNES DA SILVA", Telefone = "", Email = "COLEGIOIRACYNUNES@GMAIL.COM", DistritoId = 4 },
               new Escola { Id = 113, Inep = 29024978, Nome = "PROFª LEOPOLDINA LEAL", Telefone = "", Email = "", DistritoId = 1 },
               new Escola { Id = 114, Inep = 29024943, Nome = "PROFª MARIA DE LOURDES DUARTE", Telefone = "", Email = "ESCOLAMARIALOURDESDUARTE@HOTMAIL.COM", DistritoId = 1 },
               new Escola { Id = 115, Inep = 29024986, Nome = "PROFª MARIA FRANCA PIRES", Telefone = "(74) 3613-7115", Email = "", DistritoId = 1 },
               new Escola { Id = 116, Inep = 29362504, Nome = "PROFª MARIA JOSÉ LIMA DA ROCHA", Telefone = "", Email = "WILZAMIRANDA@HOTMAIL.COM", DistritoId = 1 },
               new Escola { Id = 117, Inep = 29025770, Nome = "PROFª MATILDE COSTA MEDRADO", Telefone = "(74) 98852-6535", Email = "ESCOLA_MATILDECOSTA@HOTMAIL.COM", DistritoId = 3 },
               new Escola { Id = 118, Inep = 29026725, Nome = "PROFª OSCARLINA TANURI", Telefone = "", Email = "", DistritoId = 5 },
               new Escola { Id = 119, Inep = 29025397, Nome = "PROFª TEREZINHA FERREIRA DE OLIVEIRA", Telefone = "", Email = "ESCOLATEREZINHA.ETFO@HOTMAIL.COM", DistritoId = 1 },
               new Escola { Id = 120, Inep = 29415160, Nome = "PROFº CARLOS DA COSTA SILVA", Telefone = "", Email = "ESCOLACARLOSCOSTA_2012@HOTMAIL.COM", DistritoId = 1 },
               new Escola { Id = 121, Inep = 29405106, Nome = "PROFº JOSÉ PEREIRA DA SILVA", Telefone = "", Email = "MUNICIPALJOSEPEREIRA@BOL.COM.BR", DistritoId = 1 },
               new Escola { Id = 122, Inep = 29024714, Nome = "PROFº LUIS CURSINO DA FRANÇA CARDOSO", Telefone = "", Email = "LUCYSOARES1@HOTMAIL.COM", DistritoId = 1 },
               new Escola { Id = 123, Inep = 29027055, Nome = "RAIMUNDO CLEMENTINO DE SOUZA", Telefone = "(74) 99975-9179", Email = "ALINEDEFATIMA92@GMAIL.COM", DistritoId = 8 },
               new Escola { Id = 124, Inep = 29424445, Nome = "RAIMUNDO MEDRADO PRIMO", Telefone = "", Email = "RAIMUNDOMEDRADOPRIMO@HOTMAIL.COM", DistritoId = 1 },
               new Escola { Id = 125, Inep = 29341779, Nome = "RURAL DE MASSAROCA - ERUM", Telefone = "(74) 99443-6003", Email = "RURALMASSAROCA@HOTMAIL.COM", DistritoId = 7 },
               new Escola { Id = 126, Inep = 29026350, Nome = "SÃO FRANCISCO DE ASSIS - MULUNGÚ", Telefone = "(74) 3617-8287", Email = "NOSSASENHORASAOFRANCISCO@GMAIL.COM", DistritoId = 4 },
               new Escola { Id = 127, Inep = 29025982, Nome = "SÃO FRANCISCO DE ASSIS - NH2", Telefone = "(74) 3618-9018", Email = "NH2SAOFRANCISCO@GMAIL.COM", DistritoId = 4 },
               new Escola { Id = 128, Inep = 29026377, Nome = "SÃO JOSÉ", Telefone = "", Email = "", DistritoId = 4 },
               new Escola { Id = 129, Inep = 29025931, Nome = "SÃO SEBASTIÃO", Telefone = "", Email = "", DistritoId = 4 },
               new Escola { Id = 130, Inep = 29026288, Nome = "SANTA INÊS", Telefone = "", Email = "", DistritoId = 4 },
               new Escola { Id = 131, Inep = 29026300, Nome = "SANTA TEREZINHA", Telefone = "(74) 8811-0653", Email = "", DistritoId = 4 },
               new Escola { Id = 132, Inep = 29025010, Nome = "SANTO ANTÔNIO", Telefone = "(74) 3618-7055", Email = "", DistritoId = 4 },
               new Escola { Id = 133, Inep = 29026520, Nome = "VEREADOR AMADEUS DAMÁSIO", Telefone = "(74) 3538-1883", Email = "ESCOLAAMADEUSDAMASIO@GMAIL.COM", DistritoId = 5 }
            };

            builder.Entity<Escola>().HasData(escolas);

            var cursos = new List<Curso>
            {
                new Curso { Id = 1, Nome = "Educação Infantil", Sigla = "EI" },
                new Curso { Id = 2, Nome = "Ensino Fundamental", Sigla = "EF" },
                new Curso { Id = 3, Nome = "Educação de Jovens e Adultos", Sigla = "EJA" }
            };

            builder.Entity<Curso>().HasData(cursos);

            var segmentos = new List<Segmento>
            {
                new Segmento { Id = 1, CursoId = 1, Nome = "Creche", Sigla = "CR" },
                new Segmento { Id = 2, CursoId = 1, Nome = "Pré-Escola", Sigla = "PE" },
                new Segmento { Id = 3, CursoId = 2, Nome = "Ensino Fundamental I", Sigla = "EF I" },
                new Segmento { Id = 4, CursoId = 2, Nome = "Ensino Fundamental II", Sigla = "EF II" },
                new Segmento { Id = 5, CursoId = 3, Nome = "Educação de Jovens e Adultos I", Sigla = "EJA I" },
                new Segmento { Id = 6, CursoId = 3, Nome = "Educação de Jovens e Adultos II", Sigla = "EJA II" },
                new Segmento { Id = 7, CursoId = 2, Nome = "Correção de Fluxo", Sigla = "CF" }
            };

            builder.Entity<Segmento>().HasData(segmentos);

            var etapas = new List<Etapa>
            {
                new Etapa { Id = 1, SegmentoId = 1, Nome = "Berçário" },
                new Etapa { Id = 2, SegmentoId = 1, Nome = "Infantil I" },
                new Etapa { Id = 3, SegmentoId = 1, Nome = "Infantil II" },
                new Etapa { Id = 4, SegmentoId = 1, Nome = "Infantil III" },
                new Etapa { Id = 5, SegmentoId = 2, Nome = "Infantil IV" },
                new Etapa { Id = 6, SegmentoId = 2, Nome = "Infantil V" },
                new Etapa { Id = 7, SegmentoId = 3, Nome = "1º Ano" },
                new Etapa { Id = 8, SegmentoId = 3, Nome = "2º Ano" },
                new Etapa { Id = 9, SegmentoId = 3, Nome = "3º Ano" },
                new Etapa { Id = 10, SegmentoId = 3, Nome = "4º Ano" },
                new Etapa { Id = 11, SegmentoId = 3, Nome = "5º Ano" },
                new Etapa { Id = 12, SegmentoId = 4, Nome = "6º Ano" },
                new Etapa { Id = 13, SegmentoId = 4, Nome = "7º Ano" },
                new Etapa { Id = 14, SegmentoId = 4, Nome = "8º Ano" },
                new Etapa { Id = 15, SegmentoId = 4, Nome = "9º Ano" },
                new Etapa { Id = 16, SegmentoId = 5, Nome = "Etapa I" },
                new Etapa { Id = 17, SegmentoId = 5, Nome = "Etapa II" },
                new Etapa { Id = 18, SegmentoId = 5, Nome = "Etapa III" },
                new Etapa { Id = 19, SegmentoId = 6, Nome = "Etapa IV" },
                new Etapa { Id = 20, SegmentoId = 6, Nome = "Etapa V" },
                new Etapa { Id = 21, SegmentoId = 7, Nome = "Se Liga" },
                new Etapa { Id = 22, SegmentoId = 7, Nome = "Acelera" }
            };

            builder.Entity<Etapa>().HasData(etapas);

            var turnos = new List<Turno>
            {
                new Turno { Id = 1, Nome = "Manhã", Regime = "Parcial" },
                new Turno { Id = 2, Nome = "Tarde", Regime = "Parcial" },
                new Turno { Id = 3, Nome = "Noite", Regime = "Parcial" },
                new Turno { Id = 4, Nome = "Integral", Regime = "Integral" },
                new Turno { Id = 5, Nome = "Não Informado", Regime = "Parcial" }
            };

            builder.Entity<Turno>().HasData(turnos);

            var formas = new List<Forma>
            {
                new Forma { Id = 1, Nome = "Seriada" },
                new Forma { Id = 2, Nome = "Multi" }
            };

            builder.Entity<Forma>().HasData(formas);

            var salas = new List<Sala>
            {
                new Sala { Id = 1, EscolaId = 1, Nome = "02 DE JULHO" },
                new Sala { Id = 2, EscolaId = 1, Nome = "02 DE JULHO" },
                new Sala { Id = 3, EscolaId = 1, Nome = "15 DE JULHO" }
            };

            builder.Entity<Sala>().HasData(salas);

            var turmas = new List<Turma>
            {
                new Turma { Id = 1, SalaId = 1, EtapaId = 11, TurnoId = 1, FormaId = 1, Nome = "A", QtdAlunos = 15, Extinta = false }
            };

            builder.Entity<Turma>().HasData(turmas);

            var alunos = new List<Aluno>
            {
                new Aluno { Id = 1, Nome = "Maria Luz" },
                new Aluno { Id = 2, Nome = "João carlos" }
            };

            builder.Entity<Aluno>().HasData(alunos);

            var disciplinas = new List<Disciplina>
            {
                new Disciplina { Id = 1, Nome = "Português", Sigla = "Port" },
                new Disciplina { Id = 2, Nome = "Matemática", Sigla = "Mat" },
                new Disciplina { Id = 3, Nome = "Ciências", Sigla = "Cie" }
            };

            builder.Entity<Disciplina>().HasData(disciplinas);

            var temas = new List<Tema>
            {
                new Tema { Id = 1, DisciplinaId = 1, Nome = "Tema 01 - Disciplina Port" },
                new Tema { Id = 2, DisciplinaId = 1, Nome = "Tema 02 - Disciplina Port" },
                new Tema { Id = 3, DisciplinaId = 1, Nome = "Tema 03 - Disciplina Port" },
                new Tema { Id = 4, DisciplinaId = 1, Nome = "Tema 04 - Disciplina Port" },
                new Tema { Id = 5, DisciplinaId = 1, Nome = "Tema 05 - Disciplina Port" },
                new Tema { Id = 6, DisciplinaId = 2, Nome = "Tema 01 - Disciplina Mat" },
                new Tema { Id = 7, DisciplinaId = 2, Nome = "Tema 02 - Disciplina Mat" },
                new Tema { Id = 8, DisciplinaId = 2, Nome = "Tema 03 - Disciplina Mat" },
                new Tema { Id = 9, DisciplinaId = 2, Nome = "Tema 04 - Disciplina Mat" },
                new Tema { Id = 10, DisciplinaId = 2, Nome = "Tema 05 - Disciplina Mat" },
                new Tema { Id = 11, DisciplinaId = 3, Nome = "Tema 01 - Disciplina Cie" },
                new Tema { Id = 12, DisciplinaId = 3, Nome = "Tema 02 - Disciplina Cie" },
                new Tema { Id = 13, DisciplinaId = 3, Nome = "Tema 03 - Disciplina Cie" },
                new Tema { Id = 14, DisciplinaId = 3, Nome = "Tema 04 - Disciplina Cie" },
                new Tema { Id = 15, DisciplinaId = 3, Nome = "Tema 05 - Disciplina Cie" }
            };

            builder.Entity<Tema>().HasData(temas);

            var descritores = new List<Descritor>
            {
                new Descritor { Id = 1, TemaId = 1, Nome = "Descritor 01 - Tema 1" },
                new Descritor { Id = 2, TemaId = 1, Nome = "Descritor 02 - Tema 1" },
                new Descritor { Id = 3, TemaId = 1, Nome = "Descritor 03 - Tema 1" },
                new Descritor { Id = 4, TemaId = 1, Nome = "Descritor 04 - Tema 1" },
                new Descritor { Id = 5, TemaId = 1, Nome = "Descritor 05 - Tema 1" },
                new Descritor { Id = 6, TemaId = 2, Nome = "Descritor 01 - Tema 2" },
                new Descritor { Id = 7, TemaId = 2, Nome = "Descritor 02 - Tema 2" },
                new Descritor { Id = 8, TemaId = 2, Nome = "Descritor 03 - Tema 2" },
                new Descritor { Id = 9, TemaId = 2, Nome = "Descritor 04 - Tema 2" },
                new Descritor { Id = 10, TemaId = 2, Nome = "Descritor 05 - Tema 2" },
                new Descritor { Id = 11, TemaId = 3, Nome = "Descritor 01 - Tema 3" },
                new Descritor { Id = 12, TemaId = 3, Nome = "Descritor 02 - Tema 3" },
                new Descritor { Id = 13, TemaId = 3, Nome = "Descritor 03 - Tema 3" },
                new Descritor { Id = 14, TemaId = 3, Nome = "Descritor 04 - Tema 3" },
                new Descritor { Id = 15, TemaId = 3, Nome = "Descritor 05 - Tema 3" }
            };

            builder.Entity<Descritor>().HasData(descritores);

            var questoes = new List<Questao>
            {
                new Questao { Id = 1, DescritorId = 1, Item = "P01", Descricao = "Questao 01 - Descritor 1" },
                new Questao { Id = 2, DescritorId = 1, Item = "P02", Descricao = "Questao 02 - Descritor 1" },
                new Questao { Id = 3, DescritorId = 1, Item = "P03", Descricao = "Questao 03 - Descritor 1" },
                new Questao { Id = 4, DescritorId = 1, Item = "P04", Descricao = "Questao 04 - Descritor 1" },
                new Questao { Id = 5, DescritorId = 2, Item = "P01", Descricao = "Questao 01 - Descritor 2" },
                new Questao { Id = 6, DescritorId = 2, Item = "P02", Descricao = "Questao 02 - Descritor 2" },
                new Questao { Id = 7, DescritorId = 3, Item = "P01", Descricao = "Questao 01 - Descritor 3" },
                new Questao { Id = 8, DescritorId = 4, Item = "P01", Descricao = "Questao 01 - Descritor 4" },
                new Questao { Id = 9, DescritorId = 4, Item = "P02", Descricao = "Questao 02 - Descritor 4" },
                new Questao { Id = 10, DescritorId = 2, Item = "P01", Descricao = "Teste" },
                new Questao { Id = 11, DescritorId = 3, Item = "P02", Descricao = "Teste2" }
            };

            builder.Entity<Questao>().HasData(questoes);

            var alternativas = new List<Alternativa>
            {
                new Alternativa { Id = 1, QuestaoId = 1, Descricao = "20", Correta = true },
                new Alternativa { Id = 2, QuestaoId = 1, Descricao = "15", Correta = false },
                new Alternativa { Id = 3, QuestaoId = 1, Descricao = "12", Correta = false },
                new Alternativa { Id = 4, QuestaoId = 1, Descricao = "17", Correta = false },
                new Alternativa { Id = 5, QuestaoId = 2, Descricao = "Pedro", Correta = false },
                new Alternativa { Id = 6, QuestaoId = 2, Descricao = "Carlos", Correta = true },
                new Alternativa { Id = 7, QuestaoId = 2, Descricao = "José", Correta = false },
                new Alternativa { Id = 8, QuestaoId = 2, Descricao = "Maria", Correta = false },
                new Alternativa { Id = 9, QuestaoId = 3, Descricao = "1,5", Correta = false },
                new Alternativa { Id = 10, QuestaoId = 3, Descricao = "2,9", Correta = false },
                new Alternativa { Id = 11, QuestaoId = 3, Descricao = "5,4", Correta = true },
                new Alternativa { Id = 12, QuestaoId = 3, Descricao = "4,2", Correta = false },
                new Alternativa { Id = 13, QuestaoId = 4, Descricao = "Ronaldo", Correta = false },
                new Alternativa { Id = 14, QuestaoId = 4, Descricao = "Adriana", Correta = false },
                new Alternativa { Id = 15, QuestaoId = 4, Descricao = "Marcelo", Correta = false },
                new Alternativa { Id = 16, QuestaoId = 4, Descricao = "Simone", Correta = true },
                new Alternativa { Id = 17, QuestaoId = 5, Descricao = "20", Correta = false },
                new Alternativa { Id = 18, QuestaoId = 5, Descricao = "10", Correta = true },
                new Alternativa { Id = 19, QuestaoId = 5, Descricao = "30", Correta = false },
                new Alternativa { Id = 20, QuestaoId = 5, Descricao = "40", Correta = false }
            };

            builder.Entity<Alternativa>().HasData(alternativas);

            var avaliacaoDisciplinas = new List<AvaliacaoDisciplina>
            {
                new AvaliacaoDisciplina { AvaliacaoId = 1, DisciplinaId = 1 },
                new AvaliacaoDisciplina { AvaliacaoId = 1, DisciplinaId = 2 },
                new AvaliacaoDisciplina { AvaliacaoId = 2, DisciplinaId = 3 }
            };

            builder.Entity<AvaliacaoDisciplina>().HasData(avaliacaoDisciplinas);

            var avaliacaoDistritos = new List<AvaliacaoDistrito>
            {
                new AvaliacaoDistrito { AvaliacaoId = 1, DistritoId = 1 }
            };

            builder.Entity<AvaliacaoDistrito>().HasData(avaliacaoDistritos);

            var turmaAlunos = new List<TurmaAluno>
            {
                new TurmaAluno { TurmaId = 1, AlunoId = 1 },
                new TurmaAluno { TurmaId = 1, AlunoId = 2 }
            };

            builder.Entity<TurmaAluno>().HasData(turmaAlunos);

            var respostaAlunos = new List<RespostaAluno>
            {
                new RespostaAluno { AvaliacaoId = 1, AlunoId = 1, AlternativaId = 5 },
                new RespostaAluno { AvaliacaoId = 1, AlunoId = 1, AlternativaId = 11 },
                new RespostaAluno { AvaliacaoId = 1, AlunoId = 2, AlternativaId = 7 },
                new RespostaAluno { AvaliacaoId = 1, AlunoId = 2, AlternativaId = 10 }
            };

            builder.Entity<RespostaAluno>().HasData(respostaAlunos);

            var etapaDescritores = new List<EtapaDescritor>
            {
                new EtapaDescritor { EtapaId = 11, DescritorId = 1 }
            };

            builder.Entity<EtapaDescritor>().HasData(etapaDescritores);

            var questaoAvaliacoes = new List<QuestaoAvaliacao>
            {
                new QuestaoAvaliacao { QuestaoId = 1, AvaliacaoId = 1 },
                new QuestaoAvaliacao { QuestaoId = 2, AvaliacaoId = 1 }
            };

            builder.Entity<QuestaoAvaliacao>().HasData(questaoAvaliacoes);

            return builder;
        }
    }
}

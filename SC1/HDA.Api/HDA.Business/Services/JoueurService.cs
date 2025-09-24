using Microsoft.IdentityModel.Tokens;
using HDA.Business.Interfaces;
using HDA.Business.Models;
using HDA.Domain.Entities;
using HDA.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HDA.Business.Services
{
    public class JoueurService : IJoueurService
    {
        private readonly IJoueurRepository _joueurRepository;
        public JoueurService(IJoueurRepository joueurRepository)
        {
            _joueurRepository = joueurRepository;
        }

        public async Task<IEnumerable<JoueurDTO>> GetAllJoueurs()
        {
            var list = await _joueurRepository.GetAllJoueurs();
            var listSortie = new List<JoueurDTO>();
            foreach (var item in list)
            {
                var joueur = new JoueurDTO()
                {
                    Mail = item.Mail,
                    Id = item.Id,
                    Nom = item.Nom,
                    Prenom = item.Prenom,
                    DateAjout = item.DateAjout,
                };
                listSortie.Add(joueur);
            }
            return listSortie;
        }
        public async Task<int> GetJoueurByMail(string mail)
        {
            var id = await _joueurRepository.GetJoueurByMail(mail);
            return id;
        }

        /// <returns>0 si ok, 1 si le joueur existe déjà, -1 si erreur</returns>
        public async Task<int> AddJoueur(AddJoueurToBDDDTO dto)
        {
            //regarde si le joueur existe déjà
            var joueur = await _joueurRepository.GetJoueurByMail(dto.Mail);
            if (joueur != 0) return 1;

            TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;

            var newJoueur = new Joueur() { 
                Nom = dto.Nom.ToUpper(), 
                Prenom = textInfo.ToTitleCase(dto.Prenom),
                Mail = dto.Mail,
                DateAjout = DateTime.Now,
            };
            int id = await _joueurRepository.AddJoueur(newJoueur);
            if (id == 0) return -1;

            return 0;
        }

        /// <summary>
        /// Ajoute une liste de joueurs en se basant sur le contenu du A et/ou du CC d'un mail
        /// </summary>
        public async Task<bool> AddListJoueurs(string joueurs)
        {
            if (string.IsNullOrWhiteSpace(joueurs)) return false; 

            string[] joueursList = joueurs.Split(';', StringSplitOptions.RemoveEmptyEntries);

            TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;

            foreach (var joueur in joueursList)
            {
                if(joueur != null)
                {
                    char[] separators = new char[] { ' ', '<', '>' };

                    string[] dataJoueur = joueur.Split(separators, StringSplitOptions.RemoveEmptyEntries);

                    var mail = dataJoueur.Last();

                    //regarde si le joueur existe déjà
                    var joueurExistant = await _joueurRepository.GetJoueurByMail(mail);

                    if(joueurExistant == 0)
                    {
                        //si on a coupé la partie en 2 bouts ou 1 seul, c'est qu'il n'y a que le mail de renseigné
                        if (dataJoueur.Length <= 2)
                        {
                            await AddNomPrenomFromMail(mail);
                        }
                        else
                        {
                            var newJoueur = new Joueur()
                            {
                                Nom = dataJoueur[0].ToUpper(),
                                Prenom = textInfo.ToTitleCase(dataJoueur[1]),
                                Mail = mail,
                                DateAjout = DateTime.Now,
                            };
                            await _joueurRepository.AddJoueur(newJoueur);
                        }
                   
                    }
                };
            }

            return true;
        }

        public async Task<bool> AddNomPrenomFromMail(string mail)
        {
            if (mail == null) { return false; }

            TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;

            //on récupère la partie avant le @
            char[] separators = new char[] { '@' };
            string[] nomprenom = mail.Split(separators, StringSplitOptions.RemoveEmptyEntries);

            separators = new char[] { '>', '<',' ', '.', '-', '_' };
            nomprenom = nomprenom[0].Split(separators, StringSplitOptions.RemoveEmptyEntries);

            var newJoueur = new Joueur()
            {
                Mail = mail,
                DateAjout = DateTime.Now,
            };
            if (nomprenom.Length ==0) { return false; }
            else if (nomprenom.Length == 1)
            {
                newJoueur.Nom = nomprenom[0].ToUpper();
            }
            else
            {
                newJoueur.Nom = nomprenom[1].ToUpper();
                newJoueur.Prenom = textInfo.ToTitleCase(nomprenom[0]);
            }
            
            int id = await _joueurRepository.AddJoueur(newJoueur);
            if (id == 0) return false;

            return true;

        }

        /// <returns>0 ok, 1 si mail déjà utilisé, -1 si joueur n'existe pas, -2 MAJ échoué</returns>
        public async Task<int> UpdateJoueur(UpdateJoueurDTO dto)
        {
            var joueurOrigin = await _joueurRepository.GetJoueurById(dto.Id);
            if (joueurOrigin == null) return -1;

            TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;

            //regarde si le mail est déjà utilisé
            var joueur = await _joueurRepository.GetJoueurByMail(dto.Mail);
            if (joueur != 0 && joueur != dto.Id) return 1;

            if (!String.IsNullOrEmpty(dto.Nom)) joueurOrigin.Nom = dto.Nom;
            if (!String.IsNullOrEmpty(dto.Prenom)) joueurOrigin.Prenom = textInfo.ToTitleCase(dto.Prenom);
            if (!String.IsNullOrEmpty(dto.Mail)) joueurOrigin.Mail = dto.Mail;

            var b = await _joueurRepository.UpdateJoueur(joueurOrigin);
            if (!b) return -2;

            return 0;
        }

        public async Task<bool> DeleteJoueur(int id)
        {
            return await _joueurRepository.DeleteJoueur(id);
        }
    }
}
